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
    public bool isMoving = false;

    //REFERENCES//
    public GameObject minimapIcon; //reference to the minimap icon 
    private Animator playerAnim; //reference to Player's Animator 
    private Rigidbody2D rb2d;
    AudioSource playerAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerAnim = this.gameObject.GetComponent<Animator>();
        rb2d = this.gameObject.GetComponent<Rigidbody2D>();
        playerAudioSource = GetComponent<AudioSource>();
        isMoving = false;
        canMove = true;
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
        //horizontalInput = Input.GetAxis("Horizontal");
        //verticalInput = Input.GetAxis("Vertical");

        if (canMove == true)
        {
            horizontalInput = Input.GetAxis("Horizontal") * playerSpeed;
            verticalInput = Input.GetAxis("Vertical") * playerSpeed;
            if(rb2d.velocity.x != 0)
            {
                isMoving = true;
                playerAnim.SetBool("Moving", true);
            }
            else
            {
                isMoving = false;
                playerAnim.SetBool("Moving", false);
            }

            if(isMoving)
            {
                if(!playerAudioSource.isPlaying)
                {
                    playerAudioSource.Play();
                }
            }
            else
            {
                playerAudioSource.Stop();
            }

            //Move the player to the right 
            //transform.Translate(Vector3.right * Time.deltaTime * playerSpeed * horizontalInput);

            //Move the player upwards 
            //transform.Translate(Vector3.up * Time.deltaTime * playerSpeed * verticalInput);

            /*
        * If the player is inputting left,
        * Keep the sprite facing left
        */
            if (horizontalInput < 0)
            {
                minimapIcon.GetComponent<SpriteRenderer>().flipX = true;
                lastDir = false;
                playerAnim.SetBool("Moving", true);
                playerAnim.SetBool("FacingRight", false);
            }

            /*
             * If the player is inputting right,
             * Flip the sprite so it is facing right 
             */
            else if (horizontalInput > 0)
            {
                minimapIcon.GetComponent<SpriteRenderer>().flipX = false;
                lastDir = true;
                playerAnim.SetBool("Moving", true);
                playerAnim.SetBool("FacingRight", true);
            }

            /*
             * If the player is inputting up,
             * Keep the sprite facing up
             */
            else if (verticalInput > 0)
            {
                Debug.Log(verticalInput);
                minimapIcon.transform.eulerAngles = new Vector3(0, 0, 90);
                lastVertDir = true;
                playerAnim.SetBool("Moving", true);
                playerAnim.SetBool("FacingDown", false);
                playerAnim.SetBool("FacingRight", false);
                playerAnim.SetBool("FacingUp", true);

            }

            /*
             * In all other situations,
             * Flip the sprite depending on the direction the player is inputting (accounts for diagonals). 
             */
            else if (verticalInput < 0)
            {
                minimapIcon.transform.eulerAngles = new Vector3(0, 0, -90);
                playerAnim.SetBool("Moving", true);
                playerAnim.SetBool("FacingDown", true);
                playerAnim.SetBool("FacingRight", false);
                playerAnim.SetBool("FacingUp", false);
            }

            else if(verticalInput == 0 && horizontalInput == 0)
            {
                playerAnim.SetBool("Moving", false);
               // playerAnim.SetBool("FacingDown", false);
               // playerAnim.SetBool("FacingUp", false);
            }
        }


    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(horizontalInput, verticalInput);
    }
}
