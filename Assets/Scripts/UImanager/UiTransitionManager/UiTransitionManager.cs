using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public enum PanelTypes
{
    Main,
    Secondary
}

public enum ItemTypes
{
    Inside,
    Outside
}

public enum TransitionTypes
{
    Hard,
    Normal,
    Soft,
    None
}

[RequireComponent(typeof(UiTransitionManagerData))]
public class UiTransitionManager : MonoBehaviour
{
    private Panel[] uiTransitionList = new Panel[2];
    private int counterTransition;
    protected ITextPanel iInfoPanel;
    
    protected virtual void Start()
    {
        Panel.onClickBtn += OnClickBtnTransition;
        Panel.onDirectionTransition += OnClickDirectionBtnTransition;
    }

    protected virtual void OnDestroy()
    {
        Panel.onClickBtn -= OnClickBtnTransition;
        Panel.onDirectionTransition -= OnClickDirectionBtnTransition;
    }

    private void OnClickBtnTransition(Panel panel, ItemTypes itemType)
    {
        counterTransition++;
        uiTransitionList[(int) itemType] = panel;

        if (!UiTransitionManagerData.UiTransitionDict.ContainsKey(panel)) return;

        if (counterTransition >= 2 && uiTransitionList[0] != uiTransitionList[1])
        {
            MakeTransition(uiTransitionList[0], uiTransitionList[1]);
            uiTransitionList.ToList().Clear();
            counterTransition = 0;
        }
    }

    private void OnClickDirectionBtnTransition(Panel panelFrom, string toPanel)
    {
        foreach (var panel in UiTransitionManagerData.UiTransitionDict.Keys)
        {
            if (panel.GetType().Name == toPanel)
            {
                MakeTransition(panelFrom, panel);
            }
        }
    }

    private void MakeTransition(Panel panelFrom, Panel panelTo)
    {
        switch (UiTransitionManagerData.UiTransitionDict[panelFrom][panelTo])
        {
            case TransitionTypes.Hard:
                HardClosePanels(panelFrom);
                break;
            case TransitionTypes.Normal:
                panelFrom.HidePanel();
                break;
            case TransitionTypes.Soft:
                break;
        }

        panelTo.ShowPanel();
    }

    private void HardClosePanels(Panel panel)
    {
        panel.HidePanel();
        if (panel.PanelType == PanelTypes.Main)
        {
            UiTransitionManagerData.RelationPanelDict[panel].ForEach(x => x.HidePanel());
        }
        else if(panel.PanelType == PanelTypes.Secondary)
        {
            foreach (var p in UiTransitionManagerData.RelationPanelDict)
            {
                if (p.Value.Contains(panel))
                { 
                    p.Key.HidePanel();
                    foreach (var p1 in p.Value)
                    {
                        p1.HidePanel();
                    }

                    break;
                }
            }
        }
    }
}
