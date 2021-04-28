using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models.Settings;
using System;
using System.Linq.Expressions;
using System.Linq;
using MMR.Randomizer.Extensions;

namespace MMR.Randomizer.Attributes
{
    public class GossipCompetitiveHintAttribute : Attribute
    {
        public int Priority { get; private set; }
        public Func<GameplaySettings, bool> Condition { get; private set; }

        public GossipCompetitiveHintAttribute(int priority = 0)
        {
            Priority = priority;
        }

        public GossipCompetitiveHintAttribute(int priority, string condition, object conditionValue)
        {
            Priority = priority;

            if (condition != null)
            {
                var parameter = Expression.Parameter(typeof(GameplaySettings));
                Condition = Expression.Lambda<Func<GameplaySettings, bool>>(Expression.Equal(Expression.Property(parameter, condition), Expression.Constant(conditionValue)), parameter).Compile();
            }
        }

        public GossipCompetitiveHintAttribute(int priority, ItemCategory itemCategory, bool doesContain)
        {
            Priority = priority;
            Condition = settings => settings.CustomItemList.Any(item => item.ItemCategory() == itemCategory) == doesContain;
        }
    }
}
