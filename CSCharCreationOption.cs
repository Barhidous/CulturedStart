using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameState;
using TaleWorlds.Core;
using TaleWorlds.ObjectSystem;
using TaleWorlds.SaveSystem;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using StoryMode.CharacterCreationSystem;

namespace zCulturedStart
{
    public class CSCharCreationOption
    {
        
        private static int _CSSelcOption;

        // 1 = Default 2 = merchant 3 = Exiled 4 = merc 5 = looter
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
        
        private static int _CSGameOption;


        // 0 = FP default 1 = Fp nezzy 2 = fp sandbox 3 = default 4= nezzy 5 = sandbox no kingdom
        //[SaveableProperty(1)]
        public static int CSGameOption
        {
            get
            {
                return _CSGameOption;
            }
            set
            {
                _CSGameOption = value;
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
        public static CultureObject SelectedCulture
        {
            get
            {
                return _SelectedCulture;
            }
            set
            {
                _SelectedCulture = value;
            }
        }
        private static CultureObject _SelectedCulture;
        public static List<CultureObject> AddtlCulturesList;
        
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
            
            return Settlement.FindAll((Settlement x) => x.IsTown).GetRandomElement<Settlement>();
            
        }

        public static Settlement CSOptionSettlement()
        {
            int opt = CSCharCreationOption.CSLocationOption;
            switch (opt)
            {
                case 0:
                    return CSCharCreationOption.cultureSettlement(Hero.MainHero);
                case 1:
                    return CSCharCreationOption.RandcultureSettlement();
                case 2:
                    return Settlement.Find("town_A8");
                case 3:
                    return Settlement.Find("town_B2");
                case 4:
                    return Settlement.Find("town_EW2");
                case 5:
                    return Settlement.Find("town_S2");
                case 6:
                    return Settlement.Find("town_K4");
                case 7:
                    return Settlement.Find("town_V3");
                case 8:
                    return CSCharCreationOption.cultureSettlement(Hero.MainHero);
                default:
                    return Settlement.Find("tutorial_training_field");
            }


        }
    }
}
