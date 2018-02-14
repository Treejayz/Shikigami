using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneIdleState : State {

	private CharacterController player;

    public CraneIdleState(Character character) : base(character)
    {
    }

	public override void OnStateEnter()
	{
		player = character.GetComponent<CharacterController>();
        character.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        character.gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public override void Tick() {

		

        

        if (!player.isGrounded) {
			character.SetState(new CraneFallingState(character));
		}
		if (Input.GetAxis("Vertical") != 0.0f || Input.GetAxis("Horizontal") != 0.0f)
        {
            character.SetState(new CraneMovingState(character));

        } 
		if (Input.GetAxis("Jump") != 0.0f) {
			character.SetState(new CraneJumpState(character));
		}

        if (Input.GetKeyDown(KeyCode.E))
        {
            character.SetState(new FrogIdleState(character));
        }
    }

	public override void PhysicsTick() {
		Vector3 target = new Vector3(0f, 0f, 0f);
		character.momentum = Vector3.Lerp(character.momentum, target, 0.05f);

		player.Move(character.momentum * Time.fixedDeltaTime);
		player.Move(Vector3.down * 10f * Time.fixedDeltaTime);
	}

	public override void OnColliderHit(ControllerColliderHit hit)
	{
		Vector3 hitNormal = hit.normal;
		bool isGrounded = (Vector3.Angle(Vector3.up, hitNormal) <= player.slopeLimit);
		if (!isGrounded)
		{
			float slideX = (1f - hitNormal.y) * hitNormal.x * (.7f);
			float slideZ = (1f - hitNormal.y) * hitNormal.z * (.7f);
			player.Move(new Vector3(slideX, 0f, slideZ) * Time.deltaTime);
		} else
		{
			character.SetState(new CraneIdleState(character));
		}
	}
}
