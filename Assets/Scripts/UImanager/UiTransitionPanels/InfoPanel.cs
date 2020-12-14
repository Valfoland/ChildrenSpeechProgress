using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DataInfoPanel
{
    public Text InfoText;
}

public interface ITextPanel
{
    void SetTextInfo(string nameActivePanel);
}

public class InfoPanel : Panel, ITextPanel
{
    public DataInfoPanel DataInfoPanel;

    public InfoPanel(DataPanel dataPanel, DataInfoPanel dataInfoPanel, PanelTypes panelType) : base(dataPanel, panelType)
    {
        try
        {
            this.DataInfoPanel = dataInfoPanel;
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
        DataInfoPanel.InfoText.text = textInfo;
    }
}

