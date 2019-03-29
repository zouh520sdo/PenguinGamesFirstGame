using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuoPot : Wandable {

    public List<Medicine> medicines;
    public GameObject junkMedicinePrefab;
    protected List<Recipe> recipes;
    public List<GameObject> producedMedicines;

    protected override void OnStart()
    {
        base.OnStart();
        recipes = new List<Recipe>(GetComponents<Recipe>());
    }

    protected override void OnUpdate()
    {
        base.OnUpdate();
        if (Input.GetKeyDown(KeyCode.G))
        {
            stir();
        }
    }

    public override void OnReset()
    {
        base.OnReset();

        // Clear up all produced medicines
        foreach (GameObject g in producedMedicines)
        {
            Destroy(g);
        }
        producedMedicines.Clear();

        // Clear up all medicines in pot
        medicines.Clear();
    }

    public void stir ()
    {
        foreach (Recipe r in recipes)
        {
            int i = 0;
            for (; i<medicines.Count; i++)
            {
                if (i >= r.medicines.Count)
                {
                    i = -1;
                    break;
                }
                if (!medicines[i].isInGoodCondition || r.medicines[i].type != medicines[i].type)
                {
                    break;
                }
            }

            if (r.medicines.Count == i)
            {
                CleanAllMedicines();
                // Produce corresponding medicine
                producedMedicines.Add(Instantiate(r.producedMedicinePrefab, transform.position, Quaternion.identity));
                return;
            }
        }

        // Produce junk medicine
        CleanAllMedicines();
        if (junkMedicinePrefab)
        { 
            GameObject temp = Instantiate(junkMedicinePrefab);
            producedMedicines.Add(temp);
        }
    }

    protected void CleanAllMedicines()
    {
        // Hide all medicines
        medicines.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        Medicine medicine = other.GetComponent<Medicine>();
        if (medicine)
        {
            Pickable pickable = other.GetComponent<Pickable>();
            if (medicine.goodHeatLow <= containingHeat && medicine.goodHeatHigh >= containingHeat)
            {
                medicine.isInGoodCondition = true;
            }
            else
            {
                medicine.isInGoodCondition = false;
            }
            medicines.Add(medicine);
            if (pickable)
            {
                pickable.isPickable = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Medicine medicine = other.GetComponent<Medicine>();
        if (medicine)
        {
            Pickable pickable = other.GetComponent<Pickable>();
            medicines.Remove(medicine);
            if (pickable)
            {
                pickable.isPickable = true;
            }
        }
    }
}
