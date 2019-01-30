using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Wandable : MonoBehaviour {

    public float maxHeat;
    public float minHeat;
    public float containingHeat;
    public float volume;

    protected Renderer _renderer;
    protected float _animingTime;

	// Use this for initialization
	void Start () {
        _renderer = GetComponent<Renderer>();
        _animingTime = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnAiming ()
    {
        _animingTime += Time.deltaTime;
        Color targetColor = Color.Lerp(Color.cyan, Color.yellow, (containingHeat-minHeat) / (maxHeat - minHeat));
        _renderer.material.color = Color.Lerp(Color.white, targetColor, Mathf.Sin(2 * Mathf.PI * _animingTime));
    }

    public void OffAiming ()
    {
        _renderer.material.color = Color.white;
        _animingTime = 0;
    }

    /// <summary>
    /// Attemp to decrease heat in this object
    /// </summary>
    /// <param name="amount">amount to decrease</param>
    /// <param name="wandRemaining">amount remaining space in wand to be empty</param>
    /// <returns>The actual decreasing amount </returns>
    public float heatLose(float amount, float wandRemaining)
    {
        float temp = Mathf.Max(minHeat, containingHeat - amount);
        float diff = Mathf.Min(containingHeat - temp, wandRemaining);
        containingHeat -= diff;
        return diff;
    }

    /// <summary>
    /// Attemp to increase heat in this object
    /// </summary>
    /// <param name="amount">amount to increase</param>
    /// <param name="wandRemaining">amount remaining space in wand to be full</param>
    /// <returns>The actual increasing amount</returns>
    public float heatGain(float amount, float wandRemaining)
    {
        float temp = Mathf.Min(maxHeat, containingHeat + amount);
        float diff = Mathf.Min(temp - containingHeat, wandRemaining);
        containingHeat += diff;
        return diff;
    }
}
