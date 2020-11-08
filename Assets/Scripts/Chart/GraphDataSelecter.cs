using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CodeMonkey.Utils;
using UnityEngine;
using UnityEngine.UI;

public class GraphDataSelecter : MonoBehaviour
{
   public System.Action<bool> onInteractableButtons;
   [SerializeField] private WindowGraph windowGraph;
   [SerializeField] private Dropdown selectChildDropdown;
   [SerializeField] private Dropdown selectSectionDropdown;
   [SerializeField] private Dropdown selectMissionDropdown;
   [SerializeField] private Text textNoData;

   private string dataKeyChild;

   private void Awake()
   {
      StatsPanel.onSetStats += InitDropDowns;
      selectChildDropdown.onValueChanged.AddListener(OnSelectChild);
      selectSectionDropdown.onValueChanged.AddListener(OnSelectSection);
      selectMissionDropdown.onValueChanged.AddListener(OnSelectMission);
   }

   private void OnDestroy()
   {
      StatsPanel.onSetStats -= InitDropDowns;
   }

   private void OnDisable()
   {
      selectChildDropdown.options.Clear();
      selectMissionDropdown.options.Clear();
      selectSectionDropdown.options.Clear();
   }

   private void InitDropDowns()
   {
      foreach (var childrenData in ChildrenManager.ChildDataList)
      {
         selectChildDropdown.options.Add(new Dropdown.OptionData
         {
            text = childrenData.Name
         });
      }
      
      for (int i = 0; i < DataGame.SectionDataList.Count; i++)
      {
         selectSectionDropdown.options.Add(new Dropdown.OptionData
         {
            text = "Раздел " + (i + 1)
         });
      }


      InitMissionsDropDown();

      selectChildDropdown.value = Child.CurrentChildrenData.IdChild;
      selectSectionDropdown.value = DataGame.IdSelectSection;
      selectMissionDropdown.value = DataGame.IdSelectMission;

      ChooseData(selectChildDropdown.value, selectSectionDropdown.value, selectMissionDropdown.value);
   }
   private void InitMissionsDropDown(int id = 0)
   {
      for (int i = 0; i < DataGame.SectionDataList[id].MissionDataList.Count; i++)
      {
         selectMissionDropdown.options.Add(new Dropdown.OptionData
         {
            text = "Миссия " + (i + 1)
         });
      }
   }

   private void OnSelectChild(int id = 0)
   {
      ChooseData(id, selectSectionDropdown.value, selectMissionDropdown.value);
   }

   private void OnSelectSection(int id = 0)
   {
      InitMissionsDropDown(id);
      ChooseData(selectChildDropdown.value, id, selectMissionDropdown.value);
   }

   private void OnSelectMission(int id = 0)
   {
      ChooseData(selectChildDropdown.value, selectSectionDropdown.value, id);
   }

   private void ChooseData(int idChild = 0, int idSection = 0, int idMission = 0)
   {
      var items = ChildrenManager.ChildDataList
         .Where(x => x.IdChild == idChild).ToList();
      var resultList = items[0].ResultMission[idSection.ToString() + idMission]
         .Where(x => x > 0).ToList();

      textNoData.gameObject.SetActive(false);
      if (resultList.Count == 0)
      {
         textNoData.gameObject.SetActive(true);
         windowGraph.SetGraph();
         onInteractableButtons?.Invoke(false);
      }
      else
      {
         windowGraph.SetGraph(resultList);
         onInteractableButtons?.Invoke(true);
      }
   }
}
