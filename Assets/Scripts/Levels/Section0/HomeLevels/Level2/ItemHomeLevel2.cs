using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Section0.HomeLevels
{
    public class ItemHomeLevel2 : MonoBehaviour
    {
        [SerializeField] private Animation animImage;
        [SerializeField] private Image imageItem;
        [SerializeField] private SocketItem socketItem;
        [SerializeField] private Text textItem;

        private void Awake()
        {
            HomeLevel2.onInstanceItem += SetData;
            HomeLevel2.onPutItem += SetInteractable;
            HomeLevel2.onDestroy += DestroyItem;
        }

        private void OnDestroy()
        {
            HomeLevel2.onInstanceItem -= SetData;
            HomeLevel2.onDestroy -= DestroyItem;
            HomeLevel2.onPutItem -= SetInteractable;
        }

        private void SetData(GameObject item, Sprite spriteItem, string currentLetter)
        {
            if (gameObject == item)
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
        }

        private void SetInteractable(GameObject item, bool isRightPlace)
        {
            if (gameObject == item)
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

        private void DestroyItem()
        {
            Destroy(gameObject);
        }

    }

}
