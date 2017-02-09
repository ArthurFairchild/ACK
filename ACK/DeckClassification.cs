using System;
using System.Collections.Generic;
using System.Linq;

namespace ACK
{

    #region Extension

    #endregion

    #region DeckClassification

    public static class DCExtension
    {
        public static bool IsHighlander<T1>(this IList<T1> list)
        {
            return list.Count == list.Distinct().Count();
        }

        public static bool IsHighlander<T1>(this IList<T1> list, bool reqAll, params T1[] types)
        {
            return reqAll ? list.IsHighlander() && list.ContainsAll(types) :
                list.ContainsSome(types);
        }

        public static bool HasRace<T1>(this IList<T1> list, T1 element, int count)
        {
            return list.Contains(element) && list.Count(arg => Equals(arg, element)) >= count;
        }
        /// <summary>
        /// This is a workaround for utilizing CardTemplate without causing compilation issue with smartbot. 
        /// </summary>
        /// <param name="hc"></param>
        /// <returns></returns>
        public static HeroClass ToHeroClass(this string hc)
        {
            return (HeroClass) Enum.Parse(typeof(HeroClass), hc);
        }
        
    }
    public class DeckClassification
    {
        /// <summary>
        /// Main Deck Styles
        /// Combo is not included as it is a sub category of other decks
        /// </summary>
        public enum Style
        {
            Aggro,
            Midrange,
            Control
        }
        /// <summary>
        /// DeckList in question
        /// </summary>
        public List<string> DeckList { get; set; }

        /// <summary>
        /// Does the deck contain pirates. Requires at least 2 pirates and Patches.
        /// </summary>
        public bool Pirates { get; private set; }

        /// <summary>
        /// Counts Number of Pirates in the deck
        /// </summary>
        public int PirateCount { get; private set; }

        /// <summary>
        /// List of Pirate Cards
        /// </summary>
        public List<string> PiratesCards { get; private set; }

        /// <summary>
        /// Does the deck contain murlocs. Does not affect mulligan. Entry exists for future expansions. 
        /// </summary>
        public bool Murlocs { get; private set; }

        /// <summary>
        /// Counts number of murlocs in the deck
        /// </summary>
        public int MurlocCount { get; private set; }

        /// <summary>
        /// List of Murloc Cards
        /// </summary>
        public List<string> MurlocCards { get; private set; }

        /// <summary>
        /// Does the deck contain Dragons, or dragon synergy AllCards. Requires "If you are holding a dragon" mechanic. 
        /// </summary>
        public bool Dragons { get; private set; }

        /// <summary>
        /// Counts number of Dragons in the deck
        /// </summary>
        public int DragonCount { get; private set; }

        /// <summary>
        /// List of Dragon Cards
        /// </summary>
        public List<string> DragonCards { get; private set; }

        /// <summary>
        /// If you are holding a dragon combo
        /// </summary>
        public List<string> HoldingDragonCards { get; private set; }

        /// <summary>
        /// Does the deck contain mechs, or mech synergy AllCards. GVG exclusive, so only !Standard decks should be identified as Mech
        /// </summary>
        public bool Mechs { get; private set; }

        /// <summary>
        /// Counts Number of Mechs in the deck
        /// </summary>
        public int MechCount { get; private set; }

        /// <summary>
        /// List of Mech Cards
        /// </summary>
        public List<string> MechCards { get; private set; }

        /// <summary>
        /// Does the deck contain strcitly unique AllCards. (Reno Style)
        /// </summary>
        public bool Kabal { get; private set; }

        /// <summary>
        /// Is the deck Standard approved. If not it's wild (duh!)
        /// Set Breakdown
        /// CORE - CORE SET
        /// EXPERT1 - CLASSIC SET
        /// MISSIONS - TUTORIAL
        /// NONE - BEN BRODE UNICORN CARDS
        /// CHEAT - ADVENTURE/BRAWL 
        /// BLANK - WHISPERS OF THE OLD GODS
        /// FP1 - NAXXRAMAS 
        /// PE1 - GOBLINS VS GNOMES
        /// BRM - BLACKROCK MOUNTEIN - FINALLY A NORMAL SIMPLIFICATION
        /// TGT - THE GRAND TOURNAMENT 
        /// HERRO_SKINS - ALTERNATE HEROES
        /// LOE - LEAGUE OF EXPLORERS
        /// BLANK - A messup by wimate, but he has a year to fix it. KARA AND MEAN STREETS OF KABAL
        /// 
        /// </summary>
        public bool Standard { get; private set; }

        /// <summary>
        /// Class of the deck. Used to pindown some archetypes.
        /// </summary>
        public HeroClass DeckClass { get; private set; }

        /// <summary>
        /// Deck Contains Jade Cards
        /// </summary>
        public bool Jade { get; private set; }

        /// <summary>
        /// Deck Contains Goons
        /// </summary>
        public bool Goons { get; private set; }

        /// <summary>
        /// Does the deck contain OTK elements win conditions. 
        /// </summary>
        public bool Combo { get; private set; }

