﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesApi.Models.Subscription;
using SalesApi.Repositories.Subscription;
using SalesApi.Services.Subscription;

namespace SalesApi.Web.Controllers.Bases
{
    public abstract class SubscriptionController<T>: SalesController<T>
    {
        protected readonly ISubscriptionService<T> SubscriptionService;
        protected readonly ISubscriptionDayRepository SubscriptionDayRepository;

        protected SubscriptionController(ISubscriptionService<T> subscriptionService) : base(subscriptionService)
        {
            SubscriptionService = subscriptionService;
            SubscriptionDayRepository = SubscriptionService.SubscriptionDayRepository;
        }

        [NonAction]
        protected async Task<bool> HasSubscriptionDayBeenInitialized(DateTime? date = null)
        {
            var dateStr = !date.HasValue ? Now.AddDays(1).Date.ToString("yyyy-MM-dd") : date.Value.ToString("yyyy-MM-dd");
            var item = await SubscriptionDayRepository.GetSingleAsync(x => x.Date == dateStr);
            return item != null && item.Initialized;
        }

        [NonAction]
        protected async Task<SubscriptionDay> GetLatestInitializedSubscriptionDay()
        {
            var item = await SubscriptionDayRepository.All.OrderByDescending(x => x.Date).FirstOrDefaultAsync();
            return item;
        }
    }
}
