using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Levels;

namespace Section0.EntertainingCleaningLevels.MissionsDecorator
{
    public class ItemLevel : MonoBehaviour
    {
        [SerializeField] protected Animation animFalse;
        [SerializeField] protected Image imageBox;
        [SerializeField] protected Text textBox;
        public Button BtnBox;
        
        private string wordBox;
        public static System.Action<string, ItemLevel> onClickBox;
        
        public void AnimBox()
        {
            animFalse.Play();
        }
        
        public void SetDataBox(KeyValuePair<string, Sprite> spriteItem)
        {
            textBox.text = spriteItem.Value.name.StartsWith("TemplateSprite") ? spriteItem.Key : "";
            wordBox = spriteItem.Key;
            imageBox.sprite = spriteItem.Value;
        }

        protected void ClickBox()
        {
            onClickBox?.Invoke(wordBox, this);
        }
    }
}

