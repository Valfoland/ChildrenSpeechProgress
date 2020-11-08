using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataMenuPanel
{
    public static System.Action onCheckChild;
    public Button[] GoToSectionsButton;
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

    public override void HidePanel()
    {
        if (PlayerPrefs.GetInt("countChild") <= 0)
        {
            DataMenuPanel.onCheckChild?.Invoke();
        }
        base.HidePanel();
    }

    private void GetBtnSection()
    {
        for (int i = 0; i < dataMenuPanel.GoToSectionsButton.Length; i++)
        {
            var i1 = i;
            dataMenuPanel.GoToSectionsButton[i].onClick.AddListener(() => GoToSections(i1));
        }
    }

    private void GoToSections(int idSect)
    {
        DataGame.TypeOnr = (TypeOnr) idSect;
        OnDirectionTransition(this, "SectionsPanel");
    }
}

