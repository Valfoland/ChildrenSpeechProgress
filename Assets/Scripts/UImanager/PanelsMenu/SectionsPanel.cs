using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

            for (int i = 0; i < sectionsPanel.GoToMissionsBtn.Length; i++)
            {
                var i1 = i;
                sectionsPanel.GoToMissionsBtn[i].onClick.AddListener(() => HideSectionPanel(i1));
            }
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
        DataGame.IdSelectSection = idSect;
    }
}

