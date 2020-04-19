using System;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.EventSystems;
using Vector2 = UnityEngine.Vector2;

/// <summary>
/// Класс drag and drop агентов
/// </summary>
public class SocketItem : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private static Action<RectTransform> onPutItem;
    private static Action onCheckBusy;
    public static Action<GameObject, bool, bool> onPutAgent;
    
    [SerializeField] private StateRect stateRect;
    [SerializeField] private bool disableBackSocket;
    [SerializeField] private bool isInsertableSocket;

    private const string FIELD_PUT_NAME = "FieldPut";
    private const string NAME_ITEM = "Agent";
    private int countEnter;
    private bool isBusy;
    private static GameObject startSocket;
    
    private RectTransform mainRect;
    public static  RectTransform TempContainer;
    private RectTransform startParentTransform;
    private static Dictionary<RectTransform, bool> socketDict = new Dictionary<RectTransform, bool>();
    
    private void Start()
    {
        mainRect = GetComponent<RectTransform>();
        startParentTransform = mainRect.parent.GetComponent<RectTransform>();
        
        SubscribeEvents();
        AddSocketsToDict();
    }

    private void OnDestroy()
    {
        DescribeEvents();
    }

    private void SubscribeEvents()
    {
        onPutItem += PutItem;
        onCheckBusy += GetBusySocket;
    }

    private void DescribeEvents()
    {
        onPutItem -= PutItem;
        onCheckBusy -= GetBusySocket;
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
    /// Здесь проверяем наши сокеты на их доступБ также оповещяем другие сокеты о том что надо поместить агента
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        onCheckBusy?.Invoke();
        if (stateRect == StateRect.Item && countEnter > 0)
        {
            onPutItem?.Invoke(mainRect);
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

    private void PutItem(RectTransform itemRect)
    {
        if (stateRect == StateRect.Socket && countEnter > 0 && !isBusy && isInsertableSocket)
        {
            SetItemToSocket(itemRect, mainRect);
            if (startSocket != gameObject)
            {
                if (startSocket.name.StartsWith(FIELD_PUT_NAME) && !gameObject.name.StartsWith(FIELD_PUT_NAME) ||
                    !startSocket.name.StartsWith(FIELD_PUT_NAME) && gameObject.name.StartsWith(FIELD_PUT_NAME))
                {
                    onPutAgent?.Invoke(itemRect.gameObject, false, false);
                }
            }
        }
    }
    
    private void BackToStartPos()
    {
        SetItemToSocket(mainRect, FindFreeSocket(startParentTransform));
        if (startSocket != transform.parent.gameObject)
        {
            onPutAgent?.Invoke(mainRect.gameObject, true, false);
        }
    }

    private void SetItemToSocket(RectTransform itemRect, RectTransform socketRect)
    {
        itemRect.SetParent(socketRect);
        itemRect.anchoredPosition = Vector2.zero;
        itemRect.anchorMin = new Vector2(0.5f, 0.5f);
        itemRect.anchorMax = new Vector2(0.5f, 0.5f);
        itemRect.sizeDelta = socketRect.sizeDelta;
    }

    private void GetBusySocket()
    {
        isBusy = false;
        if (stateRect == StateRect.Socket)
        {
            foreach (Transform child in transform)
            {
                if (child.name.Contains(NAME_ITEM) && child.gameObject.activeSelf)
                {
                    isBusy = true;
                }
            }
            if (socketDict.ContainsKey(mainRect))
            {
                socketDict[mainRect] = isBusy;
            }
        }
    }

    private void AddSocketsToDict()
    {
        if (stateRect == StateRect.Socket && !disableBackSocket)
        {
            GetBusySocket();
            socketDict.Add(mainRect, isBusy);
        }
    }
    
    private RectTransform FindFreeSocket(RectTransform startSocket)
    {
        foreach (var socket in socketDict)
        {
            if (socket.Key == startSocket && socket.Value == false)
            {
                return startSocket;
            }
        }

        foreach (var socket in socketDict)
        {
            if (socket.Value == false)
            {
                return socket.Key;
            }
        }
        
        return null;
    }

    [Serializable]
    internal enum StateRect
    {
        Socket,
        Item
    }
}
