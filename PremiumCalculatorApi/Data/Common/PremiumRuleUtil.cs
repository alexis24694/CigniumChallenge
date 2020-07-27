using PremiumCalculatorApi.Data.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PremiumCalculatorApi.Data.Common
{
    public static class PremiumRuleUtil
    {
        /// <summary>
        /// This function search for the first occurrence of the rule in the colection that matches all the restrictions given by the parameters
        /// </summary>
        public static PremiumRule SearchPremiumRule(IEnumerable<PremiumRule> PremiumRules, DateTime DateOfBirth, string State, int Age)
        {
            return PremiumRules
                .Where(p =>
                        (
                            p.State.Equals("*") || 
                            p.State.Equals(State)
                        ) &&
                        (
                            p.MonthOfBirth.Equals("*") ||                            
                            DateTime.ParseExact(p.MonthOfBirth, "MMMM", CultureInfo.InvariantCulture).Month.Equals(DateOfBirth.Month)//Transforming month text to the corresponding number: August => 8
                        ) &&
                        (
                            p.Age.Equals("*") || 
                            (p.Age.Contains("+") && Age >= int.Parse(GetNumbers(p.Age))) || //Comparing in minimum age case: 65+                            
                            (p.Age.Contains("-") && Age > int.Parse(p.Age.Split('-')[0]) && Age < int.Parse(p.Age.Split('-')[1])))//Comparing in age range case: 18-65
                        )
                .FirstOrDefault();
        }

        /// <summary>
        /// Removes all non numeric characters from a string
        /// Used to get the numeric part of the (Number)+ Age
        /// </summary>       
        public static string GetNumbers(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }

        public static void ValidatePremiumRuleParameteres(DateTime DateOfBirth, string State, int Age)
        {
            int calculatedAge = DateTime.Now.Year - DateOfBirth.Year;
            if (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear)
                calculatedAge--;
            if (calculatedAge != Age)
                throw new Exception("Provided age does not match with provided birth date");

            if (State.Length != 2)
                throw new Exception("Invalid State code format");
        }
    }
}
