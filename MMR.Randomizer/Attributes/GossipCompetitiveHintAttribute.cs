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

        public GossipCompetitiveHintAttribute(int priority, string settingProperty, object settingValue)
        {
            Priority = priority;

            if (settingProperty != null)
            {
                var parameter = Expression.Parameter(typeof(GameplaySettings));
                Condition = Expression.Lambda<Func<GameplaySettings, bool>>(Expression.Equal(Expression.Property(parameter, settingProperty), Expression.Constant(settingValue)), parameter).Compile();
            }
        }

        public GossipCompetitiveHintAttribute(int priority, ItemCategory itemCategory, bool doesContain)
        {
            Priority = priority;
            Condition = settings => settings.CustomItemList.Any(item => item.ItemCategory() == itemCategory) == doesContain;
        }

        public GossipCompetitiveHintAttribute(int priority, ItemCategory itemCategory, bool doesContain, string settingFlagProperty, int flagValue, bool hasFlag)
        {
            Priority = priority;
            Func<GameplaySettings, bool> itemCategoryFunc = settings => settings.CustomItemList.Any(item => item.ItemCategory() == itemCategory) == doesContain;
            var parameter = Expression.Parameter(typeof(GameplaySettings));

            // settings => (((int)settings[settingFlagProperty] & flagValue) != 0) == hasFlag
            var flagExpression = Expression.Equal(
                Expression.NotEqual(
                    Expression.And(
                        Expression.Convert(Expression.Property(parameter, settingFlagProperty), typeof(int)),
                        Expression.Constant(flagValue)
                        ),
                    Expression.Constant(0)
                    ),
                Expression.Constant(hasFlag)
            );
            var flagFunc = Expression.Lambda<Func<GameplaySettings, bool>>(flagExpression, parameter).Compile();

            Condition = settings => itemCategoryFunc(settings) && flagFunc(settings);
        }
    }
}
