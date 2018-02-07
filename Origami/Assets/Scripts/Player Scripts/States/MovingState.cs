using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : State {


    private Rigidbody rb;

    public MovingState(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
        MonoBehaviour.print("entering move state");
        rb = character.GetComponent<Rigidbody>();
    }

    public override void Tick()
    {
        MonoBehaviour.print("moving");
        rb.AddForce(character.transform.forward * 15f);

        if (Input.GetAxis("Vertical") == 0.0f)
        {
            character.SetState(new IdleState(character));
        }
    }
}
