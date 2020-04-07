using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;
using TaleWorlds.Localization;
using StoryMode.CharacterCreationSystem;

namespace zCulturedStart
{
    public class CSCharCreationOption
    {
        // if true skip quest stages on load
        //private static bool _SkipFolly;
        //public static bool SkipFolly
        //{
          //  get 
           // { 
            //  return _SkipFolly; 
           // }
           // set
           // {
           //     _SkipFolly = value;
           // }
        //}
      //  private static bool _CSWithFamily;
      //  public static bool CSWithFamily
      //  {
           // get
           // {
           //     return _CSWithFamily;
           // }
           // set
            //{
                //_CSWithFamily = value;
            //}
       // }
        private static int _CSSelcOption;

        // 1 = Default 2 = SkipFolly 3 = Exiled
        public static int CSSelectOption
        {
            get
            {
                return _CSSelcOption;
            }
            set
            {
                _CSSelcOption = value;
            }
        }

        private static int _CSLocationOption;

        // 0 = home town 1 = Random location 2 - 8 = specific town
        public static int CSLocationOption
        {
            get
            {
                return _CSLocationOption;
            }
            set
            {
                _CSLocationOption = value;
            }
        }

        public static Settlement cultureSettlement(Hero hero)
        {
            //var result;
            string sCulture = hero.MapFaction.Culture.StringId;
            switch (sCulture)
            {
                
                case "sturgia":
                    return Settlement.Find("town_S2");         
                case "aserai":
                    return Settlement.Find("town_A8");       
                case "vlandia":
                    return Settlement.Find("town_V3");          
                case "battania":
                    return Settlement.Find("town_B2");                    
                case "khuzait":
                    return Settlement.Find("town_K4"); 
                default:
                    return Settlement.Find("town_ES3");                  
                    

            }
        }
        public static Settlement cultureSettlement(string sCulture)
        {
            //var result;
            //string sCulture = hero.MapFaction.Culture.StringId;
            switch (sCulture)
            {

                case "sturgia":
                    return Settlement.Find("town_S2");
                case "aserai":
                    return Settlement.Find("town_A8");
                case "vlandia":
                    return Settlement.Find("town_V3");
                case "battania":
                    return Settlement.Find("town_B2");
                case "khuzait":
                    return Settlement.Find("town_K4");
                default:
                    return Settlement.Find("town_EW2");

            }
        }
        public static Settlement RandcultureSettlement()
        {
            //var result;
            Random rnd = new Random();
            int city = rnd.Next(5);
            switch (city)
            {

                case 0 :
                    return Settlement.Find("town_S2");
                case 1:
                    return Settlement.Find("town_A8");
                case 2:
                    return Settlement.Find("town_V3");
                case 3:
                    return Settlement.Find("town_B2");
                case 4:
                    return Settlement.Find("town_K4");
                case 5:
                    return Settlement.Find("town_EW2");
                default:
                    return Settlement.Find("defaultcastle_village_ES1_2");


            }
        }
    }
}
