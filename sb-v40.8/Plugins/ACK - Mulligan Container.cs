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

            
            Enabled = true;
            
        }
        public void Refresh()
        {
            
        }
    }

    public class MulliganCore : Plugin
    {
        public override void OnStarted()
        {
            using (StreamWriter sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\MulliganProfiles\\ACK.ini", false))
            {
                BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
                List<object> fieldValues = ((ACKMulliganCore) DataContainer)
                    .GetType().GetFields(bindingFlags)
                    .Select(field => field.GetValue(((ACKMulliganCore) DataContainer))).ToList();
                sw.WriteLine(string.Join(":" , fieldValues));
                
            }
        }
        public override void OnPluginCreated()
        {
            ((ACKMulliganCore)DataContainer).Refresh();

            base.OnPluginCreated();
        }
    }
    
}