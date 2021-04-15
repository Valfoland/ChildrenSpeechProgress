using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataChecker : MonoBehaviour
{
    /// <summary>
    /// HomeLevels 0 1 2
    /// PatternLevels 
    /// EntertainingCleaningLevels 0 1
    /// HelpAstrLevels
    /// IllumLevels
    /// MarsLevels
    /// </summary>
    
    private void Start()
    {
        Section0.HomeLevels.Level0.DataLevelManager data0 = new Section0.HomeLevels.Level0.DataLevelManager();
        Section0.HomeLevels.Level1.DataLevelManager data1 = new Section0.HomeLevels.Level1.DataLevelManager();
        Section0.HomeLevels.Level2.DataLevelManager data2 = new Section0.HomeLevels.Level2.DataLevelManager();
        Section0.PatternsLevel.DataLevelManager data3 = new Section0.PatternsLevel.DataLevelManager();
        Section0.EntertainingCleaningLevels.Level0.DataLevelManager data4 = new Section0.EntertainingCleaningLevels.Level0.DataLevelManager(1);
        Section0.EntertainingCleaningLevels.Level1.DataLevelManager data5 = new Section0.EntertainingCleaningLevels.Level1.DataLevelManager(1);
        Section0.LettersBasketsLevels.DataLevelManager data6 = new Section0.LettersBasketsLevels.DataLevelManager();
        Section1.HelpAstronautLevels.Level0.DataLevelManager data7 = new Section1.HelpAstronautLevels.Level0.DataLevelManager(1);
        Section1.IlluminatorLevels.Level0.DataLevelManager data8 = new Section1.IlluminatorLevels.Level0.DataLevelManager(1);
        Section1.MarsLevels.Level0.DataLevelManager data9 = new Section1.MarsLevels.Level0.DataLevelManager(1);
    }
}
