using System;
using MMR.Randomizer.GameObjects;

namespace MMR.Randomizer.Attributes.Actor
{
    class ObjectListIndexAttribute : Attribute
    {
        /// <summary>
        ///  this is the object list index
        ///    reminder: In this game's developer terminology, an "object" is a blob of 3d/2d model assets
        ///      models, animations, collision data, textures, etc
        ///    the game has one sequential list for objects, this value represents an index of that list
        /// </summary>

        public int Index => (int)ObjectValue;
        public GameObjects.Object ObjectValue { get; }

        public ObjectListIndexAttribute(GameObjects.Object obj)
        {
            ObjectValue = obj;
        }

        public ObjectListIndexAttribute(int objInt)
        {
            ObjectValue = (GameObjects.Object) objInt;
        }

    }
}
