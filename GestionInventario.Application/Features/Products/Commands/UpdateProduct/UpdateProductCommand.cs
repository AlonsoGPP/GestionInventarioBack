using GestionInventario.Domain.DTOs.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Application.Features.Products.Commands.UpdateProduct
{
    public record UpdateProductCommand(
        Guid Id,
        string Name,
        string? Description,
        decimal Price,
        int Quantity,
        Guid CategoryId
    ) : IRequest<ResponseBase>;
}
