using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneFallingState : State {

	private CharacterController player;

	private float Gravity = 30f;
	private float maxFallSpeed = 30f;
    private float glideSpeed = 4f;
    private float slideFriction = 0.3f;

    private float fallSpeed;
	private Vector3 direction;

    private bool shiftHeld;

    public CraneFallingState(Character character) : base(character)
	{
	}

	public override void OnStateEnter()
	{
		player = character.GetComponent<CharacterController>();
        character.craneAnimator.SetBool("Falling", true);
        if (Input.GetAxis("Ability1") != 0.0f)
        {
            shiftHeld = true;
        } else { shiftHeld = false; }

        AkSoundEngine.PostEvent("WindBegin", character.gameObject);
    }

	public override void Tick() {

        if (Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            direction = forwardtest.forward;
        }
        else
        {
            direction = new Vector3(0f, 0f, 0f);
        }

        if (Input.GetAxis("Jump") != 0.0f)
        {
            if (character.yVelocity < glideSpeed)
            {
                character.yVelocity += Gravity * Time.deltaTime;
            }
            else if (character.yVelocity > glideSpeed)
            {
                character.yVelocity -= 40f * Time.deltaTime;
            }
        } else
        {
            if (character.yVelocity < maxFallSpeed)
            {
                character.yVelocity += Gravity * Time.deltaTime;
            }
            else if (character.yVelocity > maxFallSpeed)
            {
                character.yVelocity = maxFallSpeed;
            }
        }

        if (shiftHeld && Input.GetAxis("Ability1") == 0.0f)
        {
            MonoBehaviour.print("shift unheld");
            shiftHeld = false;
        }
        if (Input.GetAxis("Ability1") != 0.0f && character.canDash && !shiftHeld)
        {
            MonoBehaviour.print("woosh");
            character.SetState(new CraneDashState(character));
        }
        
    }

	public override void PhysicsTick() {
		character.momentum = Vector3.Lerp(character.momentum, direction * character.moveSpeed, 0.015f);
		player.Move(character.momentum * Time.fixedDeltaTime);

		player.Move(Vector3.down * character.yVelocity * Time.fixedDeltaTime);
	}

    public override void OnStateExit()
    {
        character.craneAnimator.SetBool("Falling", false);
        character.jumped = true;
        AkSoundEngine.PostEvent("WindStop", character.gameObject);
        character.canDash = true;
    }

    public override void OnColliderHit(ControllerColliderHit hit)
    {
        Vector3 hitNormal = hit.normal;
        bool isGrounded = (Vector3.Angle(Vector3.up, hitNormal) <= (player.slopeLimit));
        bool iswall = (Vector3.Angle(Vector3.up, hitNormal) <= 90);
        if (!isGrounded && iswall)
        {
            float slideX = (1f - hitNormal.y) * hitNormal.x * (1f - slideFriction);
            float slideZ = (1f - hitNormal.y) * hitNormal.z * (1f - slideFriction);
			player.Move(new Vector3(slideX, 0f, slideZ) * Time.fixedDeltaTime);
        } else if (isGrounded)
        {
            character.SetState(new CraneIdleState(character));
        }
    }
}
