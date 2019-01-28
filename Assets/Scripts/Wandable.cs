using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Wandable : MonoBehaviour {

    public float maxHeat;
    public float minHeat;
    public float containingHeat;

    private Renderer _renderer;
    private float _animingTime;

	// Use this for initialization
	void Start () {
        _renderer = GetComponent<Renderer>();
        _animingTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnAniming ()
    {
        _animingTime += Time.deltaTime;
        Color targetColor = Color.Lerp(Color.cyan, Color.yellow, (containingHeat-minHeat) / (maxHeat - minHeat));
        _renderer.material.color = Color.Lerp(Color.white, targetColor, Mathf.Sin(2 * Mathf.PI * _animingTime));
    }

    public void OffAniming ()
    {
        _renderer.material.color = Color.white;
        _animingTime = 0;
    }
}
