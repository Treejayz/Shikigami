using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : State {

	private CharacterController player;


	private float Gravity = 20f;
	private float slowGravity = 10f;
	private float jumpSpeed = 10f;
	private float currentSpeed;

	public JumpState (Character character) : base(character)
	{
	}

	public override void OnStateEnter()
	{
		player = character.GetComponent<CharacterController>();
		currentSpeed = jumpSpeed;
	}

	public override void PhysicsTick() {

	}

	public override void Tick() {

		Vector3 direction = ((character.transform.forward * Input.GetAxis("Vertical")) 
			+ (character.transform.right * Input.GetAxis("Horizontal")));
		direction.Normalize();

		player.Move(direction * Time.deltaTime * 10f);

		if (currentSpeed > 0.0f) {
			currentSpeed -= slowGravity * Time.deltaTime;
		} else {
			character.SetState(new FallingState(character));
		}
		player.Move(Vector3.up * currentSpeed * Time.deltaTime);

	}
}