        /// <summary>
        /// Deck Style (Aggro, Midrange, Control)
        /// All face decks are aggro.
        /// All combo decks are control. 
        /// Not debatable because it doesn't matter in 99% of the cases. It won't change mulligan, or general profile behavior. 
        /// </summary>
        public Style DeckStyle { get; private set; }

        /// <summary>
        /// Has Single Target Removal
        /// </summary>
        public bool Removal { get; private set; }

        /// <summary>
        /// Has Target Burn
        /// </summary>
        public bool Burn { get; private set; }

        /// <summary>
        /// Has AOE Removal
        /// </summary>
        public bool AOERemoval { get; private set; }

        /// <summary>
        /// Deck has Card Draw
        /// </summary>
        public bool CardDraw { get; private set; }

        /// <summary>
        /// Deck has Discover cards
        /// </summary>
        public bool Discover { get; private set; }

        /// <summary>
        /// Count Pre 5 drops. 1-4 mana cost. 
        /// </summary>
        public int Pre_5_Drops { get; private set; }

        /// <summary>
        /// Count Post 5 drops (5+)
        /// </summary>
        public int Post_5_Drops { get; private set; }

        /// <summary>
        /// Secrets
        /// </summary>
        public bool Secrets { get; private set; }

        /// <summary>
        /// List of all secrets
        /// </summary>
        public List<string> SecretsList { get; private set; }

        /// <summary>
        /// In case reno exists with duplicates
        /// </summary>
        public bool Reno { get; private set; }

        /// <summary>
        /// Count number of 1 drop cards
        /// </summary>
        public int OneDropsCount { get; private set; }

        /// <summary>
        /// List of all 1 drops
        /// </summary>
        public List<string> OneDropCards { get; private set; }

        /// <summary>
        /// Count number of 2 drop cards
        /// </summary>
        public int TwoDropsCount { get; private set; }

        /// <summary>
        /// List of all two drop cards
        /// </summary>
        public List<string> TwoDropCards { get; private set; }

        /// <summary>
        /// Count number of 3 drop cards
        /// </summary>
        public int ThreeDropsCount { get; private set; }

        /// <summary>
        /// List of all 3 drop cards
        /// </summary>
        public List<string> ThreeDropCards { get; private set; }

        /// <summary>
        /// Name of the deck
        /// Pirate Warrior, Miracle Rogue, Jade Shaman etc. 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Deck has Weapons
        /// </summary>
        public bool Weapons { get; private set; }

        /// <summary>
        /// List of all weapons
        /// </summary>
        public List<string> WeaponsList { get; set; }

        /// <summary>
        /// Count number of 4 drop cards
        /// </summary>
        public int FourDropsCount { get; private set; }

        /// <summary>
        /// List of all 4 drop cards
        /// </summary>
        public List<string> FourDropCards { get; private set; }

        private readonly List<string> _holdingDragons = new List<string>
        {
            AllCards.NetherspiteHistorian,
            AllCards.BlackwingCorruptor,
            AllCards.BlackwingTechnician,
            AllCards.TwilightGuardian,
            AllCards.BookWyrm,
            AllCards.Chillmaw,
            AllCards.RendBlackhand,
            AllCards.AlexstraszasChampion,
            AllCards.TwilightWhelp,
            AllCards.WyrmrestAgent,
            AllCards.DrakonidCrusher,
            AllCards.NightbaneTemplar
        };

        private readonly List<string> _jadePackage = new List<string>
        {
            AllCards.JadeIdol,
            AllCards.JadeBlossom,
            AllCards.JadeBehemoth,
            AllCards.JadeShuriken,
            AllCards.JadeSwarmer,
            AllCards.JadeClaws,
            AllCards.JadeLightning,
            AllCards.JadeChieftain,
            AllCards.JadeSpirit,
            AllCards.AyaBlackpaw
        };

        private readonly List<string> _handBuffPackage = new List<string>
        {
            AllCards.SmugglersCrate,
            AllCards.HiddenCache,
            AllCards.TroggBeastrager,
            AllCards.ShakyZipgunner,
            AllCards.GrimscaleChum,
            AllCards.SmugglersRun,
            AllCards.GrimestreetOutfitter,
            AllCards.GrimestreetEnforcer,
            AllCards.HobartGrapplehammer,
            AllCards.StolenGoods,
            AllCards.GrimestreetPawnbroker,
            AllCards.BrassKnuckles,
            AllCards.GrimyGadgeteer,
            AllCards.GrimestreetSmuggler,
            AllCards.DonHanCho,
        };

        private readonly List<string> _comboPackage = new List<string>
        {
            AllCards.Aviana,
            AllCards.KuntheForgottenKing,
            AllCards.IceLance,
            AllCards.Frostbolt,
            AllCards.Fireball,
            AllCards.Pyroblast,
            AllCards.Archmage,
            AllCards.FirelandsPortal,
            AllCards.EmperorThaurissan,
            AllCards.AnyfinCanHappen,
            AllCards.BlessedChampion,
            AllCards.LeeroyJenkins,
            AllCards.FacelessManipulator,
            AllCards.MindBlast,
            AllCards.InnerFire,
            AllCards.ProphetVelen,
            AllCards.ColdBlood,
            AllCards.Conceal,
            AllCards.GadgetzanAuctioneer,
            AllCards.PowerOverwhelming,
            AllCards.Darkbomb,
            AllCards.Malygos,
            AllCards.Alexstrasza,
        };

