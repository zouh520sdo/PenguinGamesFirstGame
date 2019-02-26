using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooksGroup : MonoBehaviour {

    ObjectIdentifier[] children;

    public AudioSource OnFire;

    public bool hasBookOnFire;

    void Start()
    {
        children = gameObject.GetComponentsInChildren<ObjectIdentifier>();
        OnFire = GetComponent<AudioSource>();
        hasBookOnFire = false;
    }

    void Update()
    {
        foreach (ObjectIdentifier child in children)
        {
            Ignitable book = child.GetComponent<Ignitable>();
            if (book && book.isOnFire)
            {
                hasBookOnFire = true;
                break;
               
            }
            else //if(book && !book.isOnFire)
            {
                hasBookOnFire = false;
            }
        }

        if (hasBookOnFire)
        {
            if (!OnFire.isPlaying)
            {
                //Debug.Log("play");
                OnFire.Play();
            }
        }
        else
        {
            OnFire.Stop();
        }
    }

    public void OnReset()
    {
        foreach (ObjectIdentifier child in children)
        {
            Ignitable book = child.GetComponent<Ignitable>();
            if (book)
            {
                book.OnReset();
            }
        }
    }
}
