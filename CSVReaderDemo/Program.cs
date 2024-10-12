using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.IO;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;


namespace CSVReaderDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var streamReader = new StreamReader(@"C:\Users\location\csvReaderDemo.csv"))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<FighterLaunchClassMap>();
                    var records = csvReader.GetRecords<FighterLaunch>().ToList();

                    foreach (var record in records)
                    {
                        if (record.Name == "giorgi")
                        {
                            record.Name = "CHAMAAAA";
                        }
                        Console.WriteLine(record.Id + " " + record.Name + " " + record.Surname + " " + record.Age);
                    }

                    using (var streamWriter = new StreamWriter(@"C:\Users\location\csvReaderDemoModified.csv"))
                    {
                        using (var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                        {
                            csvWriter.Context.RegisterClassMap<FighterLaunchClassMap>();
                            csvWriter.WriteRecords(records);
                        }
                    }


                    Console.WriteLine(records.Count);
                }
            }

            Console.ReadKey();
        }
    }

    public class FighterLaunchClassMap : ClassMap<FighterLaunch>
    {
        public FighterLaunchClassMap()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.Name).Name("Name");
            Map(m => m.Surname).Name("Surname");
            Map(m => m.Age).Name("Age");
        }

    }

    public class FighterLaunch
    {
        //[Name("Id")] // Commented because we created ClassMap above this class
        public int Id { get; set; }
        //[Name("Name")] //column names in CSV
        public string Name { get; set; }
        //[Name("Surname")]
        public string Surname { get; set; }
        //[Name("Age")]
        public int Age { get; set; }
    }
}
