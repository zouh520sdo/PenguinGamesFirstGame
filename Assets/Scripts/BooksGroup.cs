using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooksGroup : MonoBehaviour {

    ObjectIdentifier[] children;

    void Start()
    {
        children = gameObject.GetComponentsInChildren<ObjectIdentifier>();
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
