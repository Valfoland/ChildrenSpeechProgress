using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  CodeMonkey.Utils;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public interface IGraphVisual
{
    IGraphVisualObject CreateGraphVisualObject(Vector2 graphPos, float graphPosWidth, string toolTipTxt, bool isEnd = false);
}

public interface IGraphVisualObject
{
    void SetGraphVisualObjectInfo(Vector2 graphPos, float graphPosWidth, string toolTipTxt, bool isEnd = false);
    void CleanUp();
}

public class WindowGraph : MonoBehaviour
{
    private static WindowGraph instance;
    private List<int> valuesList = new List<int>();
    private IGraphVisual graphVisual;
    private int maxVisibleAmount;
    private Func<int, string> getAxisLabelX;
    private Func<float, string> getAxisLabelY;
    private float yMin;
    private float yMax;
    private float xSize;
    
    [SerializeField] private Sprite dotSprite;
    [SerializeField] private RectTransform graphContainer;
    [SerializeField] private RectTransform labelTemplateX;
    [SerializeField] private RectTransform labelTemplateY;
    [SerializeField] private RectTransform dashTemplateX;
    [SerializeField] private RectTransform dashTemplateY;
    [SerializeField] private GameObject toolTip;
    private Text toolTipTxt;
    private RectTransform toolTipRect;
    private List<GameObject> gameObjectList = new List<GameObject>();
    private List<IGraphVisualObject> graphVisualObjects = new List<IGraphVisualObject>();
    private IGraphVisual[] graphVisuals = new IGraphVisual[2];
    
    private void Awake()
    {
        instance = this;
        graphVisuals[0] = new LineGraphVisual(graphContainer, dotSprite, Color.blue, Color.gray);
        graphVisuals[1] = new BarGraphVisual(graphContainer, Color.blue, 0.7f);
        toolTipTxt = toolTip.GetComponentInChildren<Text>();
        toolTipRect = toolTip.GetComponent<RectTransform>();
    }

    public void ChangeGraph(int idGraph)
    {
        switch (idGraph)
        {
            case 0: 
                SetGraphVisual(graphVisuals[0]);
                break;
            case 1: 
                SetGraphVisual(graphVisuals[1]);
                break;
        }
    }

    public void ZoomGraph(bool isPlusZoom)
    {
        if (isPlusZoom) 
            ZoomPlusBtn();
        else 
            ZoomMinusBtn();
    }
    
    public static void ShowToolTipStatic(string toolTipTxt, Vector2 anchoredPos)
    {
        instance.ShowToolTip(toolTipTxt, anchoredPos);
    }
    
    public static void HideToolTipStatic()
    {
        instance.HideToolTip();
    }
    
    private void ShowToolTip(string toolTipTxt, Vector2 anchoredPos)
    {
        toolTip.SetActive(true);
        toolTipRect.anchoredPosition = new Vector2(anchoredPos.x + 20f, anchoredPos.y + 20f);
        this.toolTipTxt.text = toolTipTxt;
        float textPaddingSize = 4f;
        toolTipRect.sizeDelta = new Vector2(
            this.toolTipTxt.preferredWidth + textPaddingSize * 2f, 
            this.toolTipTxt.preferredHeight + textPaddingSize / 2f);
        toolTip.transform.SetAsLastSibling();
    }

    private void HideToolTip()
    {
        toolTip.SetActive(false);
    }

    private void ZoomMinusBtn() 
    {
       ShowGraph(valuesList, graphVisual, maxVisibleAmount - 1, getAxisLabelX, getAxisLabelY);
    }
    
    private void ZoomPlusBtn()
    {
        ShowGraph(valuesList, graphVisual, maxVisibleAmount + 1, getAxisLabelX, getAxisLabelY);
    }
    
    private void SetGraphVisual(IGraphVisual graphVisual)
    {
        ShowGraph(valuesList, graphVisual, maxVisibleAmount, getAxisLabelX, getAxisLabelY);
    }

    public void SetGraph(List<int> valuesList = null)
    {
        if (valuesList != null)
        {
            ShowGraph(
                valuesList,
                graphVisuals[0],
                -1,
                i => (i + 1).ToString(),
                f => Mathf.RoundToInt(f) + "%");
        }
        else
        {
            DestroyGraph();
        }
    }

    private void ShowGraph(List<int> valuesList, IGraphVisual graphVisual, int maxVisibleAmount = -1, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null)
    {
        this.valuesList = valuesList;
        this.graphVisual = graphVisual;
        this.getAxisLabelX = getAxisLabelX;
        this.getAxisLabelY = getAxisLabelY;
        
        if (maxVisibleAmount <= 0)
            maxVisibleAmount = valuesList.Count;
        if (maxVisibleAmount >= valuesList.Count)
            maxVisibleAmount = valuesList.Count;
        
        this.maxVisibleAmount = maxVisibleAmount;
        
        DestroyGraph();
        
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        yMax = valuesList[0];
        yMin = valuesList[0];
        xSize = graphWidth / (maxVisibleAmount + 1);

        for (int i = Mathf.Max(valuesList.Count - maxVisibleAmount, 0); i < valuesList.Count; i++)
        {
            int value = valuesList[i];
            if (value > yMax)
                yMax = value;
            if (value < yMin)
                yMin = value;
        }

        float yDiff = yMax - yMin;
        int xIndex = 0;
        
        if (yDiff <= 0) yDiff = 5;
        
        yMax = yMax + yDiff * 0.2f;
        yMin = 0;
        
        for (int i = Mathf.Max(valuesList.Count - maxVisibleAmount, 0); i < valuesList.Count; i++)
        {
            float xPos = xSize + xIndex * xSize;
            float yPos = (valuesList[i] - yMin) / (yMax - yMin) * graphHeight;
            string toolTipTxt = getAxisLabelY(valuesList[i]);
            
            if (i == valuesList.Count - 1)
                graphVisualObjects.Add(graphVisual.CreateGraphVisualObject(new Vector2(xPos, yPos), xSize, toolTipTxt, true));
            else
                graphVisualObjects.Add(graphVisual.CreateGraphVisualObject(new Vector2(xPos, yPos), xSize, toolTipTxt));
            
            CreateSeparatorsX(i, xPos);
            xIndex++;
        }
        
        CreateSeparatorsY(yMax, yMin, graphHeight);
    }

    private void DestroyGraph()
    {
        gameObjectList.ForEach(Destroy);
        gameObjectList.Clear();
        graphVisualObjects.ForEach(x => x.CleanUp());
        graphVisualObjects.Clear();
    }
    
    private void CreateSeparatorsX(int i, float xPos)
    {
        string axisLabel = getAxisLabelX(i);
        Vector2 anchoredPosLabel = new Vector2(xPos, labelTemplateX.anchoredPosition.y);
        Vector2 anchoredPosDash = new Vector2(xPos, dashTemplateX.anchoredPosition.y);
        
        CreateLabels(labelTemplateX, anchoredPosLabel, axisLabel);
        CreateDashes(dashTemplateX, anchoredPosDash);
    }
    
    private void CreateSeparatorsY(float yMax, float yMin, float graphHeight)
    {
        int separatorCount = 10;

        for (int i = 0; i <= separatorCount; i++)
        {
            float normalizedValue = i * 1f / separatorCount;
            string axisLabel = getAxisLabelY(yMin + normalizedValue * (yMax - yMin));
            Vector2 anchoredPosLabel = new Vector2(labelTemplateY.anchoredPosition.x, normalizedValue * graphHeight);
            Vector2 anchoredPosDash = new Vector2(dashTemplateY.anchoredPosition.x, normalizedValue * graphHeight);

            CreateLabels(labelTemplateY, anchoredPosLabel, axisLabel);
            CreateDashes(dashTemplateY, anchoredPosDash);
        }
    }

    private void CreateLabels(RectTransform labelTemplate, Vector2 anchoredPosition, string axisLabel)
    {
        RectTransform label = Instantiate(labelTemplate, graphContainer, true);
        gameObjectList.Add(label.gameObject);
        label.gameObject.SetActive(true);
        label.anchoredPosition = anchoredPosition;
        label.GetComponent<Text>().text = axisLabel;
    }

    private void CreateDashes(RectTransform dashTemplate, Vector2 anchoredPosition)
    {
        RectTransform dashY = Instantiate(dashTemplate, graphContainer);
        gameObjectList.Add(dashY.gameObject);
        dashY.gameObject.SetActive(true);
        dashY.anchoredPosition = anchoredPosition;
    }
}
