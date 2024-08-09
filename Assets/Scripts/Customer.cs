using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2;
    [SerializeField] float rotateSpeed = 45;
    [SerializeField] GameObject canves;
    [SerializeField] Text text;

    Transform tagetTransform;
    Vector3 tagetPos;
    Quaternion tagetRotationl;

    Drink drink;
    string firstSentence = "";
    string secondSentence = "";
    
    bool isNotArrive = false;
    bool isRotate = false;
    bool once = false;

    Action actionAfterArrived;

    public Drink Drink { get { return drink; } }

    void Update()
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
        StartCoroutine(Wit());
    }

    IEnumerator Wit()
    {
        yield return new WaitForSeconds(5);
        startRotate();
    }

    void startRotate()
    {
        tagetRotationl = transform.rotation;
        tagetRotationl.eulerAngles += (Vector3.up * 180);
        canves.SetActive(false);
        isRotate = true;
    }


    void MoveTo()
    {
        if (isNotArrive)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                    tagetPos, moveSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, tagetPos) < .01f)
            {
                transform.position = tagetPos;
                isNotArrive = false;

                if (actionAfterArrived != null)
                    actionAfterArrived();
                actionAfterArrived = null;
            }
        }
    }



    void Rotate()
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

    void ApperCanves()
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
    void DestroyCustomer()
    {
        Events.onCustomerGoen.Invoke(tagetTransform);
        Destroy(gameObject);
    }
    public void intiCustomer(Transform _tagetPos, Drink _drink, string _firstSen, string _secSen)
    {
        tagetTransform = _tagetPos;
        tagetPos = _tagetPos.position;
        drink = _drink;
        firstSentence = _firstSen;
        secondSentence = _secSen;
        isNotArrive = true;
        actionAfterArrived = ApperCanves;
    }
    


}
