using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DataLosePanel
{
    public string TextLose;
}

public class LosePanel : PanelEndGameDecorator
{
    private DataLosePanel dataLosePanel;
    public LosePanel(Panel panel, DataPanel dataPanel, DataEndGamePanel endGamePanel, DataLosePanel dataLosePanel) : 
        base(panel, dataPanel, endGamePanel)
    {
        try
        {
            this.dataLosePanel = dataLosePanel;
        }
        catch (Exception e)
        {
            // ignored
        }
    }
  
    public override void ShowPanel()
    {
        dataPanel.DataPanelBtns[1].BtnPanel.onClick.AddListener(Restart);
        dataEndGamePanel.TxtOther.text = dataLosePanel.TextLose;
        panel.ShowPanel();
    }

    private void Restart()
    {
        dataPanel.DataPanelBtns[1].BtnPanel.onClick.RemoveListener(Restart);
        SceneManager.LoadScene("Game");
    }
}

