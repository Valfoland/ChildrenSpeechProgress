using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataSettingsPanel
{
    //Здесь будут доп.данные
}

public class SettingsPanel : Panel
{
    public SettingsPanel(DataPanel dataPanel,  DataSettingsPanel dataSettingsPanel, PanelTypes panelType) : base(dataPanel, panelType)
    {
        AddButtonListener();
    }
}

