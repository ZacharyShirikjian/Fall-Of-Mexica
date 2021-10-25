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
    public TextMeshProUGUI dialogueText; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        DialogueBox.SetActive(false);
        dialogueText = DialogueBox.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        dialogueText.SetText("");
        interactPrompt = GameObject.Find("InteractPromptText");
        interactPrompt.SetActive(false);
        interactPromptText = interactPrompt.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    //Pop up the prompt for the player to press an input to interact with something (NPC, object, etc) 
    public void InteractPrompt(string prompt)
    {
        if(player.GetComponent<PlayerMovement>().canInteract == true)
        {
            interactPrompt.SetActive(true);
            interactPromptText.SetText(prompt);
        }

        else if (player.GetComponent<PlayerMovement>().canInteract == false)
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
