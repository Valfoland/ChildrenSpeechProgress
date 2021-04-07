using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiShower : MonoBehaviour
{
    public void ShowObjects(List<RectTransform> uiObjectsList)
    {
        uiObjectsList.ForEach(i => i.gameObject.SetActive(true));
    }

    public void HideObjects(List<RectTransform> uiObjectsList)
    {
        uiObjectsList.ForEach(i => i.gameObject.SetActive(false));
    }
    
    public void MoveIntoObjects(Dictionary<RectTransform, Vector2> uiObjectsDict)
    {
        
    }

    public void MoveOutObjects(Dictionary<RectTransform, Vector2> uiObjectsDict)
    {
        
    }
}
