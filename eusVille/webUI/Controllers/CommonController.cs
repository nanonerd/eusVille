using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using webUI.Models;

namespace webUI.Controllers
{    
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }
                
        // POST
        public void Common(string Filter, string FilterData)
        {
            string jsonReturn = "";

            try
            {
                switch (Filter)
                {
                    case "ErrorHandle":  // log error into eusCommon.dbo.Admin_ErrorLog
                        {
                            // Parse out JSON data
                            JObject o = JObject.Parse(FilterData);

                            string user = o.SelectToken("user").ToString();
                            string detail =  o.SelectToken("detail").ToString();
                            string action =  o.SelectToken("action").ToString();
                            string errorLocation =  o.SelectToken("errorLocation").ToString();
                            string level = o.SelectToken("level").ToString();

                            if (level == "high")
                            {
                                // email admin to notify !!
                            }


                            using (eusCommonEntities ent = new eusCommonEntities())
                            {
                                Admin_ErrorLog log = new Admin_ErrorLog();

                                log.User = user;
                                log.Action = action;
                                log.Details = detail;
                                log.ErrorLocation = errorLocation;
                                log.Level = level; 
                                log.Timestamp = DateTime.Now;
                                log.IsOpen = true;

                                ent.Admin_ErrorLog.Add(log);
                                ent.SaveChanges();
                                                                
                                break;
                            }
                        }

                    case "Email":
                        {
                            // Send email !
                            break;
                        }

                    default:
                        {
                            //var seriesContent = new Dictionary<string, string>
                            //        {
                            //            {"error", "true"} , {"message", "Controller: eusVote_Topic.Topic doesn't have matching filter for filter = " + Filter }
                            //        };

                            //jsonReturn = JsonConvert.SerializeObject(seriesContent);
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                var seriesContent = new Dictionary<string, string>
                {
                    {"user", "inside catch"}, {"detail", ex.ToString()}, {"action", "Failed to log error."},
                    {"errorLocation", "CommonController > ErrorHandle "}
                };

                jsonReturn = JsonConvert.SerializeObject(seriesContent);

                // Log error
                CommonController cont = new CommonController();
                cont.Common("ErrorHandle", jsonReturn);
            }

            //return jsonReturn;    
        }
    }
}