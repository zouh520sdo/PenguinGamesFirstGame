using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooksGroup : MonoBehaviour {

    ObjectIdentifier[] children;

    public AudioSource OnFire;

    void Start()
    {
        children = gameObject.GetComponentsInChildren<ObjectIdentifier>();
        OnFire = GetComponent<AudioSource>();
    }

    void Update()
    {
        foreach (ObjectIdentifier child in children)
        {
            Ignitable book = child.GetComponent<Ignitable>();
            if (book && book.isOnFire)
            {
                if(!OnFire.isPlaying)
                {
                    //Debug.Log("play");
                    OnFire.Play();
                }
            }
            else //if(book && !book.isOnFire)
            {
                OnFire.Stop();
            }
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
