using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  CodeMonkey.Utils;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private RectTransform graphContainer;
    [SerializeField] private RectTransform labelTemplateX;
    [SerializeField] private RectTransform labelTemplateY;
    [SerializeField] private RectTransform dashTemplateX;
    [SerializeField] private RectTransform dashTemplateY;
    
    
    private void Awake()
    {
        List<int> valuesList = new List<int>() {5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33};
        ShowGraph(valuesList);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11,11);
        rectTransform.anchorMin = new Vector2(0 ,0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void ShowGraph(List<int> valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMax = 100f;
        float xSize = 25f;

        GameObject lastCircleObject = null;
        for (int i = 0; i < valueList.Count; i++)
        {
            float xPos = xSize + i * xSize;
            float yPos = (valueList[i] / yMax) * graphHeight;
            GameObject circleObject = CreateCircle(new Vector2(xPos, yPos));
            
            if (lastCircleObject != null)
            {
                CreateDotConnection(lastCircleObject.GetComponent<RectTransform>().anchoredPosition,
                    circleObject.GetComponent<RectTransform>().anchoredPosition);
            }

            lastCircleObject = circleObject;
            
            RectTransform labelX = Instantiate(labelTemplateX);
            labelX.SetParent(graphContainer);
            labelX.gameObject.SetActive(true);
            labelX.anchoredPosition = new Vector2(xPos, labelTemplateX.anchoredPosition.y);
            labelX.GetComponent<Text>().text = i.ToString();
            
            RectTransform dashX = Instantiate(dashTemplateX);
            dashX.SetParent(graphContainer);
            dashX.gameObject.SetActive(true);
            dashX.anchoredPosition = new Vector2(xPos + 2f, labelTemplateX.anchoredPosition.y);
        }

        int separatorCount = 10;

        for (int i = 0; i <= separatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateX);
            labelY.SetParent(graphContainer);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(labelTemplateY.anchoredPosition.x, normalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = Mathf.RoundToInt(normalizedValue * yMax).ToString();
            
            RectTransform dashY = Instantiate(dashTemplateY);
            dashY.SetParent(graphContainer);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(labelTemplateY.anchoredPosition.x + 2f, normalizedValue * graphHeight);
        }
    }

    private void CreateDotConnection(Vector2 dotPosA, Vector2 dotPosB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPosB - dotPosA).normalized;
        float distance = Vector2.Distance(dotPosA, dotPosB);
        rectTransform.anchorMin = new Vector2(0 ,0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPosA + dir * distance * 0.5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
    }
}
