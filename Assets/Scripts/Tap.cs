using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tap : MonoBehaviour
{
    [SerializeField] Color color;
    [SerializeField] float minRotDeg, maxRotDeg;
    [SerializeField] float pourFactor;
    [SerializeField] float rotationSpeed;

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
        
    }

    private void OnMouseDrag()
    {
        float rot = -Input.GetAxis("Mouse Y") * rotationSpeed;
        if (transform.localEulerAngles.x + rot > minRotDeg && transform.localEulerAngles.x + rot < maxRotDeg)
            transform.Rotate(rot, 0, 0);

        Debug.Log(transform.gameObject.name + " " + PourValue + "%");
    }
}
