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

    //REFERENCE TO SFXMANAGER
    private GameObject sfxManager;
    public AudioSource audioSource;
    private AudioClip dialogue;

    //Array of sentence2 objects (separate from the sentence objects, which are only used for the NPC Dialogue),
    //Sentence2 objects are ONLY used for cutscenes 
    public Sentence2[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject continueButton;

    //REFERENCE TO CUTSCENE BGs//
    public Sprite cutsceneBG2;
    public Sprite cutsceneBG3;
    public Sprite cutsceneBG4;
    public GameObject curCutsceneBG;
    private SpriteRenderer curCutsceneBGSprite;

    public bool paused = false;
    public GameObject PauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        npcNameText.text = npcName;
        sfxManager = GameObject.Find("SFXManager");
        dialogue = sfxManager.GetComponent<SFXManager>().dialouge;
        curCutsceneBGSprite = curCutsceneBG.GetComponent<SpriteRenderer>();
        PauseMenu.SetActive(false); 
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

        if (Input.GetKeyDown(KeyCode.Escape) && paused == false)
        {
            PauseGame();

        }

        else if (Input.GetKeyDown(KeyCode.Escape) && paused == true)
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
        Debug.Log(index);
        continueButton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;

            //If the player clicks the continue button, continue to the next sentence
            if (yes)
            {
                index = sentences[index].yes;
                if(index == 4)
                {
                    curCutsceneBGSprite.sprite = cutsceneBG2;
                }

                else if(index == 6)
                {
                    curCutsceneBGSprite.sprite = cutsceneBG3;
                }

                else if (index == 10)
                {
                    curCutsceneBGSprite.sprite = cutsceneBG4;
                }
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
        SceneManager.LoadScene("Zach_TestScene2");
    }
}
