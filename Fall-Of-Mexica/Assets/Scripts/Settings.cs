using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    //SerializedField means that value is saved when you come back to the game after quitting it 
    [SerializeField] public static int volume = 100;

    public static void ChangeVolume(int value)
    {
        volume = value;
    }
}
