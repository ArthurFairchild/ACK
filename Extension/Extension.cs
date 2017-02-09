using System.Collections.Generic;
using System.Linq;
using SmartBot.Database;
using SmartBot.Plugins;
using SmartBot.Plugins.API;

public static class Extension
{
    /// <summary>
    /// Simple add or update an IDictionary
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="map"></param>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> map, TKey key, TValue value)
    {
        map[key] = value;
    }
    /// <summary>
    /// Reference plugin and it's variables
    /// </summary>
    /// <param name="list">List of all plugins</param>
    /// <param name="name">Name of the plugin we want to use</param>
    /// <param name="data">Variable we want to fetch from the plugin</param>
    /// <returns></returns>
    public static object Fetch(this List<Plugin> list, string name, string data)
    {
        return list.Find(c => c.DataContainer.Name == name).GetProperties()[data];
    }
    /// <summary>
    /// Returns true if deck list contains a race, or required amount of race minions
    /// Overloaded with minimum requirment. 
    /// </summary>
    /// <param name="list">List in question</param>
    /// <param name="race">Race in questions</param>
    /// <param name="count">Min number of race members required to return true '>' </param>
    /// <returns></returns>
    public static bool HasRace(this IList<Card.Cards> list, Card.CRace race, int count = 0)
    {
        if (count == 0)
            return list.Any(q => CardTemplate.LoadFromId(q).Race == race);
        return list.Count(q => CardTemplate.LoadFromId(q).Race == race) >= count;
    }
    /// <summary>
    /// Checks if a list of cards contains a certain set
    /// </summary>
    /// <param name="list">List of Cards, most likely deck</param>
    /// <param name="items">Sets in question</param>
    /// <returns></returns>
    public static bool HasSets(this IList<Card.Cards> list, params Card.CSet[] items)
    {
        return list.Select(c => CardTemplate.LoadFromId(c).Set).Distinct().Intersect(items).Any();
    }
    /// <summary>
    /// Checks if a list of a type contains all elements of similar type (mostly Card.Cards, but works with stings as well, or Card). 
    /// Requires to have all the cards
    /// </summary>
    /// <typeparam name="T1">Type</typeparam>
    /// <param name="list">List, deck, objects</param>
    /// <param name="items">List of items we need to have</param>
    /// <returns></returns>
    public static bool ContainsAll<T1>(this IList<T1> list, params T1[] items)
    {
        return !items.Except(list).Any();
    }
    /// <summary>
    /// Checks if the list contains some elements of the same type
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <param name="list">List in question</param>
    /// <param name="items">Some variables we are looking fore</param>
    /// <returns></returns>
    public static bool ContainsSome<T1>(this IList<T1> list, params T1[] items)
    {
        return list.Intersect(items).Any();
    }
    /// <summary>
    /// Check if a list contains a min requirments of cards from a list we feed it.
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <param name="list">List of cards</param>
    /// <param name="minReq">Minimum number of items required to be in the list</param>
    /// <param name="items">Items that need to be in the list</param>
    /// <returns></returns>
    public static bool ContainsAtLeast<T1>(this IList<T1> list, int minReq, params T1[] items)
    {
        return list.Intersect(items).Count() >= minReq;
    }
    /// <summary>
    /// Count number of races in the list
    /// </summary>
    /// <param name="list">List in question</param>
    /// <param name="wCrace">Race we are looking for</param>
    /// <returns></returns>
    public static int RaceCount(this IList<Card.Cards> list, Card.CRace wCrace)
    {
        return list.Count(cards => CardTemplate.LoadFromId(cards).Race == wCrace);
    }
    /// <summary>
    /// Check if mode is arena
    /// </summary>
    /// <param name="mode"></param>
    /// <returns></returns>
    public static bool IsArena(this Bot.Mode mode)
    {
        return mode == Bot.Mode.Arena || mode == Bot.Mode.ArenaAuto;
    }
    /// <summary>
    /// Count number of cards of a specific quality
    /// </summary>
    /// <param name="list">List of cards</param>
    /// <param name="qQuality"></param>
    /// <returns></returns>
    public static int QualityCount(this IList<Card.Cards> list, Card.CQuality qQuality)
    {
        return list.Count(cards => CardTemplate.LoadFromId(cards).Quality == qQuality);
    }
    /// <summary>
    /// Check if deck is a reno/kabal deck
    /// </summary>
    /// <param name="list">List of cards</param>
    /// <param name="treshhold">Number of unique cards required to clacify it as reno/kabal</param>
    /// <returns></returns>
    public static bool IsRenoDeck(this IList<Card.Cards> list, int treshhold = 10)
    {
        bool renoCheck = list.Count == list.Distinct().Count();
        return (renoCheck && list.Count >= treshhold) || list.Contains(Cards.RenoJackson);
    }
    /// <summary>
    /// Check for C'thun interaction cards.
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool IsCthun(this IList<Card.Cards> list)
    {
        return list.ContainsSome(Cards.KlaxxiAmberWeaver, Cards.DarkArakkoa, Cards.HoodedAcolyte, Cards.TwilightDarkmender
            , Cards.BladeofCThun, Cards.UsherofSouls, Cards.AncientShieldbearer, Cards.TwilightGeomancer, Cards.DiscipleofCThun, Cards.TwilightElder
            , Cards.CThunsChosen, Cards.CrazedWorshipper, Cards.SkeramCultist, Cards.TwinEmperorVeklor, Cards.Doomcaller);
    }


    /// <summary>
    /// Adds every card to a whitelist
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="map"></param>
    /// <param name="value"></param>
    /// <param name="keys"></param>
    public static void AddAll<TKey, TValue>(this IDictionary<TKey, TValue> map, TValue value, params TKey[] keys)
    {
        foreach (TKey key in keys)
            map.AddOrUpdate(key, value);
    }

    /// <summary>
    /// Removes unwanted cards from whitelist
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="map"></param>
    /// <param name="keys"></param>
    public static void BlackListAll<TKey, TValue>(this IDictionary<TKey, TValue> map, params TKey[] keys)
    {
        foreach (TKey key in keys.Where(map.ContainsKey))
            map.Remove(key);
    }
    /// <summary>
    /// Tells if any card in a list is present in another list (more than 1 card)
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="map"></param>
    /// <param name="entry"></param>
    /// <returns></returns>
    public static bool HasAny<TKey>(this IList<TKey> map, params TKey[] entry)
    {
        return map.Intersect(entry).Any();
    }


    /// <summary>
    /// Has all
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <param name="list"></param>
    /// <param name="items"></param>
    /// <returns></returns>
    public static bool HasAll<T1>(this IList<T1> list, params T1[] items)
    {
        return !items.Except(list).Any();
    }

    /// <summary>
    /// Checks if a race is present in a list (usually choices)
    /// </summary>
    /// <param name="list"></param>
    /// <param name="race"></param>
    /// <returns></returns>
    public static bool HasRace(this IList<Card.Cards> list, Card.CRace race)
    {
        return list.Any(q => CardTemplate.LoadFromId(q).Race == race);
    }
    /// <summary>
    /// I am lazy, so I just check Card Template with an extension 
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public static bool IsMinion(this Card.Cards card)
    {
        return CardTemplate.LoadFromId(card).Type == Card.CType.MINION;
    }
    public static CardTemplate Template(this Card.Cards card)
    {
        return CardTemplate.LoadFromId(card);
    }
    /// <summary>
    /// Same as above, lazyness
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public static bool SecretClass(this Card.CClass card)
    {
        return card == Card.CClass.MAGE || card == Card.CClass.HUNTER || card == Card.CClass.PALADIN;
    }
    /// <summary>
    /// Same as above, lazyness, that is why
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public static bool WeaponClass(this Card.CClass card)
    {
        return card == Card.CClass.ROGUE || card == Card.CClass.WARRIOR || card == Card.CClass.HUNTER || card == Card.CClass.PALADIN || card == Card.CClass.SHAMAN;
    }
    /// <summary>
    /// Should I repeat myself?
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public static bool IsSpell(this Card.Cards card)
    {
        return CardTemplate.LoadFromId(card).Type == Card.CType.SPELL;
    }
    /// <summary>
    /// Check if a card is a wepon
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public static bool IsWeapon(this Card.Cards card)
    {
        return CardTemplate.LoadFromId(card).Type == Card.CType.MINION;
    }
    /// <summary>
    /// Check if the list has ramp cards. 
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool HasRamp(this List<Card.Cards> list)
    {
        return list.HasAny(Cards.Innervate, Cards.WildGrowth, Cards.JadeBlossom, Cards.DarnassusAspirant);
    }
    /// <summary>
    /// Shorten version to get cost of a card.
    /// Lazy version to avoid CardTemplate
    /// </summary>
    /// <param name="card"></param>
    /// <returns></returns>
    public static int Cost(this Card.Cards card)
    {
        return CardTemplate.LoadFromId(card).Cost;
    }
    /// <summary>
    /// Counts how many cards you have for a particular turn for that list
    /// Intended to be used with a full deck of 30 cards, otherwise it's kind of useless
    /// </summary>
    /// <param name="deck"></param>
    /// <param name="turn"></param>
    /// <returns></returns>
    public static int TurnPlayCount(this IList<Card.Cards> deck, int turn)
    {
        return deck.Count(cards => CardTemplate.LoadFromId(cards).Cost == turn);
    }
    /// <summary>
    /// Check if mode is standard.
    /// </summary>
    /// <param name="mode"></param>
    /// <returns></returns>
    public static bool IsStandard(this Bot.Mode mode)
    {
        return mode == Bot.Mode.RankedStandard || mode == Bot.Mode.UnrankedStandard;
    }
    /// <summary>
    /// Check if unknown card is in the range cost
    /// </summary>
    /// <param name="cost">Cost of card</param>
    /// <param name="min">Min bound</param>
    /// <param name="max">Upper bound</param>
    /// <returns></returns>
    public static bool Range(this int cost, int min, int max)
    {
        return cost >= min && cost <= max;
    }
    /// <summary>
    /// List itteration method, check if a card is the list.
    /// Usage card.IsOneOf(LepperGnome, Innervate).
    /// </summary>
    /// <param name="id"></param>
    /// <param name="list"></param>
    /// <returns></returns>
    public static bool IsOneOf(this Card.CClass id, params Card.CClass[] list)
    {
        return list.Any(q => q == id);
    }
    /// <summary>
    /// Whitelist cards in order
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="map">Dictionary</param>
    /// <param name="max">Number of cards to whitelist</param>
    /// <param name="list">from where</param>
    /// <param name="value">2, or 1 copy? </param>
    /// <param name="keys">List of cards we are dealing with</param>
    public static void AddInOrder<TKey, TValue>(this IDictionary<TKey, TValue> map, int max, IList<TKey> list, TValue value, params TKey[] keys)
    {
        int endpoint = 0;
        foreach (var q in keys.TakeWhile(q => endpoint < max).Where(list.Contains))
        {
            map[q] = value;
            endpoint++;
        }
    }
}