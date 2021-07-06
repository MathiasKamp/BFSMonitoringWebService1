using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using BFSMonitoringWebService1.Models;
using Microsoft.AspNetCore.Mvc;



namespace BFSMonitoringWebService1.Controllers
{
    
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class BfsAgent : ApiController
    {
        private IList<StatusMessage> message = null;

        private  DatabaseManager dataBaseManager = new DatabaseManager();
        // GET
        [Microsoft.AspNetCore.Mvc.HttpGet]
        public HttpStatusCodeResult GetAllMessages()
        {

            if (message.Count == 0)
            {
                return new HttpNotFoundResult();
            }

            return new HttpStatusCodeResult(200);
            
        }

        public void AddMessageToList(StatusMessage statusMessage)
        {
            message.Add(statusMessage);
        }

        [Microsoft.AspNetCore.Mvc.AcceptVerbs("POST","PUT")]
        public void Add(StatusMessage statusMessage)
        {
            dataBaseManager.InsertStatusMessage(statusMessage);
           //return new HttpStatusCodeResult(200);
        }
    }
}
