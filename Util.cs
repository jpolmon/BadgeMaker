using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Collections.Generic;

namespace CatWorx.BadgeMaker
{
    class Util
    {
        public static void PrintEmployees(List<Employee> employees)
        {
            for (int i = 0; i < employees.Count; i++) 
            {
                string template = "{0,-10}\t{1,-20}\t{2}";
                Console.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetName(), employees[i].getPhotoUrl()));
            }
        }

        public static void MakeCSV(List<Employee> employees)
        {
            if (!Directory.Exists("data"))
            {
                Directory.CreateDirectory("data");
            }
            using (StreamWriter file = new StreamWriter("data/eployees.csv")) 
            {
                for (int i = 0; i < employees.Count; i++) 
                {
                    string template = "{0,-10}\t{1,-20}\t{2}";
                    file.WriteLine(String.Format(template, employees[i].GetId(), employees[i].GetName(), employees[i].getPhotoUrl()));
                }
            }

        }

        public static void MakeBadges(List<Employee> employees)
        {
            // Layout variables
            int BADGE_WIDTH = 669;
            int BADGE_HEIGHT = 1044;

            int COMPANY_NAME_START_X = 0;
            int COMPANY_NAME_START_Y = 110;
            int COMPANY_NAME_WIDTH = 100;

            int PHOTO_START_X = 184;
            int PHOTO_START_Y = 215;
            int PHOTO_WIDTH = 302;
            int PHOTO_HEIGHT = 302;

            int EMPLOYEE_NAME_START_X = 0;
            int EMPLOYEE_NAME_START_Y = 560;
            int EMPLOYEE_NAME_WIDTH = BADGE_WIDTH;
            int EMPLOYEE_NAME_HEIGHT = 100;

            int EMPLOYEE_ID_START_X = 0;
            int EMPLOYEE_ID_START_Y = 690;
            int EMPLOYEE_ID_WIDTH = BADGE_WIDTH;
            int EMPLOYEE_ID_HEIGHT = 100;

            // Graphics objects
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            int FONT_SIZE = 32;
            Font font = new Font("Arial", FONT_SIZE);
            Font monoFont = new Font("Courier New", FONT_SIZE);

            SolidBrush brush = new SolidBrush(Color.Black);
            
            // Instance of WebClient is disposed after the code is run
            using(WebClient client = new WebClient()) 
            {
                for (int i = 0; i < employees.Count; i++) 
                {
                    Image photo = Image.FromStream(client.OpenRead(employees[i].getPhotoUrl()));
                    Image background = Image.FromFile("badge.png");
                    Image badge = new Bitmap(BADGE_WIDTH, BADGE_HEIGHT);
                    Graphics canvas = Graphics.FromImage(badge);
                    canvas.DrawImage(background, new Rectangle(0, 0, BADGE_WIDTH, BADGE_HEIGHT));
                    canvas.DrawImage(photo, new Rectangle(PHOTO_START_X, PHOTO_START_Y, PHOTO_WIDTH, PHOTO_HEIGHT));

                    // Adding company name
                    canvas.DrawString(
                        employees[i].GetCompanyName(), 
                        font, 
                        new SolidBrush(Color.White),
                        new Rectangle(
                            COMPANY_NAME_START_X,
                            COMPANY_NAME_START_Y,
                            BADGE_WIDTH,
                            COMPANY_NAME_WIDTH
                        ),
                        format
                    );

                    // Adding employee name
                    canvas.DrawString(
                        employees[i].GetName(), 
                        font, 
                        brush,
                        new Rectangle(
                            EMPLOYEE_NAME_START_X,
                            EMPLOYEE_NAME_START_Y,
                            EMPLOYEE_NAME_WIDTH,
                            EMPLOYEE_NAME_HEIGHT
                        ),
                        format
                    );

                    // Adding employee ID
                    canvas.DrawString(
                    employees[i].GetId().ToString(),
                    monoFont,
                    brush,
                    new Rectangle(
                        EMPLOYEE_ID_START_X,
                        EMPLOYEE_ID_START_Y,
                        EMPLOYEE_ID_WIDTH,
                        EMPLOYEE_ID_HEIGHT
                    ),
                    format
                    );

                    string fileName = String.Format("data/{0}_badge.png", employees[i].GetId().ToString());
                    badge.Save(fileName);
                    // http://placekitten.com/200/300

                }
            }
        }
    }
}