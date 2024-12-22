using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private bool isPaused = false;

    void Start()=>Pause();
    
    public void PlayGame()
    {   
        SoundManager.Instance.StopMainMenuMusic();
        SoundManager.Instance.PlayGameSound();
        SoundManager.Instance.PlayRandomNoise();
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    private void Pause()
    {
        SoundManager.Instance.PlayMainMenuMusic();
        gameObject.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Quit()=>Application.Quit();
}