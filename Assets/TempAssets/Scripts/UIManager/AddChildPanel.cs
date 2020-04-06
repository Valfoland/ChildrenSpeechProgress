using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataSetAddChildPanel
{
    public GameObject AddChildPanelObject;
    public Button BtnBackToMenu;
    public Button BtnOpenAddChildPanel;
    public Button BtnAddChild;
    public InputField NameField;
    public InputField AgeField;
    public InputField GroupField;
    public static System.Action<DataSetAddChildPanel> onAddChild;
}

public class AddChildPanel : Panel 
{
    private DataSetAddChildPanel addChildPanel;

    public AddChildPanel(DataSetAddChildPanel addChildPanel) : base(addChildPanel.AddChildPanelObject) 
    {
        
        try
        {
            this.addChildPanel = addChildPanel;
            addChildPanel.BtnBackToMenu.onClick.AddListener(HidePanel);
            addChildPanel.BtnOpenAddChildPanel.onClick.AddListener(ShowPanel);
            addChildPanel.BtnAddChild.onClick.AddListener(AddChild);
        }
        catch (System.NullReferenceException)
        {

        }
    }

    protected override void ShowPanel()
    {
        panelObject.SetActive(true);
    }

    protected override void HidePanel()
    {
        base.HidePanel();
    }

    private void AddChild()
    {
        DataSetAddChildPanel.onAddChild?.Invoke(addChildPanel);
        HidePanel();
    }
}
