using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoreCard.Models
{
    public partial class Setting
    {
        public static bool AllowCorrection {
            get
            {
                Setting r;
                using (scoreDB s = new scoreDB())
                {
                    r = s.Fetch<Setting>().FirstOrDefault();
                }
                return r.AllowIonCorrection;
            }
            set 
            {
                var n = new Setting() { SettingsId = 1, AllowIonCorrection = value };

                n.Update();
            }
        }
    }
}