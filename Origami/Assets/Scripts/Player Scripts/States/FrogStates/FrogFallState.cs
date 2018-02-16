using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogFallState : State {

    private CharacterController player;


    private float Gravity = 30f;
    private float maxFallSpeed = 20f;
    private float slideFriction = 0.3f;

    private float fallSpeed;
	private Vector3 direction;

	private float wallJumpLimit = 85f;
	private Vector3 wallJump;
	private bool fromWall;

    public FrogFallState(Character character) : base(character)
    {
		fromWall = false;
	}

	public FrogFallState(Character character, Vector3 wall) : base(character)
	{
		fromWall = true;
		wallJump = wall;
	}

    public override void OnStateEnter()
    {
        player = character.GetComponent<CharacterController>();
        fallSpeed = 0.0f;
    }

    public override void Tick()
    {
        direction = ((character.transform.forward * Input.GetAxis("Vertical"))
            + (character.transform.right * Input.GetAxis("Horizontal")));
        direction.Normalize();

        if (fallSpeed < maxFallSpeed)
        {
                fallSpeed += Gravity * Time.deltaTime;
        }
        else if (fallSpeed > maxFallSpeed)
        {
            fallSpeed = maxFallSpeed;
        }

        if (!player.isGrounded)
        {
            // idk
        }
        else
        {
            character.SetState(new FrogIdleState(character));
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
		bool wall = (Vector3.Angle(Vector3.up, hitNormal) <= wallJumpLimit);

		if (!wall) {
			if (!fromWall) {
				character.SetState(new FrogWallState(character, hitNormal));
			} else {
				bool diffWall = (Vector3.Angle(wallJump, hitNormal) >= 100f);
				if (diffWall) {
					character.SetState(new FrogWallState(character, hitNormal));
				}
			}
		}

        bool isGrounded = (Vector3.Angle(Vector3.up, hitNormal) <= player.slopeLimit);
        if (!isGrounded)
        {
            float slideX = (1f - hitNormal.y) * hitNormal.x * (1f - slideFriction);
            float slideZ = (1f - hitNormal.y) * hitNormal.z * (1f - slideFriction);
            player.Move(new Vector3(slideX, 0f, slideZ) * Time.deltaTime);
        }
        else
        {
            character.SetState(new FrogIdleState(character));
        }
    }
}
