using Credit_Card_Fraud_Detection.Data;
using Credit_Card_Fraud_Detection.Dtos;
using Credit_Card_Fraud_Detection.Helpers;
using Credit_Card_Fraud_Detection.MLModels;
using Credit_Card_Fraud_Detection.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Credit_Card_Fraud_Detection.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FraudController : ControllerBase
    {
        private AppDbContext _context;
        private FraudPredictionService _predictionService;

        public FraudController(AppDbContext context, FraudPredictionService predictionService)
        {
            _context = context;
            _predictionService = predictionService;
        }

        [HttpPost("predict")]
        public async Task<IActionResult> Predict(TransactionInputDto dto)
        {
            var user = await _context.Users.FindAsync(dto.UserId);
            if (user == null)
                return NotFound("User not found");

            var input = new OnnxTransactionInput
            {
                category = dto.Category,
                amt = (double)dto.Amount,
                gender = dto.Gender,
                state = dto.State ?? "Unknown",
                job = dto.Job ?? "Unknown",

                city_pop = dto.CityPop ?? 0,           
                hour = dto.TimeStamp.Hour,             
                day_of_week = (int)dto.TimeStamp.DayOfWeek,
                age = AgeHelper.CalculateAge(user.joinDate) 
            };




            int label = _predictionService.PredictLabel(input);

            return Ok(new
            {
                modelExecuted = true,
                fraudLabel = label
            });

        }


    }
}
