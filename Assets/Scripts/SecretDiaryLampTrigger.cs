using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDiaryLampTrigger : Trigger {

    public bool isActive;
    public GameObject targetObject;
    public LampWandable lampWandable;

    public override void Start()
    {
        base.Start();
        if (!lampWandable)
        {
            lampWandable = GetComponent<LampWandable>();
        }

        if (!isActive)
        {
            targetObject.SetActive(false);
        }
        else
        {
            targetObject.SetActive(true);
        }
    }

    public override void Update()
    {
        base.Update();

        if (!isActive)
        {
            if (lampWandable.isOnLight)
            {
                isActive = true;
                StartCoroutine(MoveToTarget());
            }
        }
    }

    public IEnumerator MoveToTarget ()
    {
        while (Vector3.Distance(transform.position, targetObject.transform.position) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetObject.transform.position, 10f * Time.deltaTime);
            yield return null;
        }
        gameObject.SetActive(false);
        targetObject.SetActive(true);
    }
}
