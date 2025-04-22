using System;
using System.Net;
using WhatsAppBotAPi.Services.Response;

namespace WhatsAppBotAPi.Services.Exceptions
{
    public class WhatsAppBotAPiException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public WhatsAppErrorResponse whatsAppErrorResponse { get; set; }

        public WhatsAppBotAPiException()
        {

        }

        public WhatsAppBotAPiException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        public WhatsAppBotAPiException(Exception ex, HttpStatusCode statusCode, WhatsAppErrorResponse whatsAppError) : base(ex.Message)
        {
            StatusCode = statusCode;
            whatsAppErrorResponse = whatsAppError;
        }
    }
}
