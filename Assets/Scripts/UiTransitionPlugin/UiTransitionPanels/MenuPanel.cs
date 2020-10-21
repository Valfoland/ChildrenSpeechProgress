using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataMenuPanel
{
    public static System.Action onCheckChild;
}

public class MenuPanel : Panel
{
    public MenuPanel(DataPanel dataPanel, DataMenuPanel menuPanel, PanelTypes panelTypes) : base(dataPanel, panelTypes)
    {
        AddButtonListener();
    }

    public override void HidePanel()
    {
        if (PlayerPrefs.GetInt("countChild") <= 0)
        {
            DataMenuPanel.onCheckChild?.Invoke();
        }
        base.HidePanel();
    }
}

