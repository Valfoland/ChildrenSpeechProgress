using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiTransitionManagerMenu : UiTransitionManager
{
    [SerializeField]
    private UiTransitionManagerMenuData uiTransitionManagerMenuData;

    protected override void Start()
    {
        base.Start();
        Panel.onSetInfoPanel += SetTextInfo;
        DataMenuPanel.onCheckChild += ShowAddChildPanel;
        DataAddChildPanel.onCheckEmptyPanel += CheckEmptyPanel;
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Panel.onSetInfoPanel -= SetTextInfo;
        DataMenuPanel.onCheckChild -= ShowAddChildPanel;
        DataAddChildPanel.onCheckEmptyPanel -= CheckEmptyPanel;
    }
    
    private void CheckEmptyPanel()
    {
        if (uiTransitionManagerMenuData.ChildrenPanel.PanelObject.activeSelf == false)
        {
            uiTransitionManagerMenuData.MenuPanel.ShowPanel();
        }
    }
    
    private void SetTextInfo(string nameActivePanel = "")
    {
        Debug.Log(DataGame.IdSelectSection);
        Debug.Log(DataGame.IdSelectMission);
        iInfoPanel = (ITextPanel) uiTransitionManagerMenuData.InfoPanel;
        string text = "";
        
        if (nameActivePanel == "Missions")
        {
            text = DataGame
                .SectionDataList[DataGame.IdSelectSection]
                .TextSection;
        }
        else if (nameActivePanel == "Levels")
        {
            text = uiTransitionManagerMenuData.DataInfoPanel.InfoText.text = 
                DataGame
                    .SectionDataList[DataGame.IdSelectSection]
                    .MissionDataList[DataGame.IdSelectMission]
                    .TextMission;
        }

        iInfoPanel.SetTextInfo(text);
    }

    private void ShowAddChildPanel()
    {
        uiTransitionManagerMenuData.AddChildPanel.ShowPanel();
    }
}
