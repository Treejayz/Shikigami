using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogFallState : State {

    private CharacterController player;


    private float Gravity = 30f;
    private float maxFallSpeed = 20f;
    private float slideFriction = 0.3f;

    private float fallSpeed;


    public FrogFallState(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
        MonoBehaviour.print("entering falling state");
        player = character.GetComponent<CharacterController>();
        fallSpeed = 0.0f;
    }

    public override void Tick()
    {

        Vector3 direction = ((character.transform.forward * Input.GetAxis("Vertical"))
            + (character.transform.right * Input.GetAxis("Horizontal")));
        direction.Normalize();

        player.Move(direction * Time.deltaTime * 10f);

        if (fallSpeed < maxFallSpeed)
        {
                fallSpeed += Gravity * Time.deltaTime;
        }
        else if (fallSpeed > maxFallSpeed)
        {
            fallSpeed = maxFallSpeed;
        }

        if (!player.isGrounded)
        {
            player.Move(Vector3.down * fallSpeed * Time.deltaTime);
        }
        else
        {
            character.SetState(new FrogIdleState(character));
        }

    }

    public override void OnColliderHit(ControllerColliderHit hit)
    {
        Vector3 hitNormal = hit.normal;
        bool isGrounded = (Vector3.Angle(Vector3.up, hitNormal) <= player.slopeLimit);
        if (!isGrounded)
        {
            float slideX = (1f - hitNormal.y) * hitNormal.x * (1f - slideFriction);
            float slideZ = (1f - hitNormal.y) * hitNormal.z * (1f - slideFriction);
            player.Move(new Vector3(slideX, 0f, slideZ) * Time.deltaTime);
        }
        else
        {
            character.SetState(new FrogIdleState(character));
        }
    }
}
