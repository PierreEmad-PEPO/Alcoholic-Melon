using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    // Constants for clarity and maintainability
    private const float DefaultMoveSpeed = 2f;
    private const float DefaultRotateSpeed = 45f;
    private const float ArrivalThreshold = 0.01f;
    private const float RotationAngle = 180f;
    private const float ForwardDistance = 10f;
    private const float WaitTime = 3f;

    [SerializeField] private float moveSpeed = DefaultMoveSpeed;
    [SerializeField] private float rotateSpeed = DefaultRotateSpeed;
    [SerializeField] private GameObject canvas;
    [SerializeField] private Text text;

    private Transform targetTransform;
    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private Drink drink;
    private string firstSentence = "";
    private string secondSentence = "";
    private bool isMoving = false;
    private bool isRotating = false;
    private bool hasInteracted = false;

    private Action actionAfterArrival;

    public Drink Drink => drink;

    private void Update()
    {
        HandleMovement();
        HandleRotation();
    }

    private void OnMouseDown()
    {
        if (!hasInteracted)
        {
            Events.OnplayerClickOncustomer?.Invoke(Drink, this);
            hasInteracted = true;
            AddOutline();
        }
    }

    private void AddOutline()
    {
        if (gameObject.GetComponent<Outline>() == null)
        {
            gameObject.AddComponent<Outline>();
        }
    }

    public void Judge(string resultText)
    {
        text.text = resultText;
        StartCoroutine(WaitBeforeRotation());
    }

    private IEnumerator WaitBeforeRotation()
    {
        yield return new WaitForSeconds(WaitTime);
        StartRotation();
    }

    private void StartRotation()
    {
        targetRotation = transform.rotation;
        targetRotation.eulerAngles += Vector3.up * RotationAngle;
        canvas.SetActive(false);
        isRotating = true;
    }

    private void HandleMovement()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < ArrivalThreshold)
            {
                transform.position = targetPosition;
                isMoving = false;
                actionAfterArrival?.Invoke();
                actionAfterArrival = null;
            }
        }
    }

    private void HandleRotation()
    {
        if (isRotating)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, targetRotation) < ArrivalThreshold)
            {
                transform.rotation = targetRotation;
                isRotating = false;
                targetPosition = transform.position + transform.forward * ForwardDistance;
                isMoving = true;
                actionAfterArrival = DestroyCustomer;
            }
        }
    }

    private void ShowCanvas()
    {
        if (drink == null) return;

        text.text = firstSentence + drink.name + "\n";
        text.text += secondSentence + "\n";
        foreach (KeyValuePair<TapName, float> ingredient in drink.ingredients)
        {
            text.text += ingredient.Key + ", ";
        }
        text.text = text.text.TrimEnd(',', ' ');
        canvas.SetActive(true);
    }

    private void DestroyCustomer()
    {
        Events.onCustomerGoen?.Invoke(targetTransform);
        Destroy(gameObject);
    }

    public void InitCustomer(Transform target, Drink assignedDrink, string firstLine, string secondLine)
    {
        targetTransform = target;
        targetPosition = target.position;
        drink = assignedDrink;
        firstSentence = firstLine;
        secondSentence = secondLine;
        isMoving = true;
        actionAfterArrival = ShowCanvas;
    }
}