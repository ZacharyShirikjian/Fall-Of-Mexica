using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The sprite.flipX lines were taken from from Camjay's code on the following Unity Forum post:
//https://forum.unity.com/threads/flip-character-to-the-left.245111 

public class PlayerMovement : MonoBehaviour
{
    //PLAYER INPUT VARIABLES//
    private float horizontalInput; //player input in horizontal direction 
    private float verticalInput; //player input in vertical direction
    private float playerSpeed = 5f; //speed of the player 
    private bool lastDir; //the last horizontal direction the player was facing in, left = false, right = true
    private bool lastVertDir; //the last vertical direction the player was facing in, down = false, up = true
    public bool canMove = true;

    //REFERENCES//
    private SpriteRenderer sprite; //reference to the player's sprite 
    public GameObject minimapIcon; //reference to the minimap icon 

    // Start is called before the first frame update
    void Start()
    {
        sprite = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();
    }

   /*
    * Script used for moving the player, modified from Rob Kell's,
    * used in the FSU Game Jam game, Chernobyl.
    */
    void PlayerMove()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (canMove == true)
        {
            //Move the player to the right 
            transform.Translate(Vector3.right * Time.deltaTime * playerSpeed * horizontalInput);

            //Move the player upwards 
            transform.Translate(Vector3.up * Time.deltaTime * playerSpeed * verticalInput);

            /*
        * If the player is inputting left,
        * Keep the sprite facing left
        */
            if (horizontalInput < 0)
            {
                minimapIcon.GetComponent<SpriteRenderer>().flipX = true;
                sprite.flipX = false;
                lastDir = false;
            }

            /*
             * If the player is inputting right,
             * Flip the sprite so it is facing right 
             */
            else if (horizontalInput > 0)
            {
                minimapIcon.GetComponent<SpriteRenderer>().flipX = false;
                sprite.flipX = true;
                lastDir = true;
            }

            /*
             * If the player is inputting up,
             * Keep the sprite facing left
             */
            else if (verticalInput > 0)
            {
                minimapIcon.transform.eulerAngles = new Vector3(0, 0, -90);
                sprite.flipY = true;
                lastVertDir = true;
            }

            /*
             * In all other situations,
             * Flip the sprite depending on the direction the player is inputting (accounts for diagonals). 
             */
            else if (verticalInput < 0)
            {
                minimapIcon.transform.eulerAngles = new Vector3(0, 0, 90);
                sprite.flipX = lastDir;
                sprite.flipY = lastVertDir;
            }

            else if(verticalInput ==0)
            {
                minimapIcon.transform.eulerAngles = new Vector3(0, 0, 0);
            }
        }

       
    }
}
