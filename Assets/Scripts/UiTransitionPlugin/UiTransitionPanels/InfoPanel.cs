using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class TextInfoIdLevel
{
    public string TextLevel;
}

[System.Serializable]
public class TextInfoIdMissions
{
    [SerializeField] private string nameMission;
    public string TextMission;
    public List<TextInfoIdLevel> TextLevelsList;
}

[System.Serializable]
public class TextInfoIdSections
{
    [SerializeField] private string nameSection;
    public string TextSection;
    public List<TextInfoIdMissions> TextMissionsList;
}

[System.Serializable]
public class DataInfoPanel
{
    public Text InfoText;
    public List<TextInfoIdSections> TextIdSectionsList;
}

public interface ITextPanel
{
    void SetTextInfo(string nameActivePanel);
}

public class InfoPanel : Panel, ITextPanel
{
    private DataInfoPanel dataInfoPanel;

    public InfoPanel(DataPanel dataPanel, DataInfoPanel dataInfoPanel, PanelTypes panelType) : base(dataPanel, panelType)
    {
        try
        {
            this.dataInfoPanel = dataInfoPanel;
            SetTextInfo();
            
            dataPanel.DataPanelBtns[0].BtnPanel.onClick.AddListener(HidePanel);
            dataPanel.DataPanelBtns[1].BtnPanel.onClick.AddListener(ShowPanel);
        }
        catch (System.NullReferenceException) { }
    }

    public void SetTextInfo(string textInfo = "")
    {
        dataPanel.DataPanelBtns[1].BtnPanel.gameObject.SetActive(true);
        if (textInfo == "")
        {
            dataPanel.DataPanelBtns[1].BtnPanel.gameObject.SetActive(false); 
        }
        dataInfoPanel.InfoText.text = textInfo;
    }
}

