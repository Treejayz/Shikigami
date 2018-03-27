using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformState : State
{

    private CharacterController player;
    private float currentTime;
    private bool Q; // if not q, then it's e. q = true
    private bool step2;

    public TransformState(Character character) : base(character)
    {
    }
    public TransformState(Character character, bool q) : base(character)
    {
        Q = q;
    }

    public override void OnStateEnter()
    {
        player = character.GetComponent<CharacterController>();
        currentTime = 0f;
        //character.GetComponentsInChildren<ParticleSystem>()[3].Play();
        character.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        character.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        character.gameObject.transform.GetChild(2).gameObject.SetActive(false);

        step2 = false;

        switch (character.Form)
        {
            case Character.CurrentForm.CRANE:
                character.gameObject.transform.GetChild(3).gameObject.GetComponent<Animator>().SetFloat("Speed", 1f);
                character.gameObject.transform.GetChild(3).gameObject.SetActive(true);
                character.gameObject.transform.GetChild(3).gameObject.GetComponent<Animator>().Play("Crane_Fold_Test", -1, 0f);
                break;
            case Character.CurrentForm.FROG:
                character.gameObject.transform.GetChild(4).gameObject.GetComponent<Animator>().SetFloat("Speed", 1f);
                character.gameObject.transform.GetChild(4).gameObject.SetActive(true);
                character.gameObject.transform.GetChild(4).gameObject.GetComponent<Animator>().Play("Frog_Fold_Test", -1, 0f);
                break;
            case Character.CurrentForm.FOX:
                break;
        };

    }
    public override void Tick()
    {
        if (currentTime < 1.5f)
        {
            currentTime += Time.deltaTime;
            if (currentTime > .75f && !step2)
            {
                step2 = true;
                switch (character.Form)
                {
                    case Character.CurrentForm.CRANE:
                        character.gameObject.transform.GetChild(3).gameObject.SetActive(false);
                        if (character.canFox && Q)
                        {
                            character.Form = Character.CurrentForm.FOX;
                        }
                        else
                        {
                            character.Form = Character.CurrentForm.FROG;
                        }
                        break;
                    case Character.CurrentForm.FROG:
                        character.gameObject.transform.GetChild(4).gameObject.SetActive(false);
                        if (character.canFox && !Q)
                        {
                            character.Form = Character.CurrentForm.FOX;
                        }
                        else
                        {
                            character.Form = Character.CurrentForm.CRANE;
                        }
                        break;
                    case Character.CurrentForm.FOX:
                        if (Q)
                        {
                            character.Form = Character.CurrentForm.FROG;
                        }
                        else
                        {
                            character.Form = Character.CurrentForm.CRANE;
                        }
                        break;
                };

                switch (character.Form)
                {
                    case Character.CurrentForm.CRANE:
                        character.gameObject.transform.GetChild(3).gameObject.SetActive(true);
                        character.gameObject.transform.GetChild(3).gameObject.GetComponent<Animator>().SetFloat("Speed", -1f);
                        character.gameObject.transform.GetChild(3).gameObject.GetComponent<Animator>().Play("Crane_Fold_Test", -1, 1f);
                        break;
                    case Character.CurrentForm.FROG:
                        character.gameObject.transform.GetChild(4).gameObject.SetActive(true);
                        character.gameObject.transform.GetChild(4).gameObject.GetComponent<Animator>().SetFloat("Speed", -1f);
                        character.gameObject.transform.GetChild(4).gameObject.GetComponent<Animator>().Play("Frog_Fold_Test", -1, 1f);
                        break;
                    case Character.CurrentForm.FOX:
                        break;
                };
            }
        }
        else
        {
            switch (character.Form)
            {
                case Character.CurrentForm.CRANE:
                    character.gameObject.transform.GetChild(3).gameObject.SetActive(false);
                    SetForm("Crane");
                    break;
                case Character.CurrentForm.FROG:
                    character.gameObject.transform.GetChild(4).gameObject.SetActive(false);
                    SetForm("Frog");
                    break;
                case Character.CurrentForm.FOX:
                    SetForm("Fox");
                    break;
            };
        }

    }

    public override void PhysicsTick()
    {
        Vector3 target = new Vector3(0f, 0f, 0f);
        character.momentum = Vector3.Lerp(character.momentum, target, 0.03f);
        if (character.momentum.x < 0.001f && character.momentum.x > -0.001f) { character.momentum.x = 0f; }
        if (character.momentum.z < 0.001f && character.momentum.z > -0.001f) { character.momentum.z = 0f; }

        player.Move(character.momentum * Time.fixedDeltaTime);
        player.Move(Vector3.down * character.gravity * Time.fixedDeltaTime);
    }

    private void SetForm(string next)
    {
        character.switching = true;
        if (next == "Frog")
        {
            character.CraneMesh.SetActive(false);
            character.FrogMesh.SetActive(true);
            character.FoxMesh.SetActive(false);
            character.craneAnimator.enabled = false;
            character.frogAnimator.enabled = true;
            character.foxAnimator.enabled = false;

            character.SetState(new FrogIdleState(character));
        }
        else if (next == "Crane")
        {
            character.CraneMesh.SetActive(true);
            character.FrogMesh.SetActive(false);
            character.FoxMesh.SetActive(false);
            character.craneAnimator.enabled = true;
            character.frogAnimator.enabled = false;
            character.foxAnimator.enabled = false;

            character.SetState(new CraneIdleState(character));
        }
        else if (next == "Fox")
        {
            character.CraneMesh.SetActive(false);
            character.FrogMesh.SetActive(false);
            character.FoxMesh.SetActive(true);
            character.craneAnimator.enabled = false;
            character.frogAnimator.enabled = false;
            character.foxAnimator.enabled = true;

            character.SetState(new FoxIdleState(character));
        }
    }
}