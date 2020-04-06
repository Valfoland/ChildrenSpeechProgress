using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataSetChildPanel
{
    public GameObject ChildPanelObject;
    public Button BtnBackToMenu;
    public Button BtnGoToChildPanel;
}

public class ChildPanel : Panel
{
    private DataSetChildPanel childPanel;

    public ChildPanel(DataSetChildPanel childPanel) : base(childPanel.ChildPanelObject)
    {
        try
        {
            this.childPanel = childPanel;
            childPanel.BtnBackToMenu.onClick.AddListener(HidePanel);
            childPanel.BtnGoToChildPanel.onClick.AddListener(ShowPanel);
        }
        catch (System.NullReferenceException)
        {

        }
    }

    public ChildPanel(DataSetChildPanel childPanel, int i = 0) : base(childPanel.ChildPanelObject)
    {
        ShowPanel();
    }

    protected override void ShowPanel()
    {
        panelObject.SetActive(true);
    }

    protected override void HidePanel()
    {
        base.HidePanel();
    }
}
