using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBAttribute
{
    class Program
    {
        static void Main(string[] args)
        {
            //Int16 i = 2
            //var x = Convert.ChangeType(ZStatus.Grauzone, typeof(ZStatus));
            DBBase<Zigarette> db = new DBBase<Zigarette>();
            try
            {
                db.Open();

                foreach (Zigarette ziga in db.AlleLesen())
                {
                    Console.WriteLine(ziga);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                db.Close();
            }
            Console.ReadKey();
        }
    }
}
