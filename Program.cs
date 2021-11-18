using System;
using System.Collections.Generic;

namespace CatWorx.BadgeMaker
{
  class Program
  {
    static List<Employee> GetEmployees()
    {
        // Takes user input and returns the list of employees. 
        List<Employee> employees = new List<Employee>();
        while (true)
        {            
            Console.WriteLine("Please enter the employee's first name: (leave empty to exit): ");
            string firstName = Console.ReadLine();
            // Break if the user hits ENTER without typing a name.
            if (firstName == "") 
            {
                break;
            }
            Console.Write("Please enter the employee's last name: ");
            string lastName = Console.ReadLine();
            Console.Write("Please enter the employee's ID: ");
            int id = Int32.Parse(Console.ReadLine());
            Console.Write("Please enter the employee's photo url: ");
            string photoUrl = Console.ReadLine();
            Employee currentEmployee = new Employee(firstName, lastName, id, photoUrl);
            employees.Add(currentEmployee);
        }
        return employees;
    }

    static void Main(string[] args)
    {
        List<Employee> employees = GetEmployees();
        Util.PrintEmployees(employees);
    }
  }
}
