using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInDoor : MonoBehaviour {

    public bool isOpen;
    public Ignitable giantBook;

    protected Animator animator;
    protected bool originalIsOpen;

    public void OnReset()
    {
        isOpen = originalIsOpen;
    }

	// Use this for initialization
	void Start () {
        originalIsOpen = isOpen;
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (giantBook)
        {
            isOpen = !giantBook.invisibleData.isVisible;
        }
        animator.SetBool("IsOpen", isOpen);
    }
}
