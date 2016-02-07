using UnityEngine;
using System.Collections;

public class CritterMotor : BaseBehaviour
{
    [SerializeField]
    private float maxSpeed = 3f;

    [SerializeField]
    private float maxAngularSpeed = 360f;

    private float forwardSpeed = 0f;
    private float turningSpeed = 0f;


    public void MoveForward()
    {
        forwardSpeed = maxSpeed;
    }

    public void Stop()
    {
        forwardSpeed = 0;
    }

    public void TurnLeft()
    {
        turningSpeed = maxAngularSpeed;
    }

    public void TurnRight()
    {
        turningSpeed = -maxAngularSpeed;
    }

    public void StopTurning()
    {
        turningSpeed = 0;
    }

    private void CritterDied()
    {
        enabled = false;
    }

    void FixedUpdate()
    {
        CalculateMovement();
    }

    void CalculateMovement()
    {
        if(forwardSpeed != 0)
        {
            transform.position = transform.position + transform.up * forwardSpeed * Time.fixedDeltaTime;
        }
        if (turningSpeed != 0)
        {
            transform.Rotate(Vector3.forward, turningSpeed * Time.fixedDeltaTime);
        }
    }
}
