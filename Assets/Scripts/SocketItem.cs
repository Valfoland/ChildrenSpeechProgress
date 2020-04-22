using System;
using System.Collections.Generic;
using Section0.HomeLevels;
using UnityEngine;
using  UnityEngine.EventSystems;
using Vector2 = UnityEngine.Vector2;

/// <summary>
/// Класс drag and drop агентов
/// </summary>
public class SocketItem : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private static Action<RectTransform, SocketItem> onPutItem;
    public static Action<SocketItem> onPut;

    [SerializeField] private Animation animSocket;
    [SerializeField] private StateRect stateRect;
    [SerializeField] private bool isInsertableSocket;
    
    private int countEnter;
    private bool isBusy;
    private static GameObject startSocket;
    
    private RectTransform mainRect;
    public static  RectTransform TempContainer;
    private RectTransform startParentTransform;

    private void Start()
    {
        mainRect = GetComponent<RectTransform>();
        startParentTransform = mainRect.parent.GetComponent<RectTransform>();
        
        SubscribeEvents();
    }

    private void OnDestroy()
    {
        DescribeEvents();
    }

    private void SubscribeEvents()
    {
        onPutItem += PutItem;
    }

    private void DescribeEvents()
    {
        onPutItem -= PutItem;
    }

    /// <summary>
    /// В данном методе перемещаем агента
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (stateRect == StateRect.Item)
            mainRect.anchoredPosition = MousPositionDetecter.GetMousePosition();
    }

    /// <summary>
    /// Здесь устанавливаем во временный контейнер агента
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (stateRect == StateRect.Item)
        {
            startSocket = transform.parent.gameObject;
            mainRect.SetParent(TempContainer);
        }
    }

    /// <summary>
    /// Здесь оповещяем другие сокеты о том что надо поместить агента, если ставить некуда то возвращяем обратно
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        if (stateRect == StateRect.Item && countEnter > 0)
        {
            onPutItem?.Invoke(mainRect, this);
        }
        if (mainRect.parent == TempContainer)
        {
            BackToStartPos();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        countEnter++;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(countEnter > 0) countEnter--;
    }

    private void PutItem(RectTransform itemRect, SocketItem item)
    {
        if (stateRect == StateRect.Socket && countEnter > 0  && isInsertableSocket)
        {
            countEnter = 0;
            SetItemToSocket(itemRect, mainRect);
            if (startSocket != gameObject)
            {
                onPut?.Invoke(item);
            }
        }
    }

    public void BackToStartPos()
    {
        SetItemToSocket(mainRect, startParentTransform);
    }

    private void SetItemToSocket(RectTransform itemRect, RectTransform socketRect)
    {
        itemRect.SetParent(socketRect);
    }

    private void AnimSocket(bool isTrue)
    {
        if (isTrue)
        {
            
        }
        else
        {
            animSocket.Play();
        }
    }

    [Serializable]
    internal enum StateRect
    {
        Socket,
        Item
    }
}
