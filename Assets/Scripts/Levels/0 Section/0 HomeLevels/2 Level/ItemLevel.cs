using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Levels;

namespace Section0.HomeLevels.Level2
{
    public class ItemLevel : MonoBehaviour
    {
        [SerializeField] private Animation animFalse;
        [SerializeField] private Image imageBox;
        [SerializeField] private Text textItem;
        public Button BtnBox;
        
        private string wordBox;
        public static System.Action<string, ItemLevel> onClickBox;    
        
        private void Start()
        {
            BtnBox.onClick.AddListener(ClickBox);
        }

        public void AnimBox()
        {
            animFalse.Play();
        }
        
        public void SetDataBox(KeyValuePair<string, Sprite> spriteItem)
        {
            textItem.text = spriteItem.Value.name.StartsWith("TemplateSprite") ? spriteItem.Key : "";
            wordBox = spriteItem.Key;
            imageBox.sprite = spriteItem.Value;
        }

        private void ClickBox()
        {
            onClickBox?.Invoke(wordBox, this);
        }
    }
}

