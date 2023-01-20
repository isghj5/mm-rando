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

        public GossipCompetitiveHintAttribute(int priority, string settingProperty, object settingValue) : this(priority)
        {
            Condition = CreateConditionFunction(settingProperty, settingValue);
        }

        public GossipCompetitiveHintAttribute(int priority, ItemCategory itemCategory, bool doesContain) : this(priority)
        {
            Condition = CreateConditionFunction(itemCategory, doesContain);
        }

        public GossipCompetitiveHintAttribute(int priority, ItemCategory itemCategory, bool doesContain, string settingFlagProperty, int flagValue, bool hasFlag) : this(priority)
        {
            var itemCategoryFunc = CreateConditionFunction(itemCategory, doesContain);
            var flagFunc = CreateConditionFunction(settingFlagProperty, flagValue, hasFlag);
            Condition = settings => itemCategoryFunc(settings) && flagFunc(settings);
        }

        public GossipCompetitiveHintAttribute(int priority, ItemCategory itemCategory, bool doesContain, string settingProperty, object settingValue) : this(priority)
        {
            var itemCategoryFunc = CreateConditionFunction(itemCategory, doesContain);
            var settingValueFunc = CreateConditionFunction(settingProperty, settingValue);
            Condition = settings => itemCategoryFunc(settings) && settingValueFunc(settings);
        }

        private Func<GameplaySettings, bool> CreateConditionFunction(string settingFlagProperty, int flagValue, bool hasFlag)
        {
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
            return Expression.Lambda<Func<GameplaySettings, bool>>(flagExpression, parameter).Compile();
        }

        private Func<GameplaySettings, bool> CreateConditionFunction(string settingProperty, object settingValue)
        {
            var parameter = Expression.Parameter(typeof(GameplaySettings));

            // settings => settings[settingProperty] == settingValue
            var settingExpression = Expression.Equal(
                Expression.Property(parameter, settingProperty),
                Expression.Constant(settingValue)
            );
            return Expression.Lambda<Func<GameplaySettings, bool>>(settingExpression, parameter).Compile();
        }

        private Func<GameplaySettings, bool> CreateConditionFunction(ItemCategory itemCategory, bool doesContain)
        {
            return settings => settings.CustomItemList.Any(item => item.ItemCategory() == itemCategory) == doesContain;
        }
    }
}
