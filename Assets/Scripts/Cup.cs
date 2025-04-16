using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cup : MonoBehaviour
{
    // Constants for clarity and maintainability
    private const float MaxQuantity = 100f;
    private const float OverflowThreshold = 10f;
    private const float ClippingOffset = 0.02f;
    private const float GizmoSizeX = 0.1f;
    private const float GizmoSizeY = 0.005f;
    private const float GizmoSizeZ = 0.1f;

    [SerializeField] private GameObject checkArea;
    [SerializeField] private float fillingSpeed;
    [SerializeField] private Transform top, bottom;
    [SerializeField] private Renderer liquidRend;
    [SerializeField] private Judgement judgement;
    [SerializeField] private Tap currentTap;
    [SerializeField] private float quantity;
    [SerializeField] private float quantityOverflow;

    private float height;
    private int correct, wrong;
    private Dictionary<TapName, float> drinks;
    private CupColorManager colorManager;

    public Vector3 CurrentPourPoint => bottom.position + quantity / MaxQuantity * height * bottom.up;

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
        if (currentTap != null && currentTap.IsPouring)
        {
            HandlePouring();
        }
        else
        {
            colorManager.StopPour(currentTap?.color);
        }

        if ((quantity >= MaxQuantity && (currentTap == null || !currentTap.IsPouring)) || quantityOverflow > OverflowThreshold)
        {
            quantity += quantityOverflow;
            StartCoroutine(ResetCup());
        }
    }

    private void HandlePouring()
    {
        quantity += currentTap.PourValue / MaxQuantity * fillingSpeed * Time.deltaTime;
        if (quantity > MaxQuantity) quantityOverflow += quantity - MaxQuantity;
        currentTap.RemainingPercentage -= currentTap.PourValue / MaxQuantity * fillingSpeed * Time.deltaTime;
        quantity = Mathf.Clamp(quantity, 0, MaxQuantity);

        liquidRend.material.SetVector("_ClippingPosition", CurrentPourPoint - bottom.up * ClippingOffset);
        colorManager.StartPour(currentTap.color, currentTap.PourValue);

        AddDrink(currentTap.tag, currentTap.PourValue / MaxQuantity * fillingSpeed * Time.deltaTime);
    }

    public void SetCurrentTap(Tap newTap)
    {
        currentTap = newTap;
        if (currentTap != null && currentTap.RemainingPercentage > 0)
        {
            checkArea.transform.position = CurrentPourPoint + bottom.up * currentTap.RemainingPercentage / MaxQuantity * height;
        }
        else
        {
            checkArea.transform.position = Vector3.one * 1000;
        }
    }

    public void CheckCurrentFlow()
    {
        Collider[] colliders = Physics.OverlapBox(CurrentPourPoint - bottom.up * ClippingOffset, new Vector3(GizmoSizeX, GizmoSizeY, GizmoSizeZ));
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
        // Play music or feedback
    }

    private void WrongHit()
    {
        Debug.Log("Wrong");
        wrong++;
        // Play music or feedback
    }

    public void UpdateCurrentTap()
    {
        drinks = new Dictionary<TapName, float>();
        SetCurrentTap(currentTap);
    }

    public void AddDrink(string tag, float amount)
    {
        if (Enum.TryParse(tag, out TapName tapName))
        {
            if (!drinks.ContainsKey(tapName))
            {
                drinks[tapName] = 0;
            }
            drinks[tapName] += amount;
        }
    }

    private IEnumerator ResetCup()
    {
        judgement.Judge(correct, wrong);
        quantity = 0;
        quantityOverflow = 0;
        correct = wrong = 0;
        checkArea.transform.position = Vector3.one * 1000;
        yield return new WaitForSeconds(2);
        liquidRend.material.SetVector("_ClippingPosition", CurrentPourPoint - bottom.up * 200f);
        colorManager.ResetColor();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(CurrentPourPoint - bottom.up * ClippingOffset, new Vector3(GizmoSizeX, GizmoSizeY, GizmoSizeZ));
    }
}