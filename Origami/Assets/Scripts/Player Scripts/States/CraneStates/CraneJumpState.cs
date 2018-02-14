using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneJumpState : State {

	private CharacterController player;


	private float Gravity = 20f;
	private float slowGravity = 10f;
	private float jumpSpeed = 11f;
	private float currentSpeed;

	public CraneJumpState(Character character) : base(character)
	{
	}

	public override void OnStateEnter()
	{
		player = character.GetComponent<CharacterController>();
		currentSpeed = jumpSpeed;
	}

	public override void Tick() {

		Vector3 direction = ((character.transform.forward * Input.GetAxis("Vertical")) 
			+ (character.transform.right * Input.GetAxis("Horizontal")));
		direction.Normalize();

        character.momentum = Vector3.Lerp(character.momentum, direction * 10f, 0.015f);
        player.Move(character.momentum * Time.deltaTime);

        if (currentSpeed > 0.0f) {
			currentSpeed -= Gravity * Time.deltaTime;
		} else {
			character.SetState(new CraneFallingState(character));
		}
		player.Move(Vector3.up * currentSpeed * Time.deltaTime);
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
