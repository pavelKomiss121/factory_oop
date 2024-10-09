using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
using System.Globalization;


namespace course3
{
    class Program
    {
        static void Main(string[] args)
        {
            int countDesigners;     //кол-во проектировщиков
            int countProject;       //кол-во проектов
            int numberProject = 0;
            int orderReceipt;       //среднее время для появления проекта
            int borderOrder;        //погрешность во времени
            int orderReceiptDay;    //фактический день появления проекта
            bool checkProjects = false;

            settingsReading settings = new settingsReading();       //настройки
            DaysSwitch days = new DaysSwitch();                     //смена дней
            RandomGenerator random = new RandomGenerator();         //генератор чисел

            
            List<Designer> designersList = new List<Designer>();      //список проектировщиков
            List<Project> projectsList = new List<Project>();         //список проектов
            List<Project> projectsListTemp = new List<Project>();     //список проектов (который уменьшается)

            settings.read(out countDesigners, out countProject, out orderReceipt, out borderOrder); //чтение настроек
            
            designersList = settings.GetDesignersList();        //получение всех списков
            projectsList = settings.GetProjectsList();
            foreach(Project project in projectsList)
            {
                projectsListTemp.Add(project);
            }
            

            settings.PrintDesignersList();      //вывести списки на консоль
            settings.PrintProjectsList();

            Console.WriteLine("Нажмите ENTER, чтобы продолжить");
            Console.ReadLine();

         


            Console.WriteLine("Нужно ли останавливать программу при появлении новых проектов? (если надо, напишите 'ДА')");

            if (Console.ReadLine() == "ДА") checkProjects = true;

            Console.WriteLine("\n\n\n");

            orderReceiptDay = random.RandomPer60or40(orderReceipt - borderOrder, orderReceipt, borderOrder + orderReceipt); //день выпадения
            while (designersList[designersList.Count()-1].CompleteCounter!= countProject)    //пока все проекты не сделаны
            {

                if (days.day == orderReceiptDay && projectsListTemp.Count > 0)    //если проект поступил и проекты не закончились
                {
                    int index = random.Random(0, projectsListTemp.Count() - 1);
                    projectsListTemp[index].number = numberProject += 1;
                    designersList[0].takeProject(projectsListTemp[index]);       //передача проекта 1 проектировщику
                    Console.WriteLine("\t\tПОСТУПИЛ ПРОЕКТ: ");

                    projectsListTemp[index].printProject();
                    if (checkProjects == true)
                    { 
                        Console.WriteLine("Нажмите ENTER, чтобы продолжить");
                        Console.ReadLine();
                    }
                    orderReceiptDay = days.day % random.RandomPer60or40(orderReceipt - borderOrder, orderReceipt, borderOrder + orderReceipt); //день выпадения
                    if (orderReceiptDay == 0) orderReceiptDay = days.day;
                    if (projectsListTemp[index].urgency == ProjectUrgency.high)
                        Console.WriteLine("\t\tПРОЕКТИРОВЩИК: {0}, НАЧИНАЕТ РАБОТУ НАД СРОЧНЫМ ПРОЕКТОМ\n", 1);
                    projectsListTemp.RemoveAt(index);        //удаление из списка проектов (temp)
                }
              
                days.SwitchDay();       //смена дня
                for(int i=0; i < designersList.Count() ; i++)
                {
                    designersList[i].working();
                    Thread.Sleep(50);
                }
                Console.WriteLine("\n");
     
            }
            Console.WriteLine("\t\tВСЕ ПРОЕКТЫ ВЫПОЛНЕНЫ!\n", 1);
            Console.ReadLine();

        }
    }
}
