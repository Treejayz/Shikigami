using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneMovingState : State {


	private CharacterController player;
	private Vector3 direction;

    public CraneMovingState(Character character) : base(character)
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

		if (!player.isGrounded) {
			character.SetState(new CraneFallingState(character));
		}

		if (Input.GetAxis("Vertical") == 0.0f && Input.GetAxis("Horizontal") == 0.0f)  {
            character.SetState(new CraneIdleState(character));
        }

		if (Input.GetAxis("Jump") != 0.0f && !character.jumped) {
			character.SetState(new CraneJumpState(character));
		}
        if (Input.GetKeyDown(KeyCode.LeftShift) && character.canDash)
        {
            character.SetState(new CraneDashState(character));
        }
        if (Input.GetKeyDown(KeyCode.E) && character.canFrog)
        {
            character.SetForm("Frog");
            character.GetComponentsInChildren<ParticleSystem>()[3].Play();
            character.SetState(new FrogMoveState(character));
        }
        else if (Input.GetKeyDown(KeyCode.Q) && character.canFox)
        {
            character.SetForm("Fox");
            character.GetComponentsInChildren<ParticleSystem>()[3].Play();
            character.SetState(new FoxMoveState(character));
        }
    }

	public override void PhysicsTick() {
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
            character.SetState(new CraneFallingState(character));
        }
        else
        {
        }
    }
}
