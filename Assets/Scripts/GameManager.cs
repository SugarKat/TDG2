using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public static GameManager instance;
    public static bool GameIsOver;

    public BuildingLibrary buildings;
    public GameObject gameOverUI;
    public Transform EndPoint;

    bool cursorLocked = true;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            return;
        }
        Destroy(this);
    }

    void Start ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        GameIsOver = false;
        UIControler.instance.UpdateBuildMenu(buildings.list);
    }

	// Update is called once per frame
	void Update () {
        if (GameIsOver)
            return;

        if (PlayerStats.Lives <= 0)
        {
            EndGame();
        }
	}

    void EndGame()
    {
        GameIsOver = true;

        gameOverUI.SetActive(true);
    }
    public void ToogleCursorLock()
    {
        if(cursorLocked)
        {
            Cursor.lockState = CursorLockMode.None;
            cursorLocked = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            cursorLocked = true;
        }
        PlayerEntityManager.instance.ToogleControler();
    }
    
}
