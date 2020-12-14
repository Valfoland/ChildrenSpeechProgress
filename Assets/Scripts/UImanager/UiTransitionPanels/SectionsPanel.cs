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
    public Text OnrName;
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
        if (PlayerPrefs.GetInt("countChild") > 0)
        {
            GetBtnSection();
            SetNameSelectOnr();
            base.ShowPanel();
        }
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
        List<int> idSelectSections = new List<int>();
        int id = 0;
        var countSectionList = DataGame.SectionDataList
            .Where(x =>
            {
                if (x.TypeOnr == DataGame.TypeOnr)
                {
                    idSelectSections.Add(id);
                    id++;
                    return true;
                }
                
                id++;
                return false;
            })
            .Select(x => x.TypeOnr).ToList();
        
        for(int i = 0; i < dataSectionsPanel.GoToMissionsBtn.Length; i++)
        {
            if (i < countSectionList.Count)
            {
                dataSectionsPanel.GoToMissionsBtn[i].gameObject.SetActive(true);
                
                var i1 = i;
                SetTextBtn(i, DataGame.SectionDataList[idSelectSections[i1]].NameSection);
                dataSectionsPanel.GoToMissionsBtn[i].onClick.AddListener(() => GoToMissions(idSelectSections[i1]));
            }
            else
            {
                dataSectionsPanel.GoToMissionsBtn[i].gameObject.SetActive(false);
            }
        }
    }
    
    private void SetNameSelectOnr()
    {
        dataSectionsPanel.OnrName.text = "ОНР " + ((int)DataGame.TypeOnr + 1);
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

