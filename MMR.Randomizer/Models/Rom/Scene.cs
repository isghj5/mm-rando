using MMR.Randomizer.Utils;
using System.Collections.Generic;

namespace MMR.Randomizer.Models.Rom
{
    public class Scene
    {
        public int File;
        public byte Number;
        public List<Map> Maps = new List<Map>();
        public List<SceneSetup> Setups { get; set; } = new List<SceneSetup>();
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
