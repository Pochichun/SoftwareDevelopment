using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Di
{
    public interface IWinter
    {

    }
    public class Winter : IWinter
    {

        public  Winter()
        {
            Console.WriteLine("На улице дубак");
        }
    }

}
