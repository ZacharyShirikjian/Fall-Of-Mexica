using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPC : MonoBehaviour
{
    //REFERENCES//
    private GameManager gm;
    private GameObject player;

    // Start  is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
       
        //if (player.GetComponent<PlayerMovement>().talking == true)
        //{
        //    Talk();
        //}
        //else if (player.GetComponent<PlayerMovement>().talking == false)
        //{
        //    //Talk();
        //}
    }
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    /*
    //     * If the player enters the NPC's trigger zone, 
    //     * Prompt the player to press X to talk to the NPC. 
    //     */
    //    if(other.gameObject.tag == "Player")
    //    {
    //        player.GetComponent<PlayerInteract>().canInteract = true;
    //        gm.InteractPrompt("Press X to Talk");
    //        Debug.Log("Player has entered NPC's interactable range");
    //    }
    //}

    //private void OnTriggerExit2D(Collider2D other)
    //{
    //   /*
    //    * If the player leaves the NPC's trigger zone, 
    //    * The button prompt goes away. 
    //    */
    //    if (other.tag == "Player")
    //    {
    //        player.GetComponent<PlayerInteract>().canInteract = false;
    //        gm.InteractPrompt("");
    //        Debug.Log("Player has left NPC's interactable range");
    //    }
    //}

    //public void Talk()
    //{
    //    gm.DialogueBox.SetActive(true);
    //    for(int i = 0; i < sentences.Count; i++)
    //    {
    //        gm.dialogueText.SetText(dialogue[i]);
    //        if (i == (dialogue.Count- 1))
    //        {
    //            player.GetComponentInChildren<PlayerInteract>().talking = false;
    //            gm.DialogueBox.SetActive(false);
    //        }
    //    }
       
    //}
}
