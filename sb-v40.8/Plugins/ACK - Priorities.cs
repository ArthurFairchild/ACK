using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using SmartBot.Plugins.API;


namespace SmartBot.Plugins
{
    [Serializable]
    public class ACKPriority : PluginDataContainer
    {

        [DisplayName("Wisp")]
        public int CS2_231 { get; set; }
        [DisplayName("Murloc Tinyfin")]
        public int LOEA10_3 { get; set; }
        [DisplayName("Target Dummy")]
        public int GVG_093 { get; set; }
        [DisplayName("Goldshire Footman")]
        public int CS1_042 { get; set; }
        [DisplayName("Voidwalker")]
        public int CS2_065 { get; set; }
        [DisplayName("Murloc Raider")]
        public int CS2_168 { get; set; }
        [DisplayName("Stonetusk Boar")]
        public int CS2_171 { get; set; }
        [DisplayName("Elven Archer")]
        public int CS2_189 { get; set; }
        [DisplayName("Northshire Cleric")]
        public int CS2_235 { get; set; }
        [DisplayName("Timber Wolf")]
        public int DS1_175 { get; set; }
        [DisplayName("Voodoo Doctor")]
        public int EX1_011 { get; set; }
        [DisplayName("Grimscale Oracle")]
        public int EX1_508 { get; set; }
        [DisplayName("Blood Imp")]
        public int CS2_059 { get; set; }
        [DisplayName("Southsea Deckhand")]
        public int CS2_146 { get; set; }
        [DisplayName("Young Dragonhawk")]
        public int CS2_169 { get; set; }
        [DisplayName("Abusive Sergeant")]
        public int CS2_188 { get; set; }
        [DisplayName("Lightwarden")]
        public int EX1_001 { get; set; }
        [DisplayName("Young Priestess")]
        public int EX1_004 { get; set; }
        [DisplayName("Argent Squire")]
        public int EX1_008 { get; set; }
        [DisplayName("Angry Chicken")]
        public int EX1_009 { get; set; }
        [DisplayName("Worgen Infiltrator")]
        public int EX1_010 { get; set; }
        [DisplayName("Leper Gnome")]
        public int EX1_029 { get; set; }
        [DisplayName("Secretkeeper")]
        public int EX1_080 { get; set; }
        [DisplayName("Dust Devil")]
        public int EX1_243 { get; set; }
        [DisplayName("Flame Imp")]
        public int EX1_319 { get; set; }
        [DisplayName("Shieldbearer")]
        public int EX1_405 { get; set; }
        [DisplayName("Murloc Tidecaller")]
        public int EX1_509 { get; set; }
        [DisplayName("Mana Wyrm")]
        public int NEW1_012 { get; set; }
        [DisplayName("Hungry Crab")]
        public int NEW1_017 { get; set; }
        [DisplayName("Bloodsail Corsair")]
        public int NEW1_025 { get; set; }
        [DisplayName("Pit Snake")]
        public int LOE_010 { get; set; }
        [DisplayName("Tunnel Trogg")]
        public int LOE_018 { get; set; }
        [DisplayName("Sir Finley Mrrgglton")]
        public int LOE_076 { get; set; }
        [DisplayName("Reliquary Seeker")]
        public int LOE_116 { get; set; }
        [DisplayName("Zombie Chow")]
        public int FP1_001 { get; set; }
        [DisplayName("Webspinner")]
        public int FP1_011 { get; set; }
        [DisplayName("Undertaker")]
        public int FP1_028 { get; set; }
        [DisplayName("Shadowbomber")]
        public int GVG_009 { get; set; }
        [DisplayName("Cogmaster")]
        public int GVG_013 { get; set; }
        [DisplayName("Warbot")]
        public int GVG_051 { get; set; }
        [DisplayName("Clockwork Gnome")]
        public int GVG_082 { get; set; }
        [DisplayName("Twilight Whelp")]
        public int BRM_004 { get; set; }
        [DisplayName("Dragon Egg")]
        public int BRM_022 { get; set; }
        [DisplayName("Buccaneer")]
        public int AT_029 { get; set; }
        [DisplayName("Brave Archer")]
        public int AT_059 { get; set; }
        [DisplayName("Lowly Squire")]
        public int AT_082 { get; set; }
        [DisplayName("Tournament Attendee")]
        public int AT_097 { get; set; }
        [DisplayName("Injured Kvaldir")]
        public int AT_105 { get; set; }
        [DisplayName("Gadgetzan Jouster")]
        public int AT_133 { get; set; }
        [DisplayName("Forbidden Ancient")]
        public int OG_051 { get; set; }
        [DisplayName("Possessed Villager")]
        public int OG_241 { get; set; }
        [DisplayName("Fiery Bat")]
        public int OG_179 { get; set; }
        [DisplayName("Selfless Hero")]
        public int OG_221 { get; set; }
        [DisplayName("Vilefin Inquisitor")]
        public int OG_006 { get; set; }
        [DisplayName("N'Zoth's First Mate")]
        public int OG_312 { get; set; }
        [DisplayName("Shifter Zerus")]
        public int OG_123 { get; set; }
        [DisplayName("Tentacle of N'Zoth")]
        public int OG_151 { get; set; }
        [DisplayName("Zealous Initiate")]
        public int OG_158 { get; set; }
        [DisplayName("Bladed Cultist")]
        public int OG_070 { get; set; }
        [DisplayName("Enchanted Raven")]
        public int KAR_300 { get; set; }
        [DisplayName("Babbling Book")]
        public int KAR_009 { get; set; }
        [DisplayName("Swashburglar")]
        public int KAR_069 { get; set; }
        [DisplayName("Malchezaar's Imp")]
        public int KAR_089 { get; set; }
        [DisplayName("Arcane Anomaly")]
        public int KAR_036 { get; set; }
        [DisplayName("Runic Egg")]
        public int KAR_029 { get; set; }
        [DisplayName("Alleycat")]
        public int CFM_315 { get; set; }
        [DisplayName("Kabal Lackey")]
        public int CFM_066 { get; set; }
        [DisplayName("Grimscale Chum")]
        public int CFM_650 { get; set; }
        [DisplayName("Meanstreet Marshal")]
        public int CFM_759 { get; set; }
        [DisplayName("Mistress of Mixtures")]
        public int CFM_120 { get; set; }
        [DisplayName("Patches the Pirate")]
        public int CFM_637 { get; set; }
        [DisplayName("Small-Time Buccaneer")]
        public int CFM_325 { get; set; }
        [DisplayName("Weasel Tunneler")]
        public int CFM_095 { get; set; }
        [DisplayName("River Crocolisk")]
        public int CS2_120 { get; set; }
        [DisplayName("Frostwolf Grunt")]
        public int CS2_121 { get; set; }
        [DisplayName("Kobold Geomancer")]
        public int CS2_142 { get; set; }
        [DisplayName("Bloodfen Raptor")]
        public int CS2_172 { get; set; }
        [DisplayName("Bluegill Warrior")]
        public int CS2_173 { get; set; }
        [DisplayName("Novice Engineer")]
        public int EX1_015 { get; set; }
        [DisplayName("Acidic Swamp Ooze")]
        public int EX1_066 { get; set; }
        [DisplayName("Succubus")]
        public int EX1_306 { get; set; }
        [DisplayName("Murloc Tidehunter")]
        public int EX1_506 { get; set; }
        [DisplayName("Flametongue Totem")]
        public int EX1_565 { get; set; }
        [DisplayName("Bloodmage Thalnos")]
        public int EX1_012 { get; set; }
        [DisplayName("Ancient Watcher")]
        public int EX1_045 { get; set; }
        [DisplayName("Youthful Brewmaster")]
        public int EX1_049 { get; set; }
        [DisplayName("Mana Addict")]
        public int EX1_055 { get; set; }
        [DisplayName("Sunfury Protector")]
        public int EX1_058 { get; set; }
        [DisplayName("Crazed Alchemist")]
        public int EX1_059 { get; set; }
        [DisplayName("Pint-Sized Summoner")]
        public int EX1_076 { get; set; }
        [DisplayName("Mad Bomber")]
        public int EX1_082 { get; set; }
        [DisplayName("Loot Hoarder")]
        public int EX1_096 { get; set; }
        [DisplayName("Lorewalker Cho")]
        public int EX1_100 { get; set; }
        [DisplayName("Defias Ringleader")]
        public int EX1_131 { get; set; }
        [DisplayName("Dire Wolf Alpha")]
        public int EX1_162 { get; set; }
        [DisplayName("Lightwell")]
        public int EX1_341 { get; set; }
        [DisplayName("Argent Protector")]
        public int EX1_362 { get; set; }
        [DisplayName("Amani Berserker")]
        public int EX1_393 { get; set; }
        [DisplayName("Armorsmith")]
        public int EX1_402 { get; set; }
        [DisplayName("Patient Assassin")]
        public int EX1_522 { get; set; }
        [DisplayName("Scavenging Hyena")]
        public int EX1_531 { get; set; }
        [DisplayName("Nat Pagle")]
        public int EX1_557 { get; set; }
        [DisplayName("Cruel Taskmaster")]
        public int EX1_603 { get; set; }
        [DisplayName("Sorcerer's Apprentice")]
        public int EX1_608 { get; set; }
        [DisplayName("Mana Wraith")]
        public int EX1_616 { get; set; }
        [DisplayName("Bloodsail Raider")]
        public int NEW1_018 { get; set; }
        [DisplayName("Knife Juggler")]
        public int NEW1_019 { get; set; }
        [DisplayName("Wild Pyromancer")]
        public int NEW1_020 { get; set; }
        [DisplayName("Doomsayer")]
        public int NEW1_021 { get; set; }
        [DisplayName("Faerie Dragon")]
        public int NEW1_023 { get; set; }
        [DisplayName("Millhouse Manastorm")]
        public int NEW1_029 { get; set; }
        [DisplayName("Master Swordsmith")]
        public int NEW1_037 { get; set; }
        [DisplayName("Museum Curator")]
        public int LOE_006 { get; set; }
        [DisplayName("Dark Peddler")]
        public int LOE_023 { get; set; }
        [DisplayName("Jeweled Scarab")]
        public int LOE_029 { get; set; }
        [DisplayName("Huge Toad")]
        public int LOE_046 { get; set; }
        [DisplayName("Captain's Parrot")]
        public int NEW1_016 { get; set; }
        [DisplayName("Haunted Creeper")]
        public int FP1_002 { get; set; }
        [DisplayName("Echoing Ooze")]
        public int FP1_003 { get; set; }
        [DisplayName("Mad Scientist")]
        public int FP1_004 { get; set; }
        [DisplayName("Nerubian Egg")]
        public int FP1_007 { get; set; }
        [DisplayName("Nerub'ar Weblord")]
        public int FP1_017 { get; set; }
        [DisplayName("Unstable Ghoul")]
        public int FP1_024 { get; set; }
        [DisplayName("Snowchugger")]
        public int GVG_002 { get; set; }
        [DisplayName("Mechwarper")]
        public int GVG_006 { get; set; }
        [DisplayName("Shrinkmeister")]
        public int GVG_011 { get; set; }
        [DisplayName("Mistress of Pain")]
        public int GVG_018 { get; set; }
        [DisplayName("Goblin Auto-Barber")]
        public int GVG_023 { get; set; }
        [DisplayName("One-eyed Cheat")]
        public int GVG_025 { get; set; }
        [DisplayName("Anodized Robo Cub")]
        public int GVG_030 { get; set; }
        [DisplayName("Whirling Zap-o-matic")]
        public int GVG_037 { get; set; }
        [DisplayName("Vitality Totem")]
        public int GVG_039 { get; set; }
        [DisplayName("Shielded Minibot")]
        public int GVG_058 { get; set; }
        [DisplayName("Puddlestomper")]
        public int GVG_064 { get; set; }
        [DisplayName("Stonesplinter Trogg")]
        public int GVG_067 { get; set; }
        [DisplayName("Shadowboxer")]
        public int GVG_072 { get; set; }
        [DisplayName("Ship's Cannon")]
        public int GVG_075 { get; set; }
        [DisplayName("Explosive Sheep")]
        public int GVG_076 { get; set; }
        [DisplayName("Gilblin Stalker")]
        public int GVG_081 { get; set; }
        [DisplayName("Annoy-o-Tron")]
        public int GVG_085 { get; set; }
        [DisplayName("Steamwheedle Sniper")]
        public int GVG_087 { get; set; }
        [DisplayName("Micro Machine")]
        public int GVG_103 { get; set; }
        [DisplayName("Recombobulator")]
        public int GVG_108 { get; set; }
        [DisplayName("Fallen Hero")]
        public int AT_003 { get; set; }
        [DisplayName("Tiny Knight of Evil")]
        public int AT_021 { get; set; }
        [DisplayName("Wrathguard")]
        public int AT_026 { get; set; }
        [DisplayName("Undercity Valiant")]
        public int AT_030 { get; set; }
        [DisplayName("Cutpurse")]
        public int AT_031 { get; set; }
        [DisplayName("Darnassus Aspirant")]
        public int AT_038 { get; set; }
        [DisplayName("Druid of the Saber")]
        public int AT_042 { get; set; }
        [DisplayName("Totem Golem")]
        public int AT_052 { get; set; }
        [DisplayName("King's Elekk")]
        public int AT_058 { get; set; }
        [DisplayName("Sparring Partner")]
        public int AT_069 { get; set; }
        [DisplayName("Alexstrasza's Champion")]
        public int AT_071 { get; set; }
        [DisplayName("Garrison Commander")]
        public int AT_080 { get; set; }
        [DisplayName("Lance Carrier")]
        public int AT_084 { get; set; }
        [DisplayName("Boneguard Lieutenant")]
        public int AT_089 { get; set; }
        [DisplayName("Flame Juggler")]
        public int AT_094 { get; set; }
        [DisplayName("Argent Watchman")]
        public int AT_109 { get; set; }
        [DisplayName("Wyrmrest Agent")]
        public int AT_116 { get; set; }
        [DisplayName("Cult Sorcerer")]
        public int OG_303 { get; set; }
        [DisplayName("Undercity Huckster")]
        public int OG_330 { get; set; }
        [DisplayName("Eternal Sentinel")]
        public int OG_026 { get; set; }
        [DisplayName("Darkshire Librarian")]
        public int OG_109 { get; set; }
        [DisplayName("Bilefin Tidehunter")]
        public int OG_156 { get; set; }
        [DisplayName("Duskboar")]
        public int OG_326 { get; set; }
        [DisplayName("Nat, the Darkfisher")]
        public int OG_338 { get; set; }
        [DisplayName("Beckoner of Evil")]
        public int OG_281 { get; set; }
        [DisplayName("Twilight Geomancer")]
        public int OG_284 { get; set; }
        [DisplayName("Twisted Worgen")]
        public int OG_247 { get; set; }
        [DisplayName("Kindly Grandmother")]
        public int KAR_005 { get; set; }
        [DisplayName("Medivh's Valet")]
        public int KAR_092 { get; set; }
        [DisplayName("Netherspite Historian")]
        public int KAR_062 { get; set; }
        [DisplayName("Pompous Thespian")]
        public int KAR_011 { get; set; }
        [DisplayName("Trogg Beastrager")]
        public int CFM_338 { get; set; }
        [DisplayName("Grimestreet Outfitter")]
        public int CFM_753 { get; set; }
        [DisplayName("Mana Geode")]
        public int CFM_606 { get; set; }
        [DisplayName("Gadgetzan Ferryman")]
        public int CFM_693 { get; set; }
        [DisplayName("Jade Swarmer")]
        public int CFM_691 { get; set; }
        [DisplayName("Hobart Grapplehammer")]
        public int CFM_643 { get; set; }
        [DisplayName("Public Defender")]
        public int CFM_300 { get; set; }
        [DisplayName("Blowgill Sniper")]
        public int CFM_647 { get; set; }
        [DisplayName("Dirty Rat")]
        public int CFM_790 { get; set; }
        [DisplayName("Friendly Bartender")]
        public int CFM_654 { get; set; }
        [DisplayName("Gadgetzan Socialite")]
        public int CFM_659 { get; set; }
        [DisplayName("Grimestreet Informant")]
        public int CFM_321 { get; set; }
        [DisplayName("Ironbeak Owl")]
        public int CS2_203 { get; set; }
        [DisplayName("Magma Rager")]
        public int CS2_118 { get; set; }
        [DisplayName("Raid Leader")]
        public int CS2_122 { get; set; }
        [DisplayName("Wolfrider")]
        public int CS2_124 { get; set; }
        [DisplayName("Ironfur Grizzly")]
        public int CS2_125 { get; set; }
        [DisplayName("Silverback Patriarch")]
        public int CS2_127 { get; set; }
        [DisplayName("Ironforge Rifleman")]
        public int CS2_141 { get; set; }
        [DisplayName("Razorfen Hunter")]
        public int CS2_196 { get; set; }
        [DisplayName("Shattered Sun Cleric")]
        public int EX1_019 { get; set; }
        [DisplayName("Warsong Commander")]
        public int EX1_084 { get; set; }
        [DisplayName("Dalaran Mage")]
        public int EX1_582 { get; set; }
        [DisplayName("Earthen Ring Farseer")]
        public int CS2_117 { get; set; }
        [DisplayName("Injured Blademaster")]
        public int CS2_181 { get; set; }
        [DisplayName("Alarm-o-Bot")]
        public int EX1_006 { get; set; }
        [DisplayName("Acolyte of Pain")]
        public int EX1_007 { get; set; }
        [DisplayName("King Mukla")]
        public int EX1_014 { get; set; }
        [DisplayName("Jungle Panther")]
        public int EX1_017 { get; set; }
        [DisplayName("Scarlet Crusader")]
        public int EX1_020 { get; set; }
        [DisplayName("Thrallmar Farseer")]
        public int EX1_021 { get; set; }
        [DisplayName("Questing Adventurer")]
        public int EX1_044 { get; set; }
        [DisplayName("Coldlight Oracle")]
        public int EX1_050 { get; set; }
        [DisplayName("Tinkmaster Overspark")]
        public int EX1_083 { get; set; }
        [DisplayName("Mind Control Tech")]
        public int EX1_085 { get; set; }
        [DisplayName("Arcane Golem")]
        public int EX1_089 { get; set; }
        [DisplayName("Demolisher")]
        public int EX1_102 { get; set; }
        [DisplayName("Coldlight Seer")]
        public int EX1_103 { get; set; }
        [DisplayName("SI:7 Agent")]
        public int EX1_134 { get; set; }
        [DisplayName("Emperor Cobra")]
        public int EX1_170 { get; set; }
        [DisplayName("Unbound Elemental")]
        public int EX1_258 { get; set; }
        [DisplayName("Felguard")]
        public int EX1_301 { get; set; }
        [DisplayName("Void Terror")]
        public int EX1_304 { get; set; }
        [DisplayName("Aldor Peacekeeper")]
        public int EX1_382 { get; set; }
        [DisplayName("Tauren Warrior")]
        public int EX1_390 { get; set; }
        [DisplayName("Raging Worgen")]
        public int EX1_412 { get; set; }
        [DisplayName("Murloc Warleader")]
        public int EX1_507 { get; set; }
        [DisplayName("Harvest Golem")]
        public int EX1_556 { get; set; }
        [DisplayName("Mana Tide Totem")]
        public int EX1_575 { get; set; }
        [DisplayName("Blood Knight")]
        public int EX1_590 { get; set; }
        [DisplayName("Imp Master")]
        public int EX1_597 { get; set; }
        [DisplayName("Frothing Berserker")]
        public int EX1_604 { get; set; }
        [DisplayName("Kirin Tor Mage")]
        public int EX1_612 { get; set; }
        [DisplayName("Edwin VanCleef")]
        public int EX1_613 { get; set; }
        [DisplayName("Southsea Captain")]
        public int NEW1_027 { get; set; }
        [DisplayName("Flesheating Ghoul")]
        public int tt_004 { get; set; }
        [DisplayName("Unearthed Raptor")]
        public int LOE_019 { get; set; }
        [DisplayName("Desert Camel")]
        public int LOE_020 { get; set; }
        [DisplayName("Fierce Monkey")]
        public int LOE_022 { get; set; }
        [DisplayName("Mounted Raptor")]
        public int LOE_050 { get; set; }
        [DisplayName("Brann Bronzebeard")]
        public int LOE_077 { get; set; }
        [DisplayName("Shade of Naxxramas")]
        public int FP1_005 { get; set; }
        [DisplayName("Deathlord")]
        public int FP1_009 { get; set; }
        [DisplayName("Dark Cultist")]
        public int FP1_023 { get; set; }
        [DisplayName("Stoneskin Gargoyle")]
        public int FP1_027 { get; set; }
        [DisplayName("Dancing Swords")]
        public int FP1_029 { get; set; }
        [DisplayName("Iron Sensei")]
        public int GVG_027 { get; set; }
        [DisplayName("Grove Tender")]
        public int GVG_032 { get; set; }
        [DisplayName("Spider Tank")]
        public int GVG_044 { get; set; }
        [DisplayName("Metaltooth Leaper")]
        public int GVG_048 { get; set; }
        [DisplayName("Ogre Brute")]
        public int GVG_065 { get; set; }
        [DisplayName("Flying Machine")]
        public int GVG_084 { get; set; }
        [DisplayName("Illuminator")]
        public int GVG_089 { get; set; }
        [DisplayName("Gnomish Experimenter")]
        public int GVG_092 { get; set; }
        [DisplayName("Goblin Sapper")]
        public int GVG_095 { get; set; }
        [DisplayName("Lil' Exorcist")]
        public int GVG_097 { get; set; }
        [DisplayName("Gnomeregan Infantry")]
        public int GVG_098 { get; set; }
        [DisplayName("Scarlet Purifier")]
        public int GVG_101 { get; set; }
        [DisplayName("Tinkertown Technician")]
        public int GVG_102 { get; set; }
        [DisplayName("Hobgoblin")]
        public int GVG_104 { get; set; }
        [DisplayName("Soot Spewer")]
        public int GVG_123 { get; set; }
        [DisplayName("Flamewaker")]
        public int BRM_002 { get; set; }
        [DisplayName("Imp Gang Boss")]
        public int BRM_006 { get; set; }
        [DisplayName("Druid of the Flame")]
        public int BRM_010 { get; set; }
        [DisplayName("Blackwing Technician")]
        public int BRM_033 { get; set; }
        [DisplayName("Spellslinger")]
        public int AT_007 { get; set; }
        [DisplayName("Shadowfiend")]
        public int AT_014 { get; set; }
        [DisplayName("Shady Dealer")]
        public int AT_032 { get; set; }
        [DisplayName("Tuskarr Totemic")]
        public int AT_046 { get; set; }
        [DisplayName("Stablemaster")]
        public int AT_057 { get; set; }
        [DisplayName("Dreadscale")]
        public int AT_063t { get; set; }
        [DisplayName("Orgrimmar Aspirant")]
        public int AT_066 { get; set; }
        [DisplayName("Warhorse Trainer")]
        public int AT_075 { get; set; }
        [DisplayName("Dragonhawk Rider")]
        public int AT_083 { get; set; }
        [DisplayName("Saboteur")]
        public int AT_086 { get; set; }
        [DisplayName("Argent Horserider")]
        public int AT_087 { get; set; }
        [DisplayName("Ice Rager")]
        public int AT_092 { get; set; }
        [DisplayName("Silent Knight")]
        public int AT_095 { get; set; }
        [DisplayName("Silver Hand Regent")]
        public int AT_100 { get; set; }
        [DisplayName("Light's Champion")]
        public int AT_106 { get; set; }
        [DisplayName("Coliseum Manager")]
        public int AT_110 { get; set; }
        [DisplayName("Fencing Coach")]
        public int AT_115 { get; set; }
        [DisplayName("Master of Ceremonies")]
        public int AT_117 { get; set; }
        [DisplayName("Fjola Lightbane")]
        public int AT_129 { get; set; }
        [DisplayName("Eydis Darkbane")]
        public int AT_131 { get; set; }
        [DisplayName("Addled Grizzly")]
        public int OG_313 { get; set; }
        [DisplayName("Twilight Flamecaller")]
        public int OG_083 { get; set; }
        [DisplayName("Steward of Darkshire")]
        public int OG_310 { get; set; }
        [DisplayName("Darkshire Councilman")]
        public int OG_113 { get; set; }
        [DisplayName("Bloodsail Cultist")]
        public int OG_315 { get; set; }
        [DisplayName("Ravaging Ghoul")]
        public int OG_149 { get; set; }
        [DisplayName("Am'gam Rager")]
        public int OG_248 { get; set; }
        [DisplayName("Disciple of C'Thun")]
        public int OG_162 { get; set; }
        [DisplayName("Silithid Swarmer")]
        public int OG_034 { get; set; }
        [DisplayName("Spawn of N'Zoth")]
        public int OG_256 { get; set; }
        [DisplayName("Squirming Tentacle")]
        public int OG_327 { get; set; }
        [DisplayName("Twilight Elder")]
        public int OG_286 { get; set; }
        [DisplayName("Cloaked Huntress")]
        public int KAR_006 { get; set; }
        [DisplayName("Nightbane Templar")]
        public int KAR_010 { get; set; }
        [DisplayName("Deadly Fork")]
        public int KAR_094 { get; set; }
        [DisplayName("Silverware Golem")]
        public int KAR_205 { get; set; }
        [DisplayName("Moroes")]
        public int KAR_044 { get; set; }
        [DisplayName("Violet Illusionist")]
        public int KAR_712 { get; set; }
        [DisplayName("Zoobot")]
        public int KAR_095 { get; set; }
        [DisplayName("Pantry Spider")]
        public int KAR_030a { get; set; }
        [DisplayName("Celestial Dreamer")]
        public int CFM_617 { get; set; }
        [DisplayName("Rat Pack")]
        public int CFM_316 { get; set; }
        [DisplayName("Shaky Zipgunner")]
        public int CFM_336 { get; set; }
        [DisplayName("Manic Soulcaster")]
        public int CFM_660 { get; set; }
        [DisplayName("Wickerflame Burnbristle")]
        public int CFM_815 { get; set; }
        [DisplayName("Kabal Talonpriest")]
        public int CFM_626 { get; set; }
        [DisplayName("Shadow Rager")]
        public int CFM_636 { get; set; }
        [DisplayName("Shaku, the Collector")]
        public int CFM_781 { get; set; }
        [DisplayName("Unlicensed Apothecary")]
        public int CFM_900 { get; set; }
        [DisplayName("Grimestreet Pawnbroker")]
        public int CFM_755 { get; set; }
        [DisplayName("Auctionmaster Beardo")]
        public int CFM_807 { get; set; }
        [DisplayName("Backstreet Leper")]
        public int CFM_646 { get; set; }
        [DisplayName("Blubber Baron")]
        public int CFM_064 { get; set; }
        [DisplayName("Fel Orc Soulfiend")]
        public int CFM_609 { get; set; }
        [DisplayName("Grimestreet Smuggler")]
        public int CFM_853 { get; set; }
        [DisplayName("Hired Gun")]
        public int CFM_653 { get; set; }
        [DisplayName("Kabal Courier")]
        public int CFM_649 { get; set; }
        [DisplayName("Sergeant Sally")]
        public int CFM_341 { get; set; }
        [DisplayName("Street Trickster")]
        public int CFM_039 { get; set; }
        [DisplayName("Toxic Sewer Ooze")]
        public int CFM_655 { get; set; }
        [DisplayName("Water Elemental")]
        public int CS2_033 { get; set; }
        [DisplayName("Oasis Snapjaw")]
        public int CS2_119 { get; set; }
        [DisplayName("Stormwind Knight")]
        public int CS2_131 { get; set; }
        [DisplayName("Gnomish Inventor")]
        public int CS2_147 { get; set; }
        [DisplayName("Sen'jin Shieldmasta")]
        public int CS2_179 { get; set; }
        [DisplayName("Chillwind Yeti")]
        public int CS2_182 { get; set; }
        [DisplayName("Ogre Magi")]
        public int CS2_197 { get; set; }
        [DisplayName("Houndmaster")]
        public int DS1_070 { get; set; }
        [DisplayName("Dragonling Mechanic")]
        public int EX1_025 { get; set; }
        [DisplayName("Windspeaker")]
        public int EX1_587 { get; set; }
        [DisplayName("Kor'kron Elite")]
        public int NEW1_011 { get; set; }
        [DisplayName("Silvermoon Guardian")]
        public int EX1_023 { get; set; }
        [DisplayName("Twilight Drake")]
        public int EX1_043 { get; set; }
        [DisplayName("Dark Iron Dwarf")]
        public int EX1_046 { get; set; }
        [DisplayName("Spellbreaker")]
        public int EX1_048 { get; set; }
        [DisplayName("Ancient Brewmaster")]
        public int EX1_057 { get; set; }
        [DisplayName("Defender of Argus")]
        public int EX1_093 { get; set; }
        [DisplayName("Keeper of the Grove")]
        public int EX1_166 { get; set; }
        [DisplayName("Ethereal Arcanist")]
        public int EX1_274 { get; set; }
        [DisplayName("Pit Lord")]
        public int EX1_313 { get; set; }
        [DisplayName("Summoning Portal")]
        public int EX1_315 { get; set; }
        [DisplayName("Lightspawn")]
        public int EX1_335 { get; set; }
        [DisplayName("Mogu'shan Warden")]
        public int EX1_396 { get; set; }
        [DisplayName("Arathi Weaponsmith")]
        public int EX1_398 { get; set; }
        [DisplayName("Ancient Mage")]
        public int EX1_584 { get; set; }
        [DisplayName("Auchenai Soulpriest")]
        public int EX1_591 { get; set; }
        [DisplayName("Cult Master")]
        public int EX1_595 { get; set; }
        [DisplayName("Master of Disguise")]
        public int NEW1_014 { get; set; }
        [DisplayName("Dread Corsair")]
        public int NEW1_022 { get; set; }
        [DisplayName("Violet Teacher")]
        public int NEW1_026 { get; set; }
        [DisplayName("Tomb Pillager")]
        public int LOE_012 { get; set; }
        [DisplayName("Rumbling Elemental")]
        public int LOE_016 { get; set; }
        [DisplayName("Keeper of Uldaman")]
        public int LOE_017 { get; set; }
        [DisplayName("Gorillabot A-3")]
        public int LOE_039 { get; set; }
        [DisplayName("Tomb Spider")]
        public int LOE_047 { get; set; }
        [DisplayName("Jungle Moonkin")]
        public int LOE_051 { get; set; }
        [DisplayName("Elise Starseeker")]
        public int LOE_079 { get; set; }
        [DisplayName("Eerie Statue")]
        public int LOE_107 { get; set; }
        [DisplayName("Ancient Shade")]
        public int LOE_110 { get; set; }
        [DisplayName("Old Murk-Eye")]
        public int EX1_062 { get; set; }
        [DisplayName("Wailing Soul")]
        public int FP1_016 { get; set; }
        [DisplayName("Voidcaller")]
        public int FP1_022 { get; set; }
        [DisplayName("Anub'ar Ambusher")]
        public int FP1_026 { get; set; }
        [DisplayName("Baron Rivendare")]
        public int FP1_031 { get; set; }
        [DisplayName("Goblin Blastmage")]
        public int GVG_004 { get; set; }
        [DisplayName("Fel Cannon")]
        public int GVG_020 { get; set; }
        [DisplayName("Siltfin Spiritwalker")]
        public int GVG_040 { get; set; }
        [DisplayName("Screwjank Clunker")]
        public int GVG_055 { get; set; }
        [DisplayName("Dunemaul Shaman")]
        public int GVG_066 { get; set; }
        [DisplayName("Burly Rockjaw Trogg")]
        public int GVG_068 { get; set; }
        [DisplayName("Lost Tallstrider")]
        public int GVG_071 { get; set; }
        [DisplayName("Kezan Mystic")]
        public int GVG_074 { get; set; }
        [DisplayName("Mechanical Yeti")]
        public int GVG_078 { get; set; }
        [DisplayName("Arcane Nullifier X-21")]
        public int GVG_091 { get; set; }
        [DisplayName("Jeeves")]
        public int GVG_094 { get; set; }
        [DisplayName("Piloted Shredder")]
        public int GVG_096 { get; set; }
        [DisplayName("Enhance-o Mechano")]
        public int GVG_107 { get; set; }
        [DisplayName("Mini-Mage")]
        public int GVG_109 { get; set; }
        [DisplayName("Wee Spellstopper")]
        public int GVG_122 { get; set; }
        [DisplayName("Fireguard Destroyer")]
        public int BRM_012 { get; set; }
        [DisplayName("Core Rager")]
        public int BRM_014 { get; set; }
        [DisplayName("Axe Flinger")]
        public int BRM_016 { get; set; }
        [DisplayName("Dragonkin Sorcerer")]
        public int BRM_020 { get; set; }
        [DisplayName("Hungry Dragon")]
        public int BRM_026 { get; set; }
        [DisplayName("Dalaran Aspirant")]
        public int AT_006 { get; set; }
        [DisplayName("Holy Champion")]
        public int AT_011 { get; set; }
        [DisplayName("Spawn of Shadows")]
        public int AT_012 { get; set; }
        [DisplayName("Twilight Guardian")]
        public int AT_017 { get; set; }
        [DisplayName("Dreadsteed")]
        public int AT_019 { get; set; }
        [DisplayName("Savage Combatant")]
        public int AT_039 { get; set; }
        [DisplayName("Wildwalker")]
        public int AT_040 { get; set; }
        [DisplayName("Draenei Totemcarver")]
        public int AT_047 { get; set; }
        [DisplayName("Magnataur Alpha")]
        public int AT_067 { get; set; }
        [DisplayName("Murloc Knight")]
        public int AT_076 { get; set; }
        [DisplayName("Maiden of the Lake")]
        public int AT_085 { get; set; }
        [DisplayName("Tournament Medic")]
        public int AT_091 { get; set; }
        [DisplayName("Frigid Snobold")]
        public int AT_093 { get; set; }
        [DisplayName("Armored Warhorse")]
        public int AT_108 { get; set; }
        [DisplayName("Refreshment Vendor")]
        public int AT_111 { get; set; }
        [DisplayName("Evil Heckler")]
        public int AT_114 { get; set; }
        [DisplayName("Crowd Favorite")]
        public int AT_121 { get; set; }
        [DisplayName("Gormok the Impaler")]
        public int AT_122 { get; set; }
        [DisplayName("Fandral Staghelm")]
        public int OG_044 { get; set; }
        [DisplayName("Klaxxi Amber-Weaver")]
        public int OG_188 { get; set; }
        [DisplayName("Mire Keeper")]
        public int OG_202 { get; set; }
        [DisplayName("Infested Wolf")]
        public int OG_216 { get; set; }
        [DisplayName("Demented Frostcaller")]
        public int OG_085 { get; set; }
        [DisplayName("Hooded Acolyte")]
        public int OG_334 { get; set; }
        [DisplayName("Shifting Shade")]
        public int OG_335 { get; set; }
        [DisplayName("Southsea Squidface")]
        public int OG_267 { get; set; }
        [DisplayName("Xaril, Poisoned Mind")]
        public int OG_080 { get; set; }
        [DisplayName("Flamewreathed Faceless")]
        public int OG_024 { get; set; }
        [DisplayName("Master of Evolution")]
        public int OG_328 { get; set; }
        [DisplayName("Bloodhoof Brave")]
        public int OG_218 { get; set; }
        [DisplayName("Aberrant Berserker")]
        public int OG_150 { get; set; }
        [DisplayName("Blackwater Pirate")]
        public int OG_322 { get; set; }
        [DisplayName("C'Thun's Chosen")]
        public int OG_283 { get; set; }
        [DisplayName("Cyclopian Horror")]
        public int OG_337 { get; set; }
        [DisplayName("Eater of Secrets")]
        public int OG_254 { get; set; }
        [DisplayName("Evolved Kobold")]
        public int OG_082 { get; set; }
        [DisplayName("Faceless Shambler")]
        public int OG_174 { get; set; }
        [DisplayName("Infested Tauren")]
        public int OG_249 { get; set; }
        [DisplayName("Midnight Drake")]
        public int OG_320 { get; set; }
        [DisplayName("Polluted Hoarder")]
        public int OG_323 { get; set; }
        [DisplayName("Twilight Summoner")]
        public int OG_272 { get; set; }
        [DisplayName("Barnes")]
        public int KAR_114 { get; set; }
        [DisplayName("Arcanosmith")]
        public int KAR_710 { get; set; }
        [DisplayName("Priest of the Feast")]
        public int KAR_035 { get; set; }
        [DisplayName("Wicked Witchdoctor")]
        public int KAR_021 { get; set; }
        [DisplayName("Dispatch Kodo")]
        public int CFM_335 { get; set; }
        [DisplayName("Shadow Sensei")]
        public int CFM_694 { get; set; }
        [DisplayName("Jinyu Waterspeaker")]
        public int CFM_061 { get; set; }
        [DisplayName("Lotus Illusionist")]
        public int CFM_697 { get; set; }
        [DisplayName("Crystalweaver")]
        public int CFM_610 { get; set; }
        [DisplayName("Seadevil Stinger")]
        public int CFM_699 { get; set; }
        [DisplayName("Grimy Gadgeteer")]
        public int CFM_754 { get; set; }
        [DisplayName("Backroom Bouncer")]
        public int CFM_658 { get; set; }
        [DisplayName("Daring Reporter")]
        public int CFM_851 { get; set; }
        [DisplayName("Genzo, the Shark")]
        public int CFM_808 { get; set; }
        [DisplayName("Hozen Healer")]
        public int CFM_067 { get; set; }
        [DisplayName("Jade Spirit")]
        public int CFM_715 { get; set; }
        [DisplayName("Kabal Chemist")]
        public int CFM_619 { get; set; }
        [DisplayName("Kazakus")]
        public int CFM_621 { get; set; }
        [DisplayName("Kooky Chemist")]
        public int CFM_063 { get; set; }
        [DisplayName("Naga Corsair")]
        public int CFM_651 { get; set; }
        [DisplayName("Tanaris Hogchopper")]
        public int CFM_809 { get; set; }
        [DisplayName("Worgen Greaser")]
        public int CFM_665 { get; set; }
        [DisplayName("Big Game Hunter")]
        public int EX1_005 { get; set; }
        

