using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    public void Start()
    {
       // pauseMenu.SetActive(false);
    }
    public GameObject pauseMenu;
    public bool gameIsPaused;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter)&&gameIsPaused==true)
        {
            ResumeGame();
        }
        Physics.Raycast(transform.position + new Vector3(0f, 1.5f, 0f), transform.forward);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
        gameObject.GetComponent<CameraMovement>().enabled = false;
        Debug.Log("Game paused. timeScale is:" + Time.timeScale);
        gameIsPaused = true;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        gameObject.GetComponent<CameraMovement>().enabled = true;
        gameIsPaused = false;
        Debug.Log("game resumed. TimeScale is:" + Time.timeScale);

    }
}
