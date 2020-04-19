using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Section0
{
    public class ResultKeeper : MonoBehaviour
    {
       //TODO взятие результатов с левелов  + сохранение их 
       private void Start()
       {
           AttemptCounter.onSetResult += SetResultSession;
       }

       private void OnDestroy()
       {
           AttemptCounter.onSetResult -= SetResultSession;
       }

       public static void SetResultSession(float result)
       {
           if (DataTasks.IdSelectSection == 0)
           {
               
           }
       }
    }
}

