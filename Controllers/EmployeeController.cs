using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Data;
using SchedulingSys.Models;
using Microsoft.Data.SqlClient;

namespace SchedulingSys.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : Controller
    {      
        [HttpPost]
        public ActionResult Post ()
        {

            using StreamReader reader = new("JsonFiles/employees.json");
            var json = reader.ReadToEnd();
            List<Employees> emps = JsonConvert.DeserializeObject<List<Employees>>(json) ??
                                   throw new InvalidOperationException(); 

            string query = "IF NOT EXISTS ( SELECT ID FROM EMPLOYEE WHERE NAME = @NAME ) ";
            query += "BEGIN ";
            query += "INSERT INTO EMPLOYEE (ID,NAME,MONDAY,TUESDAY,WEDNESDAY,THURSDAY) VALUES (@ID,@NAME,@MONDAY,@TUESDAY,@WEDNESDAY,@THURSDAY) ";
            query += "END";


            int row = -1;

            if (emps != null)
            {
                foreach (var emp in emps)
                {
                    var parameters = new IDataParameter[]

                        {
                            new SqlParameter("@id", emp.Id),
                            new SqlParameter("@Name", emp.Name),
                            new SqlParameter("@Monday", emp.Availability.Monday),
                            new SqlParameter("@Tuesday", emp.Availability.Tuesday),
                            new SqlParameter("@Wednesday", emp.Availability.Wednesday),
                            new SqlParameter("@Thursday", emp.Availability.Thursday)
                        };

                  row =  Database.StoreData(query, parameters);
                }
            }

             if (row > -1)  return Ok( new { Result = "Stored in Employee Table" });
             else return Ok(new { Result = "No need to save duplicates; the information has already been saved!" });
        }

    }
}
