using MMR.Randomizer.GameObjects;
using MMR.Randomizer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MMR.Randomizer.Attributes.Setting
{
    public class SettingTabAttribute : Attribute
    {

    }

    public class SettingIgnoreAttribute : Attribute
    {

    }

    public class SettingNameAttribute : Attribute
    {
        public string Name { get; set; }
        public SettingNameAttribute(string name)
        {
            Name = name;
        }
    }

    public class SettingTypeAttribute: Attribute
    {
        public string Type { get; set; }
        public SettingTypeAttribute(string type)
        {
            Type = type;
        }
    }

    public class SettingItemListAttribute : Attribute
    {
        public IEnumerable<Item> ItemList { get; }

        public SettingItemListAttribute(string itemUtilsPropertyName)
        {
            ItemList = (IEnumerable<Item>)typeof(ItemUtils).GetMethod(itemUtilsPropertyName).Invoke(null, new object[0]);
        }
    }
}
