using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Levels;

namespace Section0.EntertainingCleaningLevels.Level2
{
    public class ItemLevel : MissionsDecorator.ItemLevel
    {
        private void Start()
        {
            BtnBox.onClick.AddListener(ClickBox);
        }
    }
}

