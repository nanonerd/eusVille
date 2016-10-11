using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webUI.Models;

namespace webUI.Common
{    
    // All things identity related.
    public class Identity
    {
        private eusCommonEntities entCommon = new eusCommonEntities();

        public Int64 GetUserID(string userName)
        {
            Int64 userID = 0;

            try
            {
                userID = (from i in entCommon.AspNetUsers
                              where i.UserName == userName
                              select i.UserID).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
            return userID;            
        }
    }
}