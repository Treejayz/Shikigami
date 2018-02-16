using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneJumpState : State {

	private CharacterController player;


	private float Gravity = 20f;
	private float fastGravity = 40f;
	private float jumpSpeed = 11f;
	private float currentSpeed;
	private Vector3 direction;

	public CraneJumpState(Character character) : base(character)
	{
	}

	public override void OnStateEnter()
	{
		player = character.GetComponent<CharacterController>();
		currentSpeed = jumpSpeed;
	}

	public override void Tick() {

		direction = ((character.transform.forward * Input.GetAxis("Vertical")) 
			+ (character.transform.right * Input.GetAxis("Horizontal")));
		direction.Normalize();

		if (currentSpeed > 0.0f)
		{
			if (Input.GetAxis("Jump") != 0f) {
				currentSpeed -= Gravity * Time.deltaTime;
			} else {
				currentSpeed -= fastGravity * Time.deltaTime;
			}

		} else {
			character.SetState(new CraneFallingState(character));
		}

	}

	public override void PhysicsTick() {
		character.momentum = Vector3.Lerp(character.momentum, direction * character.moveSpeed, 0.015f);
		player.Move(character.momentum * Time.fixedDeltaTime);
		player.Move(Vector3.up * currentSpeed * Time.fixedDeltaTime);
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
