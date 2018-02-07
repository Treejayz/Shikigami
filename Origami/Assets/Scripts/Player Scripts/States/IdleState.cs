using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State {

	private CharacterController player;

    public IdleState(Character character) : base(character)
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
			character.SetState(new FallingState(character));
		}


        //Do idle stuff
		if (Input.GetAxis("Vertical") != 0.0f || Input.GetAxis("Horizontal") != 0.0f)
        {
            character.SetState(new MovingState(character));

        } 
		if (Input.GetAxis("Jump") != 0.0f) {
			character.SetState(new JumpState(character));
		}

    }

	public override void PhysicsTick() {
	}
}
