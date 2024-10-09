using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course3
{
    abstract class AbProject:IProject
    {
        
        public string name { get; set; }        //название проекта
        public int stage { get; set; }          //выполняющий проектировщик
        public ProjectUrgency urgency { get; set; } //срочность проекта
        public int needDays { get; set; }       //необходимое кол-во дней на отработку (на каждом этапе разное)

        public int number { get; set; }         //номер проекта

        public void printName() //вывод названия
        {
            Console.WriteLine("Название проекта: {0}", name);   
        }
      
        public void printUrgency()  //вывод срочности
        {
            Console.WriteLine("Срочность проекта: {0}", Enum.GetName(typeof(ProjectUrgency), urgency));
        }
        abstract public bool elaborationProject();     //проработка проекта на этапе
        abstract public void printProject();    //вывод всего проекта на консоль
    }
}

