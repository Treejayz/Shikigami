using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogIdleState : State {

    private CharacterController player;

    public FrogIdleState(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
        player = character.GetComponent<CharacterController>();
        character.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        character.gameObject.transform.GetChild(1).gameObject.SetActive(true);
        character.frogAnimator.SetBool("Moving", false);
        character.frogAnimator.SetBool("Jumping", false);
    }

    public override void Tick()
    {
        if (!player.isGrounded)
        {
            character.SetState(new FrogFallState(character));
        }
        if (Input.GetAxis("Vertical") != 0.0f || Input.GetAxis("Horizontal") != 0.0f)
        {
            character.SetState(new FrogMoveState(character));

        }
        if (Input.GetAxis("Jump") != 0.0f)
        {
            character.SetState(new FrogJumpState(character));
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            character.SetForm("Crane");
            character.GetComponentsInChildren<ParticleSystem>()[3].Play();
            character.SetState(new CraneIdleState(character));
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
        character.frogAnimator.SetBool("Moving", true);
    }
}
