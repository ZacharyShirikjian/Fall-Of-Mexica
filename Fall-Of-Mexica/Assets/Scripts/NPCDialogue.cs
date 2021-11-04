using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]

public class Sentence
{
    //Expand the size of the TextArea for the Textboxes so it's easier to add the dialogue for the NPCs
    [TextArea(1, 3)]
    public string line;
    public bool option1;
    public bool question;
    public bool option2;
    public int yes;
    public int no;
    public string yesText;
    public string noText;
}
public class NPCDialogue : MonoBehaviour
{
    //REFERENCES//

    //NPCNameText, name for the NPC that the player is talking to 
    public TextMeshProUGUI npcNameText;

    public GameObject DialogueBox;

    //Name of the NPC who is being talked to (can be changed in Inspector)
    public string npcName;

    //Reference to GameManager
    private GameManager gm;

    //Reference to the player 
    private GameObject player;

    //VARIABLES//
    public TextMeshProUGUI textDisplay;
    public Sentence[] sentences;
    public int index;
    public float typingSpeed;
    public GameObject continueButton;
    public GameObject option1Button;
    public GameObject option2Button;

    //OPTIONAL, OBJECTIVE THAT NPC GIVES PLAYER
    public string objective = ""; //optional 

    // Start is called before the first frame update
    void Start()
    {
        npcNameText.text = npcName;
        player = GameObject.FindWithTag("Player");
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //StartCoroutine(Type());
    }

    //When this script gets enabled (when the player talks to an NPC)
    //Start the Typing Coroutine to begin the dialouge again

    private void OnEnable()
    {
        StartCoroutine(Type());

        //Disable the DialogueIcon when talking to the NPC 
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        Debug.Log("Player is no longer talking to the NPC");
        this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //If the player is talking, enable the DialogueBox and begin the conversation 
        if(index == 0 && player.GetComponentInChildren<PlayerInteract>().talking == true)
        {
            DialogueBox.SetActive(true);
        }
        //If the player presses space, display all of the text for that particular sentence. 
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    textDisplay.text = sentences[index].line;
        //    StopAllCoroutines();
        //}

        // This makes sure that the user cannot spam the Continue (>>>) button.
        if (textDisplay.text == sentences[index].line)
        {
            if (sentences[index].question)
            {

                option1Button.SetActive(true);
                option2Button.SetActive(true);
                continueButton.SetActive(false);
                if (sentences[index].yesText != "")
                {
                    option1Button.GetComponentInChildren<TextMeshProUGUI>().text = sentences[index].yesText;
                    option2Button.GetComponentInChildren<TextMeshProUGUI>().text = sentences[index].noText;
                }
                else
                {
                    option1Button.GetComponentInChildren<TextMeshProUGUI>().text = "Yes";
                    option2Button.GetComponentInChildren<TextMeshProUGUI>().text = "No";
                }
            }
            else
            {
                option1Button.SetActive(false);
                continueButton.SetActive(false);
                continueButton.SetActive(true);
            }
        }
    }

    //Prints out the NPC's sentences, with adjustable typing speeds 
    IEnumerator Type()
    {
        option1Button.SetActive(false);
        option2Button.SetActive(false);
        foreach (char letter in sentences[index].line.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Goes onto the next sentence.
    public void NextSentence(bool yes)
    {
        continueButton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            //If the player clicks the option1 button, move on to the sentence responding to option 1
            if (yes)
            {
                index = sentences[index].yes;
                Debug.Log(index);
            }

            //If the player clicks the option2 button, move onto the sentence responding to option 2 
            else if (yes == false)
            {
                index = sentences[index].no;
                Debug.Log(index);
            }

            ////If there is an answer and the player clicks a button,
            //if (sentences[index].answer == true)
            //{
            //    //If the player selects the correct option, 
            //    if (sentences[index].correctAnswer == true)
            //    {

            //    }

            //    //If the player selects the incorrect option, 
            //    else if (sentences[index].correctAnswer == false)
            //    {

            //    }
            //}
            textDisplay.text = "";
            StartCoroutine(Type());
        }

        //Reset the conversation so the player can talk to the NPC again once they have finished talking with the NPC.
        else
        {
            textDisplay.text = "";
            player.GetComponentInChildren<PlayerInteract>().canInteract = true;
            player.GetComponentInChildren<PlayerInteract>().talking = false;
            DialogueBox.SetActive(false);
            //If the NPC has an objective to give to the player (assigned in inspector),
            //update the current objective of the game (call the method that the GM has) 
            if(objective != "")
            {
                gm.UpdateCurrentObjective(objective);
            }
            StopAllCoroutines();
            index = 0;
            player.GetComponent<PlayerMovement>().canMove = true;
            this.enabled = false;
        }
    }
}
