using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneFallingState : State {

	private CharacterController player;


	private float Gravity = 30f;
	private float maxFallSpeed = 20f;
    private float glideSpeed = 4f;
    private float slideFriction = 0.3f;

    private float fallSpeed;
	private Vector3 direction;


    public CraneFallingState(Character character) : base(character)
	{
	}

	public override void OnStateEnter()
	{
		player = character.GetComponent<CharacterController>();
		fallSpeed = 0.0f;
	}

	public override void Tick() {

		direction = ((character.transform.forward * Input.GetAxis("Vertical")) 
			+ (character.transform.right * Input.GetAxis("Horizontal")));
		direction.Normalize();

        if (Input.GetAxis("Jump") != 0.0f)
        {
            if (fallSpeed < glideSpeed)
            {
                fallSpeed += Gravity * Time.deltaTime;
            }
            else if (fallSpeed > glideSpeed)
            {
                fallSpeed -= 40f * Time.deltaTime;
            }
        } else
        {
            if (fallSpeed < maxFallSpeed)
            {
                fallSpeed += Gravity * Time.deltaTime;
            }
            else if (fallSpeed > maxFallSpeed)
            {
                fallSpeed = maxFallSpeed;
            }
        }

		if (!player.isGrounded) {
			//player.Move(Vector3.down * fallSpeed * Time.deltaTime);
		} else {
			character.SetState(new CraneIdleState(character));
		}

	}

	public override void PhysicsTick() {
		character.momentum = Vector3.Lerp(character.momentum, direction * character.moveSpeed, 0.015f);
		player.Move(character.momentum * Time.fixedDeltaTime);

		player.Move(Vector3.down * fallSpeed * Time.fixedDeltaTime);
	}

    public override void OnColliderHit(ControllerColliderHit hit)
    {
        Vector3 hitNormal = hit.normal;
        bool isGrounded = (Vector3.Angle(Vector3.up, hitNormal) <= player.slopeLimit);
        if (!isGrounded)
        {
            float slideX = (1f - hitNormal.y) * hitNormal.x * (1f - slideFriction);
            float slideZ = (1f - hitNormal.y) * hitNormal.z * (1f - slideFriction);
            player.Move(new Vector3(slideX, 0f, slideZ) * Time.deltaTime);
        } else
        {
            //character.SetState(new CraneIdleState(character));
        }
    }
}
