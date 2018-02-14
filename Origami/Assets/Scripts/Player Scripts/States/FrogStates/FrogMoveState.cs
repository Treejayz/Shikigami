using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMoveState : State {



    private CharacterController player;

    public FrogMoveState(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
        MonoBehaviour.print("entering frog move state");
        player = character.GetComponent<CharacterController>();
    }

    public override void Tick()
    {

        Vector3 direction = ((character.transform.forward * Input.GetAxis("Vertical"))
            + (character.transform.right * Input.GetAxis("Horizontal")));
        direction.Normalize();

        character.momentum = Vector3.Lerp(character.momentum, direction * 10f, 0.08f);
        player.Move(character.momentum * Time.deltaTime);

        player.Move(Vector3.down * 10f * Time.deltaTime);

        if (!player.isGrounded)
        {
            character.SetState(new FrogFallState(character));
        }

        if (Input.GetAxis("Vertical") == 0.0f && Input.GetAxis("Horizontal") == 0.0f)
        {
            character.SetState(new FrogIdleState(character));
        }

        if (Input.GetAxis("Jump") != 0.0f)
        {
            character.SetState(new FrogJumpState(character));
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
        }
    }
}
