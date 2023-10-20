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
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intellect {  get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
        public int Armor { get; set; }
        public int Haste { get; set; }
        public string Country { get; set; }
        public string Money { get; set; }
        public string Description { get; set; }

        public int Actrobatic { get; set; }
        public int AnimalTaiming { get; set; }
        public int Arcana { get; set; }
        public int Athletics { get; set; }
        public int Deception { get; set; }
        public int History { get; set; }
        public int Insight { get; set; }
        public int Intimidation { get; set; }
        public int Investigation { get; set; }
        public int Medicine { get; set; }
        public int Nature { get; set; }
        public int Perception { get; set; }
        public int Performance { get; set; }
        public int Persuasion { get; set; }
        public int SleightOfHand { get; set; }
        public int Stealth {  get; set; }
        public int Survival { get; set; }

        public ObservableCollection<SkillViewModel> Skills { get; set; } = new ObservableCollection<SkillViewModel>();
        public ObservableCollection<RelationViewModel> Friends { get; set; } = new ObservableCollection<RelationViewModel>();
        public ObservableCollection<RelationViewModel> Enemies { get; set; } = new ObservableCollection<RelationViewModel>();

        public Gender Gender { get; set; }
        public Type Type { get; set; }
        public int Age { get; set; }
        public string ImagePath { get; set; }
    }
}
