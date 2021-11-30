using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    //AudioClips
    private AudioClip buttonClick;
    private AudioClip buttonHover;

    //Reference to the SFXManager GameObject
    private GameObject sfxManager;

    //Reference to the SFXManager's AudioSource
    private AudioSource sfxSource;


    // Start is called before the first frame update
    void Start()
    {
        sfxManager = GameObject.Find("SFXManager");
        sfxSource = sfxManager.GetComponent<AudioSource>();
        buttonClick = sfxManager.GetComponent<SFXManager>().buttonClick;
        buttonHover = sfxManager.GetComponent<SFXManager>().buttonHover;
    }

    //This method gets called when the player hovers their mouse over a UI button  (in the Pause Menu or Title Screen). 
    //Play the SFX for hovering over a UI button.
    public void PlayButtonHoverSFX()
    {
        sfxSource.PlayOneShot(buttonHover);
    }


    //This method gets called when the player clicks on a UI button (in the Pause Menu or Title Screen). 
    //Play the SFX for clicking over a UI button.
    public void PlayButtonClickSFX()
    {
        sfxSource.PlayOneShot(buttonClick);
    }
}
