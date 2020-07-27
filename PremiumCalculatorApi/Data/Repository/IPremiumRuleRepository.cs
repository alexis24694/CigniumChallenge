using System;

namespace PremiumCalculatorApi.Data.Repository
{
    public interface IPremiumRuleRepository
    {
        decimal? GetPremiumRulesValue(DateTime DateOfBirth, string State, int Age);
    }
}
