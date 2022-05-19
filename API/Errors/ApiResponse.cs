using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDafaultMessageForStatusCode(statusCode);
        }

        public int StatusCode { get; set; } 

        public string Message { get; set; }      
        

        private string GetDafaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "A bad request, you have made",
                401 => "Autorized, you are not",
                404 => "Resource Found, It was not",
                500 => "Internal Error, Hahaha",
                _ => null
            };
        }   
    }
}