using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseFunctions : MonoBehaviour
{

    //This method gets called when clicking "RESTART" on the pause menu 
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //This method gets called when clicking "OPTIONS" on the pause menu (will be implemented later) 
    public void OpenOptionsMenu()
    {
        //Set Active OptionsPanel w/ options to increase/decrease volume, 
    }
    //This method gets called when clicking "QUIT" on the pause menu 
    public void ReturnToTitle()
    {
        SceneManager.LoadScene(0);
    }
}
