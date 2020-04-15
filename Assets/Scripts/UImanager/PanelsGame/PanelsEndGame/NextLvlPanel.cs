using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextLvlPanel : PanelEndGameDecorator
{
    
    public NextLvlPanel(Panel panel, DataSetEndGamePanel endGamePanel) : base(panel, endGamePanel)
    {
        try
        {
            
        }
        catch (System.NullReferenceException)
        {

        }
    }

    public override void ShowPanel()
    {
        panel.ShowPanel();
    }

    public override void HidePanel()
    {
        panel.HidePanel();
    }
}

