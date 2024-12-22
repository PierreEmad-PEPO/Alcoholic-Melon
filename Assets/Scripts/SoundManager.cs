using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip noise;
    [SerializeField] private AudioClip mainMenu;
    [SerializeField] private AudioClip[] handleSounds;

    public static SoundManager Instance { get; private set; }
    private AudioSource audioSource;

    private void Start()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        audioSource = GetComponent<AudioSource>();
    }
    
    public void PlayHandlesSound(GameObject tapRotation,float maxRotDeg)
    {
        if (tapRotation.transform.localEulerAngles.x > maxRotDeg / 2 && tapRotation.transform.localEulerAngles.x < 47)
            audioSource.PlayOneShot(handleSounds[0], 0.1f);
        if (tapRotation.transform.localEulerAngles.x > 85 && tapRotation.transform.localEulerAngles.x < 90)
            audioSource.PlayOneShot(handleSounds[1], 0.05f);
    }

    public void PlayRandomNoise()=>audioSource.PlayOneShot(noise, Random.Range(0f,0.9f));
    
}