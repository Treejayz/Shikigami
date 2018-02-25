using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxJumpState : State {

    private CharacterController player;


    private float Gravity = 20f;
    private float fastGravity = 40f;
    private float jumpSpeed = 8f;
    private float currentSpeed;
    private Vector3 direction;

    public FoxJumpState(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
        player = character.GetComponent<CharacterController>();
        currentSpeed = jumpSpeed;
    }

    public override void Tick()
    {

        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            direction = forwardtest.forward;
        }
        else
        {
            direction = new Vector3(0f, 0f, 0f);
        }
        if (currentSpeed > 0.0f)
        {
            if (Input.GetAxis("Jump") != 0f)
            {
                currentSpeed -= Gravity * Time.deltaTime;
            }
            else
            {
                currentSpeed -= fastGravity * Time.deltaTime;
            }
        }
        else
        {
            character.SetState(new FoxFallState(character));
        }
    }

    public override void PhysicsTick()
    {
        character.momentum = Vector3.Lerp(character.momentum, direction * character.moveSpeed, 0.005f);
        player.Move(character.momentum * Time.fixedDeltaTime);
        player.Move(Vector3.up * currentSpeed * Time.fixedDeltaTime);
    }

    public override void OnStateExit()
    {
    }

    public override void OnColliderHit(ControllerColliderHit hit)
    {
        Vector3 hitNormal = hit.normal;
        if (hit.normal.y < 0f)
        {
            if (currentSpeed > 2f)
            {
                currentSpeed = 2f;
            }
            return;
        }
        bool isGrounded = (Vector3.Angle(Vector3.up, hitNormal) <= (90 - player.slopeLimit));
        if (!isGrounded)
        {
            player.Move(Vector3.up * Time.deltaTime);
        }
        else
        {
        }
    }
}
