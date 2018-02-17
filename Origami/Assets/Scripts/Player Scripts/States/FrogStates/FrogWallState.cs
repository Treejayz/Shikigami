using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogWallState : State {

	private CharacterController player;
	private float wallTime = .4f;
	private float currentTime;

	private bool jumpHeld;

	private Vector3 wallHit;

	public FrogWallState(Character character, Vector3 hit) : base(character)
	{
		wallHit = hit;
		wallHit.y = 0f;
		wallHit.Normalize();
	}

	public override void OnStateEnter()
	{
		player = character.GetComponent<CharacterController>();
		character.momentum = new Vector3(0f,0f,0f);
		currentTime = 0f;
		if (Input.GetAxis("Jump") != 0f) {
			jumpHeld = true;
		} else {
			jumpHeld = false;
		}
	}

	public override void Tick() {
		currentTime += Time.deltaTime;
		//if (currentTime > wallTime) {
        if (currentTime > wallTime && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)) {
			character.momentum = wallHit * 4f;
			character.SetState(new FrogFallState(character, wallHit));
		}

		if (Input.GetAxis("Jump") != 0f && !jumpHeld) {
			character.momentum = wallHit * 10f;
			character.SetState(new FrogJumpState(character, wallHit));
		}

		if (jumpHeld && Input.GetAxis("Jump") == 0f) {
			jumpHeld = false;
		}
	}
}
