using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogJumpState : State {

    private CharacterController player;

    private float Gravity = 20f;
    private float fastGravity = 40f;
    private float jumpSpeed = 18f;
    private float currentSpeed;


	private Vector3 direction;

	private float wallJumpLimit = 85f;
	private float wallJumpSpeed = 12f;
	private Vector3 wallJump;
	private bool fromWall;

    public FrogJumpState(Character character) : base(character)
    {
		fromWall = false;
    }

	public FrogJumpState(Character character, Vector3 wall) : base(character)
	{
		fromWall = true;
		wallJump = wall;
	}

    public override void OnStateEnter()
    {
        player = character.GetComponent<CharacterController>();
		if (fromWall) {
			currentSpeed = wallJumpSpeed;
		} else {
		currentSpeed = jumpSpeed;
		}
    }

    public override void Tick()
    {

        direction = ((character.transform.forward * Input.GetAxis("Vertical"))
            + (character.transform.right * Input.GetAxis("Horizontal")));
        direction.Normalize();

        if (currentSpeed > 0.0f)
        {
			if (Input.GetAxis("Jump") != 0f || fromWall) {
				currentSpeed -= Gravity * Time.deltaTime;
			} else {
				currentSpeed -= fastGravity * Time.deltaTime;
			}
            
        }
        else
        {
			if (fromWall) {
				character.SetState(new FrogFallState(character, wallJump));
			} else {
				character.SetState(new FrogFallState(character));
			}
        }
    }

	public override void PhysicsTick() {
		character.momentum = Vector3.Lerp(character.momentum, direction * 10f, 0.015f);
		player.Move(character.momentum * Time.fixedDeltaTime);
		player.Move(Vector3.up * currentSpeed * Time.fixedDeltaTime);
	}


    public override void OnColliderHit(ControllerColliderHit hit)
    {
        Vector3 hitNormal = hit.normal;
		bool wall = (Vector3.Angle(Vector3.up, hitNormal) <= wallJumpLimit);

		if (!wall) {
			if (!fromWall) {
				//character.SetState(new FrogWallState(character, hitNormal));
			} else {
				bool diffWall = (Vector3.Angle(wallJump, hitNormal) >= 70f);
				if (diffWall) {
					character.SetState(new FrogWallState(character, hitNormal));
				}
			}
		}

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
