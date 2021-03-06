﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuoPot : Wandable {

    public List<Medicine> medicines;
    public GameObject junkMedicinePrefab;
    protected List<Recipe> recipes;
    public List<GameObject> producedMedicines;
    public float dampingSpeed = 5f;
    public float roomTemperature = 50f;
    public TextMesh heatTemp;
    public AudioSource failefect;

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

        // Heat damping to room's heat
        float heatDiff = containingHeat - roomTemperature;
        float change = dampingSpeed * Time.deltaTime;
        if (change < Mathf.Abs(heatDiff))
        {
            //containingHeat = Mathf.Lerp(containingHeat, roomTemperature, change);
            if (heatDiff >= 0)
            {
                containingHeat -= change;
            }
            else
            {
                containingHeat += change;
            }
        }
        else
        {
            containingHeat = roomTemperature;
        }

        heatTemp.text = ((int)containingHeat).ToString(); 
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
                GameObject tempMedicine = Instantiate(r.producedMedicinePrefab, transform.position, Quaternion.identity);
                producedMedicines.Add(tempMedicine);

                Rigidbody rigidMed = tempMedicine.GetComponent<Rigidbody>();
                if (rigidMed)
                {
                    Vector3 offVec = Vector3.zero;
                    offVec.x = Random.Range(-5f, 5f);
                    offVec.z = Mathf.Sqrt(25f - offVec.x * offVec.x);
                    if (Random.Range(0f, 1f) < 0.5f)
                    {
                        offVec.z *= -1;
                    }
                    rigidMed.AddForce(Vector3.up * 8f + offVec, ForceMode.Impulse);
                }
                return;
            }
        }

        // Produce junk medicine
        int originalCount = medicines.Count;
        CleanAllMedicines();
        if (junkMedicinePrefab && originalCount != 0)
        { 
            GameObject temp = Instantiate(junkMedicinePrefab, transform.position, Quaternion.identity);
            failefect.Play();
            Rigidbody rigidMed = temp.GetComponent<Rigidbody>();
            if (rigidMed)
            {
                Vector3 offVec = Vector3.zero;
                offVec.x = Random.Range(-5f, 5f);
                offVec.z = Mathf.Sqrt(25f - offVec.x * offVec.x);
                if (Random.Range(0f, 1f) < 0.5f)
                {
                    offVec.z *= -1;
                }
                rigidMed.AddForce(Vector3.up * 8f + offVec, ForceMode.Impulse);
            }
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
