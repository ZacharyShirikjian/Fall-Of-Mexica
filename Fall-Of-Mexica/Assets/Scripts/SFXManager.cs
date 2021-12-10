using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{

    public AudioClip buttonClick;
    public AudioClip buttonHover;
    public AudioClip dialouge;
    public AudioClip pickupSFX;
    public AudioClip interactSFX;
    private AudioSource thisSource;
    // Start is called before the first frame update
    void Start()
    {
        thisSource = GetComponent<AudioSource>();
    }
}
