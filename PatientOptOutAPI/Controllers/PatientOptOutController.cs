using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PatientOptOutAPI.Data;
using PatientOptOutAPI.Models;
using PatientOptOutAPI.Models.ViewModel;
using PatientOptOutAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.Extensions.Options;

namespace PatientOptOutAPI.Controllers
{
    [EnableCors("CorsPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientOptOutController : ControllerBase
    {
        private readonly DataWarehouseContext _context;
        public readonly IOptions<ApplicationSettings> _config;

        //Injects controller with the controller context
        public PatientOptOutController(DataWarehouseContext context, IOptions<ApplicationSettings> config)
        {
            _context = context;
            _config = config;
        }
        
        [HttpGet]
        [Route("/api/startup")]
        public dynamic StartUp()
        {
            return new { Username = User.GetUsernameWithoutDomain(), Access = User.CheckAccess(_config.Value.ActiveDirectoryGroupName) };
        }
        
        [HttpPost]
        [Authorize(Policy = "OptOutCheckerAccess")]
        //Maps the inputs to the NumbersViewModel
        public async Task<IActionResult>PostNumbers([FromBody] List<string> numbers)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //Fetches database columns and checks to see if they contain the input(s). If so, it is add to a list
            var listOfNHSMatches = _context.vw_PatientOptOut.Where(row => numbers.Contains(row.NHSNumber)).Select(itemInList => itemInList.NHSNumber).ToList();
            var listOfHospitalMatches = _context.vw_PatientOptOut.Where(row => numbers.Contains(row.HospitalNumber)).Select(itemInList => itemInList.HospitalNumber).ToList();

            //Combines the lists to make a list of total matches
            var listOfMatches = listOfNHSMatches.Union(listOfHospitalMatches);

            //Returns 'True' if the input is contained within the database
            var result = numbers.Select(n => new NumbersViewModel { Number = n, OptOut = listOfMatches.Contains(n) });
            return Ok(result);
        }
    }
}
