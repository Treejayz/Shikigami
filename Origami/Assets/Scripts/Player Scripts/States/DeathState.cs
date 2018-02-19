using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{

    private CharacterController player;

    private float deathTimer = 2f;

    private float currentTime;

    public DeathState(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
        player = character.GetComponent<CharacterController>();
        currentTime = 0f;
        character.dead = true;
    }
    public override void Tick()
    {
        if (currentTime < deathTimer)
        {
            currentTime += Time.deltaTime;
        } else
        {
            character.dead = false;
            character.transform.position = Checkpoint.GetPoint();
            character.SetState(new CraneIdleState(character));
        }

    }

    public override void PhysicsTick()
    {
        Vector3 target = new Vector3(0f, 0f, 0f);
        character.momentum = Vector3.Lerp(character.momentum, target, 0.05f);
        if (character.momentum.x < 0.001f && character.momentum.x > -0.001f) { character.momentum.x = 0f; }
        if (character.momentum.z < 0.001f && character.momentum.z > -0.001f) { character.momentum.z = 0f; }

        player.Move(character.momentum * Time.fixedDeltaTime);
        player.Move(Vector3.down * character.gravity * Time.fixedDeltaTime);
    }
}