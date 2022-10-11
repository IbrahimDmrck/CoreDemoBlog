using BlogApiDemo.DataAccessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public IActionResult EmployeeList()
        {
            using (var context = new Context())
            {
                var values = context.Employees.ToList();
                return Ok(values);
            }
        }

        [HttpPost]
        public IActionResult EmployeeAdd(Employee employee)
        {
            using (var context = new Context())
            {
               context.Add(employee);
                context.SaveChanges();
                return Ok();
            }
        }


        [HttpGet("{id}")]
        public IActionResult EmployeeGet(int id)
        {
            using (var context = new Context())
            {
                var value = context.Employees.Find(id);

                if (value==null)
                {
                    return NotFound();
                }
                return Ok();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult EmployeeDelete(int id)
        {
            using (var context = new Context())
            {
                var value = context.Employees.Find(id);
                if (value!=null)
                {
                    context.Remove(value);
                    context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }


        [HttpPut]
        public IActionResult EmployeeUpdate(Employee employee)
        {
            using (var context=new Context())
            {
                var value = context.Find<Employee>(employee.ID);
                if (value!=null)
                {
                    value.Name = employee.Name;
                    context.Update(value);
                    context.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
        }
    }
}
