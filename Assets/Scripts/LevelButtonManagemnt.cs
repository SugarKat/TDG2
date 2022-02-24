using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelButtonManagemnt : MonoBehaviour {

    public GameObject pauseUi;

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            PauseToggle();
        }
    }

    public void PauseToggle ()
    {
        pauseUi.SetActive(!pauseUi.activeSelf);

        if (pauseUi.activeSelf)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void RestartButton ()
    {
        if (pauseUi.activeSelf)
        {
            Time.timeScale = 1f;
        }

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButton ()
    {
        if (pauseUi.activeSelf)
        {
            Time.timeScale = 1f;
        }

        SceneManager.LoadScene("MainMenu");
    }

}
