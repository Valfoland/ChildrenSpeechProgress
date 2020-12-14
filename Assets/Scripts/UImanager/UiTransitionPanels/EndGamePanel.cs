using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DataEndGamePanel
{
    public Text TxtOther;
}

public class EndGamePanel : Panel
{
    private DataEndGamePanel dataEndGamePanel;
    public EndGamePanel(DataPanel dataPanel, DataEndGamePanel dataEndGamePanel, PanelTypes panelType) : base(dataPanel, panelType)
    {
        try
        {
            dataPanel.DataPanelBtns[0].BtnPanel.onClick.AddListener(BackToMenu);
        }
        catch (System.NullReferenceException)
        {

        }
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}

