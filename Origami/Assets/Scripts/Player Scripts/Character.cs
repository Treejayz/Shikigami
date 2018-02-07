using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 1f;

    private State currentState;

    private void Start()
    {
        SetState(new IdleState(this));
    }

    private void Update()
    {
        currentState.Tick();
    }
	private void FixedUpdate()
	{
		currentState.PhysicsTick();
	}

    public void SetState(State state)
    {
        if (currentState != null)
            currentState.OnStateExit();

        currentState = state;
        gameObject.name = "Player - " + state.GetType().Name;

        if (currentState != null)
            currentState.OnStateEnter();
    }
}
