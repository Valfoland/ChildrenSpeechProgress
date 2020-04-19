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
            Level2.onInstanceItem += SetImage;
            Level2.onDestroyItem += DestroyItem;
        }

        private void OnDestroy()
        {
            Level2.onInstanceItem -= SetImage;
            Level2.onDestroyItem -= DestroyItem;
        }

        private void SetImage(GameObject item, Sprite spriteItem)
        {
            if (gameObject == item)
            {
                //imageItem.sprite = spriteItem;
                textItem.text = spriteItem.name; //temp
            }
        }

        private void SetInteractable()
        {
            socketItem.enabled = false;
            imageItem.color = Color.gray;
        }

        private void DestroyItem()
        {
            Destroy(gameObject);
        }
    }

}
