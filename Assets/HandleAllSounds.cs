using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HandleAllSounds : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField]  AudioClip noise;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void playHandlesSound(GameObject tabRotation,AudioClip clip1, AudioClip clip2)
    {
        if(tabRotation.transform.eulerAngles.x>45&& tabRotation.transform.eulerAngles.x < 46) audioSource.PlayOneShot(clip1);
        if(tabRotation.transform.eulerAngles.x > 87 && tabRotation.transform.eulerAngles.x < 88) audioSource.PlayOneShot(clip2);
    }
    public void playRandomNoise()
    {
        audioSource.PlayOneShot(noise, Random.Range(0f,0.9f));
    }
}
