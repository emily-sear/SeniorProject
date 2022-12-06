using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;

using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace App1
{
    public class RestService 
    {
        HttpClient client;
        HttpClientHandler handler;
        JsonSerializer serializer;

        public RestService ()
        {
            this.client = new HttpClient();
        }

        public async Task<List<DailyLog>> GetDailyLogDataAsync(String query)
        {
            string url = "https://ga0f66930313625-chronicallytrackingdailylogs.adb.us-phoenix-1.oraclecloudapps.com/ords/admin/soda/latest/DailyLogs";
            if (query != "")
            {
                url += "?q=" + query;
            }
            Uri uri = new Uri(url);
            Console.WriteLine(url);
            HttpResponseMessage response = await client.GetAsync(uri);
            
            var responseBody = (JObject)JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
            Console.WriteLine(responseBody);
            var items = (JArray)responseBody["items"];
            if(items.Count == 0)
            {
                return new List<DailyLog>();
            }
            List<DailyLog> dailyLogs = new List<DailyLog>();
            foreach(var item in items)
            {
                DailyLog dailyLog = new DailyLog();
                dailyLog.OverallScale = (Double)item["value"]["OverallScale"];
                dailyLog.PainScale = (Double)item["value"]["PainScale"];
                dailyLog.MoodScale = (Double)item["value"]["MoodScale"];
                dailyLog.FatigueScale = (Double)item["value"]["FatigueScale"];
                dailyLog.AvgHeartRate = (Double)item["value"]["AvgHeartRate"];
                dailyLog.OnPeriod = (Boolean)item["value"]["OnPeriod"];
                dailyLog.CurrentDate = (DateTime)item["value"]["Datetime"];

                foreach(var medication in item["value"]["Medications"])
                {
                    MedicationDailyLog med = new MedicationDailyLog((string)medication["MedicationName"], (string)medication["TimeTaken"], (string) medication["Dosage"], (bool)medication["DailyMed"], (string) medication["Notes"]);
                    dailyLog.Medications.Add(med);
                }

                foreach(var sym in item["value"]["Symptoms"])
                {
                    SymptomDailyLog symptom = new SymptomDailyLog((string)sym["NameOfSymptom"], (double)sym["Severeness"], (string)sym["Timeframe"], (Boolean)sym["ContinueWithDailyLife"], (string)sym["AreaOfBody"], (string)sym["Notes"]);
                    dailyLog.Symptoms.Add(symptom);
                }
                dailyLogs.Add(dailyLog);
            }
            return dailyLogs;
        }
        public async Task<Boolean> SignUserIn(String email)
        {
            Uri uri = new Uri("https://ga0f66930313625-userdatabase.adb.us-phoenix-1.oraclecloudapps.com/ords/admin/account/user/" + email);
            HttpResponseMessage response = await client.GetAsync(uri);
            if(response.IsSuccessStatusCode)
            {
                var responseBody = (JObject)JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine(responseBody);
                var items = (JArray)responseBody["items"];
                if(items.Count == 1)
                {
                    User.firstName = (string)items[0]["first_name"];
                    User.lastName = (string)items[0]["last_name"];
                    User.email = (string)items[0]["email"];
                    if ((string)items[0]["user_type"] == "PATIENT")
                    {
                        User.userType = User.userTypes.PATIENT;
                    }
                    else if ((string)items[0]["user_type"] == "CAREGIVER")
                    {
                        User.userType = User.userTypes.CAREGIVER;
                    }
                    else
                    {
                        User.userType = User.userTypes.OTHER;
                    }
                    User.inDatabase = true;
                    return true;
                }
            }
            return false;

        }
        public async Task<Boolean> PostDailyLogDataAsync(DailyLog dailyLog)
        {
            Uri uri = new Uri("https://ga0f66930313625-chronicallytrackingdailylogs.adb.us-phoenix-1.oraclecloudapps.com/ords/admin/soda/latest/DailyLogs");
            StringContent content = new StringContent(JsonConvert.SerializeObject(new
            {
                Email = User.email,
                OverallScale = dailyLog.OverallScale,
                PainScale = dailyLog.PainScale,
                MoodScale = dailyLog.MoodScale, 
                FatigueScale = dailyLog.FatigueScale,
                AvgHeartRate = dailyLog.AvgHeartRate,
                OnPeriod = dailyLog.OnPeriod,
                Datetime = dailyLog.CurrentDate,
                Medications = dailyLog.Medications,
                Symptoms = dailyLog.Symptoms

            }), Encoding.UTF8, "application/json") ;
            HttpResponseMessage response = await client.PostAsync(uri, content);
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("There was an error with the database");
                Console.WriteLine(response);
                User.inDatabase = false;
                
            }
            return true;
        }

        public async Task<Boolean> PostAccountCreationDataAsync ()
        {
          
            Uri uri = new Uri("https://ga0f66930313625-userdatabase.adb.us-phoenix-1.oraclecloudapps.com/ords/admin/account/creation/user");
            StringContent content = new StringContent(JsonConvert.SerializeObject(new
            {
                first_name = User.firstName,
                last_name = User.lastName,
                email = User.email,
                user_type = User.userType.ToString(),
                password = "1234",
            }), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(uri, content);

            await this.PostAccountDiganoses();
            await this.PostAccountMedications();
            if (response.IsSuccessStatusCode)
            {
                
                User.inDatabase = true;
                return true;
            } else
            {
                Console.WriteLine("There was an error with the database");
                Console.WriteLine(response);
                User.inDatabase = false;
                return false;
            }
        }

        private async Task<Boolean> PostAccountDiganoses()
        {
            Uri uri = new Uri(" https://ga0f66930313625-userdatabase.adb.us-phoenix-1.oraclecloudapps.com/ords/admin/account/creation/diganoses");
            Boolean isSuccess = true;
            foreach (String name in MedicalDetails.diganoses)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(new
                {
                    diganoses_name = name,
                    user_email = User.email
                }), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, content);
                if(!response.IsSuccessStatusCode)
                {
                    isSuccess = false;
                }
            }
            if(isSuccess)
            {
                MedicalDetails.inDatabase = true;
                return true;
            } 
            else
            {
                Console.WriteLine("There was an error with the database");
                MedicalDetails.inDatabase = false;
                return false;
            }
            
        }

        private async Task<Boolean> PostAccountMedications()
        {
            Uri uri = new Uri("https://ga0f66930313625-userdatabase.adb.us-phoenix-1.oraclecloudapps.com/ords/admin/account/creation/medication");
            Boolean isSuccess = true;
            foreach (Medication med in MedicalDetails.medications)
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(new
                {
                    medication_name = med.Name,
                    dosage = med.Dosage,
                    date_started = med.DateStarted.ToString("yyyy-MM-dd"),
                    ordering_doctor = med.OrderingDoctor,
                    user_email = User.email
                }), Encoding.UTF8, "application/json");
                Console.WriteLine(med.DateStarted.ToString("yyyy-MM-dd"));
                HttpResponseMessage response = await client.PostAsync(uri, content);
                if (!response.IsSuccessStatusCode)
                {
                    isSuccess = false;
                    Console.WriteLine("There was an error with the database");
                    Console.WriteLine(response);

                }
            }
            if (isSuccess)
            {
                MedicalDetails.inDatabase = true;
                return true;
            }
            else
            {
                Console.WriteLine("There was an error with the database");
                MedicalDetails.inDatabase = false;
                return false;
            }
        }
    }
}
