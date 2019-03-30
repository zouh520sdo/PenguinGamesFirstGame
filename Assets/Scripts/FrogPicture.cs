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

    public override void Start()
    {
        base.Start();
        originalIsActive = isActive;
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
                    isActive = true;
                    wand.note = frogNote;
                    wand.SetDialogueText(frogNote.nextLine());

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
}
