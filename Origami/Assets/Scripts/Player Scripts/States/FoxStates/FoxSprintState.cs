using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxSprintState : State {

    private CharacterController player;
    private Vector3 direction;

    private float sprintSpeed = 22f;

    public FoxSprintState(Character character) : base(character)
    {
        direction = forwardtest.forward;
        direction.Normalize();
        //character.momentum = direction * sprintSpeed;

    }

    public FoxSprintState(Character character, Vector3 moveDirection) : base(character)
    {
        direction = moveDirection.normalized;
    }

    public override void OnStateEnter()
    {
        player = character.GetComponent<CharacterController>();
        character.foxAnimator.SetBool("Sprinting", true);
        character.GetComponentsInChildren<ParticleSystem>()[5].Play();
        AkSoundEngine.SetRTPCValue("MusicSpeed", 1.05f);
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
        if (Input.GetAxis("Ability1") == 0f)
        {
            character.SetState(new FoxMoveState(character));
        }
    }

    public override void PhysicsTick()
    {
        character.momentum = Vector3.Lerp(character.momentum, direction * sprintSpeed, Time.fixedDeltaTime * 3f);
        player.Move(character.momentum * Time.fixedDeltaTime);
        player.Move(Vector3.down * character.gravity * Time.fixedDeltaTime);
    }

    public override void OnStateExit()
    {
        character.foxAnimator.SetBool("Sprinting", false);
        character.GetComponentsInChildren<ParticleSystem>()[5].Stop();
        AkSoundEngine.SetRTPCValue("MusicSpeed", 1f);
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
