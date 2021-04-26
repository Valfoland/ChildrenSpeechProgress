using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildrenObjectGenerator : MonoBehaviour
{
    [SerializeField] private GameObject childPrefab;
    [SerializeField] private GameObject parentChild;

    public void DestroyChildren()
    {
        Child.ChildList.ForEach(x => Destroy(x.gameObject));
        Child.ChildList.Clear();
    }
    
    public Child InstantiateChildObject()
    {
        GameObject childObject = Instantiate(childPrefab, parentChild.transform);
        var child = childObject.GetComponent<Child>();
        child.InitChild();
        return child;
    }

    private void OnDestroy()
    {
        Child.ChildList.Clear();
    }
}
