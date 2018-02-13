using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneIdleState : State {

	private CharacterController player;

	private float drag = 30f;

	public CraneIdleState(Character character) : base(character)
	{
	}

	public override void OnStateEnter()
	{
		MonoBehaviour.print("entering idle state");
		player = character.GetComponent<CharacterController>();
		character.gameObject.transform.GetChild(1).gameObject.SetActive(false);
		character.gameObject.transform.GetChild(0).gameObject.SetActive(true);
	}

	public override void Tick() {


		Vector3 direction = new Vector3(character.xSpeed, 0, character.zSpeed);
		player.Move(direction * Time.deltaTime);
		player.Move(Vector3.down * .1f);

		if (character.xSpeed > 0.1f) {
			character.xSpeed -= drag * Time.deltaTime;
		} else if (character.xSpeed < -0.1f) {
			character.xSpeed += drag * Time.deltaTime;
		} else {
			character.xSpeed = 0f;
		}
		if (character.zSpeed > 0.1f) {
			character.zSpeed -= drag * Time.deltaTime;
		} else if (character.zSpeed < -0.1f) {
			character.zSpeed += drag * Time.deltaTime;
		} else {
			character.xSpeed = 0f;
		}



		if (!player.isGrounded) 
		{
			character.SetState(new CraneFallingState(character));
		}
		if (Input.GetAxis("Vertical") != 0.0f || Input.GetAxis("Horizontal") != 0.0f)
		{
			character.SetState(new CraneMovingState(character));
		} 
		if (Input.GetAxis("Jump") != 0.0f) 
		{
			character.SetState(new CraneJumpState(character));
		}
		if (Input.GetKeyDown(KeyCode.E))
		{
			character.SetState(new FrogIdleState(character));
		}

	}
}
