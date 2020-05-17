using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DataPanelBtn
{
    public ItemTypes ItemPanelTypes;
    public Button BtnPanel;
}

[System.Serializable]
public class DataPanel
{
    public string NamePanel;
    public GameObject PanelObject;
    public DataPanelBtn[] DataPanelBtns;
}

public abstract class Panel
{
    public static System.Action<string> onSetInfoPanel;
    public static System.Action<Panel, ItemTypes> onClickBtn;
    public static System.Action<Panel,string > onDirectionTransition;
    
    public GameObject PanelObject;
    public PanelTypes PanelType;
    protected DataPanel dataPanel;
    public virtual void ShowPanel() => PanelObject.SetActive(true);
    public virtual void HidePanel() => PanelObject.SetActive(false);

    protected Panel(DataPanel dataPanel, PanelTypes panelType = PanelTypes.Secondary)
    {
        this.dataPanel = dataPanel;
        this.PanelType = panelType;
        this.PanelObject = dataPanel.PanelObject;
    }
    
    protected void OnClickBtn(Panel panel, ItemTypes itemTypes)
    {
        onClickBtn?.Invoke(panel, itemTypes);
    }

    protected void OnDirectionTransition(Panel fromPanel, string toPanel)
    {
        onDirectionTransition?.Invoke(fromPanel, toPanel);
    }
}
