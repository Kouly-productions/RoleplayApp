using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;

namespace RoleplayApp
{
    public enum Gender
    {
        Mand,
        Kvinde
    }

    public enum Type
    {
        Menneske,
        Robot,
        Drage,
        Ukendt
    }

    public class Inventory
    {
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int ItemValue { get; set; }
    }

    public class CharacterProp
    {
        public CharacterProp() 
        {
            Inventories = new List<Inventory>();
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public Gender Gender { get; set; }
        public Type Type { get; set; }
        public int Age { get; set; }
        public string ImagePath { get; set; }
        public List<Inventory> Inventories { get; set; }
    }
}
