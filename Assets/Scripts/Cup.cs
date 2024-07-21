using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class Cup : MonoBehaviour
{
    [SerializeField] private GameObject checkArea;
    [SerializeField] private float fillingSpeed;
    [SerializeField] private Transform top, bottom;
    [SerializeField] private Renderer liquidRend;

    [SerializeField] private Tap currentTap;
    [SerializeField] private float quantity;
    [SerializeField] private float quantityOverflow;
    private float height;
    private int correct, wrong;

    Dictionary<TapName, float> drinks;

    private CupColorManager colorManager;
    public Vector3 CurrentPourPoint { get { return bottom.position + quantity/100 * height * bottom.up; } }

    
    void Start()
    {
        colorManager = FindObjectOfType<CupColorManager>();
        quantity = 0;
        correct = wrong = 0;
        height = top.position.y - bottom.position.y;
        UpdateCurrentTap();
    }

    
    void Update()
    {
        if (currentTap.IsPouring)
        {
            quantity += currentTap.PourValue/100 * fillingSpeed * Time.deltaTime;
            if (quantity > 100f) quantityOverflow += quantity - 100f;
            currentTap.RemainingPercentage -= currentTap.PourValue / 100 * fillingSpeed * Time.deltaTime;
            quantity = Mathf.Clamp(quantity, 0, 100);

            liquidRend.material.SetVector("_ClippingPosition", CurrentPourPoint - bottom.up * 0.02f);
            colorManager.StartPour(currentTap.color, currentTap.PourValue);

            AddDrink(currentTap.tag, currentTap.PourValue / 100 * fillingSpeed * Time.deltaTime);
        }
        else colorManager.StopPour(currentTap.color);

        if ((quantity >= 100 && !currentTap.IsPouring) || quantityOverflow > 10)
        {
            quantity += quantityOverflow;
            ResetCup();
        }
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

    public void CheckCurrentFlow()
    {
        Collider[] colliders = Physics.OverlapBox(CurrentPourPoint - bottom.up * 0.02f, new Vector3(0.1f, 0.005f, 0.1f));
        foreach (Collider collider in colliders) 
        {
            if (collider.gameObject.name.Equals("target"))
            {
                CorrectHit();
                return;
            }
        }
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.name.Equals("safe"))
            {
                Debug.Log("Safe");
                return;
            }
        }

        WrongHit();
    }

    private void CorrectHit()
    {
        Debug.Log("Correct");
        correct++;
        //play music
    }

    private void WrongHit()
    {
        Debug.Log("Wrong");
        wrong++;
        //play music
    }

    public void UpdateCurrentTap()
    {
        drinks = new Dictionary<TapName, float>();
        SetCurrentTap(currentTap);
    }

    public void AddDrink(string s, float per)
    {
        switch (s)
        {
            case "Red":
                if (!drinks.ContainsKey(TapName.Red))
                    drinks.Add(TapName.Red, 0);
                drinks[TapName.Red] += per;
                break;
            case "Green":
                if (!drinks.ContainsKey(TapName.Green))
                    drinks.Add(TapName.Green, 0);
                drinks[TapName.Green] += per;
                break;
            case "Blue":
                if (!drinks.ContainsKey(TapName.Blue))
                    drinks.Add(TapName.Blue, 0);
                drinks[TapName.Blue] += per;
                break;

        }
    }

    void ResetCup()
    {
        Camera.main.GetComponent<CameraFocus>().MoveToStart(correct, wrong);
        quantity = 0;
        quantityOverflow = 0;
        correct = wrong = 0;
        checkArea.transform.position = Vector3.one * 1000;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red; // Set Gizmos color

        // Draw wire cube at specified position (center) and size
        Gizmos.DrawWireCube(CurrentPourPoint - bottom.up * 0.02f, new Vector3(0.1f, 0.005f, 0.1f));
    }
}
