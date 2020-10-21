using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataStatsPanel
{
    //здесь будут доп. данные
}

public class StatsPanel : Panel
{
    public static System.Action onSetStats;

    public StatsPanel(DataPanel dataPanel, DataStatsPanel statsPanel, PanelTypes panelType) : base(dataPanel, panelType)
    {
        AddButtonListener();
    }

    public override void ShowPanel()
    {
        if (PlayerPrefs.GetInt("countChild") > 0)
        {
            base.ShowPanel();
        }
        onSetStats?.Invoke();
    }
}

