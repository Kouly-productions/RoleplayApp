using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public int ItemValue { get; set; }
    }

    public class CharacterProp
    {
        public CharacterProp() 
        {
            Inventories = new List<Inventory>();
        }

        public class SkillViewModel
        {
            public string? Skill { get; set; }
        }

        public class RelationViewModel
        {
            public string? Friend { get; set; }
            public string? Enemy { get; set; }
        }

        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int Deffence { get; set; }
        public int Agility { get; set; }
        public int Strength { get; set; }
        public int Intelect {  get; set; }
        public int Charisma { get; set; }
        public string Country { get; set; }
        public string Weapon {  get; set; }
        public string Money { get; set; }
        public string Description { get; set; }
        public ObservableCollection<SkillViewModel> Skills { get; set; } = new ObservableCollection<SkillViewModel>();
        public ObservableCollection<RelationViewModel> Friends { get; set; } = new ObservableCollection<RelationViewModel>();
        public ObservableCollection<RelationViewModel> Enemies { get; set; } = new ObservableCollection<RelationViewModel>();

        public Gender Gender { get; set; }
        public Type Type { get; set; }
        public int Age { get; set; }
        public string ImagePath { get; set; }
        public List<Inventory> Inventories { get; set; }
    }
}
