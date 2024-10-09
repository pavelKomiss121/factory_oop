using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace course3
{
    class DaysSwitch        //смена дней
    {
        List<string> days = new List<string>();
        static int daysCount=0;
        public int day { get; set; }
       

        public DaysSwitch()     //создание списка дней ннедели
        {
            
            days.Add("Понедельник");
            days.Add("Вторник");
            days.Add("Среда");
            days.Add("Четверг");
            days.Add("Пятница");
            days.Add("Суббота");
            days.Add("Воскресенье");
            days.Add("Неделя окончена");
            day = 0;
        }
        public void SwitchDay()     //смена дней
        {
            if (day==7) Console.WriteLine("{0}\n", days[day]);
            else
                Console.WriteLine("{0} ({1} день)\n", days[day], daysCount+1);
            day += 1;
            Thread.Sleep(50);
            daysCount += 1;
            if (day == 8) SwitchWeek(); //когда вся неделя прошла, запустить смену недели
        }
         void SwitchWeek()      //смена недели
        {
            daysCount -= 1;
            Console.WriteLine();
            Thread.Sleep(100);
            day = 0;
            SwitchDay();
        }
        public int getDaysCount()
        {
          
           return daysCount;
        }
    }
}
