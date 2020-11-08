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

        private void Awake()
        {
            LevelManager.onInstanceItem += SetData;
            LevelManager.onPutItem += SetInteractable;
            LevelManager.onDestroy += DestroyItem;
        }

        private void OnDestroy()
        {
            LevelManager.onInstanceItem -= SetData;
            LevelManager.onDestroy -= DestroyItem;
            LevelManager.onPutItem -= SetInteractable;
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
