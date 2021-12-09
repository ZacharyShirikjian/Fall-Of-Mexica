using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameManager : MonoBehaviour
{
    //REFERENCES//
    private GameObject interactPrompt;
    private TextMeshProUGUI interactPromptText;
    public GameObject PauseMenu;
    private GameObject player;
    public GameObject pickUpIcon; 

    private GameObject currentObjectiveUI;
    private TextMeshProUGUI currentObjectiveText;

    public GameObject option1Button;
    public GameObject option2Button; 
    public GameObject continueButton;
    public GameObject DialogueBox;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI dialogueText;

    private TextMeshProUGUI maizeCounter; 
    private int numOfMaize; 

    private GameObject mapPrompts; 
    public GameObject miniMap; //reference to Minimap GameObject 
    public GameObject fullMap; //reference to the image of the full Tenochtitlan map 

    //VARIABLES//
    public bool canOpenMap = true;
    public bool paused = false;

    private AudioClip pickupSFX;
    private AudioClip interactSFX; 
    public AudioSource audioSource;
    private GameObject sfxManager;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
        player = GameObject.FindWithTag("Player");
        mapPrompts = GameObject.Find("MapPrompts");
        pickUpIcon = GameObject.Find("PickupIcon");
        pickUpIcon.SetActive(false);
        maizeCounter = GameObject.Find("MaizeCounter").GetComponent<TextMeshProUGUI>();
        maizeCounter.SetText("");
        numOfMaize = 0;
        miniMap.SetActive(true);
        fullMap.SetActive(false);
        option1Button.SetActive(false);
        option2Button.SetActive(false);
        continueButton.SetActive(false);
        DialogueBox.SetActive(false);
        dialogueText = DialogueBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        dialogueText.SetText("");
        npcNameText.SetText("");
        interactPrompt = GameObject.Find("InteractPrompt");
        interactPromptText = interactPrompt.GetComponentInChildren<TextMeshProUGUI>();
        interactPrompt.SetActive(false);
        currentObjectiveUI = GameObject.Find("CurrentObjective");
        currentObjectiveText = currentObjectiveUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        sfxManager = GameObject.Find("SFXManager");
        pickupSFX = sfxManager.GetComponent<SFXManager>().pickupSFX;
        interactSFX = sfxManager.GetComponent<SFXManager>().interactSFX;
        UpdateCurrentObjective("");
    }

    // Update is called once per frame
    void Update()
    {

        if(player.GetComponentInChildren<PlayerInteract>().talking == true)
        {
            miniMap.SetActive(false);
            canOpenMap = false;
            mapPrompts.SetActive(false);
        }

        else if(player.GetComponentInChildren<PlayerInteract>().talking == false)
        {
            miniMap.SetActive(true);
            mapPrompts.SetActive(true);
        }

        //If player is pressing F, map appears
        if(paused == false && player.GetComponentInChildren<PlayerInteract>().talking == false && canOpenMap == true && Input.GetKeyDown(KeyCode.F))
        {

            miniMap.SetActive(false);
            fullMap.SetActive(true);
            mapPrompts.SetActive(false);
            player.GetComponent<PlayerMovement>().canMove = false; 
            canOpenMap = false; 
        }

         //If player presses F again after the map is already opened, 
        //Close the map 
        else if(paused == false && player.GetComponentInChildren<PlayerInteract>().talking == false && canOpenMap == false && Input.GetKeyDown(KeyCode.F))
        {
            fullMap.SetActive(false);
            mapPrompts.SetActive(true);
            miniMap.SetActive(true);
            player.GetComponent<PlayerMovement>().canMove = true;
            canOpenMap = true;
        }

        if(Input.GetKeyDown(KeyCode.P) && paused == false)
        {
            PauseGame();
        }

        else if(Input.GetKeyDown(KeyCode.P) && paused == true)
        {
            UnPauseGame(); 
        }
           
    } 
    void PauseGame()
    {
        paused = true;
        PauseMenu.SetActive(true);
        Time.timeScale = 0f; 
    }

    void UnPauseGame()
    {
        paused = false;
        PauseMenu.SetActive(false); 
        Time.timeScale = 1f; 
    }
    //Pop up the prompt for the player to press an input to interact with something (NPC, object, etc) 
    public void InteractPrompt(string prompt)
    {
        //interactPrompt.SetActive(true);
        //interactPromptText.SetText(prompt);
        if(player.GetComponentInChildren<PlayerInteract>().canInteract == true)
        {
            interactPrompt.SetActive(true);
            interactPromptText.SetText(prompt);
            audioSource.PlayOneShot(interactSFX);
        }

        else if (player.GetComponentInChildren<PlayerInteract>().canInteract == false)
        {
            interactPrompt.SetActive(false);
            interactPromptText.SetText("");
        }
    }

    public void UpdateCurrentObjective(string currentObjective)
    {
        if(currentObjective == "")
        {
            currentObjectiveText.SetText("Talk to Townspeople");
        }

        else
        {
            currentObjectiveText.SetText(currentObjective);

        }
    }

    public void IncreaseMaizeCounter()
    {
        numOfMaize++;
        Debug.Log(numOfMaize);
        maizeCounter.SetText(numOfMaize.ToString());
        audioSource.PlayOneShot(pickupSFX);
        if (numOfMaize >= 7)
        {
            currentObjectiveText.SetText("Bring Maize to Templo Mayor");
        }
    }



}
