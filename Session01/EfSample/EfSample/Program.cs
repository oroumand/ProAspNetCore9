using EfSample.Models;

PersonDB personDB = new PersonDB();

var people = personDB.People.ToList();

var person = new Person
{
    Id = 1,
    Name = "Foo",
    Author = "Temp",
    Description = "Bar",
};

personDB.People.Add(person);
personDB.SaveChanges();

