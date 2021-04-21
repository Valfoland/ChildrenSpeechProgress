using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataMenuPanel
{
    public Button GoToSectionsButton;
}

public class MenuPanel : Panel
{
    private DataMenuPanel dataMenuPanel;
    
    public MenuPanel(DataPanel dataPanel, DataMenuPanel dataMenuPanel, PanelTypes panelTypes) : base(dataPanel, panelTypes)
    {
        this.dataMenuPanel = dataMenuPanel;
        AddButtonListener();
        GetBtnSection();
    }

    private void GetBtnSection()
    {
        dataMenuPanel.GoToSectionsButton.onClick.AddListener(GoToSections);
    }

    private void GoToSections()
    {
        OnDirectionTransition(this, "SectionsPanel");
    }
}

