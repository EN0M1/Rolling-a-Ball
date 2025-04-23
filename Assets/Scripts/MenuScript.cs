using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    public void GoToGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Minigame");
        // StartCoroutine(WaitForSoundAndTransition("Minigame"));
    }

    public void GoToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        // StartCoroutine(WaitForSoundAndTransition("MainMenu"));
    }

    public void GoToSettings()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("SettingsMenu");
        // StartCoroutine(WaitForSoundAndTransition("SettingsMenu"));
    }

    public void GoToTutorial()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("InstructionsMenu");
        // StartCoroutine(WaitForSoundAndTransition("InstructionsMenu"));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitForSoundAndTransition(string sceneName)
    {
        AudioSource audioSource = GetComponentInChildren<AudioSource>();
        audioSource.Play();
        yield return new WaitForSeconds(audioSource.clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
