using System;
using System.Collections.Generic;
using System.Linq;
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
}
