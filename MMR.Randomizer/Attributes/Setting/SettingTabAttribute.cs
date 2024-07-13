using MMR.Randomizer.Extensions;
using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Models.Colors;
using MMR.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MMR.Randomizer.Attributes.Setting
{
    public class SettingTabAttribute : Attribute
    {
        public Type TabType { get; }
        public SettingTabAttribute(Type tabType)
        {
            TabType = tabType;
        }

        public enum Type
        {
            None,
            Gimmicks
        }
    }

    public class SettingIgnoreAttribute : Attribute
    {

    }

    public class SettingNameAttribute : Attribute
    {
        public string Name { get; }
        public SettingNameAttribute(string name)
        {
            Name = name;
        }
    }

    public class SettingTypeAttribute: Attribute
    {
        public string Type { get; }
        public List<string> Values { get; }
        public SettingTypeAttribute(string type, Type valueSourceType = null, string collectionPropertyName = null)
        {
            Type = type;

            if (valueSourceType == typeof(ColorSelectionManager))
            {
                var colorSelectionManager = (ColorSelectionManager) valueSourceType.GetField(collectionPropertyName).GetValue(null);
                Values = colorSelectionManager.GetItems().Select(item => item.Name).ToList();
            }
        }
    }

    public class SettingItemListAttribute : Attribute
    {
        public IEnumerable<Item> ItemList { get; }
        public Func<Item, string> LabelExtractor { get; }
        public Dictionary<string, Func<Item, object>> AdditionalInformationExtractors { get; }

        public SettingItemListAttribute(string itemUtilsPropertyName, bool showItemName, bool showLocationName, params string[] additionalInformation)
        {
            ItemList = (IEnumerable<Item>)typeof(ItemUtils).GetMethod(itemUtilsPropertyName).Invoke(null, new object[0]);

            AdditionalInformationExtractors = additionalInformation.ToDictionary(x => x, x =>
            {
                Func<Item, object> result = item => typeof(ItemExtensions).GetMethod(x, new Type[] { typeof(Item) }).Invoke(null, new object[] { item });
                return result;
            });

            if(showItemName && showLocationName)
            {
                LabelExtractor = (item) => $"{item.Location()} ({item.Name()})";
            }
            else if (showItemName)
            {
                LabelExtractor = (item) => item.Name();
            }
            else if (showLocationName)
            {
                LabelExtractor = (item) => item.Location();
            }
        }
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class SettingExcludeAttribute : Attribute
    {
        public object PropertyValue { get; }
        public List<string> SettingPaths { get; }

        public SettingExcludeAttribute(object propertyValue, params string[] settingPaths)
        {
            PropertyValue = propertyValue;
            SettingPaths = settingPaths.ToList();
        }
    }

    public class SettingIndexValuesAttribute : Attribute
    {
        public List<string> Labels { get; }
        public SettingIndexValuesAttribute(params string[] labels)
        {
            Labels = labels.ToList();
        }
    }
}
