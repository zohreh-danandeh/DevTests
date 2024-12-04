using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SchedulingSys.Models;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SchedulingSys.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ScheduleController : ControllerBase
    {

        [HttpPost]
        [ActionName("Creates a schedule based on the data stored in the employee and first shift tables")]
        public ActionResult Post(int shiftnumber)
        {
            int row = -1;

        
            string Empquery = "select * from Employee";
            DataTable Empdt = Database.GetDataNoparam(Empquery);

            //convert Employee datatable to List
            List<Employees> empList = Empdt.AsEnumerable().Select(row =>
            new Employees
            {
                Id = row.Field<int>("Id"),
                Name = row.Field<string>("Name"),
                Availability = new Availability
                {

                    Monday = row.Field<bool>("Monday"),
                    Tuesday = row.Field<bool>("Tuesday"),
                    Wednesday = row.Field<bool>("Wednesday"),
                    Thursday = row.Field<bool>("Thursday"),
                }

            }).ToList();



            string Shiftquery = "select * from Shifts";
            DataTable Shiftdt = Database.GetDataNoparam(Shiftquery);

            //convert Shifts datatable to List
            Shifts shift = Shiftdt.AsEnumerable().Select(row =>
            new Shifts
            {

                Monday = row.Field<int>("Monday"),
                Tuesday = row.Field<int>("Tuesday"),
                Wednesday = row.Field<int>("Wednesday"),
                Thursday = row.Field<int>("Thursday"),
                ShiftNum =  row.Field<int>("ShiftNum"),
            }).Where (e => e.ShiftNum == shiftnumber).First();


            var availability = new Dictionary<string, List<int>>();

            availability.Add("monday", empList.Where(e => e.Availability.Monday == true).Select(e => e.Id).ToList());
            availability.Add("tuesday", empList.Where(e => e.Availability.Tuesday == true).Select(e => e.Id).ToList());
            availability.Add("wednesday", empList.Where(e => e.Availability.Wednesday == true).Select(e => e.Id).ToList());
            availability.Add("thursday", empList.Where(e => e.Availability.Thursday == true).Select(e => e.Id).ToList());


            List<Schedule> schedulelist = new List<Schedule>();

            schedulelist.AddRange(GetSchedules("Monday", shift.Monday, availability["monday"], shiftnumber));
            schedulelist.AddRange(GetSchedules("Tuesday", shift.Tuesday, availability["tuesday"], shiftnumber));
            schedulelist.AddRange(GetSchedules("Wednesday", shift.Wednesday, availability["wednesday"], shiftnumber));
            schedulelist.AddRange(GetSchedules("Thursday", shift.Thursday, availability["thursday"], shiftnumber));

            //Schedule calls be able to be called multiple times without negative affects
            var dataparam = new IDataParameter[] { new SqlParameter("@ShiftNum", shiftnumber) };
            Database.StoreData("IF EXISTS (SELECT 1 FROM SCHEDULE WHERE SHIFTNUM = @SHIFTNUM) DELETE SCHEDULE WHERE SHIFTNUM = @SHIFTNUM ", dataparam);

            string query = "INSERT INTO SCHEDULE (DAY,EMPID,SHIFTNUM) VALUES (@DAY,@EMPID,@SHIFTNUM) ";
          

            foreach (var item in schedulelist)
            {
                var parameters = new IDataParameter[]
                {
                    new SqlParameter("@Day", item.Day),
                    new SqlParameter("@Empid", item.EmpId),
                    new SqlParameter("@ShiftNum", item.ShiftNum),
                };

               row = Database.StoreData(query, parameters);
            }

            return Ok(new { Result = "Stored in Schedule Table" });
        }

        //Assign shifts to each employee based on availability
        private List<Schedule> GetSchedules(string day, int shifts, List<int> eligibleEmployees, int shiftnum)
        {

            var schedulelist = new List<Schedule>();
            if (shifts > 0 && eligibleEmployees.Count > 0)
            {
                int empIndex = 0;
                for (int i = 0; i < shifts; i++)
                {
                    schedulelist.Add(new Schedule
                    {
                        Day = day,
                        EmpId = eligibleEmployees[empIndex % eligibleEmployees.Count],
                        ShiftNum = shiftnum
                    });

                    empIndex++;
                }
            }

            return schedulelist;
        }


        // An endpoint to return back the schedule
        [HttpGet]
        [ActionName("Returns back the schedule for shifts")]
        public  ActionResult Get(int shiftnumber)
        {
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@ShiftNums";
            param.Value = shiftnumber;

            string query = "Select Employee.Name, Schedule.Day, Schedule.ShiftNum from Employee inner join Schedule on Employee.id =Schedule.EmpId where Schedule.ShiftNum = @ShiftNums";
            DataTable dt = Database.GetData(query , param);

            return Ok(JsonConvert.SerializeObject(dt));
        }
    }
}