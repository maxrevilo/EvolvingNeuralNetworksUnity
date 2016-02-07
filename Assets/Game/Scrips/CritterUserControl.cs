using UnityEngine;

[RequireComponent(typeof(CritterMotor))]
public class CritterUserControl : BaseBehaviour
{
    CritterMotor critterMotor;

    void Start()
    {
        critterMotor = GetComponent<CritterMotor>();
    }

    void Update()
    {
        float vAxis = Input.GetAxisRaw("Vertical");

        if(vAxis > 0) critterMotor.MoveForward();
        else critterMotor.Stop();

        float hAxis = Input.GetAxisRaw("Horizontal");

        if (hAxis > 0) critterMotor.TurnRight();
        else if (hAxis < 0) critterMotor.TurnLeft();
        else critterMotor.StopTurning();
    }
}
