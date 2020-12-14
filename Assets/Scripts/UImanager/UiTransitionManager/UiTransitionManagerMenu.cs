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
    
    private void SetTextInfo(string textInfo = "")
    {
        iInfoPanel = (ITextPanel) uiTransitionManagerMenuData.InfoPanel;
        iInfoPanel.SetTextInfo(textInfo);
    }

    private void ShowAddChildPanel()
    {
        uiTransitionManagerMenuData.AddChildPanel.ShowPanel();
    }
}
