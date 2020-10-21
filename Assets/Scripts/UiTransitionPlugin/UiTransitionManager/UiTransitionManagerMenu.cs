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
        iInfoPanel = (ITextPanel) uiTransitionManagerMenuData.InfoPanel;
        string text = "";
        
        if (nameActivePanel == "Missions")
        {
            text = uiTransitionManagerMenuData.DataInfoPanel.TextIdSectionsList[DataGame.IdSelectSection].TextSection;
        }
        else if (nameActivePanel == "Levels")
        {
            text = uiTransitionManagerMenuData.DataInfoPanel.InfoText.text = uiTransitionManagerMenuData.DataInfoPanel
                .TextIdSectionsList[DataGame.IdSelectSection]
                .TextMissionsList[DataGame.IdSelectMission].TextMission;
        }

        iInfoPanel.SetTextInfo(text);
    }

    private void ShowAddChildPanel()
    {
        uiTransitionManagerMenuData.AddChildPanel.ShowPanel();
    }
}
