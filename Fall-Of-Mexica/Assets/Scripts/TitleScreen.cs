using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*
     * This gets called when clicking PLAY on the title screen.
     * Load the game scene (with a build index of 1, titlescreen has build index of 0)
     * And leave the title screen scene.
     */
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    /*
     * This gets called when clicking QUIT on the title screen.
     * Call the Application.Quit() method to close out of the game.
     */
    public void QuitGame()
    {
        Application.Quit();
    }

    /*
     * This gets called when clicking CREDITS on the title screen.
     * Display the CREDITS GameObject, OR load a new scene displaying the credits.
     */ 
    public void OpenCredits()
    {

    }
}
