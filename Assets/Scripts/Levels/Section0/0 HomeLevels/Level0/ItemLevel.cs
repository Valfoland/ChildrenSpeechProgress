﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace  Section0.HomeLevels.Level0
{
    public class ItemLevel : MonoBehaviour
    {
        public Button BtnBox;
        [SerializeField] private Animation animFalse;
        [SerializeField] private Image imageBox;
        [SerializeField] private Text txt; //TEMP
        private char letterBox;
        public static System.Action<char, ItemLevel, string> onClickBox;    
        
        private void Start()
        {
            BtnBox.onClick.AddListener(ClickBox);
        }

        private void OnDestroy()
        {
            
        }

        public void AnimBox()
        {
            animFalse.Play();
        }
        
        public void SetDataBox(Sprite spriteBox, char letterBox)
        {
            if (letterBox.ToString().ToLower() != "б" &&
                letterBox.ToString().ToLower() != "п")
            {
                txt.text = spriteBox.name;
            }
            else
            {
                txt.text = "";
            }
            
            this.letterBox = letterBox;
            imageBox.sprite = spriteBox;
        }

        public void ClickBox()
        {
            onClickBox?.Invoke(letterBox, this, imageBox.sprite.name);
        }
    }
}
