using System;
using System.Collections.Generic;
using System.Text;

namespace App1
{
    static class MedicalDetails
    {

        public static List<string> diganoses { get; set; }
        public static Boolean inDatabase { get; set; }

        public static String diganosesPretty {
            get
            {
                string names = "";
                for (int i = 0; i < MedicalDetails.diganoses.Count; i++)
                {
                    if (i == (MedicalDetails.diganoses.Count - 1))
                    {
                        names += MedicalDetails.diganoses[i];
                    }
                    else
                    {
                        names += MedicalDetails.diganoses[i] + ", ";
                    }
                }
                return names;
            }
        }

        public static String diganosesToCommaSeperatedString
        {
            get
            {
                string names = "";
                foreach(String name in MedicalDetails.diganoses)
                {
                    names += name + ",";
                }
                return names;
            }
        }
        public static List<Medication> medications { get; set; }


    }
}
