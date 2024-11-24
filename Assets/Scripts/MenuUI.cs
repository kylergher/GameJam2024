using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{

    public GameObject menuUI;

    public Canvas popUpMenu;


    public void Start()
    {
        popUpMenu.enabled = false;
        Time.timeScale = 1f;
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
        
    }

    public void ToggleMenu()
    {
        popUpMenu.enabled = !popUpMenu.enabled;

        if (popUpMenu.enabled )
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void QuitGame()
    {
        Debug.Log("Game is quitting...");
        Application.Quit();
    }

    public void ResetLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CloseMenu()
    {
        menuUI.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void OpenMenu()
    {
        menuUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
