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

namespace GestionInventario.Application.Features.Products.Queries.GetById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ResponseBase>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GetProductByIdQueryHandler> _logger;

        public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, ILogger<GetProductByIdQueryHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<ResponseBase> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var repo = _unitOfWork.Repository<Product>();

                var product = await repo.GetByIdAsync(r=>r.Id==request.Id);
                if (product == null)
                {
                    _logger.LogWarning("Producto no encontrado con Id: {Id}", request.Id);
                    return ResponseUtil.NotFoundRequest("Producto no encontrado");
                }

                _logger.LogInformation("Producto encontrado: {Id}", request.Id);
                return ResponseUtil.OK(product, "Producto encontrado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener producto {Id}", request.Id);
                return ResponseUtil.InternalError($"Error al obtener producto: {ex.Message}");
            }
        }
    }
}
