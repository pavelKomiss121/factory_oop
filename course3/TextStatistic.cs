using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace course3
{
    class TextStatistic
    {
        string filePath = "statistic.txt";
        int number = 1;
        static bool name = false;

       

        public void WriteProjectStats(int days, Project project)
        {
            using (StreamWriter writer = File.AppendText(filePath))
            {
                if (name == false)
                {
                    writer.WriteLine("");
                    writer.WriteLine("_______________________________________________________________________________________________");
                    writer.WriteLine("СБОР СТАТИСТИКИ О ПРОЕКТАХ:");
                    writer.WriteLine("_______________________________________________________________________________________________");
                    name = true;
                }
                writer.WriteLine("_______________________________________________________________________________________________");
                writer.WriteLine("{0}. Полностью выполнен проект: {1}\n   C уровнем срочности: {2}\n   День начала проектирования:{3}\n   День окончания проектирования:{4}", number, project.name, project.urgency, project.beginDay + 1, days);
                writer.WriteLine("   Колличество дней работы над проектом на каждом этапе:{0}дн,{1}дн,{2}дн,{3}дн", project.timeWorking[0], project.timeWorking[1], project.timeWorking[2], project.timeWorking[3]);
                writer.WriteLine("   По счету проект поступил: {0}", project.number);
                writer.WriteLine("_______________________________________________________________________________________________");
                number += 1;
            }
        }
    }
}