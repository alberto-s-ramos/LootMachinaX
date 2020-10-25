using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {


    public Transform playerCam, character, centerPoint;


    private float mouseX, mouseY;
    public float mouseSensivity = 10.0f;
    public float mouseYPosition = 1.0f;

    private float moveFB, moveLR;
    public float moveSpeed = 8f;
    private float initialMoveSpeed = 2f;

    private float zoom;
    public float zoomSpeed = 2f;

    public float zoomMin = -2f;
    public float zoomMax = -7f;

    public float rotationSpeed = 5f;


    private string fowardMovement = "Walk Forward";

    private bool isShiftKeyDown;

    private Animator anim;
    private float verticalVelocity;
    private float gravity = 13.0f;
    public float jumpForce = 8.0f;
    public float superJumpForce = 16.0f;

    private float initialY = 0f;
    private bool notAir = true;
    private float timeToWork = -1;
    public float distToTheGround = 2.2f;

    private float timeToSuperJump = 0;
    private float timeToDash = 0f;
    private float timeToJump = 0;


    public RawImage skill1;
    public RawImage skill2;
    public RawImage skill3;
    public RawImage breakCrystal;


    public RawImage fallMessage;
    public GameObject closeButton;
    public Transform whereToDemo2;
    private int numberOfTimesMessageShowedUp = 0;

    private GameObject crystal ;

    //DASH-----
    public float maxDashTime = 50.0f;
    public float dashSpeed = 20f;
    public float dashStoppingSpeed = 0.1f;
    private float dashDistance = 35;
    public Vector3 moveDashDirection;
    private float currentDashTime;


    //AUDIO-----
    public AudioClip moveAudio;
    private AudioSource moveAudioSource;
    public GameObject jumpAudioObj;
    private AudioSource jumpAudioSource;
    public GameObject dashAudioObj;
    private AudioSource dashAudioSource;
    public GameObject breakAudioObj;


    public AudioClip JumpSound;
    public AudioClip DashSound;

    private AudioSource breakAudioSource;
    public AudioClip BreakSound;


    void Start(){
        zoom = -6;
        anim = character.GetComponent<Animator>();
        currentDashTime = maxDashTime;
        crystal = GameObject.Find("C_Default");
        moveAudioSource = GetComponent<AudioSource>();
        jumpAudioSource = jumpAudioObj.GetComponent<AudioSource>();
        dashAudioSource = dashAudioObj.GetComponent<AudioSource>();
        breakAudioSource = breakAudioObj.GetComponent<AudioSource>();


    }

    void Update()
    {
        timeToSuperJump += Time.deltaTime;
        timeToJump += Time.deltaTime;

        if (timeToSuperJump > 10)
        {
            skill1.CrossFadeAlpha(1f, 0.2f, false);
        }
        if (timeToDash > 3)
        {
            skill2.CrossFadeAlpha(1f, 0.2f, false);
        }
        else if (timeToDash <= 3)
        {
            skill2.CrossFadeAlpha(0.07f, 0.2f, false);
        }

        isShiftKeyDown = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        if (character.GetComponent<PlayerState>().canJump == true)
        {
            notAir = true;
          
            if (Input.GetKeyDown(KeyCode.Space) && Time.fixedTime > timeToWork && timeToJump >1.3f)
            {
        
                timeToWork = Time.fixedTime + 3 / 2;
                anim.SetBool("Jump", true);
                verticalVelocity = jumpForce;
                notAir = false;
                timeToJump = 0f;
                jumpAudioSource.PlayOneShot(JumpSound);
            }
            else if (Input.GetKeyDown(KeyCode.Alpha1) && Time.fixedTime > timeToWork && timeToSuperJump > 10 && character.GetComponent<PlayerState>().canSuperJump == true )
            {
                print("Super Jump");
                timeToWork = Time.fixedTime + 3 / 2;
                anim.SetBool("Jump", true);
                verticalVelocity = superJumpForce;
                notAir = false;
                timeToSuperJump = 0;
                skill1.CrossFadeAlpha(0.07f, 0.2f, false);
                jumpAudioSource.PlayOneShot(JumpSound);

            }
            else
            {
                verticalVelocity -= gravity * Time.deltaTime;
                initialY = character.position.y;
            }
        }
        else if (character.GetComponent<PlayerState>().canJump == false)
        {
            verticalVelocity -= 0.3f * gravity * Time.deltaTime;
        }


        Vector3 moveVector = new Vector3(0, verticalVelocity, 0);
        character.GetComponent<CharacterController>().Move(moveVector * Time.deltaTime);

 


        //--------- Camera ZOOM---------
        zoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        if (zoom > zoomMin)
            zoom = zoomMin;

        if (zoom < zoomMax)
            zoom = zoomMax;

        playerCam.transform.localPosition = new Vector3(0, 0, zoom);
        //-----------
        if (Input.GetMouseButton(1))
        {
            mouseX += Input.GetAxis("Mouse X");
            mouseY -= Input.GetAxis("Mouse Y");
        }
        mouseY = Mathf.Clamp(mouseY, -10f, 60f);
        playerCam.LookAt(centerPoint);
        centerPoint.localRotation = Quaternion.Euler(mouseY, mouseX, 0);

        if (character.GetComponent<PlayerState>().canWalkFast == true)
        {
            moveFB = Input.GetAxis("Vertical") * moveSpeed;
            moveLR = Input.GetAxis("Horizontal") * moveSpeed;
            fowardMovement = "Run Forward";
        }
        else if (character.GetComponent<PlayerState>().canWalkFast == false)
        {
            moveFB = Input.GetAxis("Vertical") * initialMoveSpeed;
            moveLR = Input.GetAxis("Horizontal") * initialMoveSpeed;
            fowardMovement = "Walk Forward";

        }

        Vector3 movement = new Vector3(moveLR, 0, moveFB);
        movement = character.rotation * movement;

        character.GetComponent<CharacterController>().Move(movement * Time.deltaTime);
        centerPoint.position = new Vector3(character.position.x, character.position.y + mouseYPosition, character.position.z);

        if (Input.GetAxis("Horizontal") > 0 | Input.GetAxis("Horizontal") < 0 |
             Input.GetAxis("Vertical") > 0 | Input.GetAxis("Vertical") < 0)
        {
            Quaternion turnAngle = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);
            character.rotation = Quaternion.Slerp(character.rotation, turnAngle, Time.deltaTime * rotationSpeed);
        }

        /*
         * Animations before I discovered Blend Trees and animation controller
         */
        if (moveFB > 0)
        {
            anim.SetBool("Walk Backward", false);
            anim.SetBool("Strafe Left", false);
            anim.SetBool("Strafe Right", false);
            anim.SetBool("" + fowardMovement, true);
        }
        if (moveFB < 0)
        {
            anim.SetBool("Run Forward", false);
            anim.SetBool("Strafe Left", false);
            anim.SetBool("Strafe Right", false);
            anim.SetBool("" + fowardMovement, true);
        }
        if (moveLR < 0)
        {
            anim.SetBool("" + fowardMovement, false);
            anim.SetBool("Walk Backward", false);
            anim.SetBool("Strafe Right", false);
            anim.SetBool("Strafe Left", true);
        }
        if (moveLR > 0)
        {
            anim.SetBool("" + fowardMovement, false);
            anim.SetBool("Walk Backward", false);
            anim.SetBool("Strafe Left", false);
            anim.SetBool("Strafe Right", true);
        }

        if (moveLR < 0 && moveFB > 0)
        {
            anim.SetBool("Walk Backward", false);
            anim.SetBool("Strafe Left", false);
            anim.SetBool("Strafe Right", false);
            anim.SetBool("" + fowardMovement, true);
        }
        if (moveLR > 0 && moveFB > 0)
        {
            anim.SetBool("Walk Backward", false);
            anim.SetBool("Strafe Left", false);
            anim.SetBool("Strafe Right", false);
            anim.SetBool("" + fowardMovement, true);
        }
        if (moveLR < 0 && moveFB < 0)
        {
            anim.SetBool("" + fowardMovement, false);
            anim.SetBool("Strafe Left", false);
            anim.SetBool("Strafe Right", false);
            anim.SetBool("Walk Backward", true);
        }
        if (moveLR > 0 && moveFB < 0)
        {
            anim.SetBool("" + fowardMovement, false);
            anim.SetBool("Strafe Left", false);
            anim.SetBool("Strafe Right", false);
            anim.SetBool("Walk Backward", true);
        }
        if (moveFB == 0 && moveLR == 0)
        {
            anim.SetBool("" + fowardMovement, false);
            anim.SetBool("Walk Backward", false);
            anim.SetBool("Strafe Left", false);
            anim.SetBool("Strafe Right", false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && GetComponent<PlayerState>().canDash == true && Time.fixedTime > timeToWork && timeToDash > 3)
        {
            timeToWork = Time.fixedTime + 3 / 2;
            print("Dash");
            currentDashTime = 0.0f;
            timeToDash = 0;
            dashAudioSource.PlayOneShot(DashSound);

        }
        if (currentDashTime < maxDashTime)
        {
            anim.Play("Charging");
            Quaternion turnAngle2 = Quaternion.Euler(0, centerPoint.eulerAngles.y, 0);
            character.rotation = Quaternion.Slerp(character.rotation, turnAngle2, Time.deltaTime * rotationSpeed);
            moveDashDirection = centerPoint.forward * dashDistance;
            currentDashTime += dashStoppingSpeed;

        }
        else
        {
            moveDashDirection = Vector3.zero;
        }

        this.GetComponent<CharacterController>().Move(moveDashDirection * Time.deltaTime);
        timeToDash += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Alpha3) && GetComponent<PlayerState>().canBreak == true && crystal.gameObject.name != "C_Default")
        {
            breakCrystal.enabled = false;
            breakAudioSource.PlayOneShot(BreakSound);
            print("played crystal break");
            Destroy(crystal);
            crystal = GameObject.Find("C_Default");
        }
        if (crystal.gameObject.name != "C_Default"){
            skill3.CrossFadeAlpha(1f, 0.2f, false);
        }
        else if (crystal.gameObject.name == "C_Default")
        {
            skill3.CrossFadeAlpha(0.07f, 0.2f, false);
        }



        if(character.position.y>100 && character.position.z>155 && numberOfTimesMessageShowedUp==0){
            fallMessage.enabled = true;
            closeButton.SetActive(true);
            character.GetComponent<PlayerState>().setTeleportDemo(true);
            numberOfTimesMessageShowedUp++;
        }
        if(character.GetComponent<PlayerState>().getCanTeleportDemo()==true && isShiftKeyDown && Input.GetKeyDown(KeyCode.T)){
            character.transform.position = whereToDemo2.transform.position;

        }

        //SOUNDS
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            moveAudioSource.mute = false;

        }
        else if (!Input.GetButton("Horizontal") || !Input.GetButton("Vertical"))
        {
            moveAudioSource.mute = true;
        }

    }


    public void setCrystal(GameObject newcrystal){
        this.crystal = newcrystal;
    }

  

}