using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxFallState : State {

    private CharacterController player;

    private float Gravity = 30f;
    private float maxFallSpeed = 30f;
    private float slideFriction = 0.1f;

    private float fallSpeed;
    private Vector3 direction;


    public FoxFallState(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
        player = character.GetComponent<CharacterController>();
        fallSpeed = 0.0f;
        character.foxAnimator.SetBool("Jumping", true);
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
        if (fallSpeed < maxFallSpeed)
        {
            fallSpeed += Gravity * Time.deltaTime;
        }
        else if (fallSpeed > maxFallSpeed)
        {
            fallSpeed = maxFallSpeed;
        }
    }

    public override void PhysicsTick()
    {
        character.momentum = Vector3.Lerp(character.momentum, direction * character.moveSpeed, 0.005f);
        player.Move(character.momentum * Time.fixedDeltaTime);

        player.Move(Vector3.down * fallSpeed * Time.fixedDeltaTime);
    }

    public override void OnStateExit()
    {
        character.jumped = true;
        character.foxAnimator.SetBool("Jumping", false);
    }

    public override void OnColliderHit(ControllerColliderHit hit)
    {
        Vector3 hitNormal = hit.normal;
        bool isGrounded = (Vector3.Angle(Vector3.up, hitNormal) <= (90 - player.slopeLimit));
        if (!isGrounded)
        {
            float slideX = (1f - hitNormal.y) * hitNormal.x * (1f - slideFriction);
            float slideZ = (1f - hitNormal.y) * hitNormal.z * (1f - slideFriction);
            player.Move(new Vector3(slideX, 0f, slideZ) * Time.fixedDeltaTime);
        }
        else
        {
            if (Input.GetAxis("Ability2") != 0f)
            {
                character.SetState(new FoxSneakState(character));
            }
            else if (Input.GetAxis("Ability1") != 0f && (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f))
            {
                Character.sneaking = false;
                character.SetState(new FoxSprintState(character, character.momentum));
            } else
            {
                Character.sneaking = false;
                character.SetState(new FoxIdleState(character));
            }
        }
    }
}
