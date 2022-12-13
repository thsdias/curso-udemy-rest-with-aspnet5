using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;

namespace RestWithAspNetUdemy.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CalculatorController : ControllerBase
    {
        private readonly ILogger<CalculatorController> _logger;

        public CalculatorController(ILogger<CalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet("sum/{firstNumber}/{secondNumber}")]
        public IActionResult Addition(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && IsNumeric(secondNumber))
            {
                var sum = ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber);
                return Ok(sum.ToString());
            }

            return BadRequest("Invalid Input");
        }

        [HttpGet("sub/{firstNumber}/{secondNumber}")]
        public IActionResult Subtraction(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && (IsNumeric(secondNumber)))
            {
                var sub = ConvertToDecimal(firstNumber) - ConvertToDecimal(secondNumber);
                return Ok(sub.ToString());
            }

            return BadRequest("Invalid Input");
        }

        [HttpGet("div/{firstNumber}/{secondNumber}")]
        public IActionResult Division(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && (IsNumeric(secondNumber)))
            {
                var sub = ConvertToDecimal(firstNumber) / ConvertToDecimal(secondNumber);
                return Ok(sub.ToString());
            }

            return BadRequest("Invalid Input");
        }

        [HttpGet("mult/{firstNumber}/{secondNumber}")]
        public IActionResult Multiplication(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && (IsNumeric(secondNumber)))
            {
                var sub = ConvertToDecimal(firstNumber) * ConvertToDecimal(secondNumber);
                return Ok(sub.ToString());
            }

            return BadRequest("Invalid Input");
        }

        [HttpGet("mean/{firstNumber}/{secondNumber}")]
        public IActionResult Mean(string firstNumber, string secondNumber)
        {
            if (IsNumeric(firstNumber) && (IsNumeric(secondNumber)))
            {
                var med = (ConvertToDecimal(firstNumber) + ConvertToDecimal(secondNumber)) / 2;
                return Ok(med.ToString());
            }

            return BadRequest("Invalid Input");
        }

        [HttpGet("raiz/{number}")]
        public IActionResult Raiz(string number)
        {
            if (IsNumeric(number))
            {
                var raizQuadrada = Math.Sqrt((double)ConvertToDecimal(number));
                return Ok(raizQuadrada.ToString());
            }

            return BadRequest("Invalid Input");
        }

        private static bool IsNumeric(string strNumber)
        {
            return double.TryParse(strNumber, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out _);
        }

        private static decimal ConvertToDecimal(string number)
        {
            if (decimal.TryParse(number, out decimal decimalValue))
                return decimalValue;

            return 0;
        }
    }
}
