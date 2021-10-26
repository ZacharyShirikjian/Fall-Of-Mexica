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

    private Rigidbody2D rb2d; //reference to the player's rigidbody 
    private GameManager gm; //reference to the GameManager

    public GameObject currentNPC = null;
    public NPC npcScript = null;


    //OTEHR VARIABLES//
    public bool canInteract; //If the player can interact with something, this gets set to true
    public bool talking = false; //If the player is talking with NPC, this gets set to true 

    void Start()
    {
        //Reference player (parent gameobject)'s rigidbody
        rb2d = gameObject.transform.parent.GetComponent<Rigidbody2D>(); 
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //For when player is talking to NPCs 
        if (Input.GetKeyDown(KeyCode.X) && currentNPC && canInteract == true)
        {
            Debug.Log("Player is talking to an NPC");
            rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
            //npcScript.Talk();
            canInteract = false;
            currentNPC.GetComponent<NPCDialogue>().enabled = true; 
            talking = true;
        }
    }

    //When NPC enters Player's trigger zone, set the current NPC GameObject to be the one which is in the player's trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("NPC"))
        {
            currentNPC = other.gameObject;
            npcScript = currentNPC.GetComponent<NPC>();
            canInteract = true;
            object1Button.npcDialogueReference = currentNPC.GetComponent<NPCDialogue>();
            object2Button.npcDialogueReference = currentNPC.GetComponent<NPCDialogue>();
            Debug.Log("Player has entered NPC's interactable range");
            gm.InteractPrompt("Press X to Talk");
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject == currentNPC)
        {
            currentNPC = null;
            npcScript = null;
            canInteract = false;
            Debug.Log("Player has left NPC's interactable range");
            gm.InteractPrompt("");
        }
    }
}
