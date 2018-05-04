using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneMovingState : State {


	private CharacterController player;
	private Vector3 direction;
    private bool shiftHeld;

    public CraneMovingState(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
		player = character.GetComponent<CharacterController>();
        if (Input.GetAxis("Ability1") != 0f)
        {
            shiftHeld = true;
        }
        else
        {
            shiftHeld = false;
        }
        character.yVelocity = character.gravity;
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
        if (shiftHeld && Input.GetAxis("Ability1") == 0f)
        {
            shiftHeld = false;
        }

        if (Input.GetAxis("Ability1") != 0.0f && character.canDash && !shiftHeld)
        {
            character.SetState(new CraneDashState(character));
        }
        if (!character.switching)
        {
            if (Input.GetAxis("Switch2") != 0.0f && character.canFrog)
            {
                character.SetState(new TransformState(character, false));
            }
            else if (Input.GetAxis("Switch1") != 0.0f && character.canFrog)
            {
                character.SetState(new TransformState(character, true));
            }
        }
    }

	public override void PhysicsTick() {
		character.momentum = Vector3.Lerp(character.momentum, direction * character.moveSpeed, 0.08f);
		player.Move(character.momentum * Time.fixedDeltaTime);

		player.Move(Vector3.down * character.gravity * Time.fixedDeltaTime);
	}
}
