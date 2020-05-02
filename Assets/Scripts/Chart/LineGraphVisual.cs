using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  CodeMonkey.Utils;

public class LineGraphVisual : ScriptableObject, IGraphVisual
{
    private RectTransform graphContainer;
    private Sprite dotSprite;
    private LineGraphVisualObject lastLineObject;
    private Color dotColor;
    private Color dotConnectColor;

    public LineGraphVisual(RectTransform graphContainer, Sprite dotSprite, Color dotColor, Color dotConnectColor)
    {
        this.graphContainer = graphContainer;
        this.dotSprite = dotSprite;
        this.dotColor = dotColor;
        this.dotConnectColor = dotConnectColor;
        lastLineObject = null;
    }

    public IGraphVisualObject CreateGraphVisualObject(Vector2 graphPos, float graphPosWidth, string toolTipTxt,
        bool isEnd = false)
    {
        List<GameObject> gameObjectList = new List<GameObject>();
        GameObject dotObject = CreateDot(graphPos);
        gameObjectList.Add(dotObject);
        GameObject dotConnectionObject = null;

        if (lastLineObject != null)
        {
            dotConnectionObject = CreateDotConnection(
                lastLineObject.GetGraphPos(),
                dotObject.GetComponent<RectTransform>().anchoredPosition);
            gameObjectList.Add(dotConnectionObject);
        }

        LineGraphVisualObject lineGraphVisualObject =
            new LineGraphVisualObject(dotObject, dotConnectionObject, lastLineObject);
        lineGraphVisualObject.SetGraphVisualObjectInfo(graphPos, graphPosWidth, toolTipTxt);

        if (!isEnd)
            lastLineObject = lineGraphVisualObject;
        else
            lastLineObject = null;

        return lineGraphVisualObject;
    }

    private GameObject CreateDotConnection(Vector2 dotPosA, Vector2 dotPosB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        gameObject.GetComponent<Image>().color = dotConnectColor;

        return gameObject;
    }

    private GameObject CreateDot(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("dot", typeof(Image));
        gameObject.transform.SetParent(graphContainer);
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        gameObject.GetComponent<Image>().sprite = dotSprite;
        gameObject.GetComponent<Image>().color = dotColor;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(8, 8);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        gameObject.AddComponent<Button_UI>();
        return gameObject;
    }

    public class LineGraphVisualObject : IGraphVisualObject
    {
        public event EventHandler OnChangedGraphVisualObject;
        private GameObject dotGameObject;
        private GameObject dotConnectionObject;
        private LineGraphVisualObject lastVisualObject;

        public LineGraphVisualObject(GameObject dotGameObject, GameObject dotConnectionObject,
            LineGraphVisualObject lastVisualObject)
        {
            this.dotGameObject = dotGameObject;
            this.dotConnectionObject = dotConnectionObject;
            this.lastVisualObject = lastVisualObject;

            if (lastVisualObject != null)
            {
                lastVisualObject.OnChangedGraphVisualObject += LastVisualObject;
            }
        }

        private void LastVisualObject(object sender, EventArgs e)
        {
            UpdateDotConnection();
        }

        public void SetGraphVisualObjectInfo(Vector2 graphPos, float graphPosWidth, string toolTipTxt,
            bool isEnd = false)
        {
            GetGraphPos();
            UpdateDotConnection();

            Button_UI dotButton = dotGameObject.GetComponent<Button_UI>();
            dotButton.MouseOverOnceFunc = () => { WindowGraph.ShowToolTipStatic(toolTipTxt, graphPos); };
            dotButton.MouseOutOnceFunc = () => { WindowGraph.HideToolTipStatic(); };
            OnChangedGraphVisualObject?.Invoke(this, EventArgs.Empty);
        }

        public void CleanUp()
        {
            Destroy(dotGameObject);
            Destroy(dotConnectionObject);
        }

        public Vector2 GetGraphPos()
        {
            RectTransform rectTransform = dotGameObject.GetComponent<RectTransform>();
            return rectTransform.anchoredPosition;
        }

        private void UpdateDotConnection()
        {
            if (dotConnectionObject != null)
            {
                RectTransform dotConnectionRect = dotConnectionObject.GetComponent<RectTransform>();
                Vector2 dir = (lastVisualObject.GetGraphPos() - GetGraphPos()).normalized;
                float distance = Vector2.Distance(GetGraphPos(), lastVisualObject.GetGraphPos());
                dotConnectionRect.anchorMax = dotConnectionRect.anchorMin = new Vector2(0, 0);
                dotConnectionRect.sizeDelta = new Vector2(distance, 2f);
                dotConnectionRect.anchoredPosition = GetGraphPos() + dir * distance * 0.5f;
                dotConnectionRect.localEulerAngles = new Vector3(0, 0, UtilsClass.GetAngleFromVectorFloat(dir));
            }

        }
    }
}