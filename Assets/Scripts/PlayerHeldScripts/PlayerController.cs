using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
// using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    public Vector3 exitPos;
    public Vector3 startPos;
    public float moveSpeed = 5;
    public float movePosThreshold = 0.2f;
    public float enterDis = 1f;
    public bool inTower = true;
    private Transform playerPos;
    private Rigidbody2D playerRb;
    private CircleCollider2D playerCollider;
    private Vector2 movePos = new Vector2(20, 20);
    public GameObject tower;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPos = GetComponent<Transform>();
        playerRb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CircleCollider2D>();
        tower.layer = 6;
    }

    // old code for click move behavior
        /* // determines if the player reached the last clicked move point
        // and stops their velocity if they did
        float posDiff = (float)Math.Sqrt(Math.Pow(playerPos.position.x - movePos.x, 2) + Math.Pow(playerPos.position.y - movePos.y, 2));
        if(posDiff <= movePosThreshold){
            playerRb.linearVelocity = Vector2.zero;
        }*/

    // WASD controls
    void FixedUpdate()
    {
        if(!inTower){ 
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            Vector2 movement = new Vector2(x, y).normalized * moveSpeed;

            playerRb.linearVelocity = movement;
        }
    }

    //using spacebar/jump button to move in and out of tower
    void OnJump()
    {
        if (WeaponInfoController.isPaused)
        {
            return;
        }

        float distanceFromTower = (float)Math.Sqrt(Math.Pow(startPos.x - transform.position.x, 2) + 
                                                   Math.Pow(startPos.y - transform.position.y, 2));
        if(distanceFromTower <= enterDis + Math.Abs(exitPos.x)){
            if (inTower){
                tower.layer = 0;
                playerPos.position = exitPos;
            }else{
                tower.layer = 6;
                playerPos.position = startPos;
                playerRb.linearVelocity = Vector2.zero;
            }
            inTower = !inTower;
            playerCollider.isTrigger = !playerCollider.isTrigger;
        }
        
    }

    // old code for click move behavior
    /*//uses right mouse click to point to where the player is moving
    void OnMoveClick(){
        if(!inTower && !isWASD){
            Vector2 mouseWorldPosition =  Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 playerDir = new Vector2(mouseWorldPosition.x - playerPos.position.x,
                                            mouseWorldPosition.y - playerPos.position.y);
            playerRb.linearVelocity = playerDir.normalized * moveSpeed;
            movePos = mouseWorldPosition;
        }
    }*/
}
