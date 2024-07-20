using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] float minRotDeg, maxRotDeg;
    [SerializeField] float pourFactor;
    [SerializeField] float rotationSpeed;

    bool isRotated = false ;

    public float PourValue
    {
        get 
        {
            return (transform.localEulerAngles.x - minRotDeg) / maxRotDeg * pourFactor;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.localEulerAngles = new Vector3(minRotDeg, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotated && transform.rotation.x > minRotDeg)
        { 
            transform.Rotate(-Vector3.right * (rotationSpeed / 2));
            if (transform.rotation.x < minRotDeg) // if arrived
            {
                Quaternion q = transform.rotation;
                q.x = minRotDeg;
                transform.rotation = q;
            }
        }
        
    }

    private void OnMouseDrag()
    {
        float rot = -Input.GetAxis("Mouse Y") * rotationSpeed;
        if (transform.localEulerAngles.x + rot > minRotDeg && transform.localEulerAngles.x + rot < maxRotDeg)
            transform.Rotate(rot, 0, 0);

        //Debug.Log(transform.gameObject.name + " " + PourValue + "%");
        isRotated = false;
    }

    private void OnMouseUp()
    {
        isRotated = true;   
    }
}
