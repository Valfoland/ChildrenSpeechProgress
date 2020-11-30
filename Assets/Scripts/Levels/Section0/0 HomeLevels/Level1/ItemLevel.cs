using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Levels;
using Section0.HomeLevels.Level1;

namespace Section0.HomeLevels.Level1
{
    public class ItemLevel : MonoBehaviour
    {
        [SerializeField] private Animation animImage;
        [SerializeField] private Image imageItem;
        [SerializeField] private SocketItem socketItem;
        [SerializeField] private Text textItem;

        public void SetData(Sprite spriteItem, string currentLetter)
        {
            if (currentLetter.ToLower() != "а")
            {
                textItem.text = spriteItem.name;
            }
            else
            {
                textItem.text = "";
            }

            imageItem.sprite = spriteItem;
            gameObject.name = spriteItem.name;
        }

        public void SetInteractable(bool isRightPlace)
        {
            if (isRightPlace)
            {
                socketItem.enabled = false;
                imageItem.color = Color.gray;
            }
            else
            {
                animImage.Play();
            }
        }
    }
}
