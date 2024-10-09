using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace course3
{
    class Project : AbProject
    {
        TextStatistic stat = new TextStatistic();
        int workingDays = 0;        //отработанные дни над проектом
        public List<int> timeWorking = new List<int>();
        public int beginDay;
        public Project(ProjectUrgency urgency, string name, int stage = 0)
        {
            this.urgency = urgency;
            this.name = name;
            this.stage = stage;
        }
        override public bool elaborationProject()      //проработка проекта на этапе
        {
            if (workingDays == needDays)    //если проработан
            {
                timeWorking.Add(workingDays);
                //if (stage == 4) { stat.WriteProjectStats(); }
                
                workingDays = -1;
                stage += 1;
                needDays = 0;
                return true;
            }
            else return false;
            
        }
        public void work()      //работа над проектом
        {
            if (workingDays == -1)      //если проект только передался, нужно начать его со след. дня
            {
                workingDays += 1;
                Console.WriteLine("Ожидает проекта...");
                return;
            }
            workingDays += 1;
            if (workingDays== needDays)
            {
                Console.WriteLine("Этап завершен, можно передать проект {0}", name);
                return;
            }
            Console.WriteLine("Проект проработан на {0}/{1}", workingDays, needDays);
        }
        public void readInfo(string fileName, int numProject)          //чтение информации о дизайнере из файла
        {
            using (StreamReader reader = new StreamReader(fileName))   //файл для чтения
            {
                string line;

                while (numProject > 0) { line = reader.ReadLine(); numProject -= 1; }
                line = reader.ReadLine();  //считыванием строку

                string[] parts = line.Split(',');   //делим на части с помощью запятой
                                                    //записываем студента из файла
                name = parts[0];
                if (!Enum.TryParse(parts[1], out ProjectUrgency urgencyOut))
                {
                    throw new Exception("Не удалось преобразовать строку в перечисление Urgency");
                }
                urgency = urgencyOut;
                stage = int.Parse(parts[2]);
                reader.Close();
            }
        }
        public override void printProject()
        {
            printName();
            printUrgency();
            Console.WriteLine();
        }
        
    }
}
