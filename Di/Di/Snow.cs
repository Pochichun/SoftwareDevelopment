using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Di
{
    public interface ISnow
    {

    }
    internal class Snow : ISnow
    {
        public  Snow(IWinter Winter)
        {
            Console.WriteLine("Снежища то сколько навалило...");
        }
    }
}