        /// <summary>
        /// All cards below can hit face
        /// </summary>
        private readonly List<string> _burnList = new List<string>
        {
            AllCards.LivingRoots,
            AllCards.Moonfire,
            AllCards.Swipe,
            AllCards.Starfire,
            AllCards.KeeperoftheGrove,
            AllCards.OntheHunt,
            AllCards.QuickShot,
            AllCards.CobraShot,
            AllCards.ArcaneShot,
            AllCards.KillCommand,
            AllCards.DispatchKodo, //LOL 
            AllCards.IceLance,
            AllCards.ForgottenTorch,
            AllCards.Fireball,
            AllCards.DragonsBreath,
            AllCards.FirelandsPortal,
            AllCards.Pyroblast,
            AllCards.Consecration,
            AllCards.HammerofWrath,
            AllCards.HolyWrath,
            AllCards.AvengingWrath, //Very random and sittuational removal for Paladin. 
            AllCards.HolySmite,
            AllCards.MindBlast,
            AllCards.Shadowbomber,
            AllCards.HolyNova,
            AllCards.HolyFire, //Ignoring Embrace the Shadow and Auchenai interractions. They have their own list
            AllCards.SinisterStrike,
            AllCards.Eviscerate,
            AllCards.JadeShuriken,
            AllCards.Shiv,
            AllCards.Headcrack,
            AllCards.SI7Agent,
            AllCards.PerditionsBlade, //Ignoring Shadow Strike as it requires full health
            AllCards.FrostShock,
            AllCards.LightningBolt,
            AllCards.Crackle,
            AllCards.LavaShock,
            AllCards.LavaBurst,
            AllCards.JadeLightning,
            AllCards.FireElemental, //
            AllCards.Soulfire,
            AllCards.Darkbomb,
            AllCards.DrainLife,
            AllCards.Hellfire,
            AllCards.BaneofDoom,
            AllCards.DreadInfernal,
            AllCards.FelfirePotion,
            AllCards.AbyssalEnforcer, //
            AllCards.Bash,
            AllCards.MortalStrike,

        };

        /// <summary>
        /// Removal that only interracts with board. 
        /// </summary>
        private readonly List<string> _removalList = new List<string>
        {
            AllCards.Wrath,
            AllCards.KeeperoftheGrove,
            AllCards.Starfall,
            AllCards.Claw,
            AllCards.Naturalize,
            AllCards.Savagery,
            AllCards.FeralRage,
            AllCards.Mulch,
            AllCards.Bite,
            AllCards.Powershot,
            AllCards.ExplosiveShot,
            AllCards.DeadlyShot,
            AllCards.ForbiddenFlame,
            AllCards.ArcaneBlast,
            AllCards.FlameLance,
            AllCards.Stormcrack,
            AllCards.MortalCoil,
            AllCards.Demonfire,
            AllCards.ShadowBolt,
            AllCards.Implosion,
            AllCards.Demonheart,
            AllCards.BloodToIchor,
            AllCards.InnerRage,
            AllCards.ShieldSlam,
            AllCards.CruelTaskmaster,
            AllCards.Slam,
        };

        /// <summary>
        /// Aoe removal that is guarranteed to take effect during play, or at the end of the turn.
        /// DEV. Note: Anomalus is too slow to trigger, so it doesn't count as a removal, while explosive sheep is easier to proc with our own hand, therefore it's on the list. 
        /// </summary>
        private readonly List<string> _aoeRemovalList = new List<string>
        {
            AllCards.Swipe,
            AllCards.Starfall,
            AllCards.PoisonSeeds,
            AllCards.Dreadscale,
            AllCards.ExplosiveShot,
            AllCards.Powershot,
            AllCards.MultiShot, //Ignoring Explosive trap because it's not guaranteed to proc
            AllCards.ArcaneExplosion,
            AllCards.TwilightFlamecaller,
            AllCards.VolcanicPotion,
            AllCards.Blizzard,
            AllCards.Flamestrike,
            AllCards.ExplosiveSheep, //Ignoring Anomalus
            AllCards.Consecration,
            AllCards.EntertheColiseum,
            AllCards.ExcavatedEvil,
            AllCards.HolyNova,
            AllCards.DragonfirePotion,
            AllCards.ShadowWordHorror,
            AllCards.FanofKnives,
            AllCards.BladeFlurry, //Ignoring Skulker, as it requires a damaged minion. 
            AllCards.ElementalDestruction,
            AllCards.LightningStorm,
            AllCards.Demonwrath,
            AllCards.Hellfire,
            AllCards.Shadowflame,
            AllCards.DreadInfernal,
            AllCards.FelfirePotion,
            AllCards.AbyssalEnforcer,
            AllCards.TwistingNether,
            AllCards.DOOM,
            AllCards.Whirlwind,
            AllCards.Revenge,
            AllCards.RavagingGhoul,
            AllCards.Brawl, //Ignoring Deaths Bite and Sleep with the Fishes. 
            AllCards.TentacleofNZoth,
            AllCards.ExplosiveSheep,
            AllCards.CorruptedSeer,
            AllCards.BaronGeddon,
            AllCards.Doomsayer,
            AllCards.Deathwing //Abomination and Chillmaw are ignored
        };

