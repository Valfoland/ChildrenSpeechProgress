using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragableObject : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Image imageObject;
    [SerializeField] private GameObject[] visibleCollisionsObjects = new GameObject[2];
    public static List<DragableObject> DragableObjects = new List<DragableObject>();
    public static System.Action onTriggerObjects;
    private Collider2D colliderItem;

    private void Start()
    {
        colliderItem = gameObject.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DragableObjects.Add(this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        DragableObjects.Clear();
    }

    public static bool operator +(DragableObject d1, DragableObject d2)
    {
        return d1.gameObject.layer == 8 && d2.gameObject.layer == 9;
    }
    public static bool operator ==(DragableObject d1, DragableObject d2)
    {
        return d1.gameObject.name == d2.gameObject.name;
    }
    public static bool operator !=(DragableObject d1, DragableObject d2)
    {
        return d1.gameObject.name != d2.gameObject.name;
    }

    public void OnPointerDown(PointerEventData eventData) {}
    public void OnPointerUp(PointerEventData eventData)
    {
        if (DragableObjects.Count >= 2 && DragableObjects[0].gameObject.layer != DragableObjects[1].gameObject.layer)
        {
            onTriggerObjects.Invoke();
        }
        colliderItem.isTrigger = false;
        DragableObjects.Clear();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (gameObject.layer == 8)
        {
            colliderItem.isTrigger = true;
            gameObject.transform.position = eventData.position;
        }
    }

    public void VisibleFalse()
    {
        imageObject.enabled = false;
    }

    public void VisibleCollision(bool isTrueCollision)
    {
        if (isTrueCollision)
        {
            visibleCollisionsObjects[0].SetActive(false);
            visibleCollisionsObjects[0].SetActive(true);
        }
        else
        {
            visibleCollisionsObjects[1].SetActive(false);
            visibleCollisionsObjects[1].SetActive(true);
        }
    }
}
