using PremiumCalculatorApi.Data.Common;
using PremiumCalculatorApi.Data.Model;
using System;
using System.Collections.Generic;

namespace PremiumCalculatorApi.Data.Repository
{
    public class MockPremiumRuleRepository : IPremiumRuleRepository
    {
        private readonly List<PremiumRule> premiumRuleList;
        public MockPremiumRuleRepository()
        {
            premiumRuleList = new List<PremiumRule>()
            {
                new PremiumRule() { PremiumRuleId = 1, State = "NY", MonthOfBirth = "August", Age = "18-45", Premium = 150.00M },
                new PremiumRule() { PremiumRuleId = 2, State = "NY", MonthOfBirth = "January", Age = "46-65", Premium = 200.50M },
                new PremiumRule() { PremiumRuleId = 3, State = "NY", MonthOfBirth = "*", Age = "18-65", Premium = 120.99M },
                new PremiumRule() { PremiumRuleId = 4, State = "AL", MonthOfBirth = "November", Age = "18-65", Premium = 85.50M },
                new PremiumRule() { PremiumRuleId = 5, State = "AL", MonthOfBirth = "*", Age = "18-65", Premium = 100.00M },
                new PremiumRule() { PremiumRuleId = 6, State = "AK", MonthOfBirth = "December", Age = "65+", Premium = 175.20M },
                new PremiumRule() { PremiumRuleId = 7, State = "AK", MonthOfBirth = "December", Age = "18-64", Premium = 125.16M },
                new PremiumRule() { PremiumRuleId = 8, State = "AK", MonthOfBirth = "*", Age = "18-65", Premium = 100.80M },
                new PremiumRule() { PremiumRuleId = 9, State = "*",  MonthOfBirth = "*", Age = "18-65", Premium = 90.00M },
            };
        }        

        public decimal? GetPremiumRulesValue(DateTime DateOfBirth, string State, int Age)
        {
            PremiumRuleUtil.ValidatePremiumRuleParameteres(DateOfBirth, State, Age);
            return PremiumRuleUtil.SearchPremiumRule(premiumRuleList, DateOfBirth, State, Age)?.Premium;
        }

    }
}
