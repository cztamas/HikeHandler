using HikeHandler.Exceptions;
using HikeHandler.Interfaces;
using HikeHandler.ModelObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace HikeHandler_Client
{
    public class ClientDaoManager : IDAOManager
    {
        private HttpClient httpClient;
        private string host = "localhost:54786/"; //"192.168.0.180/hikehandler";  

        public ClientDaoManager()
        {
            httpClient = new HttpClient();
        }

        #region Delete

        public bool DeleteCountry(int countryID)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/DeleteCountry");
            request.Content = new StringContent(countryID.ToString());
            request.Method = new HttpMethod("DELETE");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new NotDeletableException();
                }
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
        }

        public bool DeleteCP(CPForView cp)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/DeleteCP");
            request.Content = new StringContent(JsonConvert.SerializeObject(cp), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("DELETE");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new NotDeletableException();
                }
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
        }

        public bool DeleteHike(HikeForView hike)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/DeleteHike");
            request.Content = new StringContent(JsonConvert.SerializeObject(hike), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("DELETE");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new NotDeletableException();
                }
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
        }

        public bool DeleteRegion(HikeRegionForView region)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/DeleteRegion");
            request.Content = new StringContent(JsonConvert.SerializeObject(region), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("DELETE");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    throw new NotDeletableException();
                }
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
        }

        #endregion

        #region Get/Search

        public List<NameAndID> GetAllCountryNames()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/GetAllCountryNames");
            request.Method = new HttpMethod("GET");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<List<NameAndID>>(response.Content.ReadAsStringAsync().Result);
        }

        public List<NameAndID> GetAllCPs()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/GetAllCPs");
            request.Method = new HttpMethod("GET");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<List<NameAndID>>(response.Content.ReadAsStringAsync().Result);
        }

        public List<NameAndID> GetAllCPsOfRegion(int regionID)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/GetAllCPsOfRegion/" + regionID);
            //request.Content = new StringContent(JsonConvert.SerializeObject(regionID), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("GET");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<List<NameAndID>>(response.Content.ReadAsStringAsync().Result);
        }

        public List<NameAndID> GetAllRegionsOfCountry(int countryID)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/GetAllRegionsOfCountry/" + countryID);
            //request.Content = new StringContent(JsonConvert.SerializeObject(countryID), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("GET");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<List<NameAndID>>(response.Content.ReadAsStringAsync().Result);
        }

        public BaseFormSummary GetBaseFormSummary()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/GetBaseFormSummary");
            request.Method = new HttpMethod("GET");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<BaseFormSummary>(response.Content.ReadAsStringAsync().Result);
        }

        public List<NameAndID> GetCPsFromList(List<int> cpIDList)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/GetCPsFromList");
            request.Content = new StringContent(JsonConvert.SerializeObject(cpIDList), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("POST");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<List<NameAndID>>(response.Content.ReadAsStringAsync().Result);
        }

        public List<NameAndID> GetCPTypes()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/GetCPTypes");
            request.Method = new HttpMethod("GET");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<List<NameAndID>>(response.Content.ReadAsStringAsync().Result);
        }

        public List<NameAndID> GetHikeTypes()
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/GetHikeTypes");
            request.Method = new HttpMethod("GET");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<List<NameAndID>>(response.Content.ReadAsStringAsync().Result);
        }

        public List<CountryForView> SearchCountry(CountryForSearch country)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/SearchCountry");
            request.Content = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("POST");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<List<CountryForView>>(response.Content.ReadAsStringAsync().Result);
        }

        public CountryForView SearchCountry(int countryID)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/SearchCountry/" + countryID);
            //request.Content = new StringContent(countryID.ToString());
            request.Method = new HttpMethod("GET");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<CountryForView>(response.Content.ReadAsStringAsync().Result);
        }

        public List<CPForView> SearchCP(CPForSearch cp)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/SearchCP");
            request.Content = new StringContent(JsonConvert.SerializeObject(cp), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("POST");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<List<CPForView>>(response.Content.ReadAsStringAsync().Result);
        }

        public CPForView SearchCP(int cpID)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/SearchCP/" + cpID);
            request.Content = new StringContent(cpID.ToString());
            request.Method = new HttpMethod("GET");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<CPForView>(response.Content.ReadAsStringAsync().Result);
        }

        public HikeForView SearchHike(int hikeID)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/SearchHike/" + hikeID);
            //request.Content = new StringContent(hikeID.ToString());
            request.Method = new HttpMethod("GET");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<HikeForView>(response.Content.ReadAsStringAsync().Result);
        }

        public List<HikeForView> SearchHike(HikeForSearch hike)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/SearchHike");
            request.Content = new StringContent(JsonConvert.SerializeObject(hike), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("POST");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<List<HikeForView>>(response.Content.ReadAsStringAsync().Result);
        }

        public List<HikeRegionForView> SearchRegion(HikeRegionForSearch region)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/SearchRegion");
            request.Content = new StringContent(JsonConvert.SerializeObject(region), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("POST");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<List<HikeRegionForView>>(response.Content.ReadAsStringAsync().Result);
        }

        public HikeRegionForView SearchRegion(int regionID)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/SearchRegion/" + regionID);
            //request.Content = new StringContent(regionID.ToString());
            request.Method = new HttpMethod("GET");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<HikeRegionForView>(response.Content.ReadAsStringAsync().Result);
        }

        #endregion

        #region Save

        public bool SaveCountry(CountryForSave country)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/SaveCountry");
            request.Content = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("POST");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
        }

        public bool SaveCP(CPForSave cp)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/SaveCP");
            request.Content = new StringContent(JsonConvert.SerializeObject(cp), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("POST");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
        }

        public bool SaveHike(HikeForSave hike)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/SaveHike");
            request.Content = new StringContent(JsonConvert.SerializeObject(hike), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("POST");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
        }

        public bool SaveRegion(HikeRegionForSave region)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/SaveRegion");
            request.Content = new StringContent(JsonConvert.SerializeObject(region), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("POST");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
        }

        #endregion

        #region Update

        public bool UpdateCountry(CountryForUpdate country)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/UpdateCountry");
            request.Content = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("PUT");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
        }

        public bool UpdateCP(CPForUpdate cp)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/UpdateCP");
            request.Content = new StringContent(JsonConvert.SerializeObject(cp), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("PUT");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
        }

        public bool UpdateHike(HikeForUpdate hike)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/UpdateHike");
            request.Content = new StringContent(JsonConvert.SerializeObject(hike), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("PUT");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
        }

        public bool UpdateRegion(HikeRegionForUpdate region)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://" + host + "/hikehandler/Data/UpdateRegion");
            request.Content = new StringContent(JsonConvert.SerializeObject(region), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("PUT");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<bool>(response.Content.ReadAsStringAsync().Result);
        }

        #endregion
    }
}
