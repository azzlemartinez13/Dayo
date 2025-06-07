using UnityEngine;

public class PauseGame : MonoBehaviour
{
    private BackgroundMusic backgroundMusicScript;
    private AudioSource backgroundMusicAudioSource;
    [SerializeField] private GameObject pauseMenuUI;
    private bool isPaused = false;

    private void Start()
    {
        // Uzyskaj dostęp do skryptu BackgroundMusic i jego komponentu AudioSource
        backgroundMusicScript = FindObjectOfType<BackgroundMusic>();
        if (backgroundMusicScript != null)
        {
            backgroundMusicAudioSource = backgroundMusicScript.GetComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                PauseGameplay();
            }
            else
            {
                ResumeGameplay();
            }
        }
    }

    //public void TogglePause()
    //{
    //    isPaused = !isPaused;
    //    Time.timeScale = isPaused ? 0 : 1;

    //    if (pauseMenuUI != null)
    //    {
    //        pauseMenuUI.SetActive(isPaused);
    //    }

    //    //Debug.Log(isPaused ? "Game Paused" : "Game Resumed");
    //}

    private void PauseGameplay()
    {
        Time.timeScale = 0;
        if (backgroundMusicAudioSource != null)
        {
            backgroundMusicAudioSource.Pause();
        }

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
    }

    private void ResumeGameplay()
    {
        Time.timeScale = 1;
        if (backgroundMusicAudioSource != null)
        {
            backgroundMusicAudioSource.UnPause();
        }

        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    private void OnGUI()
    {
        if (isPaused)
        {
            GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
            labelStyle.fontSize = 40;
            labelStyle.alignment = TextAnchor.MiddleCenter;

            GUI.Label(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 300, 100), "Game paused", labelStyle);
        }
    }
}
