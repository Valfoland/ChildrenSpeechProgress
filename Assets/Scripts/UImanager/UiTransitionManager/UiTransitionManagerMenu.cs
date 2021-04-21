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
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        Panel.onSetInfoPanel -= SetTextInfo;
    }

    private void SetTextInfo(string textInfo = "")
    {
        iInfoPanel = (ITextPanel) uiTransitionManagerMenuData.InfoPanel;
        iInfoPanel.SetTextInfo(textInfo);
    }
}
