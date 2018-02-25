using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxSneakState : State {

    private CharacterController player;
    private Vector3 direction;
    private float sneakSpeed = 6f;

    public FoxSneakState(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
        player = character.GetComponent<CharacterController>();
        character.frogAnimator.SetBool("Moving", true);
    }

    public override void Tick()
    {

        direction = forwardtest.forward;

        if (!player.isGrounded)
        {
            character.SetState(new FoxFallState(character));
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            if (Input.GetAxis("Vertical") == 0.0f && Input.GetAxis("Horizontal") == 0.0f)
            {
                character.SetState(new FoxIdleState(character));
            } else
            {
                character.SetState(new FoxMoveState(character));
            }
        }

        if (Input.GetAxis("Jump") != 0.0f && !character.jumped)
        {
            character.SetState(new FoxJumpState(character));
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            character.SetForm("Crane");
            character.GetComponentsInChildren<ParticleSystem>()[3].Play();
            character.SetState(new CraneMovingState(character));
        }
        else if (Input.GetKeyDown(KeyCode.E) && character.canFox)
        {
            character.SetForm("Fox");
            character.GetComponentsInChildren<ParticleSystem>()[3].Play();
            character.SetState(new FoxMoveState(character));
        }

    }

    public override void PhysicsTick()
    {
        character.momentum = Vector3.Lerp(character.momentum, direction * sneakSpeed, 0.08f);

        RaycastHit hit;
        Vector3 newpos = (character.transform.position + (character.momentum.normalized * player.radius));
        if (Physics.SphereCast(newpos, player.radius, Vector3.down, out hit, 1.5f))
        {
            Vector3 direction = hit.point - character.transform.position;
            direction.y = 0;
            direction.Normalize();
            Vector3 heading = Vector3.Project(character.momentum.normalized, direction) * character.momentum.magnitude;
            player.Move(heading * Time.fixedDeltaTime);
            player.Move(Vector3.down * character.gravity * Time.fixedDeltaTime);
        }
    }

    public override void OnColliderHit(ControllerColliderHit hit)
    {
        Vector3 hitNormal = hit.normal;
        bool isGrounded = (Vector3.Angle(Vector3.up, hitNormal) <= player.slopeLimit);
        if (!isGrounded)
        {
            character.SetState(new FoxFallState(character));
        }
        else
        {
        }
    }
}
