using GestionInventario.Domain.DTOs.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Application.Features.Products.Queries.GetById
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ResponseBase>;
}
