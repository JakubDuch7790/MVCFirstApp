﻿using MVCFirstApp.DataAcces.Data;
using MVCFirstApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCFirstApp.DataAcces.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader obj);

        void UpdateStatus(int orderHeaderId, string orderStatus, string? paymentStatus = null);

        void UpdateStripePaymentId(int orderHeaderId, string sessionId, string paymentIntentId);
    }
}
    