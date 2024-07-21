using UnityEngine;

public class Tap : MonoBehaviour
{
    [SerializeField] public Color color;
    [SerializeField] private float minRotDeg, maxRotDeg;
    [SerializeField] private float pourFactor;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform pourTransform;
    [SerializeField] private GameObject pourFlowPrefab;
    [SerializeField] private GameObject bubblesPrefab;
    [SerializeField] private Transform cupTransform;

    private float rotateBack = 200f;
    //private CupColorManager cupColorManager;
    private bool isRotated = false ;
    private bool isPouring = false;
    private GameObject pourFlow;
    private GameObject bubbles;
    private Transform bubblesTransform;
    public float PourValue { get { return (transform.localEulerAngles.x - minRotDeg) / (maxRotDeg - minRotDeg) * pourFactor; }}

    public float RemainingPercentage { get { return 100f; } }

    public bool IsPouring { get { return isPouring; } }

    public GameObject Bubbles { get { return bubbles; } }

    private void Start()
    {
        bubblesTransform = GameObject.Find("bottom").transform;
        //cupColorManager = FindObjectOfType<CupColorManager>();
        if (minRotDeg < 1) minRotDeg = 1;
        transform.localEulerAngles = new Vector3(minRotDeg, 0, 0);
    }

    private void Update()
    {
        if (isRotated && transform.localEulerAngles.x > minRotDeg)
        {
            

            transform.Rotate(-Vector3.right * (rotateBack * Time.deltaTime ));
   
            if (transform.localEulerAngles.x < minRotDeg || transform.localEulerAngles.x > maxRotDeg) // if arrived

            { 
                transform.localEulerAngles = Vector3.right * minRotDeg;
                isRotated = false;
                isPouring = false;
                //cupColorManager.StopPour(color);
            }
           

        }
        if (!isRotated && transform.localEulerAngles.x > minRotDeg)
        {
            //Pour();
            pourFlow.GetComponent<ParticleSystem>().emissionRate = 10 + (300 - 10) * (PourValue / 100);
            isPouring = true;
        }

    }
    private void Pour()
    {
        if (isPouring)
        {
            //cupColorManager.StartPour(color, PourValue);
        }
    }

    private void OnMouseDown()
    {
        pourFlow = Instantiate(pourFlowPrefab, pourTransform.position, pourTransform.rotation);
        pourFlow.GetComponent<ParticleSystem>().GetComponent<Renderer>().material.color = color;

        bubbles = Instantiate(bubblesPrefab, bubblesTransform.position, bubblesPrefab.transform.rotation);

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

        pourFlow.GetComponent<ParticleSystem>().Stop();
        bubbles.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        Destroy(pourFlow, 2f);
        Destroy(bubbles, 2f);
    }
}
