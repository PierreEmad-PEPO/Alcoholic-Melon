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

    public Vector3 CurrentPourPoint { get { return bottom.position + quantity/100 * height * bottom.up; } }

    // Start is called before the first frame update
    void Start()
    {
        quantity = 0;
        height = top.position.y - bottom.position.y;
        SetCurrentTap(currentTap);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTap.IsPouring)
        {
            quantity += currentTap.PourValue/100 * fillingSpeed * Time.deltaTime;
            quantity = Mathf.Clamp(quantity, 0, 100);

            liquidRend.material.SetVector("_ClippingPosition", CurrentPourPoint - bottom.up * 0.02f);
        }
    }

    public void SetCurrentTap(Tap newTap)
    {
        currentTap = newTap;
        checkArea.transform.position = bottom.position + bottom.up * currentTap.RemainingPercentage/100 * height;
    }
}
