using PremiumCalculatorApi.Data.Common;
using PremiumCalculatorApi.Data.Model;
using System;

namespace PremiumCalculatorApi.Data.Repository
{
    public class DbPremiumRuleRepository : IPremiumRuleRepository
    {
        private readonly AppDbContext context;

        public DbPremiumRuleRepository(AppDbContext context)
        {
            this.context = context;
        }

        public decimal? GetPremiumRulesValue(DateTime DateOfBirth, string State, int Age)
        {
            PremiumRuleUtil.ValidatePremiumRuleParameteres(DateOfBirth, State, Age);
            return PremiumRuleUtil.SearchPremiumRule(context.PremiumRules, DateOfBirth, State, Age)?.Premium;
        }

        
    }
}
