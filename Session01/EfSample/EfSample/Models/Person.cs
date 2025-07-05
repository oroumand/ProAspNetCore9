using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace EfSample.Models;
public class Person
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Author { get; set; }
}

public class PersonDB:DbContext
{
    public DbSet<Person> People { get; set; }
}
