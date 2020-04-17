using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public interface IinfOfPanel
{
    void SetInfoPanel();
}

public abstract class Panel
{
    protected GameObject panelObject;
    public abstract void ShowPanel();
    public virtual void HidePanel() => panelObject.SetActive(false);
    public Panel(GameObject panelObject = null) => this.panelObject = panelObject;
    public System.Action onHidePanel;
    public System.Action<string> onShowPanel;
}
