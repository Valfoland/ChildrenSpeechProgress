using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Section0.HomeLevels.Level2
{
    public class ItemLevel2 : MonoBehaviour
    {
        [SerializeField] private Image imageItem;
        [SerializeField] private SocketItem socketItem;
        [SerializeField] private Text textItem;

        private void Awake()
        {
            Level2.onInstanceItem += SetData;
            Level2.onPutItem += SetInteractable;
            Level2.onDestroy += DestroyItem;
        }

        private void OnDestroy()
        {
            Level2.onInstanceItem -= SetData;
            Level2.onDestroy -= DestroyItem;
            Level2.onPutItem -= SetInteractable;
        }

        private void SetData(GameObject item, Sprite spriteItem)
        {
            if (gameObject == item)
            {
                //imageItem.sprite = spriteItem;
                textItem.text = spriteItem.name; //temp
                gameObject.name = spriteItem.name;
            }
        }

        private void SetInteractable(GameObject item)
        {
            if (gameObject == item)
            {
                socketItem.enabled = false;
                imageItem.color = Color.gray;
            }
        }

        private void DestroyItem()
        {
            Destroy(gameObject);
        }

    }

}
