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

namespace GestionInventario.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ResponseBase>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteProductCommandHandler> _logger;

        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, ILogger<DeleteProductCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ResponseBase> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var repo = _unitOfWork.Repository<Product>();

                var product = await repo.GetByIdAsync(r=>r.Id==request.Id);
                if (product == null)
                {
                    return ResponseUtil.NotFoundRequest("Producto no encontrado");
                }

                repo.DeleteEntity(product);
                await _unitOfWork.Complete();

                _logger.LogInformation("Producto eliminado: {Id}", request.Id);

                return ResponseUtil.NoContent("Producto eliminado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error eliminando producto {Id}", request.Id);
                return ResponseUtil.InternalError($"Error al eliminar producto: {ex.Message}");
            }
        }
    }
}
