using System.Net;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CatWorx.BadgeMaker
{
    class PeopleFetcher
    {
        public static List<Employee> GetEmployees()
        {
            // Takes user input and returns the list of employees. 
            List<Employee> employees = new List<Employee>();
            while (true)
            {            
                Console.WriteLine("Please enter the employee's first name: (leave empty to exit): ");
                string? firstName = Console.ReadLine();
                // Break if the user hits ENTER without typing a name.
                if (firstName == "") 
                {
                    break;
                }
                Console.Write("Please enter the employee's last name: ");
                string? lastName = Console.ReadLine();
                Console.Write("Please enter the employee's ID: ");
                int id = Int32.Parse(Console.ReadLine());
                Console.Write("Please enter the employee's photo url: ");
                string? photoUrl = Console.ReadLine();
                Employee currentEmployee = new Employee(firstName, lastName, id, photoUrl);
                employees.Add(currentEmployee);
            }
            return employees;
        }

        public static List<Employee> GetFromAPI()
        {
            List<Employee> employees = new List<Employee>();
            using (WebClient? client = new WebClient())
            {
                string response = client.DownloadString("https://randomuser.me/api/?results=10&nat=us&inc=name,id,picture");
                JObject json = JObject.Parse(response);
                var valueArray = (JArray?) json["results"];

                foreach (JObject employee in valueArray)
                {
                    Console.WriteLine(employee.SelectToken("name.first"));
                    Employee emp = new Employee
                    (
                        employee.SelectToken("name.first").ToString(),
                        employee.SelectToken("name.last").ToString(),
                        Int32.Parse(employee.SelectToken("id.value").ToString().Replace("-", "")),
                        employee.SelectToken("picture.large").ToString()
                    );
                    employees.Add(emp);                    
                }
            }
            return employees;
        }
    }
}