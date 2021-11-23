using System;
using System.Collections.Generic;

namespace CatWorx.BadgeMaker
{
  class Program
  {
    static void Main(string[] args)
    {
      string company = "";
      do 
      {
        Console.WriteLine("What is your company's name? [cannot be blank]");
        company = Console.ReadLine();
      } while (company == "");

      List<Employee> employees = new List<Employee>();
      string response = ""; 
      do 
      {
        Console.WriteLine("Would you like to auto-generate employee information? [y/n]");
        response = Console.ReadLine();
      } while (response != "y" && response != "n"); 
      if (response == "y") {
        employees = PeopleFetcher.GetFromAPI();
      }
      else if (response == "n") {
        employees = PeopleFetcher.GetEmployees();
      }
      Util.MakeCSV(employees);
      Util.MakeBadges(employees, company);
    }
  }
}
