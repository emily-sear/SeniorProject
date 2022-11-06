using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    static class DailyLog
    {
        public static Boolean ValuesSet { get; set; }
        public static Boolean SymptomsSet { get; set; }
        public static Boolean MedicationsSet { get; set; }
        public static double PainScale { get; set; }
        public static double MoodScale { get; set; }
        public static double FatigueScale { get; set; }
        public static double AvgHeartRate { get; set; }
        public static Boolean OnPeriod { get; set; }

        public static List<SymptomDailyLog> Symptoms { get; set; }
        public static List<MedicationDailyLog> Medications { get; set; }
        public static DateTime CurrentDate 
        { 
            get
            {
                return DateTime.Now;
            } 
        }
    }
}
