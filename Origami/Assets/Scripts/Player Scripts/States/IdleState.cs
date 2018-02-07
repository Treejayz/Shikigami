using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State {

    public IdleState(Character character) : base(character)
    {
    }

    public override void Tick() {
        //Do idle stuff
        if (Input.GetAxis("Vertical") != 0.0f)
        {
            character.SetState(new MovingState(character));

        }

    }
}
