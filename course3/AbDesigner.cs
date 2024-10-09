using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course3
{
    abstract class AbDesigner
    {
        protected  int  number { get; set; }        //порядкой номер проектировщика
        protected  string name { get; set; }        //имя проектировщика
        protected int averageDays { get; set; }     //среднее время выполнения проекта
        protected int additionallyDays { get; set; }//погрешность времени выполнения
        public int CompleteCounter { get; set; }    //счетчик выполненных проектов



        public string  getName()    //получить имя
        {
            return name;
        }
        public int getNumber ()     //получить порядковый номер
        {
            return number;
        }
        public void printDesigner()     //вывод проектировщика
        {
            Console.WriteLine("ФИО: {0}\nПорядковый номер: {1}", name, number);
            Console.WriteLine();
        }

    }
}
