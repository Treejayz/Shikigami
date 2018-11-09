using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartState : State
{

    private CharacterController player;
    private float currentTime;
    private bool pressed, done;


    private GameObject craneFold, craneAnimator;
    private float craneStartY = 0f;
    private float craneEndY = 1f;
    private float craneEndRot = -45f;





    public StartState(Character character) : base(character)
    {
    }

    public override void OnStateEnter()
    {
		GameObject.Find ("Pause Menu Overseer").GetComponent<PauseSystem> ().cantpauseon ();
        player = character.GetComponent<CharacterController>();
        character.switching = true;
        currentTime = 0f;
        character.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        character.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        character.gameObject.transform.GetChild(2).gameObject.SetActive(false);
        craneFold = character.gameObject.transform.GetChild(3).gameObject;
        craneAnimator = (craneFold.transform.GetChild(1)).gameObject;

        pressed = false;
        done = false;
        craneFold.transform.GetChild(0).gameObject.SetActive(false);
        craneAnimator.GetComponent<Animator>().SetFloat("Speed", 0f);
        craneFold.SetActive(true);
        craneAnimator.SetActive(true);
        craneAnimator.GetComponent<Animator>().Play("Crane_Fold_Slow", 0, 1f);
        craneFold.transform.localPosition = new Vector3(0f, -.75f, 0f);

        player.GetComponent<forwardtest>().enabled = false;

        MonoBehaviour.print("Start");
    }
    public override void Tick()
    {

        if (!pressed)
        {
            if (Input.anyKeyDown || Input.GetAxis("Ability1") != 0f || Input.GetAxis("Ability2") != 0f || Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
            {
                pressed = true;
                craneAnimator.GetComponent<Animator>().SetFloat("Speed", -1f);
                craneAnimator.GetComponent<Animator>().Play("Crane_Fold_Slow", 0, 1f);
                AkSoundEngine.PostEvent("InitialTransform", player.gameObject);
            }
        }
        else if (currentTime < 3f && pressed)
        {

            currentTime += Time.deltaTime;
            float progress = currentTime / 3f;
            float craneY, craneRot;
            if (progress < 0.1f)
            {
                craneY = -7.5f * (.1f - progress) + 10 * progress;
            }
            else
            {
                craneY = (10 / 9) * (1f - progress);
            }
            if (progress < 0.5f)
            {
                craneRot = 540f * Mathf.Pow((progress * 2), 2);
            }
            else
            {
                craneRot = 1080 - 540 * Mathf.Pow(((1f - progress) * 2), 2);
            }
            craneFold.transform.localPosition = new Vector3(0, craneY, 0);
            craneFold.transform.localEulerAngles = new Vector3(0, craneRot, 0f);
        }
        else if (!done)
        {
            craneFold.SetActive(false);
            character.CraneMesh.SetActive(true);
            character.FrogMesh.SetActive(false);
            character.FoxMesh.SetActive(false);
            character.craneAnimator.enabled = true;
            character.frogAnimator.enabled = false;
            character.foxAnimator.enabled = false;

            done = true;
            character.craneAnimator.Play("Crane_Shimmy");
        } else if (done && !character.craneAnimator.GetCurrentAnimatorStateInfo(0).IsName("Crane_Shimmy"))
        {
            character.SetState(new CraneIdleState(character));
        }

    }

    public override void PhysicsTick()
    {
        player.Move(Vector3.down * character.gravity * Time.fixedDeltaTime);
    }

    public override void OnStateExit()
    {
		GameObject.Find ("Pause Menu Overseer").GetComponent<PauseSystem> ().cantpauseoff ();
        character.switching = false;
        craneFold.transform.GetChild(0).gameObject.SetActive(true);
        craneAnimator.SetActive(false);
        player.GetComponent<forwardtest>().enabled = true;
    }

}