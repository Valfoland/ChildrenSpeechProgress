using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataChildrenPanel
{
    //сюда можно вставить доп.дату
}

public class ChildrenPanel : Panel
{
    public ChildrenPanel(DataPanel dataPanel,  DataChildrenPanel dataChildrenPanel, PanelTypes panelType) : base(dataPanel, panelType)
    {
        try
        {
            AddButtonListener();
        }
        catch (System.NullReferenceException) { }
    }
}
