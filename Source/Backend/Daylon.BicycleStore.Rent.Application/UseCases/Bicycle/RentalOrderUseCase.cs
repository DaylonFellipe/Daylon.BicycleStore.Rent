﻿using Daylon.BicycleStore.Rent.Application.Interface;
using Daylon.BicycleStore.Rent.Communication.Request;
using Daylon.BicycleStore.Rent.Domain.Entity.Enum;
using Daylon.BicycleStore.Rent.Domain.Repositories;
using FluentValidation;

namespace Daylon.BicycleStore.Rent.Application.UseCases.Bicycle
{
    public class RentalOrderUseCase : IRentalOrderUseCase
    {
        private readonly IBicycleRepository _bicycleRepository;

        public RentalOrderUseCase(IBicycleRepository bicycleRepository)
        {
            _bicycleRepository = bicycleRepository;
        }

        // POST
        public async Task<Domain.Entity.RentalOrder> ExecuteRegisterRentalOrderAsync(RequestRegisterRentalOrderJson request, CancellationToken cancellationToken = default)
        {
            // Validate
            ValidateRequest(request, new RegisterRentalOrderValidator());

            // Map Properties
            var bicycle = await _bicycleRepository.GetBicycleByIdAsync(request.BicycleId);

            var rentalStart = DateTime.Now;
            var rentalEnd = rentalStart.AddDays(request.RentalDays);

            var totalPrice = bicycle.DailyRate * request.RentalDays;
            var orderStatus = OrderStatusEnum.Rented;

            // Create RentalOrder Entity
            var rentalOrder = new Domain.Entity.RentalOrder
            {
                OrderId = Guid.NewGuid(),

                RentalStart = rentalStart,
                RentalEnd = rentalEnd,
                RentalDays = request.RentalDays,
                DropOffTime = null,

                PaymentMethod = request.PaymentMethod,
                TotalPrice = totalPrice,
                OrderStatus = orderStatus,

                BicycleId = request.BicycleId,
                Bicycle = bicycle
            };

            //Save
            await _bicycleRepository.AddRentalOrderAsync(rentalOrder);

            return rentalOrder;
        }

        // PATCH
        public async Task<Domain.Entity.RentalOrder> ExecuteModifyDatesAsync(Guid id, DateTime? rentalStart, int? rentalDays, int? extraDays)
        {
            // Validate 
            var dates = new RequestModifyDatesValidatorJson
            {
                RentalStart = rentalStart,
                RentalDays = rentalDays,
                ExtraDays = extraDays
            };

            ValidateRequest(dates, new ModifyDatesValidator());

            // Get RentalOrder
            var rentalOrder = await _bicycleRepository.GetRentalOderByIdAsync(id);


            if (rentalOrder == null)
                throw new KeyNotFoundException($"Rental order with ID {id} not found.");

            // Update Dates
            if (rentalStart.HasValue && rentalStart > DateTime.Now)
                rentalOrder.RentalStart = rentalStart.Value;

            if (rentalDays.HasValue && rentalDays > 1)
                rentalOrder.RentalDays = rentalDays.Value;

            if (extraDays.HasValue && extraDays >= 0)
                rentalOrder.RentalDays += extraDays.Value;

            // Calculate Rental End Date
            rentalOrder.RentalEnd = rentalOrder.RentalStart.AddDays(rentalOrder.RentalDays);

            // Get Bicycle
            var bicycle = await _bicycleRepository.GetBicycleByIdAsync(rentalOrder.BicycleId);

            rentalOrder.TotalPrice = bicycle.DailyRate * rentalOrder.RentalDays; // Recalculate total price
            rentalOrder.OrderStatus = OrderStatusEnum.Rented; // Status remains 'Rented' after modification

            // Save
            await _bicycleRepository.UpdateRentalOrderAsync(rentalOrder);

            return rentalOrder;
        }

        private static void ValidateRequest<T>(T request, AbstractValidator<T> validator)
        {
            var result = validator.ValidateAsync(request);

            if (!result.Result.IsValid)
            {
                var erros = result.Result.Errors.Select(e => e.ErrorMessage).ToList();
                throw new ValidationException(string.Join(", ", erros));
            }
        }
    }
}
