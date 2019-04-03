using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMovement : MonoBehaviour
{
    Rigidbody rigidbody;

    bool CanJump = true;
    float JumpCooldownTime = 3f;
    float currentFuel = 100;
    float maxFuel = 100;
    float fuelRechargeRate = 1;
    float jumpSeed = 1;
    float fallSpeed = 1;

    float maxSpeed = 100;
    float currentSpeed = 0;
    float accelerationSpeed = 1;

    float turnSpeed = 50;

    bool EngineIsOn = false;
    bool canFall = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            EngineIsOn = true;
        }
        else
        {
            EngineIsOn = false;
        }

        if(Input.GetKey(KeyCode.UpArrow))
        {
            MoveForwards();
        }

        if(Input.GetKey(KeyCode.DownArrow))
        {
            MoveBackwards();
        }

        if(Input.GetKey(KeyCode.LeftArrow))
        {
            TurnRight();
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            TurnLeft();
        }

        if(Input.GetKey(KeyCode.Space))
        {
            ActivateJump();
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            canFall = true;
        }

        else
        {
            if(currentFuel + fuelRechargeRate < maxFuel)
            {
                currentFuel += fuelRechargeRate;
            }
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

            currentFuel -= fuelRechargeRate;
        }
    }

    void FallDown()
    {
        if(canFall)
        {
            this.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - fallSpeed, this.gameObject.transform.position.z);
        }
    }

    IEnumerator JumpCooldown()
    {
        yield return new WaitForSeconds(JumpCooldownTime);
        

    }

    private void OnCollisionEnter(Collision collision)
    {
        canFall = false;
    }
}
