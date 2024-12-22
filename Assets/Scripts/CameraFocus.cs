using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField] private Transform machineView;
    [SerializeField] private Transform startTransform;
    [SerializeField] private float camMoveSpeed = 5;
    [SerializeField] private float camRotSpeed = 45;

    public static CameraFocus instance;

    private void Awake()
    {
        transform.position = startTransform.position;
        transform.rotation = startTransform.rotation;
        if (instance == null)
            instance = this;
        
        else if (instance != this)
            Destroy(gameObject);
    }

    public void StartGame()
    {
        transform.position= Vector3.Lerp(transform.position, machineView.position, camMoveSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, machineView.rotation, camRotSpeed);
    }
}