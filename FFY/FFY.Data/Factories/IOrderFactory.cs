using FFY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
