using SmartBot.Plugins.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using SmartBot.Database;


namespace SmartBot.Plugins
{
    [Serializable]
    public class ACKBundleMain : PluginDataContainer
    {

        [DisplayName("Donation")]
        public string donation { get; set; }
        [DisplayName("Discord")]
        public string discord { get; set; }
        [DisplayName("Stop when legend")]
        public bool sLegend { get; set; }
        [DisplayName("Stop at rank X legend")]
        public int sLegendRank { get; set; }
        
       
       
       
        [DisplayName("Mystery")]
        public bool CVA { get; set; }
        public ACKBundleMain()
        {
            Name = "ACK - Bundle";
            donation = "http://bit.ly/ABDonationLink";
            discord = "https://discord.gg/DQJ8m9m";
            Enabled = true;
            sLegend = false;
            sLegendRank = 1000;
            CVA = false;
           
           

        }

    }

    public class Miscellaneous : Plugin
    {
        public override void OnTurnEnd()
        {
            if (((ACKBundleMain)DataContainer).CVA)
            {
                if (Bot.GetCurrentOpponentId() == -6666666666666666666)
                {
                    Bot.Log("[ACK] This guy is the devil. We stand no chance.");
                    Bot.Concede();
                }
            }
            base.OnTurnBegin();
        }

        private string _path = AppDomain.CurrentDomain.BaseDirectory;
        public override void OnPluginCreated()
        {
            if (!File.Exists(_path + "\\MulliganProfiles\\ACK-MulliganTester.ini"))
            {
                File.Create(_path + "\\MulliganProfiles\\ACK-MulliganTester.ini");
            }
            if (!File.Exists(_path + "\\MulliganProfiles\\ACK.ini"))
            {
                File.Create(_path + "\\MulliganProfiles\\ACK.ini");
            }
            if (!File.Exists(_path + "\\Logs\\ACK_MC_log"))
            {
                File.Create(_path + "\\Logs\\ACK_MC_log");
            }

        }

        public override void OnStarted()
        {
           //File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\ACK_MC_log", ""); 
           
            


        }

       
        private bool IsFileLocked(string filename)
        {
            bool Locked = false;
            try
            {
                FileStream fs =
                    File.Open(filename, FileMode.OpenOrCreate,
                    FileAccess.ReadWrite, FileShare.None);
                fs.Close();
            }
            catch (IOException ex)
            {
                Bot.Log("Error:" + ex.Message);
                Locked = true;
            }
            return Locked;
        }

        public override void OnTick()
        {
            
        }
        public override void OnGameEnd()
        {
            
            if (Bot.CurrentMode() != Bot.Mode.RankedWild || Bot.CurrentMode() != Bot.Mode.RankedStandard) return;
            if (((ACKBundleMain)DataContainer).sLegend && Bot.GetPlayerDatas().GetRank() == 0)
            {
                Bot.Log("[ACK] You are Legend, go pat yourself on the back.");
                Bot.StopBot();
            }
            if (((ACKBundleMain)DataContainer).sLegendRank <= Bot.GetPlayerDatas().GetLegendIndex())
            {
                Bot.Log("[ACK] You reached your desired legend rank. Go pat yourself on the back");
            }
            base.OnGameEnd();
        }
        

    }
    public enum Localization
    {
        English, Russian
    }
}
