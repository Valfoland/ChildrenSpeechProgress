using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  CodeMonkey.Utils;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class WindowGraph : MonoBehaviour
{
    private static WindowGraph instance;
    private List<int> valuesList = new List<int>();
    private IGraphVisual graphVisual;
    private int maxVisibleAmount;
    private Func<int, string> getAxisLabelX;
    private Func<float, string> getAxisLabelY;
    
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

    [SerializeField] private Button barChartBtn;
    [SerializeField] private Button lineChartBtn;
    [SerializeField] private Button zoomMinusBtn;
    [SerializeField] private Button zoomPlusBtn;
    
    private void Awake()
    {
        instance = this;
        List<int> valuesList = new List<int> {5, 98, 56, 45, 30, 22, 17, 15, 13, 17, 25, 37, 40, 36, 33, 50, 60, 40, 70};
        IGraphVisual graphVisualLine = new LineGraphVisual(graphContainer, dotSprite, Color.blue, Color.gray);
        IGraphVisual graphVisualBar = new BarChartVisual(graphContainer, Color.blue, 0.7f);
        ShowGraph(valuesList, graphVisualBar, -1, (int _i) => "Day " + (_i + 1), (float _f) => "$" + Mathf.RoundToInt(_f));

        barChartBtn.onClick.AddListener(() => { SetGraphVisual(graphVisualBar); });
        lineChartBtn.onClick.AddListener(() => { SetGraphVisual(graphVisualLine); });
        zoomMinusBtn.onClick.AddListener(ZoomMinusBtn);
        zoomPlusBtn.onClick.AddListener(ZoomPlusBtn);

        toolTipTxt = toolTip.GetComponentInChildren<Text>();
        toolTipRect = toolTip.GetComponent<RectTransform>();
        
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
    
    private void SetGetAxisLabelX(Func<int, string> getAxisLabelX)
    {
        ShowGraph(valuesList, graphVisual, maxVisibleAmount, getAxisLabelX, getAxisLabelY);
    }
    
    private void SetGetAxisLabelYX(Func<float, string> getAxisLabelY)
    {
        ShowGraph(valuesList, graphVisual, maxVisibleAmount, getAxisLabelX, getAxisLabelY);
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

    private void ShowGraph(List<int> valuesList, IGraphVisual graphVisual, int maxVisibleAmount = -1, Func<int, string> getAxisLabelX = null, Func<float, string> getAxisLabelY = null)
    {
        this.valuesList = valuesList;
        this.graphVisual = graphVisual;
        this.getAxisLabelX = getAxisLabelX;
        this.getAxisLabelY = getAxisLabelY;
        
        if (maxVisibleAmount <= 0)
        {
            maxVisibleAmount = valuesList.Count;
        }
        if (maxVisibleAmount >= valuesList.Count)
        {
            maxVisibleAmount = valuesList.Count;
        }
        this.maxVisibleAmount = maxVisibleAmount;
        
        if (getAxisLabelX == null)
            getAxisLabelX = delegate(int i) { return i.ToString();};
        if (getAxisLabelY == null)
            getAxisLabelY = delegate(float f) { return Mathf.RoundToInt(f).ToString();};

        foreach (var gameObject in gameObjectList)
        {
            Destroy(gameObject);
        }
        gameObjectList.Clear();

        
        float graphHeight = graphContainer.sizeDelta.y;
        float graphWidth = graphContainer.sizeDelta.x;
        float yMax = valuesList[0];
        float yMin = valuesList[0];
        float xSize = graphWidth / (maxVisibleAmount + 1);

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
            float yPos = ((valuesList[i] - yMin) / (yMax - yMin)) * graphHeight;
            string toolTipTxt = getAxisLabelY(valuesList[i]);
            if (i == valuesList.Count - 1)
                gameObjectList.AddRange(graphVisual.AddGraphVisual(new Vector2(xPos, yPos), xSize, toolTipTxt, true));
            else
                gameObjectList.AddRange(graphVisual.AddGraphVisual(new Vector2(xPos, yPos), xSize, toolTipTxt));
            CreateSeparatorsX(i, xPos);
            xIndex++;
        }
        CreateSeparatorsY(yMax, yMin, graphHeight);
    }

    private void CreateSeparatorsX(int i, float xPos)
    {
        RectTransform labelX = Instantiate(labelTemplateX);
        labelX.SetParent(graphContainer);
        labelX.gameObject.SetActive(true);
        labelX.anchoredPosition = new Vector2(xPos, labelTemplateX.anchoredPosition.y);
        labelX.GetComponent<Text>().text = getAxisLabelX(i);
        gameObjectList.Add(labelX.gameObject);
            
        RectTransform dashX = Instantiate(dashTemplateX, graphContainer);
        dashX.gameObject.SetActive(true);
        dashX.anchoredPosition = new Vector2(xPos, dashTemplateX.anchoredPosition.y);
        gameObjectList.Add(dashX.gameObject);
    }
    
    private void CreateSeparatorsY(float yMax, float yMin, float graphHeight)
    {
        int separatorCount = 10;

        for (int i = 0; i <= separatorCount; i++)
        {
            RectTransform labelY = Instantiate(labelTemplateX);
            labelY.SetParent(graphContainer);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / separatorCount;
            labelY.anchoredPosition = new Vector2(labelTemplateY.anchoredPosition.x, normalizedValue * graphHeight);
            labelY.GetComponent<Text>().text = getAxisLabelY(yMin + normalizedValue * (yMax - yMin));
            gameObjectList.Add(labelY.gameObject);
            
            RectTransform dashY = Instantiate(dashTemplateY, graphContainer);
            dashY.gameObject.SetActive(true);
            dashY.anchoredPosition = new Vector2(dashTemplateY.anchoredPosition.x, normalizedValue * graphHeight);
            gameObjectList.Add(dashY.gameObject);
        }
    }
    
    private interface IGraphVisual
    {
        List<GameObject> AddGraphVisual(Vector2 graphPos, float graphPosWidth, string toolTipTxt, bool isEnd = false);
    }
    
    private class BarChartVisual: IGraphVisual
    {
        private RectTransform graphContainer;
        private Color barColor;
        private float barWidthMultiplier;
        public BarChartVisual(RectTransform graphContainer, Color barColor, float barWidthMultiplier)
        {
            this.graphContainer = graphContainer;
            this.barColor = barColor;
            this.barWidthMultiplier = barWidthMultiplier;
        }

        public List<GameObject> AddGraphVisual(Vector2 graphPos, float graphPosWidth, string toolTipTxt, bool isEnd = false)
        {
            GameObject barObject = CreateBar(graphPos, graphPosWidth);
            Button_UI barButton = barObject.AddComponent<Button_UI>();
            barButton.MouseOverOnceFunc += () => { ShowToolTipStatic(toolTipTxt, graphPos); };
            barButton.MouseOutOnceFunc += () => { HideToolTipStatic(); };
            return new List<GameObject> {barObject};
        }
        
        private GameObject CreateBar(Vector2 graphPos, float barWidth)
        {
            GameObject gameObject = new GameObject("bar", typeof(Image));
            gameObject.transform.SetParent(graphContainer);
            gameObject.transform.localScale = new Vector3(1,1, 1);

            gameObject.GetComponent<Image>().color = barColor;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(graphPos.x, 0f);
            rectTransform.sizeDelta = new Vector2(barWidth * barWidthMultiplier, graphPos.y);
            rectTransform.anchorMin = new Vector2(0 ,0);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.pivot = new Vector2(.5f, 0f);
            return gameObject;
        }
    }

    private class LineGraphVisual: IGraphVisual
    {
        private RectTransform graphContainer;
        private Sprite dotSprite;
        private GameObject lastDotObject;
        private Color dotColor;
        private Color dotConnectColor;
        
        public LineGraphVisual(RectTransform graphContainer, Sprite dotSprite, Color dotColor, Color dotConnectColor)
        {
            this.graphContainer = graphContainer;
            this.dotSprite = dotSprite;
            this.dotColor = dotColor;
            this.dotConnectColor = dotConnectColor;
            lastDotObject = null;
        }

        public List<GameObject> AddGraphVisual(Vector2 graphPos, float graphPosWidth, string toolTipTxt, bool isEnd = false)
        {
            List<GameObject> gameObjectList = new List<GameObject>();
            GameObject dotObject = CreateDot(graphPos);
            gameObjectList.Add(dotObject);
            if (lastDotObject != null)
            {
                GameObject dotConnection = CreateDotConnection(
                    lastDotObject.GetComponent<RectTransform>().anchoredPosition,
                    dotObject.GetComponent<RectTransform>().anchoredPosition);
                gameObjectList.Add(dotConnection);
            }

            if(!isEnd)
                lastDotObject = dotObject;
            else
                lastDotObject = null;

            Button_UI dotButton = dotObject.AddComponent<Button_UI>();
            dotButton.MouseOverOnceFunc += () => { ShowToolTipStatic(toolTipTxt, graphPos); };
            dotButton.MouseOutOnceFunc += () => { HideToolTipStatic(); };
            
            return gameObjectList;
        }

        private GameObject CreateDotConnection(Vector2 dotPosA, Vector2 dotPosB)
        {
            GameObject gameObject = new GameObject("dotConnection", typeof(Image));
            gameObject.transform.SetParent(graphContainer, false);
            gameObject.transform.localScale = new Vector3(1,1, 1);
            gameObject.GetComponent<Image>().color = dotConnectColor;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            Vector2 dir = (dotPosB - dotPosA).normalized;
            float distance = Vector2.Distance(dotPosA, dotPosB);
            rectTransform.anchorMin = new Vector2(0 ,0);
            rectTransform.anchorMax = new Vector2(0, 0);
            rectTransform.sizeDelta = new Vector2(distance, 2f);
            rectTransform.anchoredPosition = dotPosA + dir * distance * 0.5f;
            rectTransform.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));

            return gameObject;
        }
 
        private GameObject CreateDot(Vector2 anchoredPosition)
        {
            GameObject gameObject = new GameObject("dot", typeof(Image));
            gameObject.transform.SetParent(graphContainer);
            gameObject.transform.localScale = new Vector3(1,1, 1);
            gameObject.GetComponent<Image>().sprite = dotSprite;
            gameObject.GetComponent<Image>().color = dotColor;
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = anchoredPosition;
            rectTransform.sizeDelta = new Vector2(8,8);
            rectTransform.anchorMin = new Vector2(0 ,0);
            rectTransform.anchorMax = new Vector2(0, 0);
            return gameObject;
        }


    }
}
