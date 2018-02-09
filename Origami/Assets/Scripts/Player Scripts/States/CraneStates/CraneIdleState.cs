using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneIdleState : State {

	private CharacterController player;

    public CraneIdleState(Character character) : base(character)
    {
    }

	public override void OnStateEnter()
	{
		MonoBehaviour.print("entering idle state");
		player = character.GetComponent<CharacterController>();
	}

    public override void Tick() {

		player.Move(Vector3.down * .01f);

		if (!player.isGrounded) {
			character.SetState(new CraneFallingState(character));
		}


        //Do idle stuff
		if (Input.GetAxis("Vertical") != 0.0f || Input.GetAxis("Horizontal") != 0.0f)
        {
            character.SetState(new CraneMovingState(character));

        } 
		if (Input.GetAxis("Jump") != 0.0f) {
			character.SetState(new CraneJumpState(character));
		}

    }
}
