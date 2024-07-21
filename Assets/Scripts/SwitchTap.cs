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
        mouse.z = red.position.z;
        transform.position = Camera.main.ScreenToWorldPoint(mouse);
        Transform nearest = GetMinDis();
        if (Vector3.Distance(transform.position, nearest.position) < 0.7f)
        {
            transform.position = nearest.position;
            GetComponent<Cup>().SetCurrentTap(nearest.parent.GetChild(0).GetComponent<Tap>());
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
