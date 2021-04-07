using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Basket : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField] private Animator animatorBasket;
    [SerializeField] private RectTransform basketRectTransform;
    private Vector2 canvasSize;
    private static readonly int winAnimTrigger = Animator.StringToHash("Win");
    private static readonly int loseAnimTrigger = Animator.StringToHash("Lose");

    private void Start()
    {
        canvasSize = GameObject.FindWithTag(DataGame.CANVAS_LEVELS).GetComponent<RectTransform>().sizeDelta;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        basketRectTransform.anchoredPosition = 
            new Vector2(
                Mathf.Clamp(basketRectTransform.anchoredPosition.x + eventData.delta.x, -canvasSize.x / 2, canvasSize.x / 2), 
                basketRectTransform.anchoredPosition.y);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void AnimBasket(bool isTrueAttempt)
    {
        animatorBasket.SetTrigger(isTrueAttempt ? winAnimTrigger : loseAnimTrigger);
    }
}
