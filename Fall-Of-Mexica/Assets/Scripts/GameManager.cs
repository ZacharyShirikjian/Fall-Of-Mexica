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
    private GameObject player;
    public GameObject DialogueBox;
    public GameObject npcNameBox;
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI dialogueText; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        npcNameBox.SetActive(false);
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
