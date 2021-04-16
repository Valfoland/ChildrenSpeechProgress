using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallenItem : MonoBehaviour
{
    [NonSerialized] public string TypeWord;
    public RectTransform ItemRectTransform;
    public Text TextWord;
    public Image ImageWord;
    public bool CanFall;
    public Action onTouchBasket;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.ToLower().StartsWith("basket"))
            onTouchBasket?.Invoke();
    }
}
