using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2;
    [SerializeField] private float rotateSpeed = 45;
    [SerializeField] private GameObject canves;
    [SerializeField] private Text text;

    private Transform tagetTransform;
    private Vector3 tagetPos;
    private Quaternion tagetRotationl;
    private Drink drink;
    private string firstSentence = "";
    private string secondSentence = "";
    private bool isNotArrive = false;
    private bool isRotate = false;
    private bool once = false;

    Action actionAfterArrived;

    public Drink Drink { get { return drink; } }

    private void Update()
    {
        MoveTo();
        Rotate();
    }

    private void OnMouseDown()
    {
        if (!once)
        {
            Events.OnplayerClickOncustomer.Invoke(Drink, this);
            once = true;
        }
    }

    public void Judge(string text)
    {
        this.text.text = text;
        StartCoroutine(Wait());
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(3);
        StartRotation();
    }

    private void StartRotation()
    {
        tagetRotationl = transform.rotation;
        tagetRotationl.eulerAngles += (Vector3.up * 180);
        canves.SetActive(false);
        isRotate = true;
    }

    private void MoveTo()
    {
        if (isNotArrive)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                    tagetPos, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, tagetPos) < .01f)
            {
                transform.position = tagetPos;
                isNotArrive = false;
                actionAfterArrived?.Invoke();
                actionAfterArrived = null;
            }
        }
    }

    private void Rotate()
    {
        if (isRotate)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                tagetRotationl, rotateSpeed * Time.deltaTime);
            if (Quaternion.Angle(transform.rotation, tagetRotationl) < .1f)
            {
                transform.rotation = tagetRotationl;
                isRotate = false;
                tagetPos = transform.position + (transform.forward * 10);
                isNotArrive = true;
                actionAfterArrived = DestroyCustomer;

            }

        }
    }

    private void ShowCanvas()
    {
        text.text = firstSentence + drink.name + "\n";
        text.text += secondSentence + "\n";
        foreach (KeyValuePair<TapName, float> valuePair in drink.ingredients)
        {
            text.text += valuePair.Key.ToString() + ", ";
        }
        text.text = text.text.Remove(text.text.Length - 2, 2);
        canves.SetActive(true);
    }

    private void DestroyCustomer()
    {
        Events.onCustomerGoen.Invoke(tagetTransform);
        Destroy(gameObject);
    }

    public void InitCustomer(Transform _tagetPos, Drink _drink, string _firstSen, string _secSen)
    {
        tagetTransform = _tagetPos;
        tagetPos = _tagetPos.position;
        drink = _drink;
        firstSentence = _firstSen;
        secondSentence = _secSen;
        isNotArrive = true;
        actionAfterArrived = ShowCanvas;
    }
}