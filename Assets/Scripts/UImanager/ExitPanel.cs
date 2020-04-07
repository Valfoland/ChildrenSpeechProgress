using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DataSetExitPanel
{
    public GameObject ExitPanelObject;
    public Button ExitBtn;
    public Button StayBtn;
    public Button OpenExitBtn;
}

public class ExitPanel : Panel
{
    private DataSetExitPanel exitPanel;

    public ExitPanel(DataSetExitPanel exitPanel) : base(exitPanel.ExitPanelObject)
    {
        try
        {
            this.exitPanel = exitPanel;
            exitPanel.OpenExitBtn.onClick.AddListener(ShowPanel);
            exitPanel.StayBtn.onClick.AddListener(HidePanel);
            exitPanel.ExitBtn.onClick.AddListener(ExitApp);
        }
        catch (System.NullReferenceException) { }
    }

    public override void ShowPanel()
    {
        panelObject.SetActive(true);
    }

    private void ExitApp()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        if (currentScene != 0)
        {
            SceneManager.LoadScene("Menu");
        }
        else
        {
            Application.Quit();
        }
    }
}

