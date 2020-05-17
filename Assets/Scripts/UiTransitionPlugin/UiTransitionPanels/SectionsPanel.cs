using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataSectionsPanel
{
    public Button[] GoToMissionsBtn;
}

public class SectionsPanel : Panel
{
    private int idSect;
    private DataSectionsPanel dataSectionsPanel;
    public SectionsPanel(DataPanel dataPanel,  DataSectionsPanel dataSectionsPanel, PanelTypes panelType) : base(dataPanel, panelType)
    {
        try
        {
            this.dataSectionsPanel = dataSectionsPanel;
            GetBtnSection();
            foreach (var data in dataPanel.DataPanelBtns)
            {
                data.BtnPanel.onClick.AddListener(() => OnClickBtn(this, data.ItemPanelTypes));
            }
        }
        catch (System.NullReferenceException) { }
    }

    public override void ShowPanel()
    {
        onSetInfoPanel?.Invoke("");
        if (PlayerPrefs.GetInt("countChild") > 0)
        {
            base.ShowPanel();
        }
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }

    private void GetBtnSection()
    {
        for (int i = 0; i < dataSectionsPanel.GoToMissionsBtn.Length; i++)
        {
            var i1 = i;
            dataSectionsPanel.GoToMissionsBtn[i].onClick.AddListener(() => GoToMissions(i1));
        }
    }

    private void SetInfoPanel()
    {
        DataGame.IdSelectSection = idSect;
    }

    private void GoToMissions(int idSect)
    {
        this.idSect = idSect;
        SetInfoPanel();
        OnDirectionTransition(this, "MissionsPanel");
    }
}

