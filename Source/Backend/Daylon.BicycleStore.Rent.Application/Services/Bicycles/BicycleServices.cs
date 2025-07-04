﻿using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Communication.Request;
using Daylon.BicycleStore.Rent.Domain.Entity;
using Microsoft.Extensions.Logging.Abstractions;

namespace Daylon.BicycleStore.Rent.Application.Services.Bicycles
{
    public class BicycleServices : IBicycleService
    {
        private readonly Domain.Repositories.IBicycleRepository _bicycleRepository;
        private readonly IBicycleUseCase _useCase;

        public BicycleServices(
            Domain.Repositories.IBicycleRepository bicycleRepository,
            IBicycleUseCase useCase)
        {
            _bicycleRepository = bicycleRepository;
            _useCase = useCase;
        }

        // GET
        public async Task<IList<Bicycle>> GetBicyclesAsync() => await _bicycleRepository.GetBicyclesAsync();

        public async Task<Bicycle> GetBicycleByIdAsync(Guid id)
        {
            var bicycle = await _bicycleRepository.GetBicycleByIdAsync(id);

            return bicycle;
        }

        // POST
        public async Task<Bicycle> RegisterBicycleAsync(RequestRegisterBicycleJson request)
        {
            var bicycle = await _useCase.ExecuteRegisterBicycleAsync(request);

            return bicycle;
        }
    }
}
