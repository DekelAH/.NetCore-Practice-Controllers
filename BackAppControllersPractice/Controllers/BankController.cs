using BackAppControllersPractice.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackAppControllersPractice.Controllers
{
    public class BankController : Controller
    {
        #region Fields

        private readonly Account _account = new () { AccountNum = 1001, AccounHolderName = "John Doe", CurrentBalance = 500000 };

        #endregion

        [Route("overdraft-bank")]
        public IActionResult Bank()
        {
            return Content("<h1>Welcome to Overdraft Bank!</h1>\n<h2>The Bank who will make you stay on overdraft!</h2>", "text/html");
        }

        [Route("account-details")]
        public IActionResult AccountDetails()
        {
            return Json(_account);
        }

        [Route("account-statement")]
        public IActionResult AccountStatement()
        {
            return File("dummy-file.pdf","application/pdf");
        }

        [Route("get-current-balance/{accountNumber:int?}")]
        public IActionResult Balance()
        {
            if (HttpContext.Request.RouteValues.ContainsKey("accountNumber") || !string.IsNullOrEmpty(Convert.ToString(Request.Query["accountNumber"])))
            {
                int accountNum = Convert.ToInt32(HttpContext.Request.RouteValues["accountNumber"]);
                if (accountNum != 1001)
                {
                    return BadRequest("Account Number should be 1001");
                }
                else
                {
                    return Ok(_account.CurrentBalance);
                }
            }
            else
            {
                return NotFound("Account Number should be supplied");
            }
        }
    }


}

