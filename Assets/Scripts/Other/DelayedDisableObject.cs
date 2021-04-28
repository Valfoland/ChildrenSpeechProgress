using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayedDisableObject : MonoBehaviour
{
   private float time;
   
   [SerializeField] private bool canDisable;
   [SerializeField] private float forceDisableTime;

   private void OnEnable()
   {
      time = Time.time;
   }

   public void StartDelayedDisable(int timeDisable)
   {
      canDisable = true;
      forceDisableTime = timeDisable;
      time = Time.time;
   }

   private void Update()
   {
      if (Time.time - time >= forceDisableTime && canDisable)
      {
         time = Time.time;
         gameObject.SetActive(false);
         canDisable = false;
      }
   }
}
