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

        public ClientDaoManager()
        {
            httpClient = new HttpClient();
        }

        #region Delete

        public bool DeleteCountry(int countryID)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("http://localhost:54786/hikehandler/Data/DeleteCountry");
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
            request.RequestUri = new Uri("http://localhost:54786/hikehandler/Data/DeleteCP");
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
            request.RequestUri = new Uri("http://localhost:54786/hikehandler/Data/DeleteHike");
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
            request.RequestUri = new Uri("http://localhost:54786/hikehandler/Data/DeleteRegion");
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
            request.RequestUri = new Uri("http://localhost:54786/hikehandler/Data/GetAllCountryNames");
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
            request.RequestUri = new Uri("http://localhost:54786/hikehandler/Data/GetAllCPs");
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
            request.RequestUri = new Uri("http://localhost:54786/hikehandler/Data/GetAllCPsOfRegion");
            request.Content = new StringContent(regionID.ToString());
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
            request.RequestUri = new Uri("http://localhost:54786/hikehandler/Data/GetAllRegionsOfCountry");
            request.Content = new StringContent(countryID.ToString());
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
            request.RequestUri = new Uri("http://localhost:54786/hikehandler/Data/GetBaseFormSummary");
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
            request.RequestUri = new Uri("http://localhost:54786/hikehandler/Data/GetCPsFromList");
            request.Content = new StringContent(JsonConvert.SerializeObject(cpIDList), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("GET");

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
            request.RequestUri = new Uri("http://localhost:54786/hikehandler/Data/GetCPTypes");
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
            request.RequestUri = new Uri("http://localhost:54786/hikehandler/Data/GetHikeTypes");
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
            request.RequestUri = new Uri("http://localhost:54786/hikehandler/Data/SearchCountry");
            request.Content = new StringContent(JsonConvert.SerializeObject(country), Encoding.UTF8, "application/json");
            request.Method = new HttpMethod("GET");

            HttpResponseMessage response = httpClient.SendAsync(request).Result;
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
            return JsonConvert.DeserializeObject<List<CountryForView>>(response.Content.ReadAsStringAsync().Result);
        }

        public CountryForView SearchCountry(int countryID)
        {
            throw new NotImplementedException();
        }

        public List<CPForView> SearchCP(CPForSearch cp)
        {
            throw new NotImplementedException();
        }

        public CPForView SearchCP(int cpID)
        {
            throw new NotImplementedException();
        }

        public HikeForView SearchHike(int hikeID)
        {
            throw new NotImplementedException();
        }

        public List<HikeForView> SearchHike(HikeForSearch hike, bool anyCPOrder)
        {
            throw new NotImplementedException();
        }

        public List<HikeRegionForView> SearchRegion(HikeRegionForSearch region)
        {
            throw new NotImplementedException();
        }

        public HikeRegionForView SearchRegion(int regionID)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Save

        public bool SaveCountry(CountryForSave country)
        {
            throw new NotImplementedException();
        }

        public bool SaveCP(CPForSave cp)
        {
            throw new NotImplementedException();
        }

        public bool SaveHike(HikeForSave hike)
        {
            throw new NotImplementedException();
        }

        public bool SaveRegion(HikeRegionForSave region)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Update

        public bool UpdateCountry(CountryForUpdate country)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCP(CPForUpdate cp)
        {
            throw new NotImplementedException();
        }

        public bool UpdateHike(HikeForUpdate hike)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRegion(HikeRegionForUpdate region)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
