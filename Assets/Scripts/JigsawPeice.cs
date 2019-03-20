using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum JigsawType {
    triangle,
    square
}

public class JigsawPeice : MonoBehaviour {

    public JigsawType type;
    public float threshold;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        JigsawHolder jigsawHolder = other.GetComponent<JigsawHolder>();
        Pickable myPickable = GetComponent<Pickable>();

        if (myPickable && !myPickable.isPickedUp && jigsawHolder && jigsawHolder.type == type && !jigsawHolder.isPlaced) // Need to be same type
        {
            float dis = Vector3.Distance(transform.position, other.transform.position);
            print(name + " " + jigsawHolder.name + "    " + dis);
            if (dis <= threshold)
            {
                // Lock this peice to the holder
                transform.position = other.transform.position;
                transform.rotation = other.transform.rotation;
                Destroy(GetComponent<Pickable>());
                Destroy(GetComponent<Rigidbody>());
                jigsawHolder.isPlaced = true;
            }
        }
    }

}
