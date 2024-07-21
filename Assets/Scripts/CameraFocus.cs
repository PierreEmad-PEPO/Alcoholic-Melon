using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField] Transform machineViwe;
    [SerializeField] Transform startTransform;
    [SerializeField] float camMoveSpeed = 5;
    [SerializeField] float camRotSpeed = 45;
    [SerializeField] Judgement judge;

    Vector3 targetPos;
    Quaternion targetRot;


    bool isMoving = false;
    bool isRerunning = false;
    int c, w;
    // Start is called before the first frame update
    void Start()
    {
        Events.OnplayerClickOncustomer.AddListener(MoveToMachine);
    }

    // Update is called once per frame
    void Update()
    {
        MoveTo();
    }

    void MoveTo()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos
                , camMoveSpeed * Time.deltaTime );
            transform.rotation = Quaternion.RotateTowards(transform.rotation, 
                targetRot , camRotSpeed * Time.deltaTime );

            if (Vector3.Distance(transform.position, targetPos) < .01f &&
                Quaternion.Angle(transform.rotation, targetRot) < 0.1f)
            {
                isMoving = false;
                if (isRerunning)
                {
                    judge.Judge(c, w);
                    isRerunning = false;
                }
            }
        }
    }

    void MoveToMachine(Drink d, Customer c)
    {
        targetPos = machineViwe.position;
        targetRot = machineViwe.rotation;
        isMoving = true;
    }

    public void MoveToStart(int correct, int wrong)
    {
        targetPos = startTransform.position;
        targetRot = startTransform.rotation;
        isMoving = true;
        isRerunning = true;
        c = correct;
        w = wrong;
    }

}
