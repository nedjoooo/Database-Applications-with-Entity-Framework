namespace Import_Mountains
{
    using Mountains_Code_First;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Web.Script.Serialization;
    using System.Linq;

    class ImportMountains
    {
        static void Main()
        {
            var context = new MountainsContext();
            var json = File.ReadAllText(@"../../mountains.json");
            JavaScriptSerializer ser = new JavaScriptSerializer();
            var mountains = ser.Deserialize<MountainDTO[]>(json);

            foreach (var mountainDTO in mountains)
            {
                try
                {
                    AddCountryToDb(context, mountainDTO);
                    Console.WriteLine("Mountain {0} imported", mountainDTO.MountainName);
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error: {0}", ex.Message);
                }
                
            }
            context.SaveChanges();
        }

        private static void AddCountryToDb(MountainsContext context, MountainDTO mountainDTO)
        {
            if(mountainDTO.MountainName == null)
            {
                throw new Exception("Mountain name is required");
            }
            var m = new Mountain() { Name = mountainDTO.MountainName };
            foreach (var peak in mountainDTO.Peaks)
            {
                if(peak.PeakName == null)
                {
                    throw new Exception("Peak name is required");
                }
                if(peak.Elevation == null)
                {
                    throw new Exception("Peak elevation is required");
                }
                var peakToDb = new Peak()
                {
                    Name = peak.PeakName,
                    Elevation = peak.Elevation.GetValueOrDefault()
                };
                m.Peaks.Add(peakToDb);
            }
            foreach (var countryName in mountainDTO.Countries)
            {
                var country = context.Countries.FirstOrDefault(c => c.Name == countryName);
                if (country == null)
                {
                    country = new Country()
                    {
                        Code = countryName.ToUpper().Substring(0, 2),
                        Name = countryName
                    };
                }
                country.Mountains.Add(m);
                context.Countries.Add(country);
            }
            context.Mountains.Add(m);
        }
    }
}
