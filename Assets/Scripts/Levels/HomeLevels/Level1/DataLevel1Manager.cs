using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.IO;

namespace HomeLevels.Level1
{
    public class DataLevel1Manager : MonoBehaviour
    {
        public static Dictionary<string, List<Sprite>> DataLevel1Dict = new Dictionary<string, List<Sprite>>();
        public static Queue<string> DataNameList = new Queue<string>();
        private void Awake()
        {
            InitData();
        }

        private void InitData()
        {
            try
            {
                var dirLevel1 = Directory.GetDirectories("Assets/Resources/Home/Level1");
                foreach (var dir in dirLevel1)
                {
                    var dirTemp = dir.Replace("Assets/Resources/Home/Level1" + @"\", "");
                    DataNameList.Enqueue(dirTemp);
                    DataLevel1Dict.Add(dirTemp, Resources.LoadAll<Sprite>($"Home/Level1/{dirTemp}").ToList());
                }
            }
            catch (System.IO.DirectoryNotFoundException)
            {
            }
        }
    }
}

