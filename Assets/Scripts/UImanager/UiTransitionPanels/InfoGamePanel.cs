using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


[System.Serializable]
public class DataInfoGamePanel
{

}

public class InfoGamePanel : Panel
{
    private DataInfoGamePanel dataInfoPanel;

    public InfoGamePanel(DataPanel dataPanel, DataInfoGamePanel dataInfoPanel, PanelTypes panelTypes) : base(dataPanel,
        panelTypes)
    {
        this.dataInfoPanel = dataInfoPanel;
        AddButtonListener();
    }
}


