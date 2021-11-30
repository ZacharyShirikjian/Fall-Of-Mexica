using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]

public class Sentence2
{
    //Expand the size of the TextArea for the Textboxes so it's easier to add the dialogue for the NPCs
    [TextArea(1, 3)]
    public string line;
    public int yes;
    public string npcName; 
}
public class CutsceneDialogue : MonoBehaviour
{
    //REFERENCES//
    //Reference to the DialogueBox GameObject
    public GameObject DialogueBox;

    //Reference to the NPCName text, which is the name for the NPC that the player is talking to.
    public TextMeshProUGUI npcNameText;

    //The name of the NPC who is being talked to (this can be changed in the Inspector).
    public string npcName;

    //The text to display
    public TextMeshProUGUI textDisplay;

    //Array of sentence2 objects (separate from the sentence objects, which are only used for the NPC Dialogue),
    //Sentence2 objects are ONLY used for cutscenes 
    public Sentence2[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject continueButton;

    // Start is called before the first frame update
    void Start()
    {
        npcNameText.text = npcName;
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
        Debug.Log("awET");
        continueButton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;

            //If the player clicks the continue button, continue to the next sentence
            if (yes)
            {
                index = sentences[index].yes;
            }

            //Update the name of the NPCName displayed depending on which NPC is talking
            //Which can be adjusted under the "NPCName" property of a Sentence object 
            npcNameText.SetText(sentences[index].npcName);
            textDisplay.text = "";
            StartCoroutine(Type());
        }

        //End the Conversation
        else
        {
            textDisplay.text = "";
            StartCoroutine(loadSceneDelay());
            DialogueBox.SetActive(false);
        }
    }
    
    //After 3 seconds, load the next gameplay scene after the cutscene scene 
    IEnumerator loadSceneDelay()
    {
        yield return new WaitForSeconds(3.0f);

        ////UNCOMMENT THIS LATER, AFTER THE FINAL CUTSCENE SCENE IS DONE!
        //if (SceneManager.GetActiveScene().name == "FinalCutscene")
        //{
        //    SceneManager.LoadScene(0);
        //}

        //Load the next scene in the build index after the cutscene scene 
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
