using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {

    [SerializeField]
    public float moveSpeed = 15f;
    public float gravity = 10f;

    [HideInInspector]
    public Vector3 momentum;
    public bool canFrog = false;
    public bool canFox = false;

    public Animator craneAnimator;
    public Animator frogAnimator;

    [HideInInspector]
    public bool dead;

    private State currentState;

    private void Start()
    {
        SetState(new CraneIdleState(this));
        dead = false;
        if (craneAnimator == null)
        {
            craneAnimator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        }
        if (frogAnimator == null)
        {
            frogAnimator = transform.GetChild(1).gameObject.GetComponent<Animator>();
        }
    }

    private void Update()
    {
        currentState.Tick();
    }
	private void FixedUpdate()
	{
		currentState.PhysicsTick();
	}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Respawn" && !dead)
        {
            SetState(new DeathState(this));
        }
        currentState.OnColliderHit(hit);
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
