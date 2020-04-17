using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DataSetLosePanel
{
    public string TextLose;
}

public class LosePanel : PanelEndGameDecorator
{
    private DataSetLosePanel dataSetLosePanel;
    public LosePanel(Panel panel, DataSetEndGamePanel endGamePanel, DataSetLosePanel dataSetLosePanel) : base(panel, endGamePanel)
    {
        try
        {
            this.dataSetLosePanel = dataSetLosePanel;
        }
        catch (Exception e)
        {
            // ignored
        }
    }

    public override void ShowPanel()
    {
        dataSetEndGamePanel.BtnOther.onClick.AddListener(Restart);
        dataSetEndGamePanel.TxtOther.text = dataSetLosePanel.TextLose;
        panel.ShowPanel();
    }

    private void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}

