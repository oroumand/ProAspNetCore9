using ControllersWithViewSample.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ControllersWithViewSample.Controllers;

public class PeopleController : Controller
{
    private readonly PersonDbContext _personDb;

    public PeopleController(PersonDbContext personDb)
    {
        _personDb = personDb;
    }
    public IActionResult GetAll()
    {
        var pople = _personDb.People.ToList();
        ViewBag.TotalCount = pople.Count;
        ViewBag.HtmlText = "<script>alert('Alireza Oroumand')</script>";
        return View(pople);
    }
    public IActionResult Source()
    {
        TempData["UserName"] = "Oroumand";
        return RedirectToAction("Destination");
    }

    public IActionResult Destination()
    {
        //string userName = TempData.Peek("UserName").ToString();

        string userName = TempData["UserName"].ToString();
        TempData.Keep("UserName");

        return View("Destination", userName);
    }
    [HttpGet]

    public IActionResult Save()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Save(PersonViewModel person)
    {
        if (ModelState.IsValid)
        {
            Person p = new Person
            {
                FirstName = person.FirstName,
                LastName = person.LastName,
            };
            _personDb.People.Add(p);
            _personDb.SaveChanges();
            return RedirectToAction("GetAll");

        }
        return View(person);
    }
}
