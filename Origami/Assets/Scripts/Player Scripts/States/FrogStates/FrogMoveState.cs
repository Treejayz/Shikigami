using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMoveState : State {



    private CharacterController player;
	private Vector3 direction;

    public FrogMoveState(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
        player = character.GetComponent<CharacterController>();
    }

    public override void Tick()
    {

        direction = character.transform.forward;

        if (!player.isGrounded)
        {
            character.SetState(new FrogFallState(character));
        }

        if (Input.GetAxis("Vertical") == 0.0f && Input.GetAxis("Horizontal") == 0.0f)
        {
            character.SetState(new FrogIdleState(character));
        }

        if (Input.GetAxis("Jump") != 0.0f)
        {
            character.SetState(new FrogJumpState(character));
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
        if (!isGrounded)
        {

        }
        else
        {
            player.Move(Vector3.up * Time.deltaTime);
        }
    }
}
