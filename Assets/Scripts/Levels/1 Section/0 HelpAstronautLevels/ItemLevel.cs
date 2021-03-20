using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Levels;

namespace Section1.HelpAstronautLevels.Level0
{
    public class ItemLevel : MissionsDecorator.ItemLevel
    {
        private void Start()
        {
            BtnBox.onClick.AddListener(ClickBox);
        }
    }
}

