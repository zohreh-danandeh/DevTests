using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchedulingSys.Models;

namespace SchedulingSys.Controllers
{
    [Route("api/[controller]/[action]")]
    public class EmployeeShiftController : Controller
    {
        [HttpPost]
        [ActionName("Shifts")]
        public ActionResult PostFirstShift(int shiftnumber)
        {
            int row = -1;
            string file = String.Empty;

            if (shiftnumber == 1 )  file = "JsonFiles/shifts1.json";
           else if (shiftnumber == 2 )  file = "JsonFiles/shifts2.json";

            using StreamReader reader = new(file);
            var json = reader.ReadToEnd();
            List<Rowshifts> rshifts = JsonConvert.DeserializeObject<List<Rowshifts>>(json) ??
                                       throw new InvalidOperationException();

            var dictionary = rshifts.GroupBy(obj => obj.ScheduledWorkDay).ToDictionary(group => group.Key, group => group.Count());

            string query = "IF NOT EXISTS ( SELECT ID FROM SHIFTS WHERE MONDAY = @MONDAY AND TUESDAY = @TUESDAY AND WEDNESDAY = @WEDNESDAY AND THURSDAY = @THURSDAY AND SHIFTNUM = @SHIFTNUM) ";
            query += "BEGIN ";
            query += "INSERT INTO SHIFTS (MONDAY,TUESDAY,WEDNESDAY,THURSDAY,SHIFTNUM) VALUES (@MONDAY,@TUESDAY,@WEDNESDAY,@THURSDAY,@SHIFTNUM) ";
            query += "END";


            var mondaynum = 0;
            var tuesdaynum = 0;
            var wednesdaynum = 0;
            var thursdaynum = 0;


            foreach (var item in dictionary)
            {
                switch (item.Key)
                {
                    case "Monday":
                        mondaynum = item.Value;
                        break;
                    case "Tuesday":
                        tuesdaynum = item.Value;
                        break;
                    case "Wednesday":
                        wednesdaynum = item.Value;
                        break;
                    case "Thursday":
                        thursdaynum = item.Value;
                        break;
                }
            }

            var parameters = new IDataParameter[]

               {
                    new SqlParameter("@Monday", mondaynum),
                    new SqlParameter("@Tuesday", tuesdaynum),
                    new SqlParameter("@Wednesday", wednesdaynum),
                    new SqlParameter("@Thursday", thursdaynum),
                    new SqlParameter("@ShiftNum", shiftnumber)
               };

           row = Database.StoreData(query, parameters);

            if (row > -1) return Ok(new { Result = "Shifts stored in shifts Table" });
            else return Ok(new { Result = "No need to save duplicates; the information in first shift has already been saved!" });
        }
    }
}
