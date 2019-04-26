using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogPicture : Trigger {

    public MedicineType desiredType;
    public List<Medicine> nearMedicines;
    public bool isActive;
    public Wand wand;
    public Note frogNote;
    public bool noNeedReset;
    protected Note attachedFrogNote;
    protected bool originalIsActive;
    private Animator frogAnim;

    public OpeningEveNote gotExlixirTy;
    public FrogSecretTrigger secretTrigger;
    public GameObject frogShining;

    public override void Start()
    {
        base.Start();
        originalIsActive = isActive;
       
        // added for activating animation for frog --francys
        frogAnim = GetComponentInChildren<Animator>();

        if (!wand)
        {
            wand = GameObject.FindGameObjectWithTag("Player").GetComponent<Wand>();
        }
        if (!frogNote)
        {
            frogNote = GetComponent<Note>();
        }
        if (!attachedFrogNote)
        {
            attachedFrogNote = GetComponent<Note>();
        }
    }

    public override void OnReset()
    {
        base.OnReset();
        if (!noNeedReset)
        {
            isActive = originalIsActive;
            deactivateTriggees();

            frogAnim.ResetTrigger("sleep");
            frogAnim.ResetTrigger("openeyes");
        }
    }

    public override void Update()
    {
        base.Update();

        if (!isActive)
        {
            foreach (Medicine m in nearMedicines)
            {
                Pickable pickable = m.GetComponent<Pickable>();
                if (!pickable.isPickedUp && m.type == desiredType)
                {
                    // added for activating animation for frog --francys
                    //frogAnim.SetTrigger("openeyes");
                    StartCoroutine(openEyes());

                    // Found what frog wants, no need for hint
                    secretTrigger.needHint = false;

                    int foundThisDiary = PlayerPrefs.GetInt("FoundThisDiary");
                    if (foundThisDiary != 0)
                    {
                        if (!secretTrigger.isStart)
                        {
                            secretTrigger.isStart = true;
                        }

                        if (secretTrigger.isActive)
                        {
                            wand.fpc.enabled = false;
                            wand.note = frogNote;
                            wand.SetDialogueText(frogNote.nextLine());
                            if (gotExlixirTy == frogNote)
                            {
                                FrogFlyToWand();
                            }
                        }
                    }
                    else
                    {
                        wand.fpc.enabled = false;
                        wand.note = frogNote;
                        wand.SetDialogueText(frogNote.nextLine());
                        if (gotExlixirTy == frogNote)
                        {
                            FrogFlyToWand();
                        }
                    }

                    isActive = true;

                    activateTriggees();
                    // May need to update dialogue

                    break;
                }
            }
        }
    }

    public void FrogFlyToWand()
    {
        frogAnim.gameObject.SetActive(false);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SetHasGua();
        StartCoroutine(FlyingToWand());
    }

    public IEnumerator FlyingToWand()
    {
        wand.handAnimator.ResetTrigger("Hide");
        wand.handAnimator.SetTrigger("Show");
        GameObject wandInHand = wand.wandEffect.gameObject;
        while (Vector3.Distance(frogShining.transform.position, wandInHand.transform.position) > 0.2f)
        {
            frogShining.transform.position = Vector3.Lerp(frogShining.transform.position, wandInHand.transform.position, 2f * Time.deltaTime);
            yield return null;
        }
        frogShining.SetActive(false);
        attachedFrogNote.setFinished(true);
        attachedFrogNote.setCanRepeatHard(false);
        // No need to reset the blue wall and the exit door
        noNeedReset = true;
        wand.handAnimator.ResetTrigger("Show");
        wand.handAnimator.SetTrigger("Hide");
        wand.SetFrogWand();
    }

    public void OnMouseOver()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            int foundThisDiary = PlayerPrefs.GetInt("FoundThisDiary");
            if (foundThisDiary != 0)
            {
                secretTrigger.isStart = true;
            }
            
            if (secretTrigger.needHint && !secretTrigger.frogHint.inThisNote && !wand.pickable)
            {
                secretTrigger.gameManager.StartNote(secretTrigger.frogHint);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Medicine medicine = other.GetComponent<Medicine>();
        if (medicine)
        {
            nearMedicines.Add(medicine);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Medicine medicine = other.GetComponent<Medicine>();
        if (medicine)
        {
            nearMedicines.Remove(medicine);
        }
    }

    IEnumerator openEyes()
    {
        frogAnim.ResetTrigger("sleep");
        frogAnim.SetTrigger("openeyes");
        yield return new WaitForSeconds(8);
        frogAnim.ResetTrigger("openeyes");
        frogAnim.SetTrigger("sleep");
    }

    public IEnumerator openEyes(int waitingTime)
    {
        frogAnim.ResetTrigger("sleep");
        frogAnim.SetTrigger("openeyes");
        yield return new WaitForSeconds(waitingTime);
        frogAnim.ResetTrigger("openeyes");
        frogAnim.SetTrigger("sleep");
    }

    public void TriggerSecretDialogue()
    {
        frogNote = gotExlixirTy;
    }
}
