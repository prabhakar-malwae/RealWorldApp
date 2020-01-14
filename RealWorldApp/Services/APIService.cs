using Newtonsoft.Json;
using RealWorldApp.Droid.Model;
using RealWorldApp.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace RealWorldApp.Services
{
    public static class APIService
    {
        public static async Task<bool> RegisterUser(string name,string email,string password)
        {
            var registerModel = new RegisterModel()
            {
                Name = name,
                Email=email,
                Password=password
            };

            var httpClient = new HttpClient(new System.Net.Http.HttpClientHandler());
            var json=JsonConvert.SerializeObject(registerModel);
            var content=new StringContent(json, Encoding.UTF8,"application/json");
            var response = await httpClient.PostAsync("https://192.168.1.108:45457/api/accounts/register", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static async Task<bool> Login(string email, string password)
        {
            var loginModel = new LoginModel()
            {
              
                Email = email,
                Password = password
            };

            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(loginModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://localhost:45456/api/accounts/login", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                var jsonResult =  await response.Content.ReadAsStringAsync();
                var result=JsonConvert.DeserializeObject<Token>(jsonResult);
                Preferences.Set("accessToken", result.access_token);
                return true;
            }
        }

        public static async Task<bool>  ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            var changePassword = new ChangePasswordModel()
            {
                OldPassword = oldPassword,
                NewPassword = newPassword,
                ConfirmPassword = confirmPassword
            };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(changePassword);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.PostAsync("https://localhost:44383/api/accounts/ChangePassword", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else 
            {
                return true;
                
            }
        }

        public static async Task<bool> EditPhoneNumber(string phonenumber)
        {
         
            var httpClient = new HttpClient();
            var content = new StringContent("$Number={phoneNumber}", Encoding.UTF8, "application/x-www-form-urlencoded");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.PostAsync("https://localhost:44383/api/accounts/EditPhoneNumber", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;

            }
        }

        public static async Task<bool> EditUserProfile(byte[] imageArray)
        {
           
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(imageArray);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.PostAsync("https://localhost:44383/api/accounts/EditUserProfile", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;

            }
        }

        public static  async Task<UserImageModel>  GetUserProfileImage()
        {
            var httpClient = new HttpClient();
         
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var result = await httpClient.GetStringAsync("https://localhost:44383/api/accounts/UserProfileImage");
            var response=JsonConvert.DeserializeObject<UserImageModel>(result);

            return response;

        }

        public static async Task<List<Category>> GetCategories()
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var result = await httpClient.GetStringAsync("https://localhost:44383/api/Categories");
            var response = JsonConvert.DeserializeObject<List<Category>>(result);
            return response;

        }

        public static async Task<bool> AddImage(int vehicleid,byte[] imageArray)
        {
            var vehicleImage = new VehicleImage()
            {
                ImageId = vehicleid,
                ImageArray = imageArray

            };
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(vehicleImage);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.PostAsync("https://localhost:44383/api/Images", content);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            else
            {
                return true;

            }
        }

        public static async Task<VehicleDetail> GetVehicleDetail(int id)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var result = await httpClient.GetStringAsync("https://localhost:44383/api/Vehicles/VehicleDetails?id={id}");
            var response = JsonConvert.DeserializeObject<VehicleDetail>(result);
            return response;

        }

        public static async Task<List<VehicleByCategory>> GetVehicleByCategory(int categoryId)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var result = await httpClient.GetStringAsync("https://localhost:44383/api/Vehicles?categoryId?={categoryId}");
            var response = JsonConvert.DeserializeObject<List<VehicleByCategory>>(result);
            return response;

        }
        public static async Task<List<SearchVehicle>> SearchVehicle(string  search)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var result = await httpClient.GetStringAsync("https://localhost:44383/api/Vehicles/SearchVehicles?search={search}");
            var response = JsonConvert.DeserializeObject<List<SearchVehicle>>(result);
            return response;

        }


        public static async Task<Vehicle> AddVehicle(Vehicle vehicleDetails)
        {
          
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(vehicleDetails);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var response = await httpClient.PostAsync("https://localhost:44383/api/Vehicles", content);
            var jsonResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Vehicle>(jsonResult);

        }

        public static async Task<List<HotAndNewAd>> GetHotAndNewAdds()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var result = await httpClient.GetStringAsync("https://localhost:44383/api/Vehicles/HotAndNewAds");
            var response = JsonConvert.DeserializeObject<List<HotAndNewAd>>(result);
            return response;
        }

        public static async Task<List<MyAd>> GetMyAds()
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Preferences.Get("accessToken", string.Empty));
            var result = await httpClient.GetStringAsync("https://localhost:44383/api/Vehicles/MyAds");
            var response = JsonConvert.DeserializeObject<List<MyAd>>(result);
            return response;
        }

    }
}
