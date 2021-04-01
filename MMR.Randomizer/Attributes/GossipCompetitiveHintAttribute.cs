using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models.Settings;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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

            var parameter = Expression.Parameter(typeof(GameplaySettings));
            var containsMethod = typeof(List<ItemCategory>).GetMethod(nameof(List<ItemCategory>.Contains));
            var containsExpression = Expression.Call(Expression.Property(parameter, nameof(GameplaySettings.CategoriesRandomized)), containsMethod, Expression.Constant(itemCategory));
            Condition = Expression.Lambda<Func<GameplaySettings, bool>>(Expression.Equal(containsExpression, Expression.Constant(doesContain)), parameter).Compile();
        }
    }
}
