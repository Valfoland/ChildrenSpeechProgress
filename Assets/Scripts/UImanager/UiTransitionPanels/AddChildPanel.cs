using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataAddChildPanel
{
    public Button BtnAddChild;
    public Button BtnCancel;
    public InputField NameField;
    public InputField AgeField;
    public InputField GroupField;
    public static System.Action<DataAddChildPanel> onAddChild;
    public static System.Action onCheckEmptyPanel;
}

public class AddChildPanel : Panel 
{
    private DataAddChildPanel dataAddChildPanel;

    public AddChildPanel(DataPanel dataPanel, DataAddChildPanel dataAddChildPanel, PanelTypes panelType) : base(dataPanel, panelType) 
    {
        try
        {
            this.dataAddChildPanel = dataAddChildPanel;

            AddButtonListener();
            
            dataAddChildPanel.BtnCancel.onClick.AddListener(() => AddChild(false));
            dataAddChildPanel.BtnAddChild.onClick.AddListener(() => AddChild(true));
        }
        catch (System.NullReferenceException)
        {

        }
    }

    private void AddChild(bool isAdd)
    {
        if(isAdd)
            DataAddChildPanel.onAddChild?.Invoke(dataAddChildPanel);
        HidePanel();
        DataAddChildPanel.onCheckEmptyPanel?.Invoke();
    }
}
