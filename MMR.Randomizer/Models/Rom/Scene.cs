using MMR.Randomizer.Utils;
using System.Collections.Generic;

namespace MMR.Randomizer.Models.Rom
{
    [System.Diagnostics.DebuggerDisplay("{SceneEnum.ToString()}:{File}")]
    public class Scene
    {

        public enum SceneSpecialObject
        {
            /// a scene can choose to load object 2 or 3, or none, in the scene header
            None = 0,  FieldKeep = 2,  DungeonKeep = 3
        }

        public int File;    // DMA Filetable index
        public int Number;  // Scene table index
        public SceneSpecialObject SpecialObject;
        public GameObjects.Scene SceneEnum;  // Gameobject scene enum value
        public List<Map> Maps = new List<Map>();
        public List<SceneSetup> Setups { get; set; } = new List<SceneSetup>();
        public List<Actor> Doors = new List<Actor>();

        public bool HasDungeonObject()
        {
            return SpecialObject == SceneSpecialObject.DungeonKeep;
        }

        public bool HasFieldObject()
        {
            return SpecialObject == SceneSpecialObject.FieldKeep;
        }

        public List<Actor> GetAllActors()
        {
            List<Actor> actorList = new List<Actor>();
            for(int mapIndex = 0; mapIndex < Maps.Count; ++mapIndex)
            {
                actorList.AddRange(Maps[mapIndex].Actors);
            }
            return actorList;
        }

        public string ToString()
        {
            return this.SceneEnum.ToString(); // just to shorten a touch
        }

    }

    public class SceneSetup
    {
        public int? ExitListAddress { get; set; }
        public int? CutsceneListAddress { get; set; }
        public int? ActorCutsceneListAddress { get; set; }
        public List<ActorCutscene> ActorCutscenes { get; set; }
    }

    public class ActorCutscene
    {
        public ushort Unknown00 { get; set; }
        public short Length { get; set; } // -1 = constantly playing
        public short CameraIndex { get; set; }
        public short CutsceneIndex { get; set; }
        public short AdditionalActorCutsceneIndex { get; set; }
        public byte Sound { get; set; }
        public byte Unknown0B { get; set; }
        public short HUDFade { get; set; }
        public byte ReturnCameraType { get; set; }
        public byte Letterbox { get; set; }

        public ActorCutscene(byte[] data)
        {
            Unknown00 = ReadWriteUtils.Arr_ReadU16(data, 0);
            Length = ReadWriteUtils.Arr_ReadS16(data, 2);
            CameraIndex = ReadWriteUtils.Arr_ReadS16(data, 4);
            CutsceneIndex = ReadWriteUtils.Arr_ReadS16(data, 6);
            AdditionalActorCutsceneIndex = ReadWriteUtils.Arr_ReadS16(data, 8);
            Sound = data[0xA];
            Unknown0B = data[0xB];
            HUDFade = ReadWriteUtils.Arr_ReadS16(data, 0xC);
            ReturnCameraType = data[0xE];
            Letterbox = data[0xF];
        }

        public byte[] ToByteArray()
        {
            var result = new byte[0x10];
            ReadWriteUtils.Arr_WriteU16(result, 0, Unknown00);
            ReadWriteUtils.Arr_WriteU16(result, 2, (ushort)Length);
            ReadWriteUtils.Arr_WriteU16(result, 4, (ushort)CameraIndex);
            ReadWriteUtils.Arr_WriteU16(result, 6, (ushort)CutsceneIndex);
            ReadWriteUtils.Arr_WriteU16(result, 8, (ushort)AdditionalActorCutsceneIndex);
            result[0xA] = Sound;
            result[0xB] = Unknown0B;
            ReadWriteUtils.Arr_WriteU16(result, 0xC, (ushort)HUDFade);
            result[0xE] = ReturnCameraType;
            result[0xF] = Letterbox;
            return result;
        }
    }
}
