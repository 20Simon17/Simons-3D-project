using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float horizontalMove;
    float verticalMove;

    public float moveSpeed;
    public float jumpForce;

    public bool canJump;
    public bool isClimbing = false;
    public bool canMove = true;

    float currentMoveSpeedlr;
    float currentMoveSpeedfb;

    Vector3 move;
    Vector3 lrmove;

    public BoxCollider groundCheck;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");
        //måste göra speedcap
        #region normalMove
        if (canMove && !isClimbing)
        {
           

            move = transform.forward * verticalMove * moveSpeed;
            lrmove = transform.right * horizontalMove * moveSpeed;
        }

        if(Input.GetKey(KeyCode.S) == false && Input.GetKey(KeyCode.W) == false)
        {
            rb.velocity = new Vector3(0,rb.velocity.y,0); //stanna spelaren?
        }
        #endregion
        
        #region ladderMove
         if (canMove && isClimbing)
        {
            //verticalMove = Input.GetAxisRaw("Vertical");

            move = transform.up * verticalMove * (moveSpeed/4);
        }
        #endregion

        if (rb.velocity.x < 5)
        {
            
            rb.AddForce(lrmove, ForceMode.VelocityChange);
        }

        if (rb.velocity.z < 5)
        {
            rb.AddForce(move, ForceMode.VelocityChange);
        }


        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.AddForce(new Vector3(0, jumpForce * 50, 0));
        }
    }

    #region CollisionChecks
    private void OnCollisionEnter(Collision collision)
    {
        #region groundCheckEnter
        if (collision.gameObject.layer == 6)
        {
            canJump = true;
        }
        #endregion

        #region ladderCheckEnter
        if (collision.gameObject.layer == 7)
        {
            isClimbing = true;
            canJump = true;
        }
        #endregion
    }

    private void OnCollisionExit(Collision collision)
    {
        #region groundCheckExit
        if (collision.gameObject.layer == 6)
        {
            canJump = false;
        }
        #endregion

        #region ladderCheckExit
        if (collision.gameObject.layer == 7)
        {
            isClimbing = false;
            canJump = false;
        }
        #endregion
    }
    #endregion
}
