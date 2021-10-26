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
    public GameObject option1Button;
    public GameObject option2Button; 
    public GameObject DialogueBox;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI dialogueText;
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
        fullMap.SetActive(false);
        option1Button.SetActive(false);
        option2Button.SetActive(false);
        DialogueBox.SetActive(false);
        dialogueText = DialogueBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        dialogueText.SetText("");
        npcNameText.SetText("");
        interactPrompt = GameObject.Find("InteractPromptText");
        interactPromptText = interactPrompt.GetComponent<TextMeshProUGUI>();
        interactPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponentInChildren<PlayerInteract>().talking == true)
        {
            canOpenMap = false; 
        }

        //If player is pressing G, map appears
        if (Input.GetKeyDown(KeyCode.G) && canOpenMap == true && player.GetComponent<PlayerInteract>().talking == false)
        {
            fullMap.SetActive(true);
            canOpenMap = false; 
        }

        //If player presses G again after the map is already opened, 
        //Close the map 
       else if(Input.GetKeyDown(KeyCode.G) && canOpenMap == false)
        {
            fullMap.SetActive(false);
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

    //MIGHT DELETE THIS LATER AND COMBINE INTO 1 METHOD 
    public void CloseInteractPrompt()
    {
        interactPrompt.SetActive(false);
        interactPromptText.SetText("");
    }

}
