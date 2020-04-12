using System;

namespace MMR.Randomizer.Attributes
{
    public class ActorInitVariableAttribute : Attribute
    {

        public int InitVariable { get; set; }

        public ActorInitVariableAttribute(int initVariable)
        {
            InitVariable = initVariable;
        }
    }
}
