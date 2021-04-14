using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace WebApiDemo.Controllers
{
    public class EmployeesController : ApiController
    {
       

        // GET method for all data in database
        // below code IE<employee class is derieved and created from ADO.NET EF
        public IEnumerable<employee> Get() //the employee contoller is generating  the list of the employees that we want to return to client is called WebApi
        {
            // creating an instance 
            using(employeeEntities entities = new employeeEntities())
            {
                // this is a collection property which is going to return list of all employee by using ToList
                return entities.employees.ToList();
                // the  public IEnumerable<employee> Get() will return all data avaliable in employee table
            }
        }
        //GET method for specific data from datbase by passing id to GET()
        // this below code is to return only specific employee details by ID and if id not found in data it should eturn not found message
        // the return type is employee because we want only 1 employee details at time
        
        //Get Method
        public HttpResponseMessage Get(int id)
        {
            // creating an instance 
            using (employeeEntities entities = new employeeEntities())
            {
                // using lambda expression i.e employee.id is equal to the id we are passing to that method
                var entity = entities.employees.FirstOrDefault(e => e.ID == id);
                if (entity != null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, entity); // if found this will print if not else will print
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound,  "employee with id = " + id.ToString() +" " + "Not Found");
                }

            }
        }
        // Post Method
        public HttpResponseMessage Post([FromBody] employee employee)
        {
            try
            {
                using (employeeEntities entities = new employeeEntities()) 
                {
                    entities.employees.Add(employee);
                    entities.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created, employee);
                    message.Headers.Location = new Uri(Request.RequestUri + employee.ID.ToString());
                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        // Delete Method
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                using(employeeEntities entities = new employeeEntities())
                {
                    var entity = entities.employees.FirstOrDefault(e => e.ID == id);
                    if ( entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Empoyee with id " + id.ToString() + "not found");
                    }
                    else
                    {
                        entities.employees.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }

                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // Put Method
        public HttpResponseMessage Put(int id, [FromBody] employee employee)
        {
            try
            {
                using (employeeEntities entities = new employeeEntities())
                {
                    var entity = entities.employees.FirstOrDefault(e => e.ID == id);

                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Employee with Id :" + id.ToString() + "not found to update");
                    }
                    else
                    {

                        entity.FirstName = employee.FirstName;
                        entity.LastName = employee.LastName;
                        entity.Gender = employee.Gender;
                        entity.Salary = employee.Salary;

                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK, entity);
                    }
                }
                
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
