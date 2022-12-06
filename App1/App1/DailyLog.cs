using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class DailyLog
    {
        public Boolean ValuesSet { get; set; }
        public Boolean SymptomsSet { get; set; }
        public Boolean MedicationsSet { get; set; }
        private double painScale;
        private double moodScale;
        private double fatigueScale;
        private double overallScale;
        private double avgHeartRate;
        private Boolean onPeriod;
        private List<SymptomDailyLog> symptoms;
        private List<MedicationDailyLog> medications;
        public DateTime CurrentDate 
        {
            get;
            set;
        }

        public double PainScale { get => painScale; set => painScale = value; }
        public double MoodScale { get => moodScale; set => moodScale = value; }
        public double FatigueScale { get => fatigueScale; set => fatigueScale = value; }
        public double OverallScale { get => overallScale; set => overallScale = value; }
        public double AvgHeartRate { get => avgHeartRate; set => avgHeartRate = value; }
        public bool OnPeriod { get => onPeriod; set => onPeriod = value; }
        internal List<SymptomDailyLog> Symptoms { get => symptoms; set => symptoms = value; }
        internal List<MedicationDailyLog> Medications { get => medications; set => medications = value; }

        public DailyLog()
        {
            this.Symptoms = new List<SymptomDailyLog>();
            this.Medications = new List<MedicationDailyLog>();
            this.SymptomsSet = false;
            this.MedicationsSet = false;
            this.ValuesSet = false;
        }
    }
}
