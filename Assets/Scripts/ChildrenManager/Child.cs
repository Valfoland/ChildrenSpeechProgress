using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class ChildrenData
{
    public int IdChild;
    public string Name;
    public string Age;
    public string GroupName;
    public Dictionary<string, List<int>> ResultMission = new Dictionary<string, List<int>>();
    public Dictionary<string, List<bool>> CompletedLevels = new Dictionary<string, List<bool>>();
}

public interface ICompletionLevels<T>
{
    
}


public class Child : MonoBehaviour
{
    public Text Name;
    public Text Age;
    public Text GroupName;
    
    public static int CountChildren;
    private static System.Action onChooseChild;
    public static ChildrenData CurrentChildrenData;
    public ChildrenData ChildrenData = new ChildrenData();
    
    [SerializeField] private Image setChildImage;
    
    private void Start()
    {
        onChooseChild += SetChooseChild;
        AddChild();
    }

    private void OnDestroy()
    {
        onChooseChild -= SetChooseChild;
    }

    private void AddChild()
    {
        ChildrenData.IdChild = CountChildren;
        CountChildren++;
        onChooseChild?.Invoke();
    }

    public void ClickChooseChild()
    {
        PlayerPrefs.SetInt(ChildrenDataSaver.CHOOSE_CHILD, ChildrenData.IdChild);
        CurrentChildrenData = ChildrenData;
        onChooseChild?.Invoke();
    }

    private void SetChooseChild()
    {
        if (ChildrenData.IdChild != PlayerPrefs.GetInt(ChildrenDataSaver.CHOOSE_CHILD))
        {
            setChildImage.color = Color.black;
        }
        else
        {
            setChildImage.color = Color.green;
        }
    }
}

