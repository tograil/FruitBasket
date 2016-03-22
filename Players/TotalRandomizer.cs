using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Players
{
    public static class TotalRandomizer
    {
        private static readonly Random random = new Random(DateTime.Now.Second);

        public static int GetNext()
        {
            return random.Next();
        }
    }
}
