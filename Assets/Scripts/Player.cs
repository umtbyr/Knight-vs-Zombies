using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    
    
    public Animator playerAnim;
    public Rigidbody playerRigid;
    public float w_speed, wb_speed, olw_speed, rn_speed, ro_speed;
    public bool walking;
    public Transform playerTrans;
    public Transform cam;
    public float CoolDownTime = 2f;
    private float nextFireTime = 0;
    public static int numOfCliks = 0;
    private float lastClickTime;
    private float maxComboDelay = 2f;
    public static bool isAttacking = false;
    

    public bool lockCursor = true;

    

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    private void Start()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        
    }


    private void FixedUpdate()
    {
       
        if (Input.GetKey(KeyCode.W))
        {
            if (AnimaitonEventHandler.ismovable)
            {
                
                playerRigid.velocity = transform.forward * w_speed * Time.deltaTime;
            }
            
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (AnimaitonEventHandler.ismovable)
            {

                playerRigid.velocity = transform.forward * w_speed * Time.deltaTime;
            }

           
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (AnimaitonEventHandler.ismovable)
            {

                playerRigid.velocity = transform.forward * w_speed * Time.deltaTime;
            }

        }

        if (Input.GetKey(KeyCode.S))
        {
            if (AnimaitonEventHandler.ismovable)
            {
                playerRigid.velocity = transform.forward * wb_speed * Time.deltaTime;
            }
            
        }

        if (!walking || AnimaitonEventHandler.ismovable == false)
        {
            playerRigid.velocity = Vector3.zero;
        }

        
        

    }
    // Update is called once per frame
    void Update()
    {
      
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude != 0)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f,targetAngle, 0f) * Vector3.forward;
            

        }
        if (playerAnim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            numOfCliks = 0;
            playerAnim.SetBool("hit2", false);
            playerAnim.SetBool("hit3", false);
        }

        
        if (playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime>0.7 && playerAnim.GetCurrentAnimatorStateInfo(0).IsName("hit1")){
            playerAnim.SetBool("hit1", false);
        }
        if (playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7 && playerAnim.GetCurrentAnimatorStateInfo(0).IsName("hit2")){
            playerAnim.SetBool("hit2", false);
            playerAnim.SetBool("hit1", false);
        }
        if (playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.7 && playerAnim.GetCurrentAnimatorStateInfo(0).IsName("hit3")){
            playerAnim.SetBool("hit1", false);
            playerAnim.SetBool("hit2", false);
            playerAnim.SetBool("hit3", false);
            numOfCliks = 0;
        }
        if(Time.time - lastClickTime > maxComboDelay)
        {
            numOfCliks = 0;
        }
        if(Time.time > nextFireTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                onClick();
            }
        }


        if (Input.GetKey(KeyCode.W))
        {
            playerAnim.SetTrigger("walk");
            playerAnim.ResetTrigger("idle");
            walking = true;
        }
      

        if (Input.GetKeyUp(KeyCode.W))
        {
            playerAnim.ResetTrigger("walk");
            playerAnim.SetTrigger("idle"); 
            walking = false;

        }

        if (Input.GetKey(KeyCode.S))
        {
            playerAnim.SetTrigger("walk");
            playerAnim.ResetTrigger("idle");
            walking = true;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            playerAnim.ResetTrigger("walk");
            playerAnim.SetTrigger("idle");
            
            walking = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            playerAnim.SetTrigger("walk");
            playerAnim.ResetTrigger("idle");
            walking = true;


        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            playerAnim.ResetTrigger("walk");
            playerAnim.SetTrigger("idle");
            walking = false;



        }

        if (Input.GetKey(KeyCode.D))
        {
            playerAnim.SetTrigger("walk");
            playerAnim.ResetTrigger("idle");
            walking = true;


        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            playerAnim.ResetTrigger("walk");
            playerAnim.SetTrigger("idle");
            walking = false;


        }

        if (walking == true)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                w_speed = rn_speed;
                playerAnim.SetTrigger("run");
                playerAnim.ResetTrigger("walk");
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                w_speed = olw_speed;
                playerAnim.ResetTrigger("run");
                playerAnim.SetTrigger("walk");
                
            }


        }
        
        }

    void onClick()
    {
        
        
        lastClickTime = Time.time;
        numOfCliks++;
        if(numOfCliks == 1)
        {
            playerAnim.SetBool("hit1", true);
            
        }
        numOfCliks = Mathf.Clamp(numOfCliks, 0, 3);
        if( playerAnim.GetCurrentAnimatorStateInfo(0).IsName("hit1"))
        {
            playerAnim.SetBool("hit2", true);
            playerAnim.SetBool("hit1", false);
            Debug.Log("worked");
        }
        if(  playerAnim.GetCurrentAnimatorStateInfo(0).IsName("hit2"))
        {
            playerAnim.SetBool("hit3", true);
            playerAnim.SetBool("hit1", false);
            playerAnim.SetBool("hit2", false);
            
        }
        
    }









    /*
    if (comboTimer <= 0)
    {
        comboCount  = 0;
        comboTimer = 1f;
        playerAnim.ResetTrigger("LfistCombo");
    }

    if (Input.GetMouseButtonDown(0))

    {
     playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime >0.7f

        Debug.Log(comboCount);
        if (comboCount == 1 && comboTimer >= 0)
        {
            playerAnim.SetTrigger("LfistCombo");
            playerAnim.ResetTrigger("idle");
            comboCount--;

        }
        if (comboCount == 0)
        {
            playerAnim.SetTrigger("Lfist");
            playerAnim.ResetTrigger("idle");
            comboCount++;

        }
    }


    if (Input.GetMouseButtonUp(0))
    {


        playerAnim.SetTrigger("idle");
        playerAnim.ResetTrigger("Lfist");

    }

    */












}
