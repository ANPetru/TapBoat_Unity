using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    public float initSpeed = 3f;
    public float initJumpForce = 2f;
    private const int MAX_SPEED = 5;
    private Vector3 initPosition = new Vector3(0f, 2f , -4.5f);
    private Vector3 direction;
    public JumpPowerCharger jpcs;
    private GameManager gm;
    private float speed;
    private bool isGrounded;
    private Rigidbody rb;
    private GameObject currentPath;
    private float temps;
    private bool stopTime = false;
    private bool canJump = true;

    void Start () {
        Respawn();
        
        gm = GameManager.instance;
        rb = GetComponent<Rigidbody>();
    }


    void Update () {

        if (transform.position.y <= 0)
        {
            gm.GameOver(this);
        }
        if (stopTime) return;

        if (Input.GetKeyDown(KeyCode.T))
        {

        }
        if (Input.GetMouseButtonDown(0))
        {
            temps = Time.time;
        }

        if (Input.GetMouseButtonUp(0))
        {
            canJump = true;
            if ((Time.time - temps) < 0.2)
            {
                if (Input.mousePosition.x >= Screen.width / 2)
                {
                    SwitchDirection(1);
                }
                else
                {
                    SwitchDirection(-1);
                }
            }
        }

        if (Input.GetMouseButton(0) && (Time.time - temps) > 0.2 && canJump)
        {

            stopTime = true;
            jpcs.DisplayCharger( this);
            canJump = false;

        }

	}



    public void Jump(float  jumpForce)
    {
        if (isGrounded)
        {
            isGrounded = false;
            rb.AddForce(new Vector3(0f, 2f, 0f) * initJumpForce * (jumpForce*2), ForceMode.Impulse);
            stopTime = false;
        }
    }

    public void SwitchDirection(int direc)
    {
        if (isGrounded)
            direction = (direction == new Vector3(0f, 0f, 1f)) ? new Vector3(direc, 0f, 0f) : new Vector3(0f, 0f, (direction.x==-1&&direc==-1)?-1: (direction.x == -1 && direc == 1) ?1: (direction.x == 1 && direc == -1)?1:-1);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Spike"))
        {
            gm.GameOver(this);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (currentPath!=null && currentPath != collision.gameObject)
        {
            
            gm.SpawnPath();
            if (speed <= MAX_SPEED)
            {
                speed += 0.2f;


            }
        }
        currentPath = collision.gameObject;

        isGrounded = true;

    }

    private void FixedUpdate()
    {
        if (stopTime) return;
        transform.position += direction * Time.deltaTime * speed;
    }

    public void Respawn()
    {
        speed = initSpeed;
        direction = new Vector3(0f, 0f, 1f);
        transform.position = initPosition;
        transform.rotation = Quaternion.identity;
        jpcs.ResetDecreasedTime();
        
    }



    public int GetXOffset()
    {
        return gm.GetPathPosition(currentPath);

    }

}
