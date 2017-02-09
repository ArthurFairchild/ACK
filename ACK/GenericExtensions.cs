using System;
using System.Collections.Generic;
using System.Linq;

namespace ACK
{
    public static class GenericExtensions
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
        /// Check if deck is a reno/kabal deck
        /// </summary>
        /// <param name="list">List of cards</param>
        /// <param name="treshhold">Number of unique cards required to clacify it as reno/kabal</param>
        /// <returns></returns>
        public static bool IsDistinct<T1>(this IList<T1> list, int treshhold = 10)
        {
            bool renoCheck = list.Count == list.Distinct().Count();
            return renoCheck && list.Count >= treshhold;
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
        public static bool IsOneOf<T1>(this T1 id, params T1[] list)
        {
            return list.Any(q => Equals(q, id));
        }
        /// <summary>
        /// Is card a spell
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsSpell(this string str)
        {
            try
            {
                return new MinimalCardTemplate(str).Type == CardType.SPELL;
            }
            catch (Exception)//wrongly parsed or nonexistent card
            {
                return false;
            }
        }
        /// <summary>
        /// Is card a minion
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsMinion(this string str)
        {
            try
            {
                return new MinimalCardTemplate(str).Type == CardType.MINION;
            }
            catch (Exception)
            {
                return false;
            }
        }
        /// <summary>
        /// Is card a weapon
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsWeapon(this string str)
        {
            try
            {
                return new MinimalCardTemplate(str).Type == CardType.WEAPON;
            }
            catch (Exception)
            {
                return false;
            }
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
        public static void AddInOrder<TKey, TValue>(this IDictionary<TKey, TValue> map, int max, IList<TKey> list,
            TValue value, params TKey[] keys)
        {
            int endpoint = 0;
            foreach (var q in keys.TakeWhile(q => endpoint < max).Where(list.Contains))
            {
                map[q] = value;
                endpoint++;
            }
        }
    }
 }
