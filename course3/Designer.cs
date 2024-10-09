using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;


namespace course3
{
    class Designer:AbDesigner       //работа с проектировщиком
    {
        
        TextStatistic stat = new TextStatistic();
        List<Designer> designersList;   //список всех проектировщиков
        RandomGenerator randomGen = new RandomGenerator();  //генератор чисел
        DaysSwitch days = new DaysSwitch();

        public Queue<Project> projects;     //очередь из проектов
        public Designer(string name, int number, string post, Queue<Project> projects)
        {
            this.name = name;       //фио 
            this.number = number;   //порядковый номер
           
            this.projects = projects;   //присваивание очереди
            CompleteCounter = 0;        //счетчик выполненных проектов
        }
        public void setList(List<Designer> designersList)
        {
            this.designersList = designersList;
        }   //присваивание списка проектов
               
        public void readInfo(string fileName, int numDesigner)   //чтение информации о дизайнере из файла
        {
            using (StreamReader reader = new StreamReader(fileName))   //файл для чтения
            {
                string line;
                while (numDesigner > 0) { line = reader.ReadLine(); numDesigner -= 1; }
                line = reader.ReadLine();  //считыванием строку
                
                    string[] parts = line.Split(',');   //делим на части с помощью запятой
                                                        //записываем проектировщика
                    name = parts[0];
                    number =int.Parse(parts[1]);
                    
                averageDays = int.Parse(parts[3]);  //среднее кол-во дней выполнения
                additionallyDays = int.Parse(parts[4]); //погрешность дней
                reader.Close();

            }
        }

        public void working()       //работа
        {
            
            if (projects.Count() == 0)      //если очередь пустая
            {
                
                Console.WriteLine("Проектировщик: {0}, Ожидает проекта...", number);
                return;    
            }
            Project activeProject = projects.Peek();    //активный проект тот, который первый в очереди

            if (activeProject.needDays == 0)        //установка кол-ва дней для работы над проектом
            {
                activeProject.needDays=randomGen.RandomPer60or40(averageDays - additionallyDays, averageDays, additionallyDays + averageDays);
                //70% на то, что выпадет среднее значение, 30% на выпадение из диапазона (не включая среднее)
            }            

            Console.Write("Проектировщик: {0}, ", number);
            activeProject.work();   //проект прорабатывается
            if (number==designersList.Count() && activeProject.elaborationProject())        //если проект полностью выполнен (4 проектировщик сделал)
            {
                
                Console.WriteLine("\t\tПРОЕКТ {0} ПОЛНОСТЬЮ ВЫПОЛНЕН!", projects.Peek().name);
                CompleteCounter += 1;

                stat.WriteProjectStats(days.getDaysCount(), activeProject);
                projects.Dequeue();
                return;
            }

            if (activeProject.elaborationProject()) //если проект сделан
            {

                transferProject(activeProject); //передача проекта другому проектировщику
                projects.Dequeue();             //удаления сделанного проекта 
                CompleteCounter += 1;           //счетчик выполненных проектов+=1
                return;
            };

        }
        void transferProject(Project project)   //передача проектов
        {
            Console.WriteLine("\t\tПРОЕКТ: {0}, ПЕРЕДАЛСЯ ПРОЕКТИРОВЩИКУ: {1}", project.name, designersList[project.stage - 1].number);
            if (project.urgency==ProjectUrgency.low) designersList[project.stage-1].projects.Enqueue(project);
            //если проекта несрочный, то просто вставить его в конец очереди
            else
            {
                Queue<Project> projectsTemp = new Queue<Project>();  //создание новой очереди для перезаписи (чтобы вставить проект в начало)
                if (designersList[project.stage - 1].projects.Count() > 0)   //если очередь не пустая, то нужно проверить, какой проект первый в очереди 
                {
                    if (designersList[project.stage - 1].projects.Peek().urgency == ProjectUrgency.high)  //если активный проект тоже срочный
                    {
                        projectsTemp.Enqueue(designersList[project.stage - 1].projects.Dequeue());   //активный остается активным
                        projectsTemp.Enqueue(project);              //новый срочный после активного
                        foreach (Project item in projects)          //перезапись проектов 
                        {
                                projectsTemp.Enqueue(item);
                        }
                        designersList[project.stage - 1].projects.Clear();
                        foreach (Project item in projectsTemp)
                        {
                            designersList[project.stage - 1].projects.Enqueue(item);
                        }
                        return;
                    }
                }   

                Console.WriteLine("\t\tПРОЕКТИРОВЩИК: {0}, НАЧИНАЕТ РАБОТУ НАД СРОЧНЫМ ПРОЕКТОМ", designersList[project.stage - 1].number);
                
                projectsTemp.Enqueue(project);             //вставка срочного проекта в начало доп. очереди
                foreach (Project item in designersList[project.stage-1].projects) 
                {
                    projectsTemp.Enqueue(item);     //перезапись всех остальных проектов в доп очередь
                }
                designersList[project.stage-1].projects.Clear();    //очиста очереди у след проектировщика
                foreach (Project item in projectsTemp)
                {
                    designersList[project.stage-1].projects.Enqueue(item);  //перезапись проектов проектировщика из доп очереди
                }

            }
        }
        
        public void takeProject(Project project)        //получение проекта (для первого проектировщика)
        {
            project.beginDay = days.getDaysCount();
            
            if (project.urgency == ProjectUrgency.low) projects.Enqueue(project);   //если несрочный, то в конец очереди
            else        //если срочный, вставить в начало очереди
            {
                
                Queue<Project> projectsTemp = new Queue<Project>();
                if (projects.Count() != 0)
                {
                    if (projects.Peek().urgency == ProjectUrgency.high)  //если активный проект тоже срочный
                    {
                        projectsTemp.Enqueue(projects.Peek());   //активный остается активным
                        projectsTemp.Enqueue(project);              //новый срочный почле активного
                        projects.Dequeue();
                    } else projectsTemp.Enqueue(project);      //еси очередь пуста или первый в очереди несрочный, то новый проект становится активным
                } else projectsTemp.Enqueue(project);      //еси очередь пуста или первый в очереди несрочный, то новый проект становится активным


                foreach (Project item in projects)
                {
                    projectsTemp.Enqueue(item);
                }
                
                projects.Clear();
                foreach (Project item in projectsTemp)
                {
                    projects.Enqueue(item);
                }

            }
        }
    }
}
