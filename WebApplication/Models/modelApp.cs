using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class modelApp
    {
        public int app_id { get; set; }
        public string appName { get; set; }
        public double appPrice { get; set; }
        public int appAgeLimit { get; set; }
        public byte[] appImage { get; set; }

        public modelApp(apps app)
        {
            app_id = app.app_id;
            appName = app.appName;
            appPrice = (double)app.appPrice;
            appAgeLimit = (int)app.appAgeLimit;
            appImage = app.appImage;
        }
    }
}