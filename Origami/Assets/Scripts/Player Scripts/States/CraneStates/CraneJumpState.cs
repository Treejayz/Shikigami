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

    private bool shiftHeld;

	public CraneJumpState(Character character) : base(character)
	{
	}

	public override void OnStateEnter()
	{
		player = character.GetComponent<CharacterController>();
		currentSpeed = jumpSpeed;
        character.craneAnimator.SetBool("Jumping", true);
        character.craneAnimator.Play("Crane_Jump", -1, .1f);
        AkSoundEngine.PostEvent("WingJump", character.gameObject);
        if (Input.GetAxis("Ability1") != 0.0f)
        {
            character.canDash = false;
            shiftHeld = true;
        } else
        {
            character.canDash = true;
        }
	}

	public override void Tick() {

        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            direction = forwardtest.forward;
        } else
        {
            direction = new Vector3 (0f, 0f, 0f);
        }
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

        if (shiftHeld && Input.GetAxis("Ability1") == 0.0f)
        {
            shiftHeld = false;
            character.canDash = true;
        }

        if (Input.GetAxis("Ability1") != 0.0f && character.canDash && !shiftHeld)
        {
            character.SetState(new CraneDashState(character));
        }

    }

	public override void PhysicsTick() {
		character.momentum = Vector3.Lerp(character.momentum, direction * character.moveSpeed, 0.015f);
		player.Move(character.momentum * Time.fixedDeltaTime);
		player.Move(Vector3.up * currentSpeed * Time.fixedDeltaTime);
	}

    public override void OnStateExit()
    {
        character.craneAnimator.SetBool("Jumping", false);
        if (shiftHeld)
        {
            character.canDash = true;
        }
    }

    public override void OnColliderHit(ControllerColliderHit hit)
    {
        Vector3 hitNormal = hit.normal;
        if (hit.normal.y < -0.1f)
        {
            if (currentSpeed > 2f)
            {
                currentSpeed = 2f;
            }
            return;
        }
        bool isGrounded = (Vector3.Angle(Vector3.up, hitNormal) <= (90 - player.slopeLimit));
        if (!isGrounded)
        {
            player.Move(Vector3.up * Time.deltaTime);
        }
        else
        {
        }
    }
}
