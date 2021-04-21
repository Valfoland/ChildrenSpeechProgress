using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataSectionsPanel
{
    public Button[] GoToMissionsBtn;
    public Text[] GoToMissionTxt;
    public Text SectionName;
}

public class SectionsPanel : Panel
{
    private DataSectionsPanel dataSectionsPanel;
    public SectionsPanel(DataPanel dataPanel,  DataSectionsPanel dataSectionsPanel, PanelTypes panelType) : base(dataPanel, panelType)
    {
        try
        {
            AddButtonListener();
            this.dataSectionsPanel = dataSectionsPanel;
            
        }
        catch (System.NullReferenceException) { }
    }

    public override void ShowPanel()
    {
        onSetInfoPanel?.Invoke("");
        GetBtnSection();
        SetNamePanelSections();
        base.ShowPanel();
    }

    public override void HidePanel()
    {
        foreach (var btn in dataSectionsPanel.GoToMissionsBtn)
        {
            btn.onClick.RemoveAllListeners();
        }
        base.HidePanel();
    }

    private void GetBtnSection()
    {
        for (int i = 0; i < dataSectionsPanel.GoToMissionsBtn.Length; i++)
        {
            dataSectionsPanel.GoToMissionsBtn[i].gameObject.SetActive(true);

            var i1 = i;
            SetTextBtn(i, DataGame.SectionDataList[i1].NameSection);
            dataSectionsPanel.GoToMissionsBtn[i].onClick.AddListener(() => GoToMissions(i1));
        }
    }
    
    private void SetNamePanelSections()
    {
        dataSectionsPanel.SectionName.text = "Секции";
    }
    
    private void SetTextBtn(int i, string textButton)
    {
        dataSectionsPanel.GoToMissionTxt[i].text = textButton;
    }

    private void GoToMissions(int idSect)
    {
        DataGame.IdSelectSection = idSect;
        OnDirectionTransition(this, "MissionsPanel");
    }
}

