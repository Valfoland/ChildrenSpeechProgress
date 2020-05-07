using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.UI;
using CodeMonkey.Utils;
public class BarGraphVisual : ScriptableObject, IGraphVisual
{
    private RectTransform graphContainer;
    private Color barColor;
    private float barWidthMultiplier;

    public BarGraphVisual(RectTransform graphContainer, Color barColor, float barWidthMultiplier)
    {
        this.graphContainer = graphContainer;
        this.barColor = barColor;
        this.barWidthMultiplier = barWidthMultiplier;
    }

    public IGraphVisualObject CreateGraphVisualObject(Vector2 graphPos, float graphPosWidth, string toolTipTxt,
        bool isEnd = false)
    {
        GameObject barObject = CreateBar(graphPos, graphPosWidth);
        IGraphVisualObject barChartVisualObject = new BarChartVisualObject(barObject, barWidthMultiplier);
        barChartVisualObject.SetGraphVisualObjectInfo(graphPos, graphPosWidth, toolTipTxt);
        return barChartVisualObject;
    }

    private GameObject CreateBar(Vector2 graphPos, float barWidth)
    {
        GameObject gameObject = new GameObject("bar", typeof(Image));
        gameObject.transform.SetParent(graphContainer);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        gameObject.GetComponent<Image>().color = barColor;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(graphPos.x, 0f);
        rectTransform.sizeDelta = new Vector2(barWidth * barWidthMultiplier, graphPos.y);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.pivot = new Vector2(.5f, 0f);
        gameObject.AddComponent<Button_UI>();

        return gameObject;
    }

    public class BarChartVisualObject : IGraphVisualObject
    {
        private GameObject barObject;
        private float barWidthMultiplier;

        public BarChartVisualObject(GameObject barObject, float barWidthMultiplier)
        {
            this.barObject = barObject;
            this.barWidthMultiplier = barWidthMultiplier;
        }

        public void SetGraphVisualObjectInfo(Vector2 graphPos, float graphPosWidth, string toolTipTxt,
            bool isEnd = false)
        {
            RectTransform rectTransform = barObject.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(graphPos.x, 0f);
            rectTransform.sizeDelta = new Vector2(graphPosWidth * barWidthMultiplier, graphPos.y);
            
            Button_UI buttonUi = barObject.GetComponent<Button_UI>();
            buttonUi.MouseOverOnceFunc = () => { WindowGraph.ShowToolTipStatic(toolTipTxt, graphPos); };
            buttonUi.MouseOutOnceFunc = WindowGraph.HideToolTipStatic;
        }

        public void CleanUp()
        {
            Destroy(barObject);
        }
    }
}