        public ACKPriority()
        {
            Name = "ACK - Priorities";
            CS2_231 = 0;
            LOEA10_3 = 0;
            GVG_093 = 0;
            CS1_042 = 0;
            CS2_065 = 3;
            CS2_168 = 2;
            CS2_171 = 2;
            CS2_189 = 3;
            CS2_235 = 4;
            DS1_175 = 0;
            EX1_011 = 1;
            EX1_508 = 0;
            CS2_059 = 1;
            CS2_146 = 1;
            CS2_169 = 0;
            CS2_188 = 4;
            EX1_001 = 0;
            EX1_004 = 1;
            EX1_008 = 3;
            EX1_009 = 0;
            EX1_010 = 4;
            EX1_029 = 2;
            EX1_080 = 2;
            EX1_243 = -5;
            EX1_319 = 5;
            EX1_405 = 0;
            EX1_509 = 0;
            NEW1_012 = 4;
            NEW1_017 = 1;
            NEW1_025 = 1;
            LOE_010 = 2;
            LOE_018 = 5;
            LOE_076 = 4;
            LOE_116 = -1;
            FP1_001 = 10;
            FP1_011 = 3;
            FP1_028 = 0;
            GVG_009 = 0;
            GVG_013 = 2;
            GVG_051 = 1;
            GVG_082 = 3;
            BRM_004 = 2;
            BRM_022 = 0;
            AT_029 = 8;
            AT_059 = -1;
            AT_082 = 2;
            AT_097 = 1;
            AT_105 = 1;
            AT_133 = 6;
            CS2_120 = 3;
            CS2_121 = 2;
            CS2_142 = 1;
            CS2_172 = 3;
            CS2_173 = 0;
            EX1_015 = 3;
            EX1_066 = 3;
            EX1_306 = 1;
            EX1_506 = 2;
            EX1_565 = 0;
            CS2_203 = 0;
            EX1_012 = 2;
            EX1_045 = 2;
            EX1_049 = 3;
            EX1_055 = 2;
            EX1_058 = 3;
            EX1_059 = 2;
            EX1_076 = 1;
            EX1_082 = 3;
            EX1_096 = 4;
            EX1_100 = 0;
            EX1_131 = 2;
            EX1_162 = 2;
            EX1_341 = 2;
            EX1_362 = 0;
            EX1_393 = 3;
            EX1_402 = 2;
            EX1_522 = 1;
            EX1_531 = 2;
            EX1_557 = 3;
            EX1_603 = 2;
            EX1_608 = 3;
            EX1_616 = 2;
            NEW1_018 = 3;
            NEW1_019 = 4;
            NEW1_020 = 3;
            NEW1_021 = 1;
            NEW1_023 = 3;
            NEW1_029 = 4;
            NEW1_037 = 2;
            LOE_006 = 2;
            LOE_023 = 4;
            LOE_029 = 4;
            LOE_046 = 4;
            NEW1_016 = 1;
            FP1_002 = 4;
            FP1_003 = 3;
            FP1_004 = 4;
            FP1_007 = 0;
            FP1_017 = 2;
            FP1_024 = 2;
            GVG_002 = 4;
            GVG_006 = 5;
            GVG_011 = 3;
            GVG_018 = 2;
            GVG_023 = 5;
            GVG_025 = 2;
            GVG_030 = 3;
            GVG_037 = 3;
            GVG_039 = 0;
            GVG_058 = 5;
            GVG_064 = 3;
            GVG_067 = 3;
            GVG_072 = 4;
            GVG_075 = 3;
            GVG_076 = 1;
            GVG_081 = 3;
            GVG_085 = 2;
            GVG_087 = 3;
            GVG_103 = 2;
            GVG_108 = 3;
            AT_003 = 3;
            AT_021 = 3;
            AT_026 = 2;
            AT_030 = 4;
            AT_031 = 1;
            AT_038 = 10;
            AT_042 = 2;
            AT_052 = 8;
            AT_058 = 3;
            AT_069 = 3;
            AT_071 = 3;
            AT_080 = 3;
            AT_084 = 2;
            AT_089 = 4;
            AT_094 = 3;
            AT_109 = 2;
            AT_116 = 1;
            CS2_118 = -1;
            CS2_122 = 1;
            CS2_124 = -1;
            CS2_125 = 2;
            CS2_127 = 0;
            CS2_141 = 2;
            CS2_196 = 3;
            EX1_019 = 3;
            EX1_084 = 1;
            EX1_582 = 1;
            CS2_117 = 3;
            CS2_181 = 5;
            EX1_005 = 0;
            EX1_006 = 0;
            EX1_007 = 2;
            EX1_014 = 5;
            EX1_017 = 4;
            EX1_020 = 4;
            EX1_021 = 1;
            EX1_044 = 2;
            EX1_050 = 1;
            EX1_083 = 3;
            EX1_085 = 0;
            EX1_089 = 0;
            EX1_102 = 2;
            EX1_103 = 2;
            EX1_134 = 4;
            EX1_170 = 2;
            EX1_258 = 2;
            EX1_301 = 4;
            EX1_304 = 2;
            EX1_382 = 3;
            EX1_390 = 1;
            EX1_412 = 2;
            EX1_507 = 2;
            EX1_556 = 6;
            EX1_575 = 0;
            EX1_590 = 3;
            EX1_597 = 4;
            EX1_604 = 3;
            EX1_612 = 4;
            EX1_613 = 0;
            NEW1_027 = 1;
            tt_004 = 2;
            LOE_019 = 4;
            LOE_020 = 1;
            LOE_022 = 4;
            LOE_050 = 4;
            LOE_077 = 2;
            FP1_005 = 0;
            FP1_009 = 4;
            FP1_023 = 5;
            FP1_027 = 2;
            FP1_029 = 5;
            GVG_027 = 3;
            GVG_032 = 2;
            GVG_044 = 5;
            GVG_048 = 4;
            GVG_065 = 5;
            GVG_084 = 0;
            GVG_089 = 0;
            GVG_092 = 2;
            GVG_095 = 0;
            GVG_097 = 2;
            GVG_098 = 0;
            GVG_101 = 3;
            GVG_102 = 2;
            GVG_104 = 1;
            GVG_123 = 4;
            BRM_002 = 3;
            BRM_006 = 5;
            BRM_010 = 3;
            BRM_033 = 3;
            AT_007 = 4;
            AT_014 = 2;
            AT_032 = 2;
            AT_046 = 3;
            AT_057 = 2;
            AT_063t = 2;
            AT_066 = 1;
            AT_075 = 0;
            AT_083 = 4;
            AT_086 = 3;
            AT_087 = 3;
            AT_092 = 0;
            AT_095 = 5;
            AT_100 = 6;
            AT_106 = 2;
            AT_110 = 4;
            AT_115 = 1;
            AT_117 = 0;
            AT_129 = 4;
            AT_131 = 4;
            CS2_033 = 6;
            CS2_119 = 0;
            CS2_131 = 0;
            CS2_147 = 0;
            CS2_179 = 4;
            CS2_182 = 5;
            CS2_197 = 3;
            DS1_070 = 0;
            EX1_025 = 0;
            EX1_587 = 0;
            NEW1_011 = 0;
            EX1_023 = 0;
            EX1_043 = 1;
            EX1_046 = 5;
            EX1_048 = 0;
            EX1_057 = 0;
            EX1_093 = 0;
            EX1_166 = 0;
            EX1_274 = 0;
            EX1_313 = 0;
            EX1_315 = 0;
            EX1_335 = 0;
            EX1_396 = 0;
            EX1_398 = 0;
            EX1_584 = 0;
            EX1_591 = 1;
            EX1_595 = 0;
            NEW1_014 = 0;
            NEW1_022 = 0;
            NEW1_026 = 0;
            LOE_012 = 1;
            LOE_016 = 0;
            LOE_017 = 0;
            LOE_039 = 0;
            LOE_047 = 0;
            LOE_051 = 0;
            LOE_079 = 0;
            LOE_107 = 0;
            LOE_110 = 0;
            EX1_062 = 0;
            FP1_016 = 0;
            FP1_022 = 4;
            FP1_026 = 3;
            FP1_031 = 0;
            GVG_004 = 3;
            GVG_020 = 1;
            GVG_040 = 0;
            GVG_055 = 0;
            GVG_066 = 0;
            GVG_068 = 0;
            GVG_071 = 0;
            GVG_074 = 0;
            GVG_078 = 5;
            GVG_091 = 4;
            GVG_094 = 0;
            GVG_096 = 8;
            GVG_107 = 0;
            GVG_109 = 0;
            GVG_122 = 0;
            BRM_012 = 0;
            BRM_014 = 0;
            BRM_016 = 0;
            BRM_020 = 3;
            BRM_026 = 2;
            AT_006 = 3;
            AT_011 = 5;
            AT_012 = 3;
            AT_017 = 1;
            AT_019 = 0;
            AT_039 = 6;
            AT_040 = 0;
            AT_047 = 0;
            AT_067 = 0;
            AT_076 = 0;
            AT_085 = 0;
            AT_091 = 0;
            AT_093 = 0;
            AT_108 = 0;
            AT_111 = 0;
            AT_114 = 4;
            AT_121 = 0;
            AT_122 = 0;
            OG_051 = 0;
            OG_241 = 3;
            OG_179 = 3;
            OG_221 = 1;
            OG_006 = 4;
            OG_312 = 3;
            OG_123 = 1;
            OG_151 = 2;
            OG_158 = 1;
            OG_303 = 3;
            OG_330 = 2;
            OG_026 = 2;
            OG_109 = 1;
            OG_156 = 2;
            OG_326 = 0;
            OG_338 = -1;
            OG_281 = 5;
            OG_284 = 2;
            OG_247 = 3;
            OG_313 = 0;
            OG_083 = 2;
            OG_310 = 2;
            OG_113 = 3;
            OG_315 = 3;
            OG_149 = 2;
            OG_248 = 2;
            OG_162 = 3;
            OG_034 = 2;
            OG_256 = 1;
            OG_327 = 4;
            OG_286 = 5;
            OG_044 = 3;
            OG_188 = 2;
            OG_202 = 1;
            OG_216 = 1;
            OG_085 = 2;
            OG_334 = 1;
            OG_335 = 0;
            OG_267 = 1;
            OG_080 = 0;
            OG_024 = 4;
            OG_328 = 4;
            OG_218 = 2;
            OG_150 = 0;
            OG_322 = 0;
            OG_283 = 0;
            OG_337 = 1;
            OG_254 = 0;
            OG_082 = 0;
            OG_174 = 0;
            OG_249 = 4;
            OG_320 = 0;
            OG_323 = 0;
            OG_272 = 0;
            OG_070 = 5;
            KAR_300 = 5;
            KAR_006 = 3;
            KAR_005 = 4;
            KAR_009 = 2;
            KAR_092 = 2;
            KAR_010 = 4;
            KAR_094 = 0;
            KAR_069 = 4;
            KAR_205 = 1;
            KAR_089 = 3;
            KAR_114 = 4;
            KAR_044 = 2;
            KAR_712 = 2;
            KAR_095 = 1;
            KAR_710 = 2;
            KAR_030a = 2;
            KAR_062 = 2;
            KAR_011 = 3;
            KAR_036 = 4;
            KAR_029 = 2;
            KAR_035 = 5;
            KAR_021 = 2;
            CFM_315 = 4;
            CFM_066 = 2;
            CFM_650 = 3;
            CFM_759 = 1;
            CFM_120 = 4;
            CFM_637 = -1000;
            CFM_325 = 3;
            CFM_095 = 1;
            CFM_338 = 3;
            CFM_753 = 2;
            CFM_606 = 3;
            CFM_693 = 1;
            CFM_691 = 2;
            CFM_643 = 3;
            CFM_300 = 1;
            CFM_647 = 2;
            CFM_790 = 0;
            CFM_654 = 3;
            CFM_659 = 3;
            CFM_321 = 2;
            CFM_617 = 1;
            CFM_316 = 3;
            CFM_336 = 3;
            CFM_660 = 2;
            CFM_815 = 2;
            CFM_626 = 4;
            CFM_636 = 0;
            CFM_781 = 1;
            CFM_900 = 0;
            CFM_755 = 2;
            CFM_807 = 3;
            CFM_646 = 1;
            CFM_064 = 2;
            CFM_609 = 2;
            CFM_853 = 3;
            CFM_653 = 2;
            CFM_649 = 2;
            CFM_341 = 3;
            CFM_039 = 0;
            CFM_655 = 1;
            CFM_335 = 1;
            CFM_694 = 2;
            CFM_061 = 4;
            CFM_697 = 3;
            CFM_610 = 3;
            CFM_699 = 2;
            CFM_754 = 1;
            CFM_658 = 1;
            CFM_851 = 1;
            CFM_808 = 1;
            CFM_067 = 2;
            CFM_715 = 3;
            CFM_619 = 1;
            CFM_621 = 1;
            CFM_063 = 1;
            CFM_651 = 1;
            CFM_809 = 0;
            CFM_665 = 0;
            Enabled = true;

        }
        public void Refresh()
        {

        }
    }

    public class MulliganPriority : Plugin
    {
        public override void OnStarted()
        {
            BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            List<object> fieldValues = ((ACKPriority)DataContainer)
                .GetType().GetFields(bindingFlags)
                .Select(field => field.GetValue(((ACKPriority)DataContainer))).ToList();
            foreach (var q in fieldValues)
            {
                Bot.Log(q.ToString());
            }
        }
        
    }

}