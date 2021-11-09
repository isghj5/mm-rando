using MMR.Randomizer.Models.Rom;
using System.Diagnostics;
using System.Linq;

namespace MMR.Randomizer.Utils
{
    public static class EntranceUtils
    {
        public static void WriteSceneExits(int sceneNumber, byte exitIndex, ushort spawnId)
        {
            Scene scene = RomData.SceneList.Single(u => u.Number == sceneNumber);
            int f = scene.File;
            if (scene.Setups.Count > 1)
            {
                Debug.WriteLine(scene.Number);
            }
            foreach (var setup in scene.Setups)
            {
                if (setup.ExitListAddress == null)
                {
                    continue;
                }
                ReadWriteUtils.Arr_WriteU16(RomData.MMFileList[f].Data, setup.ExitListAddress.Value + exitIndex * 2, spawnId);
            }
        }

        public static void WriteCutsceneExits(int sceneNumber, byte setupIndex, byte cutsceneIndex, ushort spawnId)
        {
            Scene scene = RomData.SceneList.Single(u => u.Number == sceneNumber);
            int f = scene.File;
            var setup = scene.Setups[setupIndex];
            if (setup.CutsceneListAddress == null)
            {
                return;
            }
            ReadWriteUtils.Arr_WriteU16(RomData.MMFileList[f].Data, setup.CutsceneListAddress.Value + cutsceneIndex * 8 + 4, spawnId);
        }
    }
}
