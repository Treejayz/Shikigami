using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State {

    protected Character character;

    public abstract void Tick();
	public abstract void PhysicsTick();

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }

    public State(Character character)
    {
        this.character = character;
    }
}
