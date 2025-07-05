using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfSample.Models;
internal class PersonService
{
    void Add(Person person)
    {

    }
    public void Update(Person person) { }
    public void Delete(Person person) { }
}


public class AddPersonHandler
{
    public int age { get; set; }
    public void Handle(Person person) { }

}


public class UpdatePersonHandler
{
    public void Handle(Person person) { }

}


public class DeletePersonHandler
{
    public void Handle(Person person) { }

}