﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InMemoryDbASPNet6WebAPI.Model;
using Newtonsoft.Json;

public class EmployeeDBContext : DbContext
{
    public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options)
        : base(options)
    {
        if (!Employee.Any())
        {
            using(StreamReader r=new StreamReader("Employees.json"))
            {
                string json = r.ReadToEnd();
                var root=JsonConvert.DeserializeObject<RootEmployees>(json);
                Employee.AddRange(root.Employee);
            }
            this.SaveChanges();
        }
    }

    public DbSet<Employee> Employee { get; set; }
}
