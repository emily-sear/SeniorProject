using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class Medication
    {
        private string name { get; set; }
        private string dosage { get; set; }
        private DateTime dateStarted { get; set; }
        private string orderingDoctor { get; set; }

        public Medication(string name, string dosage, DateTime dateStarted, string orderingDoctor)
        {
            this.name = name;
            this.dosage = dosage;
            this.dateStarted = dateStarted;
            this.orderingDoctor = orderingDoctor;

        }

        public void display()
        {
            Console.WriteLine("Name: " + this.name);
            Console.WriteLine("Dosage: " + this.dosage);
            Console.WriteLine("Date Started: " + this.dateStarted.ToString());
            Console.WriteLine("Ordering Doctor: " + this.orderingDoctor);
        }

        public string getName()
        {
            return this.name;
        }
    }

}
