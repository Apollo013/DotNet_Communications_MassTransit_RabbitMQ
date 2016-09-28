using System;
using System.Configuration;

namespace MassTransit.Company.Configuration
{
    public class ConnectionProperties
    {
        public static string HostAddress { get { return ConfigurationManager.AppSettings["host"]; } }
        public static Uri HostUri { get { return new Uri(HostAddress); } }
        public static string EndPoint { get { return ConfigurationManager.AppSettings["endpoint"]; } }
        public static string UserName { get { return ConfigurationManager.AppSettings["username"]; } }
        public static string Password { get { return ConfigurationManager.AppSettings["password"]; } }
        public static string FaultEndPoint { get { return ConfigurationManager.AppSettings["faultEndpoint"]; } }

    }
}
