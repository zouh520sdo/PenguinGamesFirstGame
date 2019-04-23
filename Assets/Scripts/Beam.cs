using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour {

    private LineRenderer lRend;
    public Vector3[] points = new Vector3[5];

    private int point_Begin = 0;
    private int point_Middle_Left_2 = 1;
    private int point_Center = 2;
    private int point_Middle_Right_2 = 3;
    private int point_End = 4;

    public GameObject line;
    private float randomPosOffset = 0.01f;
    private float randomWithOffsetMax = 2f;
    private float randomWithOffsetMin = 1f;

    private WaitForSeconds customFrame = new WaitForSeconds(0.1f);

	// Use this for initialization
	void Start () {
        lRend = GetComponent<LineRenderer>();
        //StartCoroutine(BeamStart());
	}
	
    private IEnumerator BeamStart()
    {
        yield return customFrame;
        points[point_Begin] = transform.parent.gameObject.transform.position;
        points[point_End] = line.transform.position;
        CalculateMiddle();
        lRend.SetPositions(points);
        lRend.SetWidth(RandomWidthOffset(), RandomWidthOffset());
        StartCoroutine(BeamStart());
    }

    private float RandomWidthOffset ()
    {
        return Random.Range(randomWithOffsetMin, randomWithOffsetMax);
    }

    private void CalculateMiddle ()
    {
        Vector3 center = GetMiddleWithRandomness(transform.parent.gameObject.transform.position, line.transform.position);

        points[point_Center] = center;
        points[point_Middle_Left_2] = GetMiddleWithRandomness(transform.parent.gameObject.transform.position, center);
        points[point_Middle_Right_2] = GetMiddleWithRandomness(center, line.transform.position);
        /*
        points[point_Middle_Left_1] = GetMiddleWithRandomness(transform.parent.gameObject.transform.position, points[point_Middle_Left_2]);
        points[point_Middle_Left_3] = GetMiddleWithRandomness(points[point_Middle_Left_2], center);
        points[point_Middle_Right_1] = GetMiddleWithRandomness(center, points[point_Middle_Right_2]);
        points[point_Middle_Right_3] = GetMiddleWithRandomness(points[point_Middle_Right_2], line.transform.position);
    */
    }

    private Vector3 GetMiddleWithRandomness (Vector3 point1, Vector3 point2)
    {
        float x = (point1.x + point2.x) / 2;
        float finalX = Random.Range(x - randomPosOffset, x + randomPosOffset);
        float y = (point1.y + point2.y) / 2;
        float finalY = Random.Range(y - randomPosOffset, y + randomPosOffset);
        float z = (point1.z + point2.z) / 2;
        float finalZ = Random.Range(z - randomPosOffset, z + randomPosOffset);
        return new Vector3(finalX, finalY, finalZ);
    }


    void Update()
    {
        points[point_Begin] = transform.parent.gameObject.transform.position;
        points[point_End] = line.transform.position;
        CalculateMiddle();
        lRend.SetPositions(points);
        lRend.SetWidth(RandomWidthOffset(), RandomWidthOffset());
        // StartCoroutine(BeamStart());
    }
}
