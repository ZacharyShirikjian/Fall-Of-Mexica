using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    //REFERENCES//
    private GameObject interactPrompt;
    private TextMeshProUGUI interactPromptText;
    public GameObject PauseMenu;
    private GameObject player;
    public GameObject pickUpIcon;

    public GameObject maizeObjective;
    public GameObject templeObjective;
    public GameObject courtObjective; 
    public GameObject villageObjective;

    private GameObject currentObjectiveUI;
    private TextMeshProUGUI currentObjectiveText;
    public bool villageObjectiveCompleted;
    public bool maizeObjectiveCompleted;
    public bool templeObjectiveCompleted;

    public GameObject option1Button;
    public GameObject option2Button;
    public GameObject signpostContinueButton;
    public GameObject continueButton;
    public GameObject DialogueBox;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI dialogueText;

    private TextMeshProUGUI maizeCounter; 
    private int numOfMaize; 

    private GameObject mapPrompts;
    private TextMeshProUGUI mapPromptsText; 
    public GameObject miniMap; //reference to Minimap GameObject 
    public GameObject fullMap; //reference to the image of the full Tenochtitlan map 

    //VARIABLES//
    public bool canOpenMap = true;
    public bool paused = false;

    private AudioClip pickupSFX;
    private AudioClip interactSFX; 
    public AudioSource audioSource;
    private GameObject sfxManager;

    private string sceneName; 
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
        player = GameObject.FindWithTag("Player");
        mapPrompts = GameObject.Find("MapPrompts");
        mapPromptsText = mapPrompts.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        mapPromptsText.SetText("Map");
        pickUpIcon = GameObject.Find("PickupIcon");
        pickUpIcon.SetActive(false);
        maizeCounter = GameObject.Find("MaizeCounter").GetComponent<TextMeshProUGUI>();
        maizeCounter.SetText("");
        numOfMaize = 0;
        miniMap.SetActive(true);
        fullMap.SetActive(false);
        option1Button.SetActive(false);
        option2Button.SetActive(false);
        signpostContinueButton.SetActive(false);
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
        villageObjectiveCompleted = false;
        maizeObjectiveCompleted = false;
        templeObjectiveCompleted = false;
        maizeObjective.SetActive(false);
        villageObjective.SetActive(false);
        courtObjective.SetActive(false);
        UpdateCurrentObjective("");
        sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Zach_TestScene2")
        {
            templeObjective.SetActive(true);
        }
        else
        {
            templeObjective.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.activeSelf == false)
        {
            paused = false;
        }

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
            mapPromptsText.SetText("Close Map");
            //mapPrompts.SetActive(false);
            player.GetComponent<PlayerMovement>().canMove = false; 
            canOpenMap = false;
            mapPromptsText.SetText("Close Map");
        }

        //If player presses F again after the map is already opened, 
        //Close the map 
        else if(paused == false && player.GetComponentInChildren<PlayerInteract>().talking == false && canOpenMap == false && Input.GetKeyDown(KeyCode.F))
        {
            fullMap.SetActive(false);
            mapPromptsText.SetText("Map");
            mapPrompts.SetActive(true);
            miniMap.SetActive(true);
            player.GetComponent<PlayerMovement>().canMove = true;
            canOpenMap = true;
        }

        if(Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            PauseGame();
        }

        else if(Input.GetKeyDown(KeyCode.Escape) && paused == true)
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
            villageObjective.SetActive(true);
            templeObjective.SetActive(false);
            maizeObjective.SetActive(false);
        }

        else if(currentObjective == "Pickup Maize from the Fields")
        {
            currentObjectiveText.SetText("Pickup Maize from the Fields");
            villageObjective.SetActive(false);
            templeObjective.SetActive(false);
            maizeObjective.SetActive(true);
        }
        
        else if(currentObjective == "Bring Maize to Templo Mayor")
        {
            villageObjective.SetActive(false);
            templeObjective.SetActive(true);
            maizeObjective.SetActive(false);
        }
        else if(currentObjective == "Meet Montezuma at the Ullamaliztli Court")
        {
            templeObjective.SetActive(false);
            courtObjective.SetActive(true);
        }

        else if (currentObjective == "NextScene")
        {
            villageObjective.SetActive(false);
            templeObjective.SetActive(false);
            maizeObjective.SetActive(false);
            SceneManager.LoadScene(2);
        }

        else if (currentObjective == "ReturnToTitle")
        {
            villageObjective.SetActive(false);
            templeObjective.SetActive(false);
            maizeObjective.SetActive(false);
            SceneManager.LoadScene(0);
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
        if (numOfMaize >= 5)
        {
            maizeObjectiveCompleted = true;
            maizeObjective.SetActive(false);
            currentObjectiveText.SetText("Bring Maize to Templo Mayor");
        }
    }



}
