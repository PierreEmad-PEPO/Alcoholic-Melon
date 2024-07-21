using System.Collections.Generic;
using UnityEngine;

public class CupColorManager : MonoBehaviour
{
    [SerializeField] private Tap redTap;
    [SerializeField] private Tap greenTap;
    [SerializeField] private Tap blueTap;

    [SerializeField] private Renderer cupRenderer;
    private Color currentColor;
    private Dictionary<Color, float> colorPullTimes = new Dictionary<Color, float>();

    private float lerpDuration = 1f;

    void Start()
    {
        currentColor = cupRenderer.material.color;
        colorPullTimes[redTap.color] = 0f;
        colorPullTimes[greenTap.color] = 0f;
        colorPullTimes[blueTap.color] = 0f;
    }

    void Update()
    {
        float totalPullTime = 0f;
        foreach (var colorTime in colorPullTimes)
        {
            totalPullTime += colorTime.Value;
        }

        if (totalPullTime > 0)
        {
            foreach (var colorTime in colorPullTimes)
            {
                Color targetColor = colorTime.Key;
                float pullTime = colorTime.Value;

                currentColor = Color.Lerp(currentColor, targetColor, (pullTime / totalPullTime) / lerpDuration);
            }
        }

        cupRenderer.material.color = currentColor;

        foreach (var key in new List<Color>(colorPullTimes.Keys))
        {
            colorPullTimes[key] = Mathf.Max(0, colorPullTimes[key] - Time.deltaTime);
        }
    }

    public void StartPour(Color color, float pourValue)
    {

        if (colorPullTimes.ContainsKey(color))
        {
            colorPullTimes[color] += pourValue * Time.deltaTime;
            //Debug.Log("pouring");
        }
       
    }

    public void StopPour(Color color)
    {

        if (colorPullTimes.ContainsKey(color))
        {
            colorPullTimes[color] = Mathf.Max(0, colorPullTimes[color] - Time.deltaTime);
            Debug.Log("Stopped pouring color");
        }
       
    }
}
