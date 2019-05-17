using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialThanksManger : MonoBehaviour {

    //           -3.8
    //16.7 1.23 -52.7
    public List<Vector3> cameraPoses;
    public float movingRatio;
    public Camera myCamera;

    public GameObject backInCredits;
    public GameObject backInSpecialThanks;
    public GameObject nextButton;

    protected IEnumerator movingCoroutine;

	// Use this for initialization
	void Start () {
        SwitchToBackInCredits();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MoveToPosIndex(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MoveToPosIndex(1);
        }
	}

    IEnumerator SlidingToPos(Vector3 targetPos)
    {
        while (Vector3.Distance(targetPos, myCamera.transform.position) > 0.02f)
        {
            myCamera.transform.position = Vector3.Lerp(myCamera.transform.position, targetPos, movingRatio * Time.deltaTime);
            yield return null;
        }

        myCamera.transform.position = targetPos;
    }

    public void MoveToPosIndex(int index)
    {
        if (index >= 0 && index < cameraPoses.Count)
        {
            if (movingCoroutine != null)
            {
                StopCoroutine(movingCoroutine);
                movingCoroutine = null;
            }
            print(cameraPoses[index]);
            movingCoroutine = SlidingToPos(cameraPoses[index]);
            StartCoroutine(movingCoroutine);
        }
    }

    public void SwitchToBackInCredits()
    {
        backInCredits.SetActive(true);
        backInSpecialThanks.SetActive(false);
        nextButton.SetActive(true);
    }

    public void SwitchToBackInSpecialThanks()
    {
        backInCredits.SetActive(false);
        backInSpecialThanks.SetActive(true);
        nextButton.SetActive(false);
    }
}
