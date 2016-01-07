namespace ReportTestApp.Controllers
{
    using System.IO;
    using System.Web;
    using Telerik.Reporting.Cache.File;
    using Telerik.Reporting.Services;
    using Telerik.Reporting.Services.WebApi;
    using System.Configuration;
    using Telerik.Reporting;

    //The class name determines the service URL. 
    //ReportsController class name defines /api/report/ service URL.
    public class ReportsController : ReportsControllerBase
    {
        static ReportServiceConfiguration configurationInstance;

        static ReportsController()
        {
            //This is the folder that contains the XML (trdx) report definitions
            //In this case this is the app folder
          // ReportLibrary2.Report2.ChangeSqlString("Data Source=devServer;Initial Catalog=Z_Yamhill;User ID=sa;Password=data22");
            var reportsPath = HttpContext.Current.Server.MapPath("~/");

            //Add resolver for trdx report definitions, 
            //then add resolver for class report definitions as fallback resolver; 
            //finally create the resolver and use it in the ReportServiceConfiguration instance.
            var resolver = new ReportFileResolver(reportsPath)
                .AddFallbackResolver(new ReportTypeResolver());

            //Setup the ReportServiceConfiguration
            configurationInstance = new ReportServiceConfiguration
            {
                HostAppId = "MvcApp",
                Storage = new FileStorage(),
                ReportResolver = resolver,
                // ReportSharingTimeout = 0,
                // ClientSessionTimeout = 15,
            };
        }

        public ReportsController()
        {
            //Initialize the service configuration
            this.ReportServiceConfiguration = configurationInstance;
        }
    }
}