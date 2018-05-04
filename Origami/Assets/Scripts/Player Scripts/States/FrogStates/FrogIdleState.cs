using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogIdleState : State {

    private CharacterController player;
    private float time;

    public FrogIdleState(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
        player = character.GetComponent<CharacterController>();
        character.frogAnimator.SetBool("Moving", false);
        character.frogAnimator.SetBool("Jumping", false);
        character.yVelocity = character.gravity;
        time = 0f;
        // AkSoundEngine.PostEvent("FrogStick", character.gameObject);
        // POLISH GOAL - track time spent in fall or jump state. If greater than like 0.5 seconds, play sound.
    }

    public override void Tick()
    {
        time += Time.deltaTime;
        if (time > 7f)
        {
            character.frogAnimator.SetTrigger("Shimmy");
            time -= 7f;
        }

        if (!player.isGrounded)
        {
            character.SetState(new FrogFallState(character));
        }
        if (Input.GetAxis("Vertical") != 0.0f || Input.GetAxis("Horizontal") != 0.0f)
        {
            character.SetState(new FrogMoveState(character));

        }
        if (Input.GetAxis("Jump") != 0.0f && !character.jumped)
        {
            character.SetState(new FrogJumpState(character));
        }
        if (!character.switching)
        {
            if (Input.GetAxis("Switch1") != 0.0f)
            {
                character.SetState(new TransformState(character, true));
            }
            else if (Input.GetAxis("Switch2") != 0.0f)
            {
                character.SetState(new TransformState(character, false));
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
        if (character.frogAnimator.GetCurrentAnimatorStateInfo(0).IsName("Frog_Shimmy"))
        {
            character.frogAnimator.Play("Frog_Idle");
        }
    }
}