        private readonly List<string> _cardDrawList = new List<string>
        {
            AllCards.MarkofYShaarj,
            AllCards.Wrath,
            AllCards.GroveTender,
            AllCards.LunarVisions,
            AllCards.Nourish,
            AllCards.Starfire,
            AllCards.AncientofLore,
            AllCards.Tracking,
            AllCards.CallPet,
            AllCards.Flare,
            AllCards.KingsElekk,
            AllCards.QuickShot,
            AllCards.StarvingBuzzard /*sure*/,
            AllCards.ArcaneIntellect,
            AllCards.BlessingofWisdom,
            AllCards.MeanstreetMarshal,
            AllCards.DivineFavor,
            AllCards.SmallTimeRecruits,
            AllCards.HammerofWrath,
            AllCards.HolyWrath,
            AllCards.SolemnVigil,
            AllCards.LayonHands,
            AllCards.NorthshireCleric,
            AllCards.PowerWordShield,
            AllCards.Purify /*lol*/,
            AllCards.MassDispel,
            AllCards.Shiv,
            AllCards.FanofKnives,
            AllCards.ThistleTea,
            AllCards.Sprint,
            AllCards.AncestralKnowledge,
            AllCards.FarSight,
            AllCards.ManaTideTotem,
            AllCards.MalchezaarsImp,
            AllCards.MortalCoil,
            AllCards.DarkshireLibrarian,
            AllCards.SenseDemons,
            AllCards.DOOM,
            AllCards.BattleRage,
            AllCards.CommandingShout,
            AllCards.Slam,
            AllCards.ShieldBlock,
            AllCards.VarianWrynn,
            /*Sure*/ 
            AllCards.RunicEgg,
            AllCards.BloodmageThalnos,
            AllCards.LootHoarder,
            AllCards.NatPagle,
            AllCards.NoviceEngineer,
            AllCards.AcolyteofPain,
            AllCards.ColdlightOracle,
            AllCards.CultMaster,
            AllCards.GnomishInventor,
            AllCards.Jeeves,
            AllCards.PollutedHoarder,
            AllCards.AzureDrake,
            AllCards.HarrisonJones,
            AllCards.FightPromoter,
            AllCards.GadgetzanAuctioneer,
            AllCards.WindupBurglebot,
            AllCards.Wrathion,
            AllCards.TheCurator,
        };

        private readonly List<string> _discoverCardList = new List<string>
        {
            AllCards.RavenIdol,
            AllCards.EtherealConjurer,
            AllCards.ALightintheDarkness,
            AllCards.IvoryKnight,
            AllCards.MuseumCurator,
            AllCards.DrakonidOperative,
            AllCards.JourneyBelow,
            AllCards.FindersKeepers,
            AllCards.DarkPeddler,
            AllCards.IKnowaGuy,
            AllCards.GrimestreetInformant,
            AllCards.JeweledScarab,
            AllCards.NetherspiteHistorian,
            AllCards.KabalCourier,
            AllCards.GorillabotA3,
            AllCards.TombSpider,
            AllCards.LotusAgents,
            AllCards.ArchThiefRafaam,
            AllCards.Kazakus,

        };

