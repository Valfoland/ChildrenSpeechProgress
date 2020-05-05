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
    public Dictionary<string, List<float>> ResultMission = new Dictionary<string, List<float>>();
}
public class Child : MonoBehaviour
{
    public Text Name;
    public Text Age;
    public Text GroupName;
    public static int hren;
    public static int CountChildren;
    public static Dictionary<string, List<float>> ResultDict = new Dictionary<string, List<float>>();
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
            setChildImage.color = Color.white;
        }
    }
}

