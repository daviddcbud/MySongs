using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using SongTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SongTracker.Services
{
    public class JsonFormattedErrorService : IJsonFormattedErrorService
    {

        #region IJsonFormattedErrorService Members

        string TransformMessage(string message, Exception exception)
        {

            if (message.Contains("REFERENCE constraint") && message.Contains("DELETE"))
            {
                return "Unable to delete record because it is being referenced from other tables";
            }
            else
            {
                return message;
            }
        }
        public JsonResult CreateResult(HttpContext context, int statusCode, Controller controller, Exception ex)
        {
            context.Response.StatusCode = statusCode;
            var message = ex.Message;
            var exception = ex.InnerException;
            while (exception != null)
            {
                var showMessage = true;
                //there are some messages we don't need to show b/c the real error is buried down in the inner exceptions
                if (exception.Message.ToLower() == "one or more errors occurred.")
                {
                    showMessage = false;
                }
                if (exception.Message.ToLower().Contains("see the inner exception"))
                {
                    showMessage = false;
                }
                if (showMessage)
                {
                    message += " " + exception.Message;
                }
                exception = exception.InnerException;
            }

            message = TransformMessage(message, ex);
            return controller.Json(new ErrorViewModel { ExceptionMessage = message });
        }

        #endregion

    }
}
