using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course3
{
    interface IRandomGen
    {
        int RandomPer60or40(int min, int average, int max);
        int Random(int min, int max);
    }
}
