using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField] Transform machineViwe;
    [SerializeField] Transform strtaPos;
    [SerializeField] float camMoveSpeed = 5;
    [SerializeField] float camRotSpeed = 5;

    Vector3 targetPos;
    Quaternion targetRot;
    Customer customer;


    bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveTo()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos
                , camMoveSpeed * Time.deltaTime );
            transform.rotation = Quaternion.RotateTowards(transform.rotation, 
                targetRot , camRotSpeed * Time.deltaTime );

            if (Vector3.Distance(transform.position, targetPos) > .01f &&
                Quaternion.Angle(transform.rotation, targetRot) > 0.1f)
            {
                isMoving = false;
                if (customer != null) 
                {

                }
                
            }
        }
    }


}
