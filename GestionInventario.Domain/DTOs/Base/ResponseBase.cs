using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Domain.DTOs.Base
{
    public class ResponseBase
    {
        public int Code { get; set; }

        public string? Message { get; set; }

        public object? Payload { get; set; }

        public ResponseBase()
        {
        }

        public ResponseBase(int code, string? message = null, object? payload = null)
        {
            Code = code;
            Message = message;
            Payload = payload;
        }
    }
}
