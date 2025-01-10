using Common.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace Common.Helpers
{
    public static class GenericUtility
    {
        private static readonly string contentTypeJson = "application/json";

        public static ResponseBase<T> ResponseBaseCatch<T>(bool validation, Exception ex, HttpStatusCode status)
        {
            ResponseBase<T> retval = new()
            {
                Success = false,
            };
            if (validation)
            {
                retval.Code = status;
                if (retval.Code == HttpStatusCode.InternalServerError)
                {
                    retval.Message = "An error occurred while processing the request.";
                }
                else
                    retval.Message = ex.Message;
            }
            return retval;
        }

        public static IActionResult ToResponse<T>(this ResponseBase<T> data)
        {
            try
            {
                HttpResponseMessage res = new()
                {
                    StatusCode = data.Code
                };
                var serializerSetting = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    Formatting = Formatting.None,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    FloatParseHandling = FloatParseHandling.Decimal,
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    }
                };
                string content = JsonConvert.SerializeObject(data, Formatting.Indented, serializerSetting);
                res.Content = new StringContent(content, System.Text.Encoding.UTF8, contentTypeJson);
                return new HttpResponseMessageResult(res);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

        }
    }
}