        public DeckClassification(List<string> deckList, HeroClass dClass = HeroClass.NONE)
        {

            //DeckList = deckList.Select(q => (string)Enum.Parse(typeof(string), q)).ToList().Where(card => new MinimalCardTemplate(card).IsCollectible).ToList();
            DeckList = deckList;
            Pre_5_Drops = DeckList.Count(c => new MinimalCardTemplate(c).Cost < 5);
            Post_5_Drops = DeckList.Count(c => new MinimalCardTemplate(c).Cost >= 5);
            Burn = DeckList.Intersect(_burnList).Any();
            Removal = DeckList.Intersect(_removalList).Any() || Burn;
            //if we have burn that can go face, it can also be used to remove stuff
            AOERemoval = DeckList.Intersect(_aoeRemovalList).Any();
            CardDraw = DeckList.Intersect(_cardDrawList).Any();
            Discover = DeckList.Intersect(_discoverCardList).Any();
            Reno = DeckList.Contains(AllCards.RenoJackson);
            WeaponsList = DeckList.Select(c => c).Where(w => new MinimalCardTemplate(w).Type == CardType.WEAPON).ToList();
            Weapons = WeaponsList.Any();
            Secrets = DeckList.Any(c => new MinimalCardTemplate(c).IsSecret);
            Pirates = (DeckList.Select(c=> new MinimalCardTemplate(c).Race).ToList().HasRace(CardRace.PIRATE, 1) ||
                       DeckList.Contains(AllCards.PatchesthePirate))
                      || (DeckList.Select(c => new MinimalCardTemplate(c).Race).ToList().HasRace(CardRace.PIRATE, 2));

            Murlocs = (DeckList.Select(c => new MinimalCardTemplate(c).Race).ToList().HasRace(CardRace.MURLOC, 1) || DeckList.Contains(AllCards.AnyfinCanHappen));
            Dragons = DeckList.Intersect(_holdingDragons).Any()
                      //If deck contains one of the "Holding Dragon" cards, it's safe to assume it has dragons. 
                      || (DeckList.Select(c => new MinimalCardTemplate(c).Race).ToList().HasRace(CardRace.DRAGON, 1));
            Mechs = (DeckList.Select(c => new MinimalCardTemplate(c).Race).ToList().HasRace(CardRace.MECH, 1));
            Kabal = DeckList.Distinct().Count() == 30
                    || DeckList.ContainsSome(AllCards.Kazakus, AllCards.RenoJackson);
            PirateCount = DeckList.Count(c => new MinimalCardTemplate(c).Race == CardRace.PIRATE);
            PiratesCards =
                DeckList.Select(c => c).Where(w => new MinimalCardTemplate(w).Race == CardRace.PIRATE).ToList();
            MurlocCount = DeckList.Count(c => new MinimalCardTemplate(c).Race == CardRace.MURLOC);
            MurlocCards =
                DeckList.Select(c => c).Where(w => new MinimalCardTemplate(w).Race == CardRace.MURLOC).ToList();
            DragonCount = DeckList.Count(c => new MinimalCardTemplate(c).Race == CardRace.DRAGON);
            DragonCards =
                DeckList.Select(c => c).Where(w => new MinimalCardTemplate(w).Race == CardRace.DRAGON).ToList();
            MechCount = DeckList.Count(c => new MinimalCardTemplate(c).Race == CardRace.MECH);
            MechCards = DeckList.Select(c => c).Where(w => new MinimalCardTemplate(w).Race == CardRace.MECH).ToList();
            OneDropCards = DeckList.Select(c => c).Where(w => new MinimalCardTemplate(w).Cost == 1).ToList();
            OneDropsCount = OneDropCards.Count;
            TwoDropCards = DeckList.Select(c => c).Where(w => new MinimalCardTemplate(w).Cost == 2).ToList();
            TwoDropsCount = TwoDropCards.Count;
            ThreeDropCards = DeckList.Select(c => c).Where(w => new MinimalCardTemplate(w).Cost == 3).ToList();
            ThreeDropsCount = ThreeDropCards.Count;

            FourDropCards = DeckList.Select(c => c).Where(w => new MinimalCardTemplate(w).Cost == 4).ToList();
            FourDropsCount = FourDropCards.Count;
            Standard = !DeckList.ContainsSome("MISSIONS", "FP1", "PE1");
            DeckClass = dClass == HeroClass.NONE ? GetDeckClass(DeckList) : dClass;
            Jade = DeckClass.IsOneOf(HeroClass.SHAMAN, HeroClass.ROGUE, HeroClass.DRUID) 
                   //prevent some rng scenarios
                   && DeckList.Intersect(_jadePackage).Any(); //1 card is enough
            Goons = DeckClass.IsOneOf(HeroClass.PALADIN, HeroClass.WARRIOR, HeroClass.HUNTER)
                    && DeckList.Intersect(_handBuffPackage).Any();
            Combo = IdentifyComboByClass(DeckClass);
            DeckStyle = GetDeckStyle(DeckClass);
            Name = DeckName();

        }

