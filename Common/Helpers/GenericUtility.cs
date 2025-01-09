using Common.Models;
using System.Net;

namespace Common.Helpers
{
    public class GenericUtility
    {
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
                    retval.Message = "Se ha generado un error procesando la solicitud";
                }
                else
                    retval.Message = ex.Message;
            }
            return retval;
        }
    }
}
