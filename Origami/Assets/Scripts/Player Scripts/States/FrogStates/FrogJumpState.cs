﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogJumpState : State {

    private CharacterController player;

    private float Gravity = 20f;
    private float slowGravity = 10f;
    private float jumpSpeed = 20f;
    private float currentSpeed;

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

        Vector3 direction = ((character.transform.forward * Input.GetAxis("Vertical"))
            + (character.transform.right * Input.GetAxis("Horizontal")));
        direction.Normalize();

        player.Move(direction * Time.deltaTime * 10f);

        if (currentSpeed > 0.0f)
        {
            currentSpeed -= Gravity * Time.deltaTime;
        }
        else
        {
            character.SetState(new FrogFallState(character));
        }
        player.Move(Vector3.up * currentSpeed * Time.deltaTime);
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