        /// <summary>
        /// Returns style of the deck
        /// </summary>
        /// <param name="deckClass">Used to cancel a lot of the posibilities</param>
        /// <returns></returns>
        private Style GetDeckStyle(HeroClass deckClass)
        {
            Style possibleStyle = Style.Midrange;
            if (DeckList.Count < 2) return possibleStyle;
            Dictionary<Style, bool> possibleStyleList = new Dictionary<Style, bool>
            {
                {Style.Aggro, false},
                {Style.Midrange, false},
                {Style.Control, false},
            };
            
            switch (deckClass)//I am ashamed of this practice
            {
                case HeroClass.SHAMAN:
                    possibleStyle = Style.Midrange;

                    if (Pre_5_Drops > 24)
                    {

                        possibleStyle = Style.Aggro;
                    }
                    if (Pre_5_Drops < 24)
                        possibleStyle = Style.Midrange;
                    if (DeckList.Intersect(_aoeRemovalList).Count() > 4 && Removal || Kabal)
                    {
                        possibleStyle = Style.Control;
                        possibleStyle = Style.Aggro;
                    }
                    ;
                    break;
                case HeroClass.PRIEST:
                    possibleStyle = Dragons ? Style.Midrange : Style.Control;
                    if (Combo) possibleStyle = Style.Control;
                    break;

                case HeroClass.MAGE:

                    possibleStyle = Style.Control;
                    if (Mechs) possibleStyle = Style.Aggro;

                    if (Mechs && Burn) possibleStyle = Style.Aggro;

                    if (Kabal) possibleStyle = Style.Control;


                    break;

                case HeroClass.PALADIN:
                    possibleStyle = Style.Control;

                    if (DeckList.ContainsAtLeast(2, AllCards.ArgentSquire, AllCards.DivineFavor, AllCards.MusterforBattle,
                        AllCards.LeperGnome))
                        possibleStyle = Style.Aggro;


                    if (DeckList.Contains(AllCards.AnyfinCanHappen)) possibleStyle = Style.Control;

                    if (Goons) possibleStyle = Style.Midrange;

                    if (Dragons) possibleStyle = Style.Control;

                    if (AOERemoval) possibleStyle = Style.Control;

                    break;

                case HeroClass.WARRIOR:
                    possibleStyle = Style.Aggro;

                    if (AOERemoval) possibleStyle = Style.Control;
                    if (Pirates && Dragons) possibleStyle = Style.Midrange;

                    if (Pirates) possibleStyle = Style.Aggro;

                    break;

                case HeroClass.WARLOCK:
                    possibleStyle = Style.Control;

                    if (Pre_5_Drops > 24) possibleStyle = Style.Aggro;

                    break;

                case HeroClass.HUNTER:
                    possibleStyle = Style.Midrange;
                    if ((OneDropsCount + TwoDropsCount + ThreeDropsCount) > 20)
                        possibleStyle = Style.Aggro;
                    if (DeckList.ContainsAtLeast(2, AllCards.Secretkeeper, AllCards.ExplosiveTrap, AllCards.ArgentSquire) ||
                        Pirates)
                    {
                        possibleStyle = Style.Aggro;
                        possibleStyle = Style.Midrange;
                    }

                    break;

                case HeroClass.ROGUE:
                    possibleStyle = Style.Control;

                    if (Pre_5_Drops > 24) possibleStyle = Style.Aggro;
                    if (Pirates && Jade && Combo) possibleStyle = Style.Control;

                    break;

                case HeroClass.DRUID:

                    possibleStyle = Style.Control;
                    if (Pirates) possibleStyle = Style.Aggro;
                    if (DeckList.ContainsAtLeast(2, AllCards.PoweroftheWild, AllCards.HauntedCreeper, AllCards.EchoingOoze,
                        AllCards.ArgentSquire)) possibleStyle = Style.Aggro;
                    if (Pre_5_Drops > 24 && DeckList.Count == 30) possibleStyle = Style.Aggro;
                    if (Jade) possibleStyle = Style.Control;
                    break;

                case HeroClass.NONE:
                    possibleStyle = Style.Midrange;
                    break;

                case HeroClass.JARAXXUS:
                    possibleStyle = Style.Midrange;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(deckClass), deckClass, null);
            }
            foreach (var q in possibleStyleList)
                if (q.Value)
                    possibleStyle = q.Key;
            if (Kabal && DeckList.IsHighlander()) possibleStyle = Style.Control;

            return possibleStyle;

        }

        /// <summary>
        /// Private Combo Checker. Separated by classes. WIP
        /// </summary>
        /// <param name="deckClass"></param>
        /// <returns></returns>
        private bool IdentifyComboByClass(HeroClass deckClass)
        {
            switch (deckClass)
            {
                case HeroClass.SHAMAN:
                    if (DeckList.ContainsAll(AllCards.Aviana, AllCards.KuntheForgottenKing)) return true;
                    return DeckList.Intersect(_comboPackage).Count() >= 2;
                case HeroClass.PRIEST:
                    return DeckList.Intersect(_comboPackage).Count() >= 2;

                case HeroClass.MAGE:
                    return DeckList.Intersect(_comboPackage).Count() >= 2;

                case HeroClass.PALADIN:
                    if (DeckList.Contains(AllCards.AnyfinCanHappen)) return true; //if true, it must have combo murlocs
                    return DeckList.Intersect(_comboPackage).Count() > 1;

                case HeroClass.WARRIOR:
                    return DeckList.Intersect(_comboPackage).Count() >= 2;

                case HeroClass.WARLOCK:
                    return DeckList.Intersect(_comboPackage).Count() >= 2;

                case HeroClass.HUNTER:
                    return DeckList.Intersect(_comboPackage).Count() >= 2;

                case HeroClass.ROGUE:
                    return DeckList.Intersect(_comboPackage).Count() >= 2;

                case HeroClass.DRUID:
                    return DeckList.Intersect(_comboPackage).Count() >= 2;

                case HeroClass.NONE:
                    return false;

                case HeroClass.JARAXXUS:
                    return false;

                default:
                    throw new ArgumentOutOfRangeException(nameof(deckClass), deckClass, null);
            }
        }

        private static HeroClass GetDeckClass(List<string> deckList)
        {
            foreach (var q in deckList)
            {
                if (new MinimalCardTemplate(q).CardClass != HeroClass.NONE)
                    //itterate until we meet first non neutral card. Ignores triclass cards
                    return new MinimalCardTemplate(q).CardClass;
            }
            return HeroClass.NONE; //full neutral deck. Really screws over identifier

        }

