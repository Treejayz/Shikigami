using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : State {

	private CharacterController player;


	private float Gravity = 20f;
	private float maxFallSpeed = 10f;
	private float fallSpeed;

	public FallingState (Character character) : base(character)
	{
	}

	public override void OnStateEnter()
	{
		MonoBehaviour.print("entering falling state");
		player = character.GetComponent<CharacterController>();
		fallSpeed = 0.0f;
	}

	public override void PhysicsTick() {
	
	}

	public override void Tick() {

		Vector3 direction = ((character.transform.forward * Input.GetAxis("Vertical")) 
			+ (character.transform.right * Input.GetAxis("Horizontal")));
		direction.Normalize();

		player.Move(direction * Time.deltaTime * 10f);

		if (fallSpeed < maxFallSpeed) {
			fallSpeed += Gravity * Time.deltaTime;
		} else if (fallSpeed > maxFallSpeed) {
			fallSpeed = maxFallSpeed;
		}

		if (!player.isGrounded) {
			player.Move(Vector3.down * fallSpeed * Time.deltaTime);
		} else {
			character.SetState(new IdleState(character));
		}

	}

}
