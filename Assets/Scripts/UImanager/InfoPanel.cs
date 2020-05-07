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
public class DataSetInfoPanel
{
    public Button BtnGoToInfo;
    public Button BtnCloseInfo;
    public Text InfoText;
    public GameObject InfoPanelObject;
    public List<TextInfoIdSections> TextIdSectionsList;
}

public interface ITextPanel
{
    void SetTextInfo(string nameActivePanel);
}

public class InfoPanel : Panel, ITextPanel
{
    private DataSetInfoPanel infoPanel;

    public InfoPanel(DataSetInfoPanel infoPanel) : base(infoPanel.InfoPanelObject)
    {
        try
        {
            this.infoPanel = infoPanel;
            SetTextInfo();
            infoPanel.BtnGoToInfo.onClick.AddListener(ShowPanel);
            infoPanel.BtnCloseInfo.onClick.AddListener(HidePanel);
        }
        catch (System.NullReferenceException) { }
    }

    public override void ShowPanel()
    {
        panelObject.SetActive(true);
    }

    public void SetTextInfo(string nameActivePanel = "")
    {
        infoPanel.BtnGoToInfo.gameObject.SetActive(true);
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            switch (nameActivePanel)
            {
                case "Missions":
                    infoPanel.InfoText.text = infoPanel
                        .TextIdSectionsList[DataGame.IdSelectSection].TextSection;
                    break;
                case "Levels":
                    infoPanel.InfoText.text = infoPanel
                        .TextIdSectionsList[DataGame.IdSelectSection]
                        .TextMissionsList[DataGame.IdSelectMission].TextMission;
                    break;
                default:
                    infoPanel.InfoText.text = "";
                    break;
            }
        }
        else if(SceneManager.GetActiveScene().name == "Game")
        {
            infoPanel.InfoText.text = infoPanel
                .TextIdSectionsList[DataGame.IdSelectSection]
                .TextMissionsList[DataGame.IdSelectMission]
                .TextLevelsList[DataGame.IdSelectLvl].TextLevel;
        }

        if (infoPanel.InfoText.text == "")
        {
            infoPanel.BtnGoToInfo.gameObject.SetActive(false);
        }
    }
}

