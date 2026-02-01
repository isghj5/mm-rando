using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using System;
using System.Collections.Generic;

namespace MMR.Randomizer.Attributes.Entrance
{
    [AttributeUsage(AttributeTargets.Field)]
    public class ExitPolygonTypeAttribute : Attribute
    {
        public byte SceneId { get; }
        public byte PolygonType { get; }
        public IEnumerable<byte> NewExitIndices { get; }

        public ExitPolygonTypeAttribute(Scene scene, byte polygonType, params byte[] newExitIndices)
        {
            if (scene == Scene.Grottos && (polygonType == 10 || polygonType == 33))
            {
                throw new Exception("Attempt to alter exit value of generic or cow grotto.");
            }
            SceneId = scene.Id();
            PolygonType = polygonType;
            NewExitIndices = newExitIndices;
        }
    }
}
