using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]

public class Sentence3
{
    //Expand the size of the TextArea for the Textboxes so it's easier to add the dialogue for the NPCs
    [TextArea(1, 3)]
    public string line;
    public int yes;
    public string signpostName;
}
public class SignpostDialogue : MonoBehaviour
{
    //REFERENCES//
    //Reference to the DialogueBox GameObject
    public GameObject DialogueBox;

    //Reference to the NPCName text, which is the name for the NPC that the player is talking to.
    public TextMeshProUGUI signpostNameText;

    //The name of the NPC who is being talked to (this can be changed in the Inspector).
    public string signpostName;

    //The text to display
    public TextMeshProUGUI textDisplay;

    //Array of sentence2 objects (separate from the sentence objects, which are only used for the NPC Dialogue),
    //Sentence2 objects are ONLY used for cutscenes 
    public Sentence3[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject continueButton;

    //Reference to the player 
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        DialogueBox.SetActive(true); 
        signpostNameText.text = signpostName;
        StartCoroutine(Type());
    }

    // Update is called once per frame
    void Update()
    {
        //If the player presses space, display all of the text for that particular sentence. 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            textDisplay.text = sentences[index].line;
            StopAllCoroutines();
        }

        // This makes sure that the user cannot spam the Continue (>>>) button.
        if (textDisplay.text == sentences[index].line)
        {
            continueButton.SetActive(true);
        }
    }

    // This prints out the sentences; adjustable typing speeds.
    IEnumerator Type()
    {
        foreach (char letter in sentences[index].line.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    // Goes onto the next sentence.
    public void NextSentence(bool yes)
    {
        if (index < sentences.Length - 1)
        {
            //index++;

            //If the player clicks the continue button, continue to the next sentence
            if (yes)
            {
                index = sentences[index].yes;
            }

            //Update the name of the NPCName displayed depending on which NPC is talking
            //Which can be adjusted under the "NPCName" property of a Sentence object 
            signpostNameText.SetText(sentences[index].signpostName);
            textDisplay.text = "";
            StartCoroutine(Type());
        }

        //End the Conversation
        else
        {
            textDisplay.text = "";
            DialogueBox.SetActive(false);
            player.GetComponentInChildren<PlayerInteract>().canInteract = true;
            player.GetComponentInChildren<PlayerInteract>().talking = false;
            DialogueBox.SetActive(false);

            StopAllCoroutines();
            player.GetComponent<PlayerMovement>().canMove = true;
            this.enabled = false;
        }
        continueButton.SetActive(false);
    }
}
