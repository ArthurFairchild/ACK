using System;
using System.Collections.Generic;
using SmartBot.Plugins.API;

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
    {
        public ProfileParameters GetParameters(Board board)
        {
            return new ProfileParameters(BaseProfile.Default);
        }

        public Card.Cards SirFinleyChoice(List<Card.Cards> choices)
        {
            return choices[0];
        }
    }
}