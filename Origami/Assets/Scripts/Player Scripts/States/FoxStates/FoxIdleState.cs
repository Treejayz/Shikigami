using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxIdleState : State {

    private CharacterController player;

    public FoxIdleState(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
        player = character.GetComponent<CharacterController>();
    }

    public override void Tick()
    {

        if (!player.isGrounded)
        {
            character.SetState(new FoxFallState(character));
        }
        if (Input.GetAxis("Vertical") != 0.0f || Input.GetAxis("Horizontal") != 0.0f)
        {
            character.SetState(new FoxMoveState(character));

        }
        if (Input.GetAxis("Jump") != 0.0f && !character.jumped)
        {
            character.SetState(new FoxJumpState(character));
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
        Vector3 target = new Vector3(0f, 0f, 0f);
        character.momentum = Vector3.Lerp(character.momentum, target, 0.05f);
        if (character.momentum.x < 0.001f && character.momentum.x > -0.001f) { character.momentum.x = 0f; }
        if (character.momentum.z < 0.001f && character.momentum.z > -0.001f) { character.momentum.z = 0f; }

        player.Move(character.momentum * Time.fixedDeltaTime);
        player.Move(Vector3.down * character.gravity * Time.fixedDeltaTime);
    }

    public override void OnStateExit()
    {
    }

    public override void OnColliderHit(ControllerColliderHit hit)
    {
        Vector3 hitNormal = hit.normal;
        bool isGrounded = (Vector3.Angle(Vector3.up, hitNormal) <= player.slopeLimit);
        if (!isGrounded)
        {
            character.SetState(new FoxFallState(character));
        }
    }
}
