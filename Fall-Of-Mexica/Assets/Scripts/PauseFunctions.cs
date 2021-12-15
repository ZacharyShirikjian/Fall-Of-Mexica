using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseFunctions : MonoBehaviour
{
    public GameObject optionsPanel;
    //This method gets called when clicking "RESTART" on the pause menu 

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        this.gameObject.SetActive(false);
    }    
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        optionsPanel.SetActive(false); 
    }

    //This method gets called when clicking "OPTIONS" on the pause menu (will be implemented later) 
    public void OpenOptionsMenu()
    {
        optionsPanel.SetActive(true);
    }

    //This method gets called when clicking "OPTIONS" on the pause menu (will be implemented later) 
    public void CloseOptionsMenu()
    {
        optionsPanel.SetActive(false);
    }
    //This method gets called when clicking "QUIT" on the pause menu 
    public void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
