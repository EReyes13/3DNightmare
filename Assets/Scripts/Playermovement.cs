using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    public Camera Eyes;

    public Rigidbody RB;

    public bool Spawn1 = false;
    public bool Spawn2 = false;

    public bool Victory = false;

    public float Reset = 5;

    //public static bool Sharp;
    
    public float MouseSensitivity = 3;
    public float WalkSpeed = 10;
    public float JumpPower = 7;
    public List<GameObject> Floors;
    // Update is called once per frame
    void Start()
    {
        //Turn off my mouse and lock it to center screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update()
    {
        float xRot = Input.GetAxis("Mouse X") * MouseSensitivity;
        transform.Rotate(0, xRot, 0);

        //If my mouse goes up/down my aim (but not body) go up/down
        float yRot = -Input.GetAxis("Mouse Y") * MouseSensitivity;
        Eyes.transform.Rotate(yRot, 0, 0);

        //Movement code
        if (WalkSpeed > 0)
        {
            //My temp velocity variable
            Vector3 move = Vector3.zero;

            //transform.forward/right are relative to the direction my body is facing
            if (Input.GetKey(KeyCode.W))
                move += transform.forward;
            if (Input.GetKey(KeyCode.S))
                move -= transform.forward;
            if (Input.GetKey(KeyCode.A))
                move -= transform.right;
            if (Input.GetKey(KeyCode.D))
                move += transform.right;
            //I reduce my total movement to 1 and then multiply it by my speed
            move = move.normalized * WalkSpeed;

            //If I hit jump and am on the ground, I jump
            if (JumpPower > 0 && Input.GetKeyDown(KeyCode.Space) && OnGround())
                move.y = JumpPower;
            else  //Otherwise, my Y velocity is whatever it was last frame
                move.y = RB.linearVelocity.y;

            //Plug my calculated velocity into the rigidbody
            RB.linearVelocity = move;
        }
     
        if (Reset <= 0.01)
        {
            Spawn1 = false;
            Spawn2 = false;
            transform.position = new Vector3 (0,2.84f,0);
            TimerScript.Begin = true;
            Victory = false;
            Reset = 5;
        }
        if(Victory)
        {
            TimerScript.Begin = false;
            Reset -= Time.deltaTime;
        }
        // if(Sharp)
      {
       
        //if (Input.GetMouseButtonDown(0))
       /* {
          
           LayerMask layer =LayerMask.GetMask("Target");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity) )
            {
                
                Debug.Log(hit.collider);
                Debug.Log(hit.distance);
                
            }
        }*/
      }

    }
    public bool OnGround()
    {
        return Floors.Count > 0;
    }

    private void OnCollisionEnter(Collision other)
    {
        //If I touch something and it's not already in my list of things I'm touching. . .
        //Add it to the list
        if (!Floors.Contains(other.gameObject))
            Floors.Add(other.gameObject);
          if(other.gameObject.CompareTag("Hazard"))
          {
            if(Spawn1)
            {
                transform.position = new Vector3 (-6.3506f,1.624f,44.225f);
            }
            else if(Spawn2)
            {
                transform.position = new Vector3 (-37.2317f,2.603f,91.3392f);
            }
            else
            {
                transform.position = new Vector3 (0,2.84f,0);
            }
          }  
    }

    private void OnCollisionExit(Collision other)
    {
        //When I stop touching something, remove it from the list of things I'm touching
        Floors.Remove(other.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        //Spawnpoint manager
        if ( other.gameObject.CompareTag("Spawnpoint"))
        {
            Spawn1 = true;
            Spawn2 = false;
        }
        if (other.gameObject.CompareTag("Spawnpoint2"))
        {
            Spawn2 = true;
            Spawn1 = false;
        }
        /*if (other.gameObject.CompareTag("Shoot"))
        {
            Debug.Log ("works");
            ShootingScript shot = other.gameObject.GetComponent<ShootingScript>();
            if(shot != null)
            {
                Debug.Log ("works");
                shot.Shoot();
            }
        }*/
        if( other.gameObject.CompareTag("End"))
        {
            Victory = true;
        }
    }
}
