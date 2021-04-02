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

        private void Start()
        {
            BtnBox.onClick.AddListener(ClickBox);
        }

        private void ResetItemBox()
        {
            BtnBox.interactable = true;
        }
        
        public void AnimBox(bool isRight)
        {
            if (isRight)
            {
                BtnBox.interactable = false;
            }
            else
            {
                animFalse.Play();
            }
        }
        
        public void SetDataBox(KeyValuePair<string, Sprite> spriteItem)
        {
            textBox.text = spriteItem.Value.name.StartsWith("TemplateSprite") ? spriteItem.Key : "";
            wordBox = spriteItem.Key;
            imageBox.sprite = spriteItem.Value;
            ResetItemBox();
        }

        private void ClickBox()
        {
            onClickBox?.Invoke(wordBox, this);
        }
    }
}

