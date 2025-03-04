using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
// using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // Gun prefab will be parented to weaponMount on instantiation
    public Transform weaponMount;
    public Gun currentWeapon;
    public Vector3 exitPos;
    public Vector3 startPos;
    public float moveSpeed = 5;
    public float movePosThreshold = 0.2f;
    public bool isWASD = false;
    private bool inTower = true;
    private Transform playerPos;
    private Rigidbody2D playerRb;
    private Vector2 movePos = new Vector2(20, 20);
    public GameObject tower;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPos = GetComponent<Transform>();
        playerRb = GetComponent<Rigidbody2D>();
        tower.layer = 6;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector2 worldPoint2D = Camera.main.ScreenToWorldPoint(mousePosition) - transform.position;
        float angle = Mathf.Atan2(worldPoint2D.y, worldPoint2D.x) * Mathf.Rad2Deg;

        // Add 180 to angle because rotation is opposite otherwise... for some reason
        transform.rotation = Quaternion.Euler(0, 0, angle + 180);

        if (Input.GetMouseButtonDown(0))
        {
            currentWeapon.Fire(worldPoint2D);
        }
        
        // determines if the player reached the last clicked move point
        // and stops their velocity if they did
        float posDiff = (float)Math.Sqrt(Math.Pow(playerPos.position.x - movePos.x, 2) + Math.Pow(playerPos.position.y - movePos.y, 2));
        if(posDiff <= movePosThreshold){
            playerRb.linearVelocity = Vector2.zero;
        }
    }

    // WASD controls
    void FixedUpdate()
    {
        if(isWASD && !inTower){ 
            float x = Input.GetAxisRaw("Horizontal");
            float y = Input.GetAxisRaw("Vertical");

            Vector2 movement = new Vector2(x, y).normalized * moveSpeed;

            playerRb.linearVelocity = movement;
        }
    }

    //using spacebar/jump button to move in and out of tower
    void OnJump()
    {
        if (inTower)
        {
            tower.layer = 0;
            playerPos.position = exitPos;
        }
        else
        {
            tower.layer = 6;
            playerPos.position = startPos;
        }
        inTower = !inTower;
    }

    //uses right mouse click to point to where the player is moving
    void OnMoveClick(){
        if(!inTower && !isWASD){
            Vector2 mouseWorldPosition =  Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 playerDir = new Vector2(mouseWorldPosition.x - playerPos.position.x,
                                            mouseWorldPosition.y - playerPos.position.y);
            playerRb.linearVelocity = playerDir.normalized * moveSpeed;
            movePos = mouseWorldPosition;
        }
    }
}
