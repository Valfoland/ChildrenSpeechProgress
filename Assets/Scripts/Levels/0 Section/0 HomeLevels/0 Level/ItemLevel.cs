using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace  Section0.HomeLevels.Level0
{
    public class ItemLevel : MonoBehaviour
    {
        [SerializeField] private Animation animFalse;
        [SerializeField] private Image imageBox;
        [SerializeField] private Text textItem;
        public Button BtnBox;
        private char letterBox;
        public static Action<char, ItemLevel, string> onClickBox;    
        
        private void Start()
        {
            BtnBox.onClick.AddListener(ClickBox);
        }

        public void AnimBox()
        {
            animFalse.Play();
        }
        
        public void SetDataBox(KeyValuePair<string, Sprite> spriteItem, char letterBox)
        {
            textItem.text = spriteItem.Value.name.StartsWith("TemplateSprite") ? spriteItem.Key : "";
            this.letterBox = letterBox;
            imageBox.sprite = spriteItem.Value;
        }

        private void ClickBox()
        {
            onClickBox?.Invoke(letterBox, this, imageBox.sprite.name);
        }
    }
}

