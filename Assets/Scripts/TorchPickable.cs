using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchPickable : Pickable {

    public TorchHolder holder;
    public List<TorchHolder> holdersInRange;

    protected TorchHolder originalHolder;
    protected bool originalIsKinematic;

    public override void Start()
    {
        base.Start();
        originalHolder = holder;
        originalIsKinematic = GetComponent<Rigidbody>().isKinematic;
    }

    public void OnReset()
    {
        holder = originalHolder;
        GetComponent<Rigidbody>().isKinematic = originalIsKinematic;
    }

    public override void Pick(Wand p)
    {
        base.Pick(p);
        if (holder)
        {
            holder.unload(this);
            holder = null;
        }
    }

    public override void Drop(Wand p)
    {
        base.Drop(p);
        foreach (TorchHolder h in holdersInRange)
        {
            if (h && !h.torchOnHolder)
            {
                h.load(this);
                holder = h;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        TorchHolder holder = other.GetComponent<TorchHolder>();
        if (holder && !holdersInRange.Contains(holder))
        {
            holdersInRange.Add(holder);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TorchHolder holder = other.GetComponent<TorchHolder>();
        if (holder && holdersInRange.Contains(holder))
        {
            holdersInRange.Remove(holder);
        }
    }
}
