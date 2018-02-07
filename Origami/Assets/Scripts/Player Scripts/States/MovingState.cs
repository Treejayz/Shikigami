using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : State {


	private CharacterController player;

    public MovingState(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
        MonoBehaviour.print("entering move state");
		player = character.GetComponent<CharacterController>();
    }

	public override void PhysicsTick() {

	}

    public override void Tick()
    {

		Vector3 direction = ((character.transform.forward * Input.GetAxis("Vertical")) 
			+ (character.transform.right * Input.GetAxis("Horizontal")));
		direction.Normalize();

		player.Move(direction * Time.deltaTime * 10f);

		player.Move(Vector3.down * .01f);

		if (!player.isGrounded) {
			character.SetState(new FallingState(character));
		}

		if (Input.GetAxis("Vertical") == 0.0f && Input.GetAxis("Horizontal") == 0.0f)  {
            character.SetState(new IdleState(character));
        }

		if (Input.GetAxis("Jump") != 0.0f) {
			character.SetState(new JumpState(character));
		}
    }
}
