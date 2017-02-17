using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using ACK;
using SmartBot.Plugins.API;


namespace SmartBot.Plugins
{
    [Serializable]
    public class ACKMulliganCore : PluginDataContainer
    {

        [DisplayName("Require 1 drop for 2 drop")]
        public bool Tr1 { get; set; }
        [DisplayName("Require 2 drop for 3 drop")]
        public bool Tr2 { get; set; }
        [DisplayName("Require 3 drop for 4 drop")]
        public bool Tr3 { get; set; }
        [DisplayName("Treat Coin as Ramp")]
        public bool Tr4 { get; set; }

        [DisplayName("[Assumption]")]
        public Assumption ModeAssumption { get; private set; }

        [DisplayName("[Assumption] Enemy Mages")]
        public DeckClassification.Style DefaultStyleMage { get; set; }
        [DisplayName("[Assumption] Enemy Lock")]
        public DeckClassification.Style DefaultStyleWarlock { get; set; }
        [DisplayName("[Assumption] Enemy Priest")]
        public DeckClassification.Style DefaultStylePriest { get; set; }
        [DisplayName("[Assumption] Enemy Paladin")]
        public DeckClassification.Style DefaultStylePaladin { get; set; }
        [DisplayName("[Assumption] Enemy Warrior")]
        public DeckClassification.Style DefaultStyleWarrior { get; set; }
        [DisplayName("[Assumption] Enemy Hunter")]
        public DeckClassification.Style DefaultStyleHunter { get; set; }
        [DisplayName("[Assumption] Enemy Rogue")]
        public DeckClassification.Style DefaultStyleRogue { get; set; }
        [DisplayName("[Assumption] Enemy Druid")]
        public DeckClassification.Style DefaultStyleDruid { get; set; }
        [DisplayName("[Assumption] Enemy Shaman")]
        public DeckClassification.Style DefaultStyleShaman { get; set; }

        [DisplayName("Limit 1 Drops")]
        public int Max1Drops { get; set; }
        [DisplayName("Limit 1 Drops [Coin]")]
        public int Max1DropsCoin { get; set; }

        [DisplayName("Limit 2 Drops")]
        public int Max2Drops { get; set; }
        [DisplayName("Limit 2 Drops [Coin]")]
        public int Max2DropsCoin { get; set; }

        [DisplayName("Limit 3 Drops")]
        public int Max3Drops { get; set; }
        [DisplayName("Limit 3 Drops [Coin]")]
        public int Max3DropsCoin { get; set; }

        [DisplayName("Limit 4 Drops")]
        public int Max4Drops { get; set; }
        [DisplayName("Limit 4 Drops [Coin]")]
        public int Max4DropsCoin { get; set; }

        public ACKMulliganCore()
        {
            Name = "ACK - Mulligan Container";
            Tr1 = false;
            Tr2 = false;
            Tr3 = false;
            Max1Drops = 1;
            Max1DropsCoin = 2;

            Max2Drops = 2;
            Max2DropsCoin = 3;

            Max3Drops = 1;
            Max3DropsCoin = 2;

            Max4Drops = 1;
            Max4DropsCoin = 1;

            DefaultStyleMage = DeckClassification.Style.Control;
            DefaultStyleWarlock = DeckClassification.Style.Control;
            DefaultStylePriest = DeckClassification.Style.Control;

            DefaultStylePaladin = DeckClassification.Style.Control;
            DefaultStyleHunter = DeckClassification.Style.Midrange;
            DefaultStyleWarrior = DeckClassification.Style.Aggro;

            DefaultStyleRogue = DeckClassification.Style.Control;
            DefaultStyleDruid = DeckClassification.Style.Control;
            DefaultStyleShaman = DeckClassification.Style.Midrange;

            ModeAssumption = Assumption.FromPlugin;
            Enabled = true;

        }
        public void Refresh()
        {
            //Do nothing
        }
    }

    public class MulliganCore : Plugin
    {
        public override void OnStarted()
        {
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\MulliganProfiles\\ACK.ini", false))
            {
                BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
                List<object> fieldValues = ((ACKMulliganCore)DataContainer)
                    .GetType().GetFields(bindingFlags)
                    .Select(field => field.GetValue(((ACKMulliganCore)DataContainer))).ToList();
                sw.WriteLine(string.Join(":", fieldValues));

            }
        }
        public override void OnPluginCreated()
        {
            ((ACKMulliganCore)DataContainer).Refresh();

            base.OnPluginCreated();
        }
    }
    public enum Assumption 
    {
        FromHistory,
        FromPlugin

    }

}