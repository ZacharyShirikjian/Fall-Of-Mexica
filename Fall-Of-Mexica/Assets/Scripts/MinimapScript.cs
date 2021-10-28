using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//THIS SCRIPT IS FROM BRACKEY'S HOW TO MAKE A MINIMAP IN UNITY VIDEO ON YOUTUBE: 
//https://www.youtube.com/watch?v=28JTTXqMvOU 
public class MinimapScript : MonoBehaviour
{
    public Transform player;

    //Use this to follow around the player correctly 
    void LateUpdate()
    {
        //update the position of the map's player to the current position of the player 
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition; 
    }

}
