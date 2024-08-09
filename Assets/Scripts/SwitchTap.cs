using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SwitchTap : MonoBehaviour
{
    [SerializeField] Transform red, green, blue;


    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDrag()
    {
        Vector3 mouse = Input.mousePosition;
        mouse.z = Vector3.Distance(Camera.main.transform.position, red.position);
        transform.position = Camera.main.ScreenToWorldPoint(mouse);
        string curTag = transform.tag;
        Transform nearest = GetMinDis();
        if (Vector3.Distance(transform.position, nearest.position) < 0.7f)
        {
            transform.position = nearest.position;
            if (!transform.CompareTag(nearest.tag)) 
            {
                GetComponent<Cup>().SetCurrentTap(nearest.parent.GetChild(0).GetComponent<Tap>());
                transform.tag = nearest.tag;
            }
        }
    }


    Transform GetMinDis()
    {
        float r = Vector3.Distance(transform.position, red.position);
        float g = Vector3.Distance(transform.position, green.position);
        float b = Vector3.Distance(transform.position, blue.position);
        if (r <= g && r <= b) return red;
        else if (g <= r && g <= b) return green;
        else if (b <= r && b <= g) return blue;
        
        return red;
    }
}
