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
        MagiskVæsen,
        Dyr,
        Ukendt
    }

    public enum Power
    {
        MegetSvag,
        Svag,
        Menneske,
        Trænet,
        Stærk,
        Elite,
        Mystisk,
        OP,
        BROKEN
    }

    public enum AbilityType
    {
        Ild,
        Vand,
        Is,
        Elektricitet,
        Natur,
        Healing,
        Luft
    }

    public class Forces
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int AbilityLevelRequirement { get; set; }
        public string? imagePath { get; set; }
        public AbilityType abilityType { get; set; }
        public bool IsAOE { get; set; }
    }

    public class CharacterProp
    {
        public class SkillViewModel
        {
            public string? Skill { get; set; }
        }

        public class FriendViewModel
        {
            public string? Friend { get; set; }
        }

        public class EnemyViewModel
        {
            public string? Enemy { get; set; }
        }

        //Info about character
        public string Name { get; set; }
        public string Country { get; set; }
        public string Money { get; set; }
        public string Description { get; set; }
        public string CharacterHistory { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int Armor { get; set; }
        public int Haste { get; set; }

        //Points for each main skill
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intellect {  get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }

        //Amount of points chosen for that skill
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

        //Saving throws
        public int SavingStrength { get; set; }
        public int SavingDexterity { get; set; }
        public int SavingConstitution { get; set; }
        public int SavingIntellect { get; set; }
        public int SavingWisdom { get; set; }
        public int SavingCharisma { get; set; }

        //Potential lover system
        public string LoverId { get; set; }


        public ObservableCollection<SkillViewModel> Skills { get; set; } = new ObservableCollection<SkillViewModel>();
        public ObservableCollection<FriendViewModel> Friends { get; set; } = new ObservableCollection<FriendViewModel>();
        public ObservableCollection<EnemyViewModel> Enemies { get; set; } = new ObservableCollection<EnemyViewModel>();

        public List<Forces> Abilities { get; set; } = new List<Forces>();
        public int ModifiersCombined { get; set; }
        public Gender Gender { get; set; }
        public Type Type { get; set; }
        public Power Power { get; set; }
        public int Age { get; set; }
        public string ImagePath { get; set; }
    }
}
