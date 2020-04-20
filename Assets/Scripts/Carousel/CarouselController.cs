using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using System;
using Section0.HomeLevels.Level2;
using UnityEngine.Events;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Класс контроля карусели
/// </summary>
public class CarouselController : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
	#region FIELDS
	private ICarouselElement currentElement;
	private List<ICarouselElement> elements ;
	private List<ICarouselElement> elementsIndex;
	
    [SerializeField] private Button[] buttonLvl;
	[SerializeField] private float tweenSpeed = 1f;
	
	private Vector2 dragPosition;
	private float deltaX;
	private int numberLvl;

	#endregion
	#region INIT

	private void Start()
	{
		Level2.onInit += Initialization;
	}
	
	/// <summary>
	/// Инициализация элементов карусели
	/// </summary>
	public void Initialization()
	{
		StopAllCoroutines();
		buttonLvl[0].onClick.AddListener(() => ChangePage(false));
		buttonLvl[1].onClick.AddListener(() => ChangePage(true));
		
		numberLvl = 0;
		currentElement = null;
		
		elements = new List<ICarouselElement>();
		
		foreach (RectTransform child in transform)
		{
			AddElement(child);
		}
		elementsIndex = new List<ICarouselElement>(elements);

		SetCurrentElement(elementsIndex[0]);
	}

	private void AddElement(RectTransform child)
	{
		var element = child.GetComponent<ICarouselElement>();
		if (element != null && child.gameObject.activeSelf)
		{
			elements.Add(element);
			if (elements.Count > 1)
			{
				Vector2 lastPosition = elements[elements.Count - 2].Position;
				Vector2 horizontalSize = Vector2.right * child.sizeDelta;
				child.anchoredPosition = lastPosition + horizontalSize;
			}
			else
			{
				child.anchoredPosition = Vector2.zero;
			}

		}
	}

	#endregion

	public void ChangePage(bool isNext)
	{
		if (isNext)
		{
			OnNextLvlPack();
		}
		else
		{
			OnPrevLvlPack();
		}
	}

	private void OnNextLvlPack()
	{
		CheckCurrentElement(-1);
		StopAllCoroutines();
		StartCoroutine(TweenToElement(currentElement));
	}

	private void OnPrevLvlPack()
	{
		CheckCurrentElement(1);
		StopAllCoroutines();
		StartCoroutine(TweenToElement(currentElement));
	}

	#region DRAG
	/// <summary>
	/// Метод срабатывающий при старте перетаскивания панели
	/// </summary>
	/// <param name="eventData"></param>
	public void OnBeginDrag(PointerEventData eventData)
	{
		StopAllCoroutines();
		UpdateDragPosition();
	}

	/// <summary>
	/// Метод срабатывающий при перетаскивании панели 
	/// </summary>
	/// <param name="eventData"></param>
	public void OnDrag(PointerEventData eventData)
	{
		if (elements.Count > 1)
		{
			deltaX = Input.mousePosition.x - dragPosition.x;
			foreach (var element in elements)
			{
				element.Translate(Vector2.right * deltaX);
			}
			Debug.Log(deltaX);
			UpdateDragPosition();

		}
	}

	/// <summary>
	/// Метод срабатывающий при окончании перетаскивания панели
	/// </summary>
	/// <param name="eventData"></param>
	public void OnEndDrag(PointerEventData eventData)
	{
		CheckCurrentElement(deltaX);
		StartCoroutine(TweenToElement(currentElement));
	}

	#endregion

	private void CheckCurrentElement(float deltaX)
	{
		ICarouselElement centerElement = FindCenterElement(deltaX);
		if (centerElement != currentElement)
		{
			SetCurrentElement(centerElement);
		}
	}

	private void SetCurrentElement(ICarouselElement element)
	{
		buttonLvl[0].gameObject.SetActive(true);
		buttonLvl[1].gameObject.SetActive(true);
		
		if (elements.Count == 1)
		{
			buttonLvl[0].gameObject.SetActive(false);
			buttonLvl[1].gameObject.SetActive(false);
		}
		else if (numberLvl == 0)
		{
			buttonLvl[0].gameObject.SetActive(false);
		}
		else if (numberLvl == elements.Count - 1)
		{
			buttonLvl[1].gameObject.SetActive(false);
		}
		
		currentElement = element;
	}

	private void UpdateDragPosition() => dragPosition = Input.mousePosition;

	private IEnumerator TweenToElement(ICarouselElement target)
	{
		while (true)
		{
			Vector2 currentPosition = target.Position;
			Vector2 targetPosition = Vector2.Lerp(currentPosition, Vector2.zero, tweenSpeed * Time.deltaTime);
			foreach (var element in elements)
			{
				element.Translate(targetPosition - currentPosition);
			}

			if (Mathf.Abs(target.Position.x) < 0.01f)
			{
				yield break;
			}
			yield return null;
		}

	}

	private ICarouselElement FindCenterElement(float deltaX)
	{
		ICarouselElement centerElement = null;
		
		for (int i = 0; i < elements.Count; i++)
		{
			if (elements[i] == currentElement)
			{
				if (deltaX > 0 && i > 0)
				{
					centerElement = elements[i - 1];
					numberLvl = i - 1;
				}
				else if (deltaX < 0 && i < elements.Count - 1)
				{
					centerElement = elements[i + 1];
					numberLvl = i + 1;
				}
				else
				{
					centerElement = currentElement;
				}
				break;
			}
        }
		return centerElement;
	}

	#region  OnDisable/OnDestroy

	private void OnDestroy()
	{
		Level2.onInit -= Initialization;
		buttonLvl[0].onClick.RemoveAllListeners();
		buttonLvl[1].onClick.RemoveAllListeners();
		StopAllCoroutines();
	}
	#endregion
}
