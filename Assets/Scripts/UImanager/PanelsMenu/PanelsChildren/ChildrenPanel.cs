using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataSetChildrenPanel
{
    public GameObject ChildrenPanelObject;
    public Button BtnBackToMenu;
    public Button BtnGoToChildrenPanel;
}

public class ChildrenPanel : Panel
{
    private DataSetChildrenPanel childrenPanel;

    public ChildrenPanel(DataSetChildrenPanel childrenPanel) : base(childrenPanel.ChildrenPanelObject)
    {
        try
        {
            this.childrenPanel = childrenPanel;
            childrenPanel.BtnBackToMenu.onClick.AddListener(HidePanel);
            childrenPanel.BtnGoToChildrenPanel.onClick.AddListener(ShowPanel);
        }
        catch (System.NullReferenceException) { }
    }
    
    public override void ShowPanel()
    {
        panelObject.SetActive(true);
    }
}
