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
        MonoBehaviour.print("entering Frog idle state");
        player = character.GetComponent<CharacterController>();
        character.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        character.gameObject.transform.GetChild(1).gameObject.SetActive(true);
    }

    public override void Tick()
    {

        Vector3 target = new Vector3(0f, 0f, 0f);
        character.momentum = Vector3.Lerp(character.momentum, target, 0.05f);
        player.Move(character.momentum * Time.deltaTime);
        player.Move(Vector3.down * 10f * Time.deltaTime);

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
            character.SetState(new CraneIdleState(character));
        }
    }
}
