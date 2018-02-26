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

    public enum CurrentForm { CRANE, FROG, FOX };
    public CurrentForm Form;

    public Animator craneAnimator;
    public Animator frogAnimator;

    [HideInInspector]
    public bool dead;
    [HideInInspector]
    public bool jumped;

    [HideInInspector]
    public bool canDash;
    [HideInInspector]
    public bool isDashing;
    [HideInInspector]
    public bool sneaking;

    private GameObject CraneMesh, FrogMesh, FoxMesh;

    private State currentState;

    private void Start()
    {
        CraneMesh = transform.GetChild(0).gameObject;
        FrogMesh = transform.GetChild(1).gameObject;
        FoxMesh = transform.GetChild(2).gameObject;
        switch (Form)
        {
            case CurrentForm.CRANE:
                SetState(new CraneIdleState(this));
                SetForm("Crane");
                break;
            case CurrentForm.FROG:
                SetState(new FrogIdleState(this));
                SetForm("Frog");
                break;
            case CurrentForm.FOX:
                SetState(new FoxIdleState(this));
                SetForm("Fox");
                break;
        };
        dead = false;
        jumped = false;
        canDash = true;
        isDashing = false;
        sneaking = false;

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
        if (jumped && Input.GetAxis("Jump") == 0f) { jumped = false; }
        if (!canDash && !isDashing) { StartCoroutine("DashCooldown"); }
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

    public void SetForm(System.String next)
    {

        if (next == "Frog")
        {
            Form = CurrentForm.FROG;
            CraneMesh.SetActive(false);
            FrogMesh.SetActive(true);
            FoxMesh.SetActive(false);
            craneAnimator.enabled = false;
            frogAnimator.enabled = true;
        } else if (next == "Crane")
        {
            Form = CurrentForm.CRANE;
            CraneMesh.SetActive(true);
            FrogMesh.SetActive(false);
            FoxMesh.SetActive(false);
            craneAnimator.enabled = true;
            frogAnimator.enabled = false;
        } else if (next == "Fox")
        {
            Form = CurrentForm.FOX;
            CraneMesh.SetActive(false);
            FrogMesh.SetActive(false);
            FoxMesh.SetActive(true);
            craneAnimator.enabled = false;
            frogAnimator.enabled = false;
        }
    }
    public CurrentForm GetForm()
    {
        return Form;
    }

    IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(0.3f);
        while (!GetComponent<CharacterController>().isGrounded)
        {
            yield return new WaitForEndOfFrame();
        }
        canDash = true;
    }
}
