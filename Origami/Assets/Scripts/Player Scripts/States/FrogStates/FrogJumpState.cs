using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogJumpState : State {

    private CharacterController player;

    private float Gravity = 20f;
    private float slowGravity = 10f;
    private float jumpSpeed = 18f;
    private float currentSpeed;

	private Vector3 direction;

    public FrogJumpState(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
        player = character.GetComponent<CharacterController>();
        currentSpeed = jumpSpeed;
    }

    public override void Tick()
    {

        direction = ((character.transform.forward * Input.GetAxis("Vertical"))
            + (character.transform.right * Input.GetAxis("Horizontal")));
        direction.Normalize();

        if (currentSpeed > 0.0f)
        {
            currentSpeed -= Gravity * Time.deltaTime;
        }
        else
        {
            character.SetState(new FrogFallState(character));
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
