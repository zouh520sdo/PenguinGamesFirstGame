using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class BouncingMushroom : MonoBehaviour {

    public float bonusSpeed;
    private AudioSource _audioSource;
    // Use this for initialization
    void Start () {
        _audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            print("Trigger " + other.name);
            _audioSource.Play();
            FirstPersonController fpc = other.GetComponent<FirstPersonController>();
            fpc.ImpulseJump(bonusSpeed);
        }
    }
}
