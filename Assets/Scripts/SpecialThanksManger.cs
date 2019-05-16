using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialThanksManger : MonoBehaviour {

    //           -3.8
    //16.7 1.23 -52.7
    public List<Vector3> cameraPoses;
    public float movingRatio;
    public Camera myCamera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
            StartCoroutine(SlidingToPos(cameraPoses[index]));
        }
    }
}
