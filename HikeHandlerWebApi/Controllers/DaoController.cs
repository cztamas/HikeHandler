using HikeHandler.Exceptions;
using HikeHandler.Interfaces;
using HikeHandler.ModelObjects;
using HikeHandler.ServiceLayer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace HikeHandlerWebApi.Controllers
{
    [RoutePrefix("hikehandler/Data")]
    public class DaoController : ApiController, IDAOManager
    {
        private IDAOManager daoManager;

        public DaoController()
        {
            string connectionString = "server=localhost; database=hikehandler; uid=HikeHandler; pwd=szilvasunicum;";
            MySqlConnection connection = new MySqlConnection(connectionString);
            try
            {
                connection.Open();
                daoManager = new DAOManager(connection);
            }
            catch (Exception)
            { }
        }

        public DaoController(IDAOManager manager)
        {
            daoManager = manager;
        }

        #region Delete

        [Route("DeleteCountry")]
        [HttpDelete]
        public bool DeleteCountry(int countryID)
        {
            try
            {
                return daoManager.DeleteCountry(countryID);
            }
            catch (NotDeletableException)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.StatusCode = HttpStatusCode.Forbidden;
                throw new HttpResponseException(errorResponse);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("DeleteCP")]
        [HttpDelete]
        public bool DeleteCP(CPForView cp)
        {
            try
            {
                return daoManager.DeleteCP(cp);
            }
            catch (NotDeletableException)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.StatusCode = HttpStatusCode.Forbidden;
                throw new HttpResponseException(errorResponse);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("DeleteHike")]
        [HttpDelete]
        public bool DeleteHike(HikeForView hike)
        {
            try
            {
                return daoManager.DeleteHike(hike);
            }
            catch (NotDeletableException)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.StatusCode = HttpStatusCode.Forbidden;
                throw new HttpResponseException(errorResponse);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("DeleteRegion")]
        [HttpDelete]
        public bool DeleteRegion(HikeRegionForView region)
        {
            try
            {
                return daoManager.DeleteRegion(region);
            }
            catch (NotDeletableException)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.StatusCode = HttpStatusCode.Forbidden;
                throw new HttpResponseException(errorResponse);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        #endregion

        #region Get/Search

        [Route("GetAllCountryNames")]
        [HttpGet]
        public List<NameAndID> GetAllCountryNames()
        {
            try
            {
                return daoManager.GetAllCountryNames();
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("GetAllCPs")]
        [HttpGet]
        public List<NameAndID> GetAllCPs()
        {
            try
            {
                return daoManager.GetAllCPs();
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("GetAllCPsOfRegion/{regionID}")]
        [HttpGet]
        public List<NameAndID> GetAllCPsOfRegion(int regionID)
        {
            try
            {
                return daoManager.GetAllCPsOfRegion(regionID);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("GetAllRegionsOfCountry/{countryID}")]
        [HttpGet]
        public List<NameAndID> GetAllRegionsOfCountry(int countryID)
        {
            try
            {
                return daoManager.GetAllRegionsOfCountry(countryID);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("GetBaseFormSummary")]
        [HttpGet]
        public BaseFormSummary GetBaseFormSummary()
        {
            try
            {
                return daoManager.GetBaseFormSummary();
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("GetCPsFromList")]
        [HttpPost]
        public List<NameAndID> GetCPsFromList(List<int> cpIDList)
        {
            try
            {
                return daoManager.GetCPsFromList(cpIDList);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("GetCPTypes")]
        [HttpGet]
        public List<NameAndID> GetCPTypes()
        {
            try
            {
                return daoManager.GetCPTypes();
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("GetHikeTypes")]
        [HttpGet]
        public List<NameAndID> GetHikeTypes()
        {
            try
            {
                return daoManager.GetHikeTypes();
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("SearchCountry")]
        [HttpPost]
        public List<CountryForView> SearchCountry(CountryForSearch country)
        {
            try
            {
                return daoManager.SearchCountry(country);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("SearchCountry/{countryID}")]
        [HttpGet]
        public CountryForView SearchCountry(int countryID)
        {
            try
            {
                return daoManager.SearchCountry(countryID);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("SearchCP")]
        [HttpPost]
        public List<CPForView> SearchCP(CPForSearch cp)
        {
            try
            {
                return daoManager.SearchCP(cp);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("SearchCP/{cpID}")]
        [HttpGet]
        public CPForView SearchCP(int cpID)
        {
            try
            {
                return daoManager.SearchCP(cpID);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("SearchHike/{hikeID}")]
        [HttpGet]
        public HikeForView SearchHike(int hikeID)
        {
            try
            {
                return daoManager.SearchHike(hikeID);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("SearchHike")]
        [HttpPost]
        public List<HikeForView> SearchHike(HikeForSearch hike)
        {
            try
            {
                return daoManager.SearchHike(hike);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("SearchRegion")]
        [HttpPost]
        public List<HikeRegionForView> SearchRegion(HikeRegionForSearch region)
        {
            try
            {
                return daoManager.SearchRegion(region);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("SearchRegion/{regionID}")]
        [HttpGet]
        public HikeRegionForView SearchRegion(int regionID)
        {
            try
            {
                return daoManager.SearchRegion(regionID);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        #endregion

        #region Save

        [Route("SaveCountry")]
        [HttpPost]
        public bool SaveCountry(CountryForSave country)
        {
            try
            {
                return daoManager.SaveCountry(country);
            }
            catch (DuplicateItemNameException)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.StatusCode = HttpStatusCode.Forbidden;
                throw new HttpResponseException(errorResponse);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("SaveCP")]
        [HttpPost]
        public bool SaveCP(CPForSave cp)
        {
            try
            {
                return daoManager.SaveCP(cp);
            }
            catch (DuplicateItemNameException)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.StatusCode = HttpStatusCode.Forbidden;
                throw new HttpResponseException(errorResponse);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("SaveHike")]
        [HttpPost]
        public bool SaveHike(HikeForSave hike)
        {
            try
            {
                return daoManager.SaveHike(hike);
            }
            catch (DuplicateDateException)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.StatusCode = HttpStatusCode.Forbidden;
                throw new HttpResponseException(errorResponse);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("SaveRegion")]
        [HttpPost]
        public bool SaveRegion(HikeRegionForSave region)
        {
            try
            {
                return daoManager.SaveRegion(region);
            }
            catch (DuplicateItemNameException)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.StatusCode = HttpStatusCode.Forbidden;
                throw new HttpResponseException(errorResponse);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        #endregion

        #region Update

        [Route("UpdateCountry")]
        [HttpPut]
        public bool UpdateCountry(CountryForUpdate country)
        {
            try
            {
                return daoManager.UpdateCountry(country);
            }
            catch (DuplicateItemNameException)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.StatusCode = HttpStatusCode.Forbidden;
                throw new HttpResponseException(errorResponse);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("UpdateCP")]
        [HttpPut]
        public bool UpdateCP(CPForUpdate cp)
        {
            try
            {
                return daoManager.UpdateCP(cp);
            }
            catch (DuplicateItemNameException)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.StatusCode = HttpStatusCode.Forbidden;
                throw new HttpResponseException(errorResponse);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("UpdateHike")]
        [HttpPut]
        public bool UpdateHike(HikeForUpdate hike)
        {
            try
            {
                return daoManager.UpdateHike(hike);
            }
            catch (DuplicateDateException)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.StatusCode = HttpStatusCode.Forbidden;
                throw new HttpResponseException(errorResponse);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        [Route("UpdateRegion")]
        [HttpPut]
        public bool UpdateRegion(HikeRegionForUpdate region)
        {
            try
            {
                return daoManager.UpdateRegion(region);
            }
            catch (DuplicateItemNameException)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.StatusCode = HttpStatusCode.Forbidden;
                throw new HttpResponseException(errorResponse);
            }
            catch (Exception ex)
            {
                HttpResponseMessage errorResponse = new HttpResponseMessage();
                errorResponse.Content = new StringContent(ex.Message, Encoding.UTF8, "application/json");
                errorResponse.StatusCode = HttpStatusCode.InternalServerError;
                throw new HttpResponseException(errorResponse);
            }
        }

        #endregion
    }
}