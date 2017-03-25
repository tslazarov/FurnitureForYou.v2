using FFY.Models;
using System;

namespace FFY.Data.Factories
{
    public interface IOrderFactory
    {
        Order CreateOrder(
            string userId,
            User user,
            DateTime sendOn,
            decimal total,
            int addressId,
            Address address,
            string phoneNumber,
            OrderPaymentStatusType orderPaymentStatusType,
            OrderStatusType orderStatusType);
    }
}
