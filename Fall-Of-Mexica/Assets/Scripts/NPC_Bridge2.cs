using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Bridge2 : MonoBehaviour
{
    public NPCDialogue npcDialogueReference;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CallNPCDialogueNextSentence()
    {
        if (npcDialogueReference != null)
        {
                npcDialogueReference.NextSentence(false);
        }

    }
}
