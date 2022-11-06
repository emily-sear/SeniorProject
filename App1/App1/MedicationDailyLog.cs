using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    class MedicationDailyLog
    {
        private String medicationName;
        private String timeTaken;
        private String dosage;
        private Boolean dailyMed;
        private String notes;
        
        public MedicationDailyLog(String medicationName, String timeTaken, String dosage, Boolean dailyMed, String notes)
        {
            this.medicationName = medicationName;
            this.timeTaken = timeTaken;
            this.dosage = dosage;
            this.dailyMed = dailyMed;
            this.notes = notes;
        }

        public String MedicationName { get => this.medicationName; }
        public String TimeTaken { get => this.timeTaken; }
        public String Dosage { get => this.dosage; }
        public Boolean DailyMed { get => this.dailyMed; }
        public String Notes { get => this.notes; }
    }
}
