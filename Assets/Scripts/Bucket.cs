using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : Pickable {

    public float floatingHeight;
    public bool hasWater;

    public GameObject Water;
    public bool isWanding;

    public List<GameObject> iceCubes;

    protected bool originalHasWater;
    protected Vector3 originalPos;

    public override void Start()
    {
        base.Start();
        originalHasWater = hasWater;
        SetWaterActive(hasWater);
        originalPos = transform.position;
    }

    public override void OnReset()
    {
        base.OnReset();
        hasWater = originalHasWater;
        transform.position = originalPos;
        Water.SendMessage("OnReset", SendMessageOptions.DontRequireReceiver);
        SetWaterActive(hasWater);
        foreach (GameObject go in iceCubes)
        {
            Destroy(go);
        }
        iceCubes.Clear();
    }

    public void ResetOnIcePicked()
    {
        hasWater = false;
        Water.GetComponent<IceWater>().ResetOnIcePicked();
        SetWaterActive(hasWater);
    }

    public void SetWaterActive(bool active)
    {
        hasWater = active;
        Water.SetActive(active);
    }

    public override void Update()
    {
        base.Update();

        if (!isPickedUp)
        {
            if (!isWanding)
            {
                Vector3 targetPos = transform.position;
                targetPos.y = floatingHeight;

                targetPos.y += Mathf.Sin(2f * Time.time);

                transform.position = Vector3.Lerp(transform.position, targetPos, 1.5f * Time.deltaTime);
            }
        }
    }
}