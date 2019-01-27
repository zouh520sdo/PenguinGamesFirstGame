using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Wandable : MonoBehaviour {

    private Renderer _renderer;

	// Use this for initialization
	void Start () {
        _renderer = GetComponent<Renderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnAniming ()
    {
        _renderer.material.color = Color.yellow;
    }

    public void OffAniming ()
    {
        _renderer.material.color = Color.white;
    }
}
