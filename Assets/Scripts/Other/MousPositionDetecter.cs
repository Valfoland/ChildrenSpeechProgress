using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousPositionDetecter : MonoBehaviour
{
    [SerializeField] private Canvas mainCanvas;
    private static RectTransform rectCanvas;
    private static Camera camCanvas;

    private void Start()
    {
        rectCanvas = mainCanvas.transform as RectTransform;
        camCanvas = mainCanvas.worldCamera;
    }
    
    public static Vector2 GetMousePosition()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rectCanvas,
            Input.mousePosition, 
            camCanvas, 
            out pos);
        pos = new Vector2(pos.x + rectCanvas.sizeDelta.x / 2, pos.y - rectCanvas.sizeDelta.y / 2);
        return pos;
    }
}
