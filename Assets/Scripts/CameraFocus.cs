using System;
using System.Collections;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    [SerializeField] private Transform machineView;
    [SerializeField] private Transform startTransform;
    [SerializeField] private float transitionDuration = 2.0f;

    public static CameraFocus instance;
    public event Action gameStarted;

    private void Awake()
    {
        transform.position = startTransform.position;
        transform.rotation = startTransform.rotation;
        if (instance == null)
            instance = this;

        else if (instance != this)
            Destroy(gameObject);
    }

    public void StartGame()=>StartCoroutine(MoveCamera());
    
    private IEnumerator MoveCamera()
    {
        float elapsedTime = 0;
        Vector3 startingPos = transform.position;
        Quaternion startingRot = transform.rotation;

        while (elapsedTime < transitionDuration)
        {
            transform.position = Vector3.Lerp(startingPos, machineView.position,elapsedTime/transitionDuration);
            transform.rotation = Quaternion.Lerp(startingRot,machineView.rotation,elapsedTime/transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = machineView.position;
        transform.rotation = machineView.rotation;
        gameStarted.Invoke();
    }
}