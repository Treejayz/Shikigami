using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneMovingState : State {


	private CharacterController player;

    public CraneMovingState(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
        MonoBehaviour.print("entering move state");
		player = character.GetComponent<CharacterController>();
    }

    public override void Tick()
    {

		Vector3 direction = ((character.transform.forward * Input.GetAxis("Vertical")) 
			+ (character.transform.right * Input.GetAxis("Horizontal")));
		direction.Normalize();

		player.Move(direction * Time.deltaTime * 10f);

		player.Move(Vector3.down * .01f);

		if (!player.isGrounded) {
			character.SetState(new CraneFallingState(character));
		}

		if (Input.GetAxis("Vertical") == 0.0f && Input.GetAxis("Horizontal") == 0.0f)  {
            character.SetState(new CraneIdleState(character));
        }

		if (Input.GetAxis("Jump") != 0.0f) {
			character.SetState(new CraneJumpState(character));
		}
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
            //character.SetState(new CraneIdleState(character));
        }
    }
}
