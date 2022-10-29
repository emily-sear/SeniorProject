using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace App1
{
    public class RestService 
    {
        HttpClient client;
        JsonSerializer serializer;

        public RestService ()
        {
            client = new HttpClient();
        }

        public async void RefreshDataAsync () 
        {
            Uri uri = new Uri(string.Format("https://ga0f66930313625-userdatabase.adb.us-phoenix-1.oraclecloudapps.com/ords/admin/test/test"));
            HttpResponseMessage response = await client.GetAsync(uri);

            if(response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }
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
