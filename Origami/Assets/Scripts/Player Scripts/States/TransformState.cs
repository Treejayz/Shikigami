using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformState : State
{

    private CharacterController player;
    private float currentTime;
    private bool Q; // if not q, then it's e. q = true
    private bool step2;


    private GameObject craneFold, frogFold, foxFold;

    //Crane Start and End
    private float craneStartY = 0f;
    private float craneEndY = 1f;
    //private float craneStartRot = 0f;
    private float craneEndRot = -45f;

    //Frog Start and End
    private Vector3 frogStartPos = new Vector3(0f, 0, 0f);
    private Vector3 frogEndPos = new Vector3(0, 2f, .75f);
    private float frogStartScale = 1f;
    private float frogEndScale = 1.43574f;

    private float foxEndY = 0.5f;
    private float foxEndScale = 1.5f;


    public TransformState(Character character) : base(character)
    {
    }
    public TransformState(Character character, bool q) : base(character)
    {
        Q = q;
    }

    public override void OnStateEnter()
    {
        AkSoundEngine.PostEvent("Transform",character.gameObject);
        player = character.GetComponent<CharacterController>();
        character.switching = true;

        currentTime = 0f;
        character.GetComponentsInChildren<ParticleSystem>()[3].Play();
        character.gameObject.transform.GetChild(1).gameObject.SetActive(false);
        character.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        character.gameObject.transform.GetChild(2).gameObject.SetActive(false);

        craneFold = character.gameObject.transform.GetChild(3).gameObject;
        frogFold = character.gameObject.transform.GetChild(4).gameObject;
        foxFold = character.gameObject.transform.GetChild(5).gameObject;

        step2 = false;

        switch (character.Form)
        {
            case Character.CurrentForm.CRANE:
                craneFold.GetComponentInChildren<Animator>().SetFloat("Speed", 1f);
                craneFold.SetActive(true);
                craneFold.GetComponentInChildren<Animator>().Play("Crane_Fold_UV", -1, 0f);
                break;
            case Character.CurrentForm.FROG:
                frogFold.GetComponentInChildren<Animator>().SetFloat("Speed", 1f);
                frogFold.SetActive(true);
                frogFold.GetComponentInChildren<Animator>().Play("Frog_Fold_Test", -1, 0f);
                break;
            case Character.CurrentForm.FOX:
                foxFold.GetComponentInChildren<Animator>().SetFloat("Speed", 1f);
                foxFold.SetActive(true);
                foxFold.GetComponentInChildren<Animator>().Play("Fox_Fold", -1, 0f);
                break;
        };

    }
    public override void Tick()
    {
        if (character.yVelocity < 30)
        {
            character.yVelocity += 30 * Time.deltaTime;
        }
        else if (character.yVelocity > 30)
        {
            character.yVelocity = 30;
        }

        if (currentTime < 2f)
        {
            currentTime += Time.deltaTime;
            if (currentTime < 1f)
            {
                float progress = (currentTime / 1f);
                progress = progress * progress;
                switch (character.Form)
                {
                    case Character.CurrentForm.CRANE:
                        float craneY = craneStartY * (1f - progress) + craneEndY * progress;
                        float craneRot = (360 + craneEndRot) * progress;
                        craneFold.transform.localPosition = new Vector3(0, craneY, 0);
                        craneFold.transform.localEulerAngles = new Vector3(0, craneRot, 0f);
                        break;
                    case Character.CurrentForm.FROG:
                        Vector3 frogPos = frogStartPos * (1f - progress) + frogEndPos * progress;
                        float frogRot =  360f * progress;
                        float frogScale = frogStartScale * (1f - progress) + frogEndScale * progress;
                        frogFold.transform.localPosition = frogPos;
                        frogFold.transform.localEulerAngles = new Vector3(0, frogRot, 0f);
                        frogFold.transform.localScale = new Vector3(frogScale, frogScale, frogScale);
                        break;
                    case Character.CurrentForm.FOX:
                        float foxY = foxEndY * progress;
                        float foxScale = (1f - progress) + foxEndScale * progress;
                        float foxRot = 360f * progress;
                        foxFold.transform.localEulerAngles = new Vector3(0, foxRot, 0f);
                        foxFold.transform.localPosition = new Vector3(0f, foxY, 0f);
                        foxFold.transform.localScale = new Vector3(foxScale, foxScale, foxScale);
                        break;
                };
            }
            else if (currentTime > 1f && !step2)
            {
                step2 = true;
                switch (character.Form)
                {
                    case Character.CurrentForm.CRANE:
                        craneFold.SetActive(false);
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
                        frogFold.SetActive(false);
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
                        foxFold.SetActive(false);
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
                        craneFold.SetActive(true);
                        craneFold.GetComponentInChildren<Animator>().SetFloat("Speed", -1f);
                        craneFold.GetComponentInChildren<Animator>().Play("Crane_Fold_UV", -1, 1f);
                        break;
                    case Character.CurrentForm.FROG:
                        frogFold.SetActive(true);
                        frogFold.GetComponentInChildren<Animator>().SetFloat("Speed", -1f);
                        frogFold.GetComponentInChildren<Animator>().Play("Frog_Fold_Test", -1, 1f);
                        break;
                    case Character.CurrentForm.FOX:
                        foxFold.SetActive(true);
                        foxFold.GetComponentInChildren<Animator>().SetFloat("Speed", -1f);
                        foxFold.GetComponentInChildren<Animator>().Play("Fox_Fold", -1, 1f);
                        break;
                };
            }
            else
            {
                float progress = 1 - ((currentTime - 1f) / 1f);
                progress = progress * progress;
                switch (character.Form)
                {
                    case Character.CurrentForm.CRANE:
                        float craneY = craneStartY * (1f - progress) + craneEndY * progress;
                        float craneRot = -1 * (360 - craneEndRot) * progress;
                        craneFold.transform.localPosition = new Vector3(0, craneY, 0);
                        craneFold.transform.localEulerAngles = new Vector3(0, craneRot, 0f);
                        break;
                    case Character.CurrentForm.FROG:
                        Vector3 frogPos = frogStartPos * (1f - progress) + frogEndPos * progress;
                        float frogRot = -1 * 360f * progress;
                        float frogScale = frogStartScale * (1f - progress) + frogEndScale * progress;
                        frogFold.transform.localPosition = frogPos;
                        frogFold.transform.localEulerAngles = new Vector3(0, frogRot, 0f);
                        frogFold.transform.localScale = new Vector3(frogScale, frogScale, frogScale);
                        break;
                    case Character.CurrentForm.FOX:
                        float foxY = foxEndY * progress;
                        float foxScale = (1f - progress) + foxEndScale * progress;
                        float foxRot = -360f * progress;
                        foxFold.transform.localEulerAngles = new Vector3(0, foxRot, 0f);
                        foxFold.transform.localPosition = new Vector3(0f, foxY, 0f);
                        foxFold.transform.localScale = new Vector3(foxScale, foxScale, foxScale);
                        break;
                };
            }
        }
        else
        {
            switch (character.Form)
            {
                case Character.CurrentForm.CRANE:
                    craneFold.SetActive(false);
                    SetForm("Crane");
                    break;
                case Character.CurrentForm.FROG:
                    frogFold.SetActive(false);
                    SetForm("Frog");
                    break;
                case Character.CurrentForm.FOX:
                    foxFold.SetActive(false);
                    SetForm("Fox");
                    break;
            };
        }

    }

    public override void PhysicsTick()
    {
        Vector3 target = new Vector3(0f, 0f, 0f);
        if (player.isGrounded)
        {
            character.momentum = Vector3.Lerp(character.momentum, target, 0.015f);
        } else
        {
            character.momentum = Vector3.Lerp(character.momentum, target, 0.005f);
        }
        if (character.momentum.x < 0.001f && character.momentum.x > -0.001f) { character.momentum.x = 0f; }
        if (character.momentum.z < 0.001f && character.momentum.z > -0.001f) { character.momentum.z = 0f; }

        player.Move(character.momentum * Time.fixedDeltaTime);
        player.Move(Vector3.down * character.yVelocity * Time.fixedDeltaTime);
    }

    public override void OnStateExit()
    {
        character.switching = false;
    }

    private void SetForm(string next)
    {
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