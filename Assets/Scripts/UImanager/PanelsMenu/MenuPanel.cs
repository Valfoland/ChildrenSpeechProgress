using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DataSetMenuPanel
{
    public GameObject MenuPanelObject;

    public List<Button> ShowBtns;
    public List<Button> HideBtns;
}

public class MenuPanel : Panel
{
    public MenuPanel(DataSetMenuPanel menuPanel) : base(menuPanel.MenuPanelObject)
    {
        try
        {
            menuPanel.ShowBtns.ForEach(x => x.onClick.AddListener(ShowPanel));
            menuPanel.HideBtns.ForEach(x => x.onClick.AddListener(HidePanel));
        }
        catch (System.NullReferenceException)
        {
        }
    }

    public override void ShowPanel()
    {
        panelObject.SetActive(true);
    }

    public override void HidePanel()
    {
        onHidePanel?.Invoke();
        if (PlayerPrefs.HasKey("countChild"))
        {
            base.HidePanel();
        }
    }
}