        private string DeckName()
        {
            string possibleDeck = "";
            switch (DeckClass)
            {
                case HeroClass.SHAMAN:
                    switch (DeckStyle)
                    {
                        case Style.Aggro:
                            possibleDeck = "Aggro";
                            if (Pirates) possibleDeck = "Face";
                            if (Jade && Pirates) possibleDeck = "Jade Pirate";
                            if (Burn) possibleDeck = "Face";
                            break;
                        case Style.Midrange:
                            possibleDeck = Jade ? "Jade" : "";
                            break;
                        case Style.Control:
                            possibleDeck = "";
                            if (Reno) possibleDeck = "Reno";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case HeroClass.PRIEST:
                    switch (DeckStyle)
                    {
                        case Style.Aggro:
                            possibleDeck = "Weird aggro";
                            break;
                        case Style.Midrange:
                            possibleDeck = Dragons ? "Dragon" : Kabal ? "Kabal" : "Control";
                            break;
                        case Style.Control:
                            possibleDeck = Kabal ? "Kabal" : Dragons ? "Dragon" : "";
                            if (Reno) possibleDeck = "Reno";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case HeroClass.MAGE:
                    switch (DeckStyle)
                    {
                        case Style.Aggro:
                            possibleDeck = Burn ? "Face Freeze" : "Tempo";
                            break;
                        case Style.Midrange:
                            possibleDeck = Burn ? "Tempo" : "Control";
                            break;
                        case Style.Control:

                            possibleDeck = "Freeze";
                            if (Combo) possibleDeck = "Freeze";
                            if (Kabal) possibleDeck = "Kabal";
                            if (Reno) possibleDeck = "Reno";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case HeroClass.PALADIN:
                    switch (DeckStyle)
                    {
                        case Style.Aggro:
                            possibleDeck = "";

                            break;
                        case Style.Midrange:
                            possibleDeck = "";
                            if (Secrets) possibleDeck = "Secret";
                            break;
                        case Style.Control:
                            possibleDeck = "";
                            if (Reno) possibleDeck = "Reno";
                            if (Combo && DeckList.Contains(AllCards.AnyfinCanHappen)) possibleDeck = "OTK";

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case HeroClass.WARRIOR:
                    switch (DeckStyle)
                    {
                        case Style.Aggro:
                            possibleDeck = "Pirate"; //default
                            if (Pirates && !CardDraw) possibleDeck = "Face";
                            if (Pirates && Burn) possibleDeck = "Face Pirate";
                            if (Pirates && Dragons) possibleDeck = "Pirate Dragon";

                            break;
                        case Style.Midrange:
                            possibleDeck = "Dragon";
                            if (Pirates && Dragons) possibleDeck = "Dragon";

                            break;
                        case Style.Control:
                            possibleDeck = "";
                            if (AOERemoval && Removal && CardDraw) possibleDeck = "";
                            if (Dragons && (AOERemoval && Removal && CardDraw)) possibleDeck = "Dragon";
                            if (Reno) possibleDeck = "Kabal";



                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case HeroClass.WARLOCK:
                    switch (DeckStyle)
                    {
                        case Style.Aggro:
                            possibleDeck = "Zoo";
                            break;
                        case Style.Midrange:
                            possibleDeck = "Zoo";
                            break;
                        case Style.Control:
                            possibleDeck = "Reno";
                            if (Combo) possibleDeck = "Combo";
                            if (Kabal) possibleDeck = "Kabal";
                            if (Combo && Kabal) possibleDeck = "Combo Kabal";
                            if (Reno) possibleDeck = "Kabal";
                            if (Reno && Kabal) possibleDeck = "Kabal";
                            if (Reno && Kabal && Combo) possibleDeck = "Combo Reno";



                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case HeroClass.HUNTER:
                    switch (DeckStyle)
                    {

                        case Style.Aggro:
                            possibleDeck = "";
                            break;
                        case Style.Midrange:
                            possibleDeck = "";
                            if (Secrets) possibleDeck = "Secret";
                            break;
                        case Style.Control:
                            possibleDeck = "";
                            if (Reno) possibleDeck = "Reno";

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case HeroClass.ROGUE:
                    switch (DeckStyle)
                    {
                        case Style.Aggro:
                            possibleDeck = "Aggro";
                            if (DeckList.ContainsAll(AllCards.GadgetzanAuctioneer, AllCards.Conceal))
                                possibleDeck = "Miracle ";
                            break;
                        case Style.Midrange:
                            possibleDeck = "Miracle";
                            break;
                        case Style.Control:
                            possibleDeck = "Miracle";
                            if (Reno) possibleDeck = "Reno";
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case HeroClass.DRUID:
                    switch (DeckStyle)
                    {
                        case Style.Aggro:
                            possibleDeck = "";
                            break;
                        case Style.Midrange:
                            possibleDeck = "";
                            if (Jade) possibleDeck = "Jade";
                            break;
                        case Style.Control:
                            if (Jade) possibleDeck = "Jade";
                            if (Reno) possibleDeck = "Reno";
                            if (Jade && DeckList.Contains(AllCards.GadgetzanAuctioneer))
                                possibleDeck = "Infinite Jade Value";

                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case HeroClass.NONE:
                    switch (DeckStyle)
                    {
                        case Style.Aggro:
                            break;
                        case Style.Midrange:
                            break;
                        case Style.Control:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case HeroClass.JARAXXUS:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return string.Format("{0} {1} {2}", DeckStyle, possibleDeck, DeckClass);
        }

        /// <summary>
        /// ToString that prints out Relevent to the deck information
        /// </summary>
        /// <returns></returns>
        public string ClassificationSummary()
        {
            return $"{nameof(Pirates)}: {Pirates}\t" +
                   String.Join(", ",
                       DeckList.Distinct()
                           .Where(c => new MinimalCardTemplate(c).Race == CardRace.PIRATE)
                           .Select(w => new MinimalCardTemplate(w).Name)) +
                   $"\n{nameof(PirateCount)}: {PirateCount}\t" +
                   $"\n{nameof(Murlocs)}:{Murlocs}\t" +
                   String.Join(", ",
                       DeckList.Distinct()
                           .Where(c => new MinimalCardTemplate(c).Race == CardRace.MURLOC)
                           .Select(w => new MinimalCardTemplate(w).Name)) +
                   $"\n{nameof(MurlocCount)}: {MurlocCount}\t" +
                   $"\n{nameof(Dragons)}:{Dragons}\t" +
                   String.Join(", ",
                       DeckList.Distinct()
                           .Where(c => new MinimalCardTemplate(c).Race == CardRace.DRAGON)
                           .Select(w => new MinimalCardTemplate(w).Name)) +
                   $"\n{nameof(DragonCount)}: {DragonCount}\t" +
                   $"\n{nameof(Mechs)}:{Mechs}\t" +
                   String.Join(", ",
                       DeckList.Where(c => new MinimalCardTemplate(c).Race == CardRace.MECH)
                           .Select(w => new MinimalCardTemplate(w).Name)) +
                   $"\n{nameof(MechCount)}: {MechCount}\t" +
                   $"\n{nameof(Kabal)}:{Kabal}\t" + "" +
                   $"\n{nameof(Standard)}:{Standard}\t" +
                   $"\n{nameof(DeckClass)}:{DeckClass}\t" +
                   $"\n{nameof(Jade)}:{Jade}\t" +
                   String.Join(", ", DeckList.Intersect(_jadePackage).Select(w => new MinimalCardTemplate(w).Name)) +
                   $"\n{nameof(Goons)}:{Goons}\t" +
                   $"\n{nameof(Combo)}:{Combo}\t" +
                   String.Join(", ", DeckList.Intersect(_comboPackage).Select(w => new MinimalCardTemplate(w).Name)) +
                   $"\n{nameof(DeckStyle)}:{DeckStyle}\t" +
                   $"\n{nameof(Removal)}:{Removal}\t" +
                   String.Join(", ", DeckList.Intersect(_removalList).Select(w => new MinimalCardTemplate(w).Name)) +
                   $"\n{nameof(Burn)}:{Burn}\t\t" +
                   String.Join(", ", DeckList.Intersect(_burnList).Select(w => new MinimalCardTemplate(w).Name)) +
                   $"\n{nameof(AOERemoval)}:{AOERemoval}\t" +
                   String.Join(", ", DeckList.Intersect(_aoeRemovalList).Select(w => new MinimalCardTemplate(w).Name)) +
                   $"\n{nameof(CardDraw)}:{CardDraw}\t" +
                   String.Join(", ", DeckList.Intersect(_cardDrawList).Select(w => new MinimalCardTemplate(w).Name)) +
                   $"\n{nameof(Discover)}:{Discover}\t" +
                   String.Join(", ", DeckList.Intersect(_discoverCardList).Select(w => new MinimalCardTemplate(w).Name)) +
                   $"\n{nameof(Pre_5_Drops)}:{Pre_5_Drops}\t" +
                   $"\n{nameof(Post_5_Drops)}:{Post_5_Drops}\t" +
                   $"\n{nameof(Secrets)}:{Secrets}\t " +
                   String.Join(", ",
                       DeckList.Where(c => new MinimalCardTemplate(c).IsSecret)
                           .Select(w => new MinimalCardTemplate(w).Name));
        }


        /// <summary>
        /// ToString method that reports all Mulligan relevant information
        /// </summary>
        /// <returns></returns>
        public string MulliganCoreSummary()
        {
            return $"{nameof(Reno)}: {Reno}," +
                   $"\n{nameof(OneDropsCount)}: {OneDropsCount}," +
                   $"\n{nameof(TwoDropsCount)}: {TwoDropsCount}," +
                   $"\n{nameof(ThreeDropsCount)}: {ThreeDropsCount}," +
                   $"\n{nameof(FourDropsCount)}: {FourDropsCount}, " +
                   $"\n{nameof(Weapons)}: {Weapons} {String.Join(", ", WeaponsList.Select(c => new MinimalCardTemplate(c).Name))}" +
                   $"\n{nameof(OneDropCards)}: " + String.Join(", ", OneDropCards.Select(c =>  new MinimalCardTemplate(c).Name)) +
                   $"\n{nameof(TwoDropCards)}: " + String.Join(", ", TwoDropCards.Select(c =>  new MinimalCardTemplate(c).Name)) +
                   $"\n{nameof(ThreeDropCards)}: " + String.Join(", ", ThreeDropCards.Select(c =>  new MinimalCardTemplate(c).Name)) +
                   $"\n{nameof(FourDropCards)}: " + String.Join(", ", FourDropCards.Select(c =>  new MinimalCardTemplate(c).Name));


        }
    }

    #endregion




}
