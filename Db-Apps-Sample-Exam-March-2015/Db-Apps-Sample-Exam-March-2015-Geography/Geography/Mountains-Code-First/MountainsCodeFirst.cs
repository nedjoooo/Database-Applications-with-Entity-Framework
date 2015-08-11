namespace Mountains_Code_First
{
    using Mountains_Code_First.Migrations;
    using System;
    using System.Data.Entity;
    using System.Linq;

    class MountainsCodeFirst
    {
        static void Main()
        {
            

            var context = new MountainsContext();

            var query = context.Mountains
                .Select(m => new
                {
                    m.Name,
                    Countries = m.Countries.Select(c => c.Name),
                    Peaks = m.Peaks.Select(c => c.Name)
                });

            foreach (var q in query)
            {
                Console.WriteLine("{0} ; {1} ; {2}",
                    q.Name,
                    String.Join(", ", q.Countries),
                    String.Join(", ", q.Peaks));
            }
        }
    }
}
