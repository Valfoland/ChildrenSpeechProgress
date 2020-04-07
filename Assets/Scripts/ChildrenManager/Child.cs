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
}
public class Child : MonoBehaviour
{
    public Text Name;
    public Text Age;
    public Text GroupName;
    public Text Score;

    public static int CountChildren;
    [SerializeField] public ChildrenData ChildrenData;
    [SerializeField] private Image setChildImage;

    private static System.Action onChooseChild;

    private void Start()
    {
        Init();
        ChooseChild();
    }

    private void OnDestroy()
    {
        onChooseChild -= SetChooseChild;
    }

    private void Init()
    {
        onChooseChild += SetChooseChild;
        ChildrenData = new ChildrenData();
    }

    private void ChooseChild()
    {
        ChildrenData.IdChild = CountChildren;
        CountChildren++;
        onChooseChild?.Invoke();
    }

    public void ClickChooseChild()
    {
        PlayerPrefs.SetInt("ChooseChild", ChildrenData.IdChild);
        onChooseChild?.Invoke();
    }

    private void SetChooseChild()
    {
        if(ChildrenData.IdChild != PlayerPrefs.GetInt("ChooseChild"))
        {
            setChildImage.color = Color.black;
        }
        else
        {
            setChildImage.color = Color.white;
        }
    }
}

