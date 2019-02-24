﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PauseMenu : MonoBehaviour {

    [SerializeField] private GameObject PauseMenuUI;

    [SerializeField] private bool isPaused;

    private FirstPersonController fpc;

    // Use this for initialization
    void Start () {
        fpc = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
    }
	
	// Update is called once per frame
	private void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            ActivateMenu();
        }

        else
        {
            DeactivateMenu();
        }
	}

    void ActivateMenu()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        PauseMenuUI.SetActive(true);

        fpc.GetMouseLook().SetCursorLock(false);
        fpc.enabled = false;
    }

    public void DeactivateMenu()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        PauseMenuUI.SetActive(false);
        isPaused = false;

        fpc.enabled = true;
        fpc.GetMouseLook().SetCursorLock(true);

    }
}