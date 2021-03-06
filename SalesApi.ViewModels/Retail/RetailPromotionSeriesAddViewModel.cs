﻿using System.Collections.Generic;
using FluentValidation;

namespace SalesApi.ViewModels.Retail
{
    public class RetailPromotionSeriesAddViewModel : RetailPromotionSeriesViewModel
    {
        public List<RetailPromotionSeriesBonusViewModel> RetailPromotionSeriesBonuses { get; set; }
    }

    public class RetailPromotionSeriesAddViewModelValidator : AbstractValidator<RetailPromotionSeriesAddViewModel>
    {
        public RetailPromotionSeriesAddViewModelValidator()
        {
            RuleFor(x => x.StartDate).LessThanOrEqualTo(x => x.EndDate).WithMessage("开始日期不能大于结束日期");
        }
    }
}
