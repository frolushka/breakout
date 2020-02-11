using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenu;

    private void Awake()
    {
        Time.timeScale = 1;
    }

    public void TogglePause()
    {
        Time.timeScale = (Time.timeScale + 1) % 2;
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
