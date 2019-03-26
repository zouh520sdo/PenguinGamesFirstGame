using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour {

    public string note;
    public List<string> paragraph;
    protected int paragraphIndex;

	// Use this for initialization
	void Start () {
        paragraphIndex = -1;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public string nextLine()
    {
        if (paragraphIndex >= paragraph.Count-1)
        {
            paragraphIndex = -1;
            return "";
        }
        else
        {
            paragraphIndex++;
            return paragraph[paragraphIndex];
        }
    }
}
