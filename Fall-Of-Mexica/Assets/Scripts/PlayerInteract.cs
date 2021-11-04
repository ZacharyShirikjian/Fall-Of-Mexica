using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This script was referenced from the one made by Scripting is Fun,
//in their "Unity 2D Game Basics - Talking to NPCs video on YouTube:
//https://www.youtube.com/watch?v=N_0fyhmoZ0Y 

public class PlayerInteract : MonoBehaviour
{
    //REFERENCES//
    public NPC_Bridge object1Button;
    public NPC_Bridge2 object2Button;
    public NPC_Bridge_Continue continueButton;

    private Rigidbody2D rb2d; //reference to the player's rigidbody 
    private GameManager gm; //reference to the GameManager


    //Reference to the parent of the player 
    private PlayerMovement PlayerMoveRef; 

    public GameObject currentNPC = null;
    public NPC npcScript = null;

    public GameObject currentPickUp = null;

    public GameObject pickUpIcon; 

    //OTEHR VARIABLES//
    public bool canInteract; //If the player can interact with something, this gets set to true
    public bool holdingObject = false; //If the player is holding a pickup, set this to true 
    public bool talking = false; //If the player is talking with NPC, this gets set to true 

    void Start()
    {
        //Reference player (parent gameobject)'s rigidbody
        rb2d = gameObject.transform.parent.GetComponent<Rigidbody2D>(); 
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        PlayerMoveRef = gameObject.transform.parent.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        //For when player is talking to NPCs 
        if (Input.GetKeyDown(KeyCode.X) && currentNPC && canInteract == true)
        {
            Debug.Log("Player is talking to an NPC");
            canInteract = false;
            currentNPC.GetComponent<NPCDialogue>().enabled = true; 
            talking = true;
            PlayerMoveRef.canMove = false;
        }

        //For when the player is picking up a pickup,
        //Diplay the Pickup the player has on screen 
        else if(Input.GetKeyDown(KeyCode.X) && currentPickUp && canInteract == true)
        {
            canInteract = false;
            holdingObject = true;
            Destroy(currentPickUp.gameObject);
            Debug.Log("Player has picked up maize");
            gm.UpdateCurrentObjective("Bring the Maize to the Temple"); 
            pickUpIcon.SetActive(true); //tweak this later? 
        }

    }

    //For when something enters the player's interact radius 
    private void OnTriggerEnter2D(Collider2D other)
    {
        //When NPC enters Player's trigger zone, set the current NPC GameObject to be the one which is in the player's trigger zone
        if (other.CompareTag("NPC"))
        {
            currentNPC = other.gameObject;
            npcScript = currentNPC.GetComponent<NPC>();
            canInteract = true;
            object1Button.npcDialogueReference = currentNPC.GetComponent<NPCDialogue>();
            object2Button.npcDialogueReference = currentNPC.GetComponent<NPCDialogue>();
            continueButton.npcDialogueReference = currentNPC.GetComponent<NPCDialogue>();
            Debug.Log("Player has entered NPC's interactable range");
            gm.InteractPrompt("             Talk");
        }

        //If the player's interact radius is near a pickup object,
        //Prompt the player to pickup the pickup object 
        else if(other.CompareTag("Pickup"))
        {
            currentPickUp = other.gameObject;
            canInteract = true;
            Debug.Log("Player has entered Pickup's interactable range");
            gm.InteractPrompt("             Pickup");
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Pickup" || other.gameObject.tag == "NPC")
        {
            canInteract = false;
            Debug.Log("Player has left NPC's interactable range");
            gm.InteractPrompt("");
            if (other.gameObject == currentNPC)
            {
                currentNPC = null;
                npcScript = null;
            }

            else if(other.gameObject ==  currentPickUp)
            {
                currentPickUp = null;
            }
        }
    }
}
