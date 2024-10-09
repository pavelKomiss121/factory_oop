using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace course3
{
    class RandomGenerator:IRandomGen        //генеработр случайного числа
    {
        Random random = new Random((int)DateTime.UtcNow.Ticks);
        public int RandomPer60or40(int min, int average, int max)   //выпадение того или иного события с вероятностью (60 на 40)
        {
            Random random = new Random((int)DateTime.UtcNow.Ticks);
            
            int num = random.Next(1, 11); // генерируем случайное число между 1 и 10

            if (num <= 7)      //вероятность выпадения 70%
            {
                return average;
            }
            else               //вероятность 30%
            {
                random = new Random((int)DateTime.UtcNow.Ticks);
                num = random.Next(min, max+1);  

                while (num==average) num = random.Next(min, max + 1);   //исключаем момент выпадения числа с вероятностью 60%
                return  num;
            }
            
        }

        public int Random(int min, int max)
        {
            Random random = new Random((int)DateTime.UtcNow.Ticks);
            int num = random.Next(min, max + 1);    //генерация случайного в диапазоне числа
            return num;
        }
    }
}
