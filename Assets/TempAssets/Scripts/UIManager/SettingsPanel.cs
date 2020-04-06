using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataSetSettingsPanel
{
    public GameObject SettingsPanelObject;
    public Button BtnBackToMenu;
    public Button BtnGoToSettings;
}

public class SettingsPanel : Panel
{
    private DataSetSettingsPanel settingsPanel;

    public SettingsPanel(DataSetSettingsPanel settingsPanel) : base(settingsPanel.SettingsPanelObject)
    {
        try
        {
            this.settingsPanel = settingsPanel;
        
            settingsPanel.BtnBackToMenu.onClick.AddListener(HidePanel);
            settingsPanel.BtnGoToSettings.onClick.AddListener(ShowPanel);
        }
        catch (System.NullReferenceException)
        {

        }
    }

    protected override void ShowPanel()
    {
        panelObject.SetActive(true);
    }

    protected override void HidePanel()
    {
        base.HidePanel();
    }
}

