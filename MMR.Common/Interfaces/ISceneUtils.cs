using MMR.Common.Models;
using System.Collections.Generic;

namespace MMR.Common.Interfaces
{
    public interface ISceneUtils
    {
        void ReadSceneTable();
        void GetMaps();
        void GetMapHeaders();
        void GetActors();
        void UpdateScene(Scene scene);
        List<Scene> GetScenes();
        void ReenableNightBGM();
    }
}
