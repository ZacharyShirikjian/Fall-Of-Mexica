using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignpostBridge : MonoBehaviour
{
    public SignpostDialogue signpostRef;

    public void CallSignpostDialogueNextSentence()
    {
        if (signpostRef != null)
        {
            signpostRef.NextSentence(true);
        }

    }
}
