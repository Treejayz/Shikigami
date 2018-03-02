using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxMoveState : State {

    private CharacterController player;
    private Vector3 direction;

    public FoxMoveState(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
        player = character.GetComponent<CharacterController>();
    }

    public override void Tick()
    {

        direction = forwardtest.forward;
        direction.Normalize();

        if (!player.isGrounded)
        {
            character.SetState(new FoxFallState(character));
        }

        if (Input.GetAxis("Vertical") == 0.0f && Input.GetAxis("Horizontal") == 0.0f)
        {
            character.SetState(new FoxIdleState(character));
        }

        if (Input.GetAxis("Jump") != 0.0f && !character.jumped)
        {
            character.SetState(new FoxJumpState(character));
        }
        if (Input.GetAxis("Ability1") != 0f)
        {
            character.SetState(new FoxSprintState(character));
        }
        if (Input.GetAxis("Ability2") != 0f)
        {
            character.SetState(new FoxSneakState(character));
        }
        if (!character.switching)
        {
            if (Input.GetAxis("Switch2") != 0.0f)
            {
                character.SetForm("Crane");
                character.GetComponentsInChildren<ParticleSystem>()[3].Play();
                character.SetState(new CraneMovingState(character));
            }
            else if (Input.GetAxis("Switch1") != 0.0f)
            {
                character.SetForm("Frog");
                character.GetComponentsInChildren<ParticleSystem>()[3].Play();
                character.SetState(new FrogMoveState(character));
            }
        }
    }

    public override void PhysicsTick()
    {
        character.momentum = Vector3.Lerp(character.momentum, direction * character.moveSpeed, 0.08f);
        player.Move(character.momentum * Time.fixedDeltaTime);
        player.Move(Vector3.down * character.gravity * Time.fixedDeltaTime);
    }

    public override void OnColliderHit(ControllerColliderHit hit)
    {
        Vector3 hitNormal = hit.normal;
        bool isGrounded = (Vector3.Angle(Vector3.up, hitNormal) <= player.slopeLimit);
        if (!isGrounded && !player.isGrounded)
        {
            character.SetState(new FoxFallState(character));
        }
        else
        {
        }
    }
}
