using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsyncLoadVisual : MonoBehaviour
{
    [SerializeField] private GameObject circleObject;

    private void Start()
    {
        LevelsPanel.onStartLoadScene += OnLoadScene;
    }

    private void OnDestroy()
    {
        LevelsPanel.onStartLoadScene -= OnLoadScene;
    }

    private void OnLoadScene(AsyncOperation asyncOperation)
    {
        StartCoroutine(StartRotate(asyncOperation));
    }

    private IEnumerator StartRotate(AsyncOperation asyncOperation)
    {
        circleObject.SetActive(true);
        while (!asyncOperation.isDone)
        {
            circleObject.transform.Rotate(0,0, -1);
            yield return null;
        }
    }
}
