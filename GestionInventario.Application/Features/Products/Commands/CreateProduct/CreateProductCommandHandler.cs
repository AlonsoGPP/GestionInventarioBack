using GestionInventario.Application.Contracts.Persistence;
using GestionInventario.Application.Util;
using GestionInventario.Domain.DTOs.Base;
using GestionInventario.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionInventario.Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ResponseBase>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateProductCommandHandler> _logger;

        public CreateProductCommandHandler(IUnitOfWork unitOfWork, ILogger<CreateProductCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ResponseBase> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var repo = _unitOfWork.Repository<Product>();

                var existing = await repo.GetFirstAsync(
                    p => p.Name.ToLower() == request.Name.ToLower() && p.CategoryId == request.CategoryId,
                    orderBy: null,
                    includes: null,
                    disableTracking: true,
                    cancellationToken: cancellationToken
                );

                if (existing != null)
                {
                    return ResponseUtil.Conflict(existing,
                        $"Ya existe un producto con el nombre '{request.Name}' en esta categoría");
                }

                var product = Product.Create(
                    request.Name,
                    request.Description,
                    request.Price,
                    request.Quantity,
                    request.CategoryId
                );

                repo.AddEntity(product);
                await _unitOfWork.Complete();

                _logger.LogInformation("Producto creado: {Name} (Id: {Id})", request.Name, product.Id);

                return ResponseUtil.Created(new { product.Id }, "Producto creado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creando producto {Name}", request?.Name);
                return ResponseUtil.InternalError($"Error al crear producto: {ex.Message}");
            }
        }
    }

}
