
namespace Section0.HomeLevels.Level2
{
    public class DataLevel2Manager : DataLevelManager, ILevelData
    {
        public void InitData()
        {
            idLvl = DataTasks.IdSelectLvl + 1;
            InstanceData();
        }
    }
}
