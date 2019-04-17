using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogPicture : Trigger {

    public MedicineType desiredType;
    public List<Medicine> nearMedicines;
    public bool isActive;
    public Wand wand;
    public Note frogNote;
    protected bool originalIsActive;
    private Animator frogAnim;

    public OpeningEveNote gotExlixirTy;


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
    }

    public override void OnReset()
    {
        base.OnReset();
        isActive = originalIsActive;
        deactivateTriggees();

        frogAnim.ResetTrigger("sleep");
        frogAnim.ResetTrigger("openeyes");
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

                    wand.fpc.enabled = false;
                    isActive = true;
                    wand.note = frogNote;
                    wand.SetDialogueText(frogNote.nextLine());

                    if (gotExlixirTy == frogNote)
                    {
                        frogAnim.gameObject.SetActive(false);
                        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().SetHasGua();
                    }

                    activateTriggees();
                    // May need to update dialogue

                    break;
                }
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
