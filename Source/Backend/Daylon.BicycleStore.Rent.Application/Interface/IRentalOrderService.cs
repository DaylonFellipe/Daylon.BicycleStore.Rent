﻿using Daylon.BicycleStore.Rent.Communication.Request;
using Daylon.BicycleStore.Rent.Domain.Entity;

namespace Daylon.BicycleStore.Rent.Application.Interface
{
    public interface IRentalOrderService
    {
        // GET
        public Task<IList<RentalOrder>> GetRentalOrdersAsync();
        public Task<RentalOrder> GetRentalOrderByIdAsync(Guid id);

        // POST
        public Task<RentalOrder> RegisterRentalOrderAsync(RequestRegisterRentalOrderJson request);
    }
}
