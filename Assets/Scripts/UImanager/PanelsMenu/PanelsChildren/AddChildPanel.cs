using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataSetAddChildPanel
{
    public GameObject AddChildPanelObject;
    public Button BtnBackToChildrenPanel;
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
            addChildPanel.BtnBackToChildrenPanel.onClick.AddListener(HidePanel);
            addChildPanel.BtnOpenAddChildPanel.onClick.AddListener(ShowPanel);
            addChildPanel.BtnAddChild.onClick.AddListener(AddChild);
        }
        catch (System.NullReferenceException)
        {

        }
    }

    public override void ShowPanel()
    {
        panelObject.SetActive(true);
    }

    private void AddChild()
    {
        DataSetAddChildPanel.onAddChild?.Invoke(addChildPanel);
        HidePanel();
    }
}
