using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    class SymptomDailyLog
    {
        private String nameOfSymptom;
        private double severeness;
        private String timeframe;
        private Boolean continueWithDailyLife;
        private String areaOfBody;
        private String notes;

        public SymptomDailyLog(String nameOfSymptom, double severeness, String timeframe, Boolean continueWithDailyLife, String areaOfBody, String notes )
        {
            this.nameOfSymptom = nameOfSymptom;
            this.severeness = severeness;
            this.timeframe = timeframe;
            this.continueWithDailyLife = continueWithDailyLife;
            this.areaOfBody = areaOfBody;
            this.notes = notes;
        }

        public string NameOfSymptom { get => nameOfSymptom;  }
        public double Severeness { get => severeness;  }
        public string Timeframe { get => timeframe;  }
        public bool ContinueWithDailyLife { get => continueWithDailyLife; }
        public string AreaOfBody { get => areaOfBody;}
        public string Notes { get => notes; }
    }
}
