using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataSetNextLvlPanel
{
    public GameObject NextLvlPanelObject;
}

public class NextLvlPanel : Panel
{
    private DataSetNextLvlPanel nextLvlPanel;

    public NextLvlPanel(DataSetNextLvlPanel nextLvlPanel) : base(nextLvlPanel.NextLvlPanelObject)
    {
        try
        {
            this.nextLvlPanel = nextLvlPanel;
        }
        catch (System.NullReferenceException)
        {

        }
    }

    public override void ShowPanel()
    {
        panelObject.SetActive(true);
    }

    protected override void HidePanel()
    {
        base.HidePanel();
    }
}

