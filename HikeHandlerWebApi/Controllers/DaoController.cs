using HikeHandler.DAOs;
using HikeHandler.ModelObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace HikeHandlerWebApi.Controllers
{
    public class DaoController : ApiController
    {
        private CountryDao countryDao;
        private RegionDao regionDao;
        private CPDao cpDao;
        private HikeDao hikeDao;

        [Route("hikehandler/savecountry")]
        [HttpPut]
        public void SaveCountry(CountryForSave country)
        { }
    }
}