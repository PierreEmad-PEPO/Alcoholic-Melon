using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    [SerializeField] private GameObject checkArea;
    [SerializeField] private float fillingSpeed;
    [SerializeField] private Transform top, bottom;
    [SerializeField] private Renderer liquidRend;

    [SerializeField] private Tap currentTap;
    [SerializeField] private float quantity;
    private float height;

    private CupColorManager colorManager;
    public Vector3 CurrentPourPoint { get { return bottom.position + quantity/100 * height * bottom.up; } }

    
    void Start()
    {
        colorManager = FindObjectOfType<CupColorManager>();
        quantity = 0;
        height = top.position.y - bottom.position.y;
        UpdateCurrentTap();
    }

    
    void Update()
    {
        if (currentTap.IsPouring)
        {
            quantity += currentTap.PourValue/100 * fillingSpeed * Time.deltaTime;
            currentTap.RemainingPercentage -= currentTap.PourValue / 100 * fillingSpeed * Time.deltaTime;
            quantity = Mathf.Clamp(quantity, 0, 100);

            liquidRend.material.SetVector("_ClippingPosition", CurrentPourPoint - bottom.up * 0.02f);
            colorManager.StartPour(currentTap.color, currentTap.PourValue);
        }
        else colorManager.StopPour(currentTap.color);
    }

    public void SetCurrentTap(Tap newTap)
    {
        Debug.Log(currentTap.RemainingPercentage);
        currentTap = newTap;

        if (currentTap.RemainingPercentage > 0)
        {
            checkArea.transform.position = CurrentPourPoint + bottom.up * currentTap.RemainingPercentage / 100 * height;
        }
        else
        {
            checkArea.transform.position = Vector3.one * 1000;
        }
    }

    public void UpdateCurrentTap()
    {
        SetCurrentTap(currentTap);
    }
}
