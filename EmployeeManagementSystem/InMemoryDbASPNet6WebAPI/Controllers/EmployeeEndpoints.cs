using Microsoft.EntityFrameworkCore;
using InMemoryDbASPNet6WebAPI.Model;
namespace InMemoryDbASPNet6WebAPI.Controllers;

public static class EmployeeEndpoints
{
    public static void MapEmployeeEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Employee", async (EmployeeDBContext db) =>
        {
            return await db.Employee.ToListAsync();
        })
        .WithName("GetAllEmployees");

        routes.MapGet("/api/Employee/{search}", async (string search, EmployeeDBContext db) =>
        {
            return await db.Employee.Where(e => e.Name.ToLower().Contains(search.ToLower())
            || e.CompanyName.ToLower().Contains(search.ToLower())
            || e.Department.ToLower().Contains(search.ToLower())
            || e.Address.ToLower().Contains(search.ToLower())
            || e.City.ToLower().Contains(search.ToLower())
            || e.PostalCode.ToLower().Contains(search.ToLower())
            || e.Country.ToLower().Contains(search.ToLower())
            || e.Phone.ToLower().Contains(search.ToLower())
            || e.Email.ToLower().Contains(search.ToLower())
            || e.DOB.ToString().Contains(search.ToLower())
            ).ToListAsync();
        })
        .WithName("GetEmployeeByText");

        routes.MapPut("/api/Employee/{id}", async (int Id, Employee employee, EmployeeDBContext db) =>
        {
            var foundModel = await db.Employee.FindAsync(Id);

            if (foundModel is null)
            {
                return Results.NotFound();
            }

            foundModel.Name= employee.Name;
            foundModel.Address= employee.Address;
            foundModel.City= employee.City;
            foundModel.PostalCode= employee.PostalCode;
            foundModel.Country= employee.Country;
            foundModel.Phone= employee.Phone;
            foundModel.Email= employee.Email;
            foundModel.DOB= employee.DOB;
            foundModel.CompanyName= employee.CompanyName;
            foundModel.Department= employee.Department;

            db.Update(foundModel);

            await db.SaveChangesAsync();

            return Results.NoContent();
        })
        .WithName("UpdateEmployee");

        routes.MapPost("/api/Employee/", async (Employee employee, EmployeeDBContext db) =>
        {
            db.Employee.Add(employee);
            await db.SaveChangesAsync();
            return Results.Created($"/Employees/{employee.Id}", employee);
        })
        .WithName("CreateEmployee");

        routes.MapDelete("/api/Employee/{id}", async (int Id, EmployeeDBContext db) =>
        {
            if (await db.Employee.FindAsync(Id) is Employee employee)
            {
                db.Employee.Remove(employee);
                await db.SaveChangesAsync();
                return Results.Ok(employee);
            }

            return Results.NotFound();
        })
        .WithName("DeleteEmployee");
    }
}
