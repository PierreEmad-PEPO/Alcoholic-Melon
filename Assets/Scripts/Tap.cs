using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Tap : MonoBehaviour
{
    [SerializeField] public Color color;
    [SerializeField] private float minRotDeg, maxRotDeg;
    [SerializeField] private float pourFactor;
    [SerializeField] private float rotationSpeed;
    private float rotateBack = 50f;
    private CupColorManager cupColorManager;
    private bool isRotated = false ;
    private bool isPouring = false;
    public float PourValue
    {
        get 
        {
            return (transform.localEulerAngles.x - minRotDeg) / maxRotDeg * pourFactor;
        }
    }

    private void Start()
    {
        cupColorManager = FindObjectOfType<CupColorManager>();
        if (minRotDeg < 1) minRotDeg = 1;
        transform.localEulerAngles = new Vector3(minRotDeg, 0, 0);
    }

    private void Update()
    {
        if (isRotated && transform.localEulerAngles.x > minRotDeg)
        {
            
            transform.Rotate(-Vector3.right * (rotateBack * Time.deltaTime ) );
            if (transform.localEulerAngles.x < minRotDeg) // if arrived
            { 
                transform.localEulerAngles = Vector3.right * minRotDeg;
                isRotated = false;
                cupColorManager.StopPour(color);
            }
            isPouring = false;

        }
        if (!isRotated && transform.localEulerAngles.x > minRotDeg)
        {
            StartCoroutine(Pour());
            isPouring = true;
        }

    }
    private IEnumerator Pour()
    {
        while (isPouring)
        {
            cupColorManager.StartPour(color, PourValue);
            yield return null;
        }
    }
    private void OnMouseDrag()
    {
        float rot = -Input.GetAxis("Mouse Y") * rotationSpeed;
        if (transform.localEulerAngles.x + rot > minRotDeg && transform.localEulerAngles.x + rot < maxRotDeg)
            transform.Rotate(rot, 0, 0);

        //Debug.Log(transform.gameObject.name + " " + PourValue + "%");
        isRotated = false;
        isPouring = true;
    }

    private void OnMouseUp()
    {
        isRotated = true;
        isPouring = false;
    }
}
