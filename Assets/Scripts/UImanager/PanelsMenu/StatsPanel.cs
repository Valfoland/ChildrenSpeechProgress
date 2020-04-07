using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataSetStatsPanel
{
    public GameObject StatsPanelObject;
    public Button GoToStatsPanelBtn;
    public Button GoToMenuBtn;
}

public class StatsPanel : Panel
{
    private DataSetStatsPanel statsPanel;

    public StatsPanel(DataSetStatsPanel statsPanel) : base(statsPanel.StatsPanelObject)
    {
        try
        {
            this.statsPanel = statsPanel;
            statsPanel.GoToStatsPanelBtn.onClick.AddListener(ShowPanel);
            statsPanel.GoToMenuBtn.onClick.AddListener(HidePanel);
        }
        catch (System.NullReferenceException) { }
    }

    public override void ShowPanel()
    {
        if (PlayerPrefs.HasKey("countChild"))
        {
            panelObject.SetActive(true);
        }
    }
}

