using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAppFunction.Api.Classes
{
    public static class EnvironmentVariables
    {
        public const string InstrumentationKey = "APPINSIGHTS_INSTRUMENTATIONKEY";
        public const string DbConnectionString = "DBConnectionString";
        public const string DefaultEmailFromAddress = "DefaultEmailFromAddress";
        public const string DefaultEmailFromName = "DefaultEmailFromName";
        public const string SendGridApiKey = "SendGrid.ApiKey";
        public const string RegexEmail = "Regex.Email";
        public const string ClientId = "ClientId";
        public const string TenantId = "TenantId";
        public const string ClientSecret = "ClientSecret";

        public static string Get(this string environmentVariableName) =>
        Environment.GetEnvironmentVariable(environmentVariableName);

        public static List<string> GetList(this string environmentVariableName, string delimiter = ",")
            => Environment.GetEnvironmentVariable(environmentVariableName)?.Split(delimiter).ToList();

        public static short GetShort(this string environmentVariableName) => GetShortVariable(environmentVariableName);
        public static int GetInt(this string environmentVariableName) => GetIntVariable(environmentVariableName);
        public static long GetLong(this string environmentVariableName) => GetLongVariable(environmentVariableName);

        private static short GetShortVariable(string name)
        {
            var variable = Environment.GetEnvironmentVariable(name);
            return short.TryParse(variable, out var _v) ? _v : (short)0;
        }

        private static int GetIntVariable(string name)
        {
            var variable = Environment.GetEnvironmentVariable(name);
            return int.TryParse(variable, out var _v) ? _v : 0;
        }

        private static long GetLongVariable(string name)
        {
            var variable = Environment.GetEnvironmentVariable(name);
            return long.TryParse(variable, out var _v) ? _v : 0;
        }
    }
}
