using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    public class Medication
    {
        private string name;
        private string dosage;
        private DateTime dateStarted;
        private string orderingDoctor;

        public string Name { get => name; set => name = value; }
        public string Dosage { get => dosage; set => dosage = value; }
        public DateTime DateStarted { get => dateStarted; set => dateStarted = value; }
        public string OrderingDoctor { get => orderingDoctor; set => orderingDoctor = value; }

        public Medication(string name, string dosage, DateTime dateStarted, string orderingDoctor)
        {
            this.Name = name;
            this.Dosage = dosage;
            this.DateStarted = dateStarted;
            this.OrderingDoctor = orderingDoctor;

        }

        public void display()
        {
            Console.WriteLine("Name: " + this.Name);
            Console.WriteLine("Dosage: " + this.Dosage);
            Console.WriteLine("Date Started: " + this.DateStarted.ToString());
            Console.WriteLine("Ordering Doctor: " + this.OrderingDoctor);
        }
    }

}
