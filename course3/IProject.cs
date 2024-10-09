using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace course3
{
    interface IProject
    {
        string name { get; set; }   //название проекта
        int stage { get; set; }     //этап выполнения
        ProjectUrgency urgency { get; set; }    //срочность
        void printProject();    //вывести проект на экран
        bool elaborationProject();  //степень проработки

    }
}
