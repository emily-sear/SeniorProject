using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using SQLite;

namespace App1
{
    static class User
    {
 
        public static string firstName { get; set; }
        public static string lastName { get; set; }
        public static string email { get; set; }
        public static userTypes userType;
        public enum userTypes { UNDEFINED, PATIENT, CAREGIVER, OTHER }




    }
}
