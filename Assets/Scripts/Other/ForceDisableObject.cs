using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceDisableObject : MonoBehaviour
{
   private float time;
   [SerializeField] private float forceDisableTime;

   private void OnEnable()
   {
      time = Time.time;
   }

   private void Update()
   {
      if (Time.time - time >= forceDisableTime)
      {
         time = Time.time;
         gameObject.SetActive(false);
      }
   }
}
