using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataSetSectionsPanel
{
    public GameObject SectionsPanelObject;
    public Button GoToMenuBtn;
    public Button GoToSectionsBtn;
    public Button BackToSectionsBtn;
    public Button[] GoToMissionsBtn;
}

public class SectionsPanel : Panel, IinfOfPanel
{
    private int idSect;
    private DataSetSectionsPanel sectionsPanel;
    public SectionsPanel(DataSetSectionsPanel sectionsPanel) : base(sectionsPanel.SectionsPanelObject)
    {
        try
        {
            this.sectionsPanel = sectionsPanel;
            sectionsPanel.GoToSectionsBtn.onClick.AddListener(ShowPanel);
            sectionsPanel.BackToSectionsBtn.onClick.AddListener(ShowPanel);
            sectionsPanel.GoToMenuBtn.onClick.AddListener(HidePanel);
            
            sectionsPanel.GoToMissionsBtn[0].onClick.AddListener(() => HideSectionPanel(0));
            sectionsPanel.GoToMissionsBtn[1].onClick.AddListener(() => HideSectionPanel(1));
            sectionsPanel.GoToMissionsBtn[2].onClick.AddListener(() => HideSectionPanel(2));
        }
        catch (System.NullReferenceException) { }
    }

    public override void ShowPanel()
    {
        onShowPanel?.Invoke("");
        if (PlayerPrefs.HasKey("countChild"))
        {
            panelObject.SetActive(true);
        }
    }

    private void HideSectionPanel(int idSect)
    {
        this.idSect = idSect;
        SetInfoPanel();
        HidePanel();
        onHidePanel?.Invoke();
    }

    public void SetInfoPanel()
    {
        InfoOfPanel.IdSelectSection = idSect;
    }
}

