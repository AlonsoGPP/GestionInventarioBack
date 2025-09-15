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

namespace GestionInventario.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ResponseBase>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UpdateProductCommandHandler> _logger;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, ILogger<UpdateProductCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ResponseBase> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var repo = _unitOfWork.Repository<Product>();

                var product = await repo.GetByIdAsync(r=>r.Id == request.Id);
                if (product == null)
                {
                    return ResponseUtil.NotFoundRequest("Producto no encontrado");
                }

               
                var existing = await repo.GetFirstAsync(
                    p => p.Id != request.Id &&
                         p.Name.ToLower() == request.Name.ToLower() &&
                         p.CategoryId == request.CategoryId,
                    orderBy: null,
                    includes: null,
                    disableTracking: true,
                    cancellationToken: cancellationToken
                );

                if (existing != null)
                {
                    return ResponseUtil.Conflict(existing,
                        $"Ya existe otro producto con el nombre '{request.Name}' en esta categoría");
                }

                product.UpdateDetails(request.Name, request.Description, request.Price, request.CategoryId,request.Quantity);

                repo.UpdateEntity(product);
                await _unitOfWork.Complete();

                _logger.LogInformation("Producto actualizado: {Id}", request.Id);

                return ResponseUtil.OK(product, "Producto actualizado correctamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error actualizando producto {Id}", request.Id);
                return ResponseUtil.InternalError($"Error al actualizar producto: {ex.Message}");
            }
        }
    }
}
