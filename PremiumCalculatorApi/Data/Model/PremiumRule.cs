namespace PremiumCalculatorApi.Data.Model
{

    public class PremiumRule
    {
        public int PremiumRuleId { get; set; }
        public string State { get; set; } //Can be a 2 character US state code or a wildcard (*)
        public string MonthOfBirth { get; set; } //Can be the name of a month of a wildcard (*)
        public string Age { get; set; } //Can be a range (min-max), a minimum age (min+) or a wildcard (*)
        public decimal Premium { get; set; }
    }
}
