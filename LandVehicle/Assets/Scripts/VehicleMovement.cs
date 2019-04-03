using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    Rigidbody rigidbody;

    bool CanJump = true;
    float JumpCooldownTime = 3f;
    public float currentFuel = 0;
    public float maxFuel;
    float fuelRechargeRate = 1;
    float jumpSeed = 1;
    float fallSpeed = 1;

    float maxSpeed = 100;
    public float currentSpeed = 0;
    float accelerationSpeed = 1;

    float turnSpeed = 50;

    bool EngineIsOn = false;
    bool canFall = false;

    bool hitCheck = false;
    private void Awake()
    {
        maxFuel = 40;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        currentFuel = maxFuel;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S))
        {
            EngineIsOn = true;
        }
        else
        {
            EngineIsOn = false;
        }

        if(Input.GetKey(KeyCode.UpArrow) || Input.GetKey())
        {
            MoveForwards();
        }

        if(Input.GetKey(KeyCode.DownArrow) || Input.GetKey())
        {
            MoveBackwards();
        }

        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey())
        {
            TurnRight();
        }

        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey())
        {
            TurnLeft();
        }

        if(Input.GetKey(KeyCode.Space) & currentFuel > 0)
        {
            ActivateJump();
        }
        else
        {
            if(currentFuel + fuelRechargeRate < maxFuel)
            {
                currentFuel += fuelRechargeRate;
            }
        }

        if(currentFuel == 0)
        {
            canFall = true;
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            canFall = true;
        }


        AdjustCurrentSpeed();
        FallDown();
    }

    void AdjustCurrentSpeed()
    {
        if (currentSpeed >= 0)
        {
            if(EngineIsOn)
            {
                if(currentSpeed + accelerationSpeed < maxSpeed)
                {
                    currentSpeed += accelerationSpeed;
                }
            }
            else
            {
                currentSpeed -= accelerationSpeed;
            }
        }
        else if(currentSpeed < 0)
        {
            currentSpeed = 0;
        }
    }

    void TurnRight()
    {
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime * -1);
        //transform.Translate(Vector3.right * Time.deltaTime * turnSpeed * -1);
    }

    void TurnLeft()
    {
        transform.Rotate(Vector3.down, turnSpeed * Time.deltaTime * -1);
        // transform.Translate(Vector3.left * Time.deltaTime * turnSpeed * -1);
    }

    void MoveBackwards()
    {
        transform.Translate(Vector3.back * Time.deltaTime * currentSpeed);
        //rigidbody.AddForce((Vector3.back * maxSpeed),ForceMode.Acceleration);
    }

    void MoveForwards()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed);
        //rigidbody.velocity = Vector3.forward * maxSpeed;
    }

    void ActivateJump()
    {
        if(currentFuel > 0)
        {
            this.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + jumpSeed, this.gameObject.transform.position.z);

            currentFuel -= 1;
            print("Current Fuel: " + currentFuel);
        }
    }

    void FallDown()
    {
        if (!hitCheck)
        {
            if (canFall)
            {
                this.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - fallSpeed, this.gameObject.transform.position.z);
            }
        }
    }

    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(JumpCooldownTime);
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        canFall = false;
        hitCheck = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        hitCheck = false;
    }
}
