using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInDoor : Triggee {

    public bool isOpen;
    public Ignitable giantBook;

    protected Animator animator;
    protected bool originalIsOpen;

    public override void OnReset()
    {
        base.OnReset();
        Deactivate();
        isOpen = originalIsOpen;
    }

	// Use this for initialization
	public override void Start () {
        base.Start();
        originalIsOpen = isOpen;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    public override void Update () {
        base.Update();
        if (giantBook)
        {
            isOpen = !giantBook.invisibleData.isVisible;
        }
        animator.SetBool("IsOpen", isOpen);
    }

    public override void Activate()
    {
        base.Activate();
        isOpen = true;
        animator.SetBool("IsOpen", isOpen);
    }

    public override void Deactivate()
    {
        base.Deactivate();
        isOpen = false;
        animator.SetBool("IsOpen", isOpen);
    }
}
