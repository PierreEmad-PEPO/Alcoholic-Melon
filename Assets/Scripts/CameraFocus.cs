using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField] private Transform machineView;
    [SerializeField] private Transform startTransform;
    [SerializeField] private float camMoveSpeed = 5;
    [SerializeField] private float camRotSpeed = 45;

    Vector3 targetPos;
    Quaternion targetRot;

}