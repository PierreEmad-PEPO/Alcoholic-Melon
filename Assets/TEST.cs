using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST : MonoBehaviour
{
    [SerializeField] Transform lmt;
    [SerializeField] Renderer rend;
    [SerializeField] float speed;
    Vector3 lmt2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
            lmt.Translate(-lmt.up * speed * Time.deltaTime);
            lmt2 = lmt.position;
            rend.material.SetVector("_ClippingPosition", lmt2);
        }
    }
}
