using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneDashState : State {

    private CharacterController player;
    private Vector3 direction;
    private float dashDuration = .4f;
    private float dashStartSpeed = 22f;
    private float dashEndSpeed = 16f;
    private float currentSpeed;
    private float currentFall;
    private float currentTime;

    private bool ground;

    public CraneDashState(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
        player = character.GetComponent<CharacterController>();
        direction = character.transform.forward;
        direction.Normalize();
        currentSpeed = dashStartSpeed;
        currentTime = 0f;
        currentFall = 0f;
        dashEndSpeed = character.moveSpeed;
        character.canDash = false;
        character.isDashing = true;
        ground = player.isGrounded;
        AkSoundEngine.PostEvent("Dash", character.gameObject);
        character.GetComponentsInChildren<ParticleSystem>()[4].Play();
    }

    public override void Tick()
    {
        currentTime += Time.deltaTime;
        if (currentTime > dashDuration)
        {
            if (player.isGrounded)
            {
                character.SetState(new CraneIdleState(character));
            }
            else
            {
                character.SetState(new CraneFallingState(character));
            }
        }
        else
        {
            currentSpeed = dashStartSpeed - ((dashStartSpeed - dashEndSpeed) * (currentTime / dashDuration));
            direction = Vector3.Lerp(direction, forwardtest.forward, Time.deltaTime);
        }
    }

    public override void PhysicsTick()
    {
        character.momentum = direction * currentSpeed;
        player.Move(character.momentum * Time.fixedDeltaTime);
        
         if (ground)
        {
            player.Move(Vector3.down * character.gravity * Time.fixedDeltaTime);
        }
    }

    public override void OnStateExit()
    {
        character.isDashing = false;
        character.canDash = false;
        character.GetComponentsInChildren<ParticleSystem>()[4].Stop();
    }

    public override void OnColliderHit(ControllerColliderHit hit)
    {
        Vector3 hitNormal = hit.normal;
        bool isGrounded = (Vector3.Angle(Vector3.up, hitNormal) <= player.slopeLimit);
        if (!isGrounded && !player.isGrounded)
        {
            character.SetState(new CraneFallingState(character));
        }
        else
        {
        }
    }
}
