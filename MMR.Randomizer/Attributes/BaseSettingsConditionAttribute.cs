using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models.Settings;
using System;
using System.Linq.Expressions;
using System.Linq;
using MMR.Randomizer.Extensions;
using System.Collections.Generic;

namespace MMR.Randomizer.Attributes
{
    public abstract class BaseSettingsConditionAttribute : Attribute
    {
        public Func<GameplaySettings, bool> Condition { get; protected set; }

        protected Func<GameplaySettings, bool> CreateConditionFunction(string settingFlagProperty, int flagValue, bool hasFlag)
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

        protected Func<GameplaySettings, bool> CreateConditionFunction(string settingProperty, object settingValue, bool isEqual)
        {
            var parameter = Expression.Parameter(typeof(GameplaySettings));

            // settings => (settings[settingProperty] == settingValue) == isEqual
            var settingExpression = Expression.Equal(
                Expression.Equal(
                    Expression.Property(parameter, settingProperty),
                    Expression.Constant(settingValue)
                ),
                Expression.Constant(isEqual)
            );
            return Expression.Lambda<Func<GameplaySettings, bool>>(settingExpression, parameter).Compile();
        }

        protected Func<GameplaySettings, bool> CreateConditionFunction(ItemCategory itemCategory, bool doesContain)
        {
            return settings => settings.CustomItemList.Any(item => item.ItemCategory() == itemCategory) == doesContain;
        }

        protected Func<GameplaySettings, bool> CreateConditionFunction(Item item, bool isRandomized)
        {
            return settings => settings.CustomItemList.Contains(item) == isRandomized;
        }
    }
}
