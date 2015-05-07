using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exception_Handling
{
    class Person
    {
        public static string PersonID { get; set; }
        public static string FName { get; set; }
        public static string LName { get; set; }
        public static string PhoneNumber { get; set; }

        public static List<string> people = new List<string>();

        private static string path = Application.StartupPath + "//People.txt";

        public Person(string id, string first, string last, string phone)
        {
            PersonID = id;
            FName = first;
            LName = last;
            PhoneNumber = phone;
        }

        public static void addPersonToList()
        {
            people.Add(PersonID + " " + FName + " " + LName + " " + PhoneNumber);
        }

        public static void addListToFile()
        {
            try
            {
                //Write to file.
                using (StreamWriter sw = File.AppendText(path))
                {
                    foreach (var item in people)
                    {
                        sw.WriteLine(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Method used to check text file for duplicate ID.
        public static string searchList()
        {
            List<string> lines = new List<string>();

            List<string> idCollection = new List<string>();

            try
            {
                using (StreamReader r = new StreamReader(path))
                {
                    string line;
                    while ((line = r.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }

                foreach (string s in lines)
                {
                    idCollection.Add(s);
                }
            }
            catch (Exception)
            {
                var myFile = File.Create(path);
                myFile.Close();
            }
            string idCollectionString = String.Join(",", idCollection);
            return idCollectionString;
        }
    }
}