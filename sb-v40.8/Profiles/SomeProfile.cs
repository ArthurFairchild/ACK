using System;
using System.Collections.Generic;
using System.Linq;
using SmartBot.Plugins.API;
using SmartBot.Plugins;


/* Explanation on profiles :
 * 
 * All the values defined in profiles are percentage modifiers, it means that it will affect base profile's default values.
 * 
 * Modifiers values can be set within the range (-1000 - 1000)  (negative modifier has the opposite effect)
 * You can specify targets for the non-global modifiers, these target specific modifers will be added on top of global modifier + modifier for the card (without target)
 * 
 * parameters.GlobalSpellsModifier ---> Modifier applied to all spells no matter what they are. The higher is the modifier, the less likely the AI will be to play the spell
 * parameters.GlobalMinionsModifier ---> Modifier applied to all minions no matter what they are. The higher is the modifier, the less likely the AI will be to play the minion
 * 
 * parameters.GlobalAggroModifier ---> Modifier applied to enemy's health value, the higher it is, the more aggressive will be the AI
 * parameters.GlobalDefenseModifier ---> Modifier applied to friendly's health value, the higher it is, the more hp conservative will be the AI
 * 
 * parameters.SpellsModifiers ---> You can set individual modifiers to each spells there, those are ADDED to the GLOBAL modifiers !!
 * parameters.MinionsModifiers ---> You can set individual modifiers to each minions there, those are ADDED to the GLOBAL modifiers !!
 * 
 * parameters.GlobalDrawModifier ---> Modifier applied to card draw value
 * parameters.GlobalWeaponsModifier ---> Modifier applied to the value of weapons attacks
 * 
 * para.BoardFriendlyMinionsModifiers ---> Modifier applied to friendly minions on board. The higher is the modifier, the more the AI will be conservative with it and try to keep it alive.
 * para.BoardEnemyMinionsModifiers ---> Modifier applied to enemy minions on board. The higher is the modifier, the more the AI will consider it as a priority target.
 */

namespace SmartBotProfiles
{

    
    [Serializable]
    public class MidrangeDefault : Profile
    {private const Card.Cards SteadyShot = Card.Cards.DS1h_292;
        private const Card.Cards Shapeshift = Card.Cards.CS2_017;
        private const Card.Cards LifeTap = Card.Cards.CS2_056;
        private const Card.Cards Fireblast = Card.Cards.CS2_034;
        private const Card.Cards Reinforce = Card.Cards.CS2_101;
        private const Card.Cards ArmorUp = Card.Cards.CS2_102;
        private const Card.Cards LesserHeal = Card.Cards.CS1h_001;
        private const Card.Cards DaggerMastery = Card.Cards.CS2_083b;

        private readonly Dictionary<Card.Cards, int> _heroPowersPriorityTable = new Dictionary<Card.Cards, int>
        {
            {SteadyShot, 8},
            {LifeTap, 7},
            {Fireblast, 6},
			{Shapeshift, 5},
            {Reinforce, 4},
			{DaggerMastery, 3},
            {ArmorUp, 2},
            {LesserHeal, 1}
        };
		
        public ProfileParameters GetParameters(Board board)
        {
            var parameters = new ProfileParameters(BaseProfile.Default);
            if (board.EnemyClass == Card.CClass.PRIEST && /*Если враг прист */ !(board.EnemyGraveyard.Count(c=> c == Cards.ShadowWordDeath) > 1) ) //Если он использовал SW: Death
                parameters.MinionsModifiers.AddOrUpdate(Cards.RagnarostheFirelord, new Modifier(1000)); //Дефолт 100. Чем выше номер, тем меньше вероятность играть карту. 
                                                                                                        //-1000 будет играть как можно скорее
            return parameters;
        }

        public Card.Cards SirFinleyChoice(List<Card.Cards> choices)
        {
            var filteredTable = _heroPowersPriorityTable.Where(x => choices.Contains(x.Key)).ToList();
            return filteredTable.First(x => x.Value == filteredTable.Max(y => y.Value)).Key;
        }
    }
}