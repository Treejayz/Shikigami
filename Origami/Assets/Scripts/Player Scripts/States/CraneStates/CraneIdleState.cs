using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneIdleState : State {

	private CharacterController player;
    private bool shiftHeld;
    private float time;

    public CraneIdleState(Character character) : base(character)
    {
    }

	public override void OnStateEnter()
	{
		player = character.GetComponent<CharacterController>();
        character.craneAnimator.SetBool("Moving", false);
        if (Input.GetAxis("Ability1") != 0f)
        {
            shiftHeld = true;
        } else
        {
            shiftHeld = false;
        }
    }

    public override void Tick() {

        time += Time.deltaTime;
        if (time > 10f)
        {
            character.craneAnimator.SetTrigger("Shimmy");
            time -= 10f;
        }

        if (!player.isGrounded) {
			character.SetState(new CraneFallingState(character));
		}
		if (Input.GetAxis("Vertical") != 0.0f || Input.GetAxis("Horizontal") != 0.0f)
        {
            character.SetState(new CraneMovingState(character));

        } 
		if (Input.GetAxis("Jump") != 0.0f && !character.jumped) {
			character.SetState(new CraneJumpState(character));
		}

        if (shiftHeld && Input.GetAxis("Ability1") == 0f)
        {
            shiftHeld = false;
        }

        if (Input.GetAxis("Ability1") != 0.0f && character.canDash && !shiftHeld)
        {
            MonoBehaviour.print("here we go again");
            character.SetState(new CraneDashState(character));
        }
        if (!character.switching)
        {
            if (Input.GetAxis("Switch2") != 0.0f && character.canFrog)
            {
                character.SetForm("Frog");
                character.GetComponentsInChildren<ParticleSystem>()[3].Play();
                character.SetState(new FrogMoveState(character));
            }
            else if (Input.GetAxis("Switch1") != 0.0f && character.canFrog)
            {
                if (character.canFox)
                {
                    character.SetForm("Fox");
                    character.GetComponentsInChildren<ParticleSystem>()[3].Play();
                    character.SetState(new FoxMoveState(character));
                }
                else
                {
                    character.SetForm("Frog");
                    character.GetComponentsInChildren<ParticleSystem>()[3].Play();
                    character.SetState(new FrogMoveState(character));
                }
            }
        }
    }

	public override void PhysicsTick() {
		Vector3 target = new Vector3(0f, 0f, 0f);
		character.momentum = Vector3.Lerp(character.momentum, target, 0.05f);
        if (character.momentum.x < 0.001f && character.momentum.x > -0.001f) { character.momentum.x = 0f; }
        if (character.momentum.z < 0.001f && character.momentum.z > -0.001f) { character.momentum.z = 0f; }

        player.Move(character.momentum * Time.fixedDeltaTime);
		player.Move(Vector3.down * character.gravity * Time.fixedDeltaTime);
	}

    public override void OnStateExit()
    {
        character.craneAnimator.SetBool("Moving", true);
    }

    public override void OnColliderHit(ControllerColliderHit hit)
	{
		Vector3 hitNormal = hit.normal;
		bool isGrounded = (Vector3.Angle(Vector3.up, hitNormal) <= player.slopeLimit);
		if (!isGrounded)
		{
            character.SetState(new CraneFallingState(character));
        } else
		{
			//character.SetState(new CraneIdleState(character));
		}
	}
}
