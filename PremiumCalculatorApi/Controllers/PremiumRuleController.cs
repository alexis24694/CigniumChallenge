using System;
using Microsoft.AspNetCore.Mvc;
using PremiumCalculatorApi.Data.Repository;

namespace PremiumCalculatorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PremiumRuleController : ControllerBase
    {
        private readonly IPremiumRuleRepository premiumRuleRepository;
        public PremiumRuleController(IPremiumRuleRepository premiumRuleRepository)
        {
            this.premiumRuleRepository = premiumRuleRepository;
        }

        [HttpGet("premiumValue")]
        public IActionResult GetPremiumValue(DateTime dateOfBirth, string state, int age)
        {
            try
            {
                decimal? premiumValue = premiumRuleRepository.GetPremiumRulesValue(dateOfBirth, state, age);
                if (premiumValue.HasValue)
                    return new ObjectResult(new { premium = premiumValue });
                else
                    return NotFound("No rule was found");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }
    }
}