using GestionInventario.Domain.Constans.Base;
using GestionInventario.Domain.DTOs.Base;
using System.Net;


namespace GestionInventario.Application.Util
{
    public static class ResponseUtil
    {
        public static ResponseBase OK(object payload, string? message = null)
        {
            return new ResponseBase((int)HttpStatusCodeEnum.OK, message, payload);
        }

        public static ResponseBase Created(object payload, string? message = null)
        {
            return new ResponseBase((int)HttpStatusCodeEnum.Created, message, payload);
        }

        public static ResponseBase NoContent(string? message = null)
        {
            return new ResponseBase((int)HttpStatusCodeEnum.NoContent, message);
        }

        public static ResponseBase BadRequest(string message)
        {
            return new ResponseBase((int)HttpStatusCodeEnum.BadRequest, message);
        }

        public static ResponseBase BadRequest(string[] errors, string? message = null)
        {
            return new ResponseBase((int)HttpStatusCodeEnum.BadRequest, message, errors);
        }

        public static ResponseBase NotFoundRequest(string message)
        {
            return new ResponseBase((int)HttpStatusCodeEnum.NotFound, message);
        }

        public static ResponseBase InternalError(string? message = null)
        {
            return new ResponseBase((int)HttpStatusCodeEnum.InternalServerError, message);
        }

        public static ResponseBase Conflict(object payload, string? message = null)
        {
            return new ResponseBase((int)HttpStatusCodeEnum.Conflict, message, payload);
        }

        public static ResponseBase CustomResponse(HttpStatusCode statusCode, object payload, string? message)
        {
            return new ResponseBase
            {
                Code = (int)statusCode,
                Message = message,
                Payload = payload
            };
        }
    }
}
