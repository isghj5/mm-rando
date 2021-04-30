using MMR.Randomizer.Extensions;
using MMR.Randomizer.Models.Vectors;
using System.Collections.Generic;

namespace MMR.Randomizer.Models.Rom
{
    public class Actor
    {
        // this is instance data, per actor, per scene.
        //  sometimes built from romdate, sometimes generated from enums
        // if you want metadata about actor types use the enum Gameobjects.Actor

        public string Name = ""; // for debug mostly, got real sick of looking up each and every actor index
        public string OldName = ""; // for debug mostly, got real sick of looking up each and every actor index
        public int ActorID; // in-game actor list index
        public GameObjects.Actor ActorEnum; // enumerator with metadata about the actor and actor extensions
        public int ObjectID; // in-game object list index
        public int ActorSize; // todo
        public int ObjectSize; // read by enemizer at scene actor reading
        public int ActorIDFlags; // we just want to keep them when re-writing, but I'm not sure they even matter
        //public int Timeflags;
        public List<int> Variants = new List<int> { 0 };
        public bool MustNotRespawn = false;
        public int Room;           // this specific actor, which map/room was it in
        public int RoomActorIndex; // the index of this actor in its room's actor list
        public int Type;
        //public int Stationary;
        public vec16 Position = new vec16();
        public vec16 Rotation = new vec16();
        public List<GameObjects.Scene> SceneExclude = new List<GameObjects.Scene>();
        public bool IsCompanion = false;
        public bool previouslyMovedCompanion = false;

        public int sceneID; // do we still need this?
        public bool modified = false;

        public void ChangeActor(GameObjects.Actor newActorType, int vars = -1)
        {
            ActorEnum   = newActorType;
            Name        = newActorType.ToString();
            ActorID     = (int)newActorType;
            ObjectID    = newActorType.ObjectIndex();
            if (vars != -1)
            {
                Variants[0] = vars;
            }
        }

        public int GetTimeFlags()
        {
            // 10 time flags, day and night for days 0 through 4, split in the flags section of the rotation shorts
            return ((this.Rotation.x & 0x3) << 7) | (this.Rotation.z & 0x7F);
        }
    }
}
