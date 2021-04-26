using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System.IO;

public class ChildrenManager : MonoBehaviour
{
    [SerializeField] private ChildrenDataInitializer childrenDataInitializer;
    private ChildrenInitializer childrenInitializer = new ChildrenInitializer();

    private void Start()
    {
        childrenInitializer.InitData(childrenDataInitializer, new ChildrenDataSaver());

        Subscribe();
    }

    private void OnDestroy()
    {
        UnSubScribe();
    }

    private void Subscribe()
    {
        childrenDataInitializer.AuthenticationManager.onLoginSuccess += childrenInitializer.StartInitChildren;
        childrenDataInitializer.AuthenticationManager.onInternetFail += childrenInitializer.StartInitChildren;
    }

    private void UnSubScribe()
    {
        childrenDataInitializer.AuthenticationManager.onLoginSuccess -= childrenInitializer.StartInitChildren;
        childrenDataInitializer.AuthenticationManager.onInternetFail -= childrenInitializer.StartInitChildren;
    }
}