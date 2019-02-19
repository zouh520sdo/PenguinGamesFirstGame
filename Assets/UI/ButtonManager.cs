using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    public GameObject SettingPanel;

	public void NewGameBtn(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);
    }

    public void ExitGameBtn()
    {
        Application.Quit();
    }

    public void SettingBtn()
    {
        SettingPanel.SetActive(true);
    }

    public void BackBtn()
    {
        SettingPanel.SetActive(false);
    }
}
