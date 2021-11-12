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
    public GameObject DialogueBox;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI dialogueText;

    private TextMeshProUGUI maizeCounter; 
    private int numOfMaize; 

    private GameObject mapPrompts; 
    public GameObject fullMap; //reference to the image of the full Tenochtitlan map 

    //VARIABLES//
    public bool canOpenMap = true;
    public bool paused = false; 

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
        fullMap.SetActive(false);
        option1Button.SetActive(false);
        option2Button.SetActive(false);
        DialogueBox.SetActive(false);
        dialogueText = DialogueBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        dialogueText.SetText("");
        npcNameText.SetText("");
        interactPrompt = GameObject.Find("InteractPrompt");
        interactPromptText = interactPrompt.GetComponentInChildren<TextMeshProUGUI>();
        interactPrompt.SetActive(false);
        currentObjectiveUI = GameObject.Find("CurrentObjective");
        currentObjectiveText = currentObjectiveUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        UpdateCurrentObjective("");
    }

    // Update is called once per frame
    void Update()
    {

        if(player.GetComponentInChildren<PlayerInteract>().talking == true)
        {
            canOpenMap = false;
            mapPrompts.SetActive(false);
        }

        //If player is pressing F, map appears
        if (Input.GetKeyDown(KeyCode.F) && canOpenMap == true && player.GetComponentInChildren<PlayerInteract>().talking == false)
        {
            fullMap.SetActive(true);
            mapPrompts.SetActive(false);
            player.GetComponent<PlayerMovement>().canMove = false; 
            canOpenMap = false; 
        }

        //If player presses F again after the map is already opened, 
        //Close the map 
       else if(Input.GetKeyDown(KeyCode.F) && canOpenMap == false)
        {
            fullMap.SetActive(false);
            mapPrompts.SetActive(true);
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
            currentObjectiveText.SetText("Explore");
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
        if(numOfMaize >= 7)
        {
            currentObjectiveText.SetText("Bring Maize to Templo Mayor");
        }
    }



}
