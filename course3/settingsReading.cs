using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace course3
{
    class settingsReading
    {
        int countDesigners { get; set; }     //кол-во проектировщиков
        int countProject;       //кол-во проектов
        int orderReceipt;
        int borderOrder;
        

        List<Designer> designersList = new List<Designer>();      //списки проектировщиков и проектов
        List<Project> projectsList = new List<Project>();
        

        public void read(out int countDesigners, out int countProject, out int orderReceipt, out int borderOrder)
        {

            StreamReader reader = new StreamReader("countWorkerAndWork.txt");   //файл для чтения настроек
            countDesigners = int.Parse(reader.ReadLine());                     
            countProject = int.Parse(reader.ReadLine());
            string line = reader.ReadLine();
            string[] parts = line.Split(',');
            orderReceipt = int.Parse(parts[0]);
            borderOrder = int.Parse(parts[1]);
            reader.Close();

            this.countDesigners = countDesigners;
            this.countProject = countProject;
            this.designersList = designersList;
            this.projectsList = projectsList;
            this.borderOrder = borderOrder;
            this.orderReceipt = orderReceipt;
        }
        public List<Designer> GetDesignersList()
        {
            for (int i = 0; i < countDesigners; i++)       //создание списка проектировщиков
            {
                Queue<Project> queue = new Queue<Project>();
                Designer designer = new Designer("", 0, "", queue);
                designersList.Add(designer);
                designersList[i].readInfo("designers.txt", i);
            }
            for (int i = 0; i < countDesigners; i++)       //присвоение каждому проектировщику списка сотрудников
            {

                designersList[i].setList(designersList);
            }
            return designersList;
        }

        public List<Project> GetProjectsList()
        {
            for (int i = 0; i < countProject; i++)       //создание списка проектов
            {
                Project project = new Project(0, "");
                projectsList.Add(project);
                projectsList[i].readInfo("projects.txt", i);
            }
            return projectsList;
        }

        public void PrintProjectsList()
        {
            Console.WriteLine("\n\t\tСПИСОК ВСЕХ ПРОЕКТОВ:\n");       //вывод всех проектов на экран
            for (int i = 0; i < countProject; i++)
            {
                Console.Write("{0}. ", i + 1);
                projectsList[i].printProject();
            }
        }
        public void PrintDesignersList()
        {
            Console.WriteLine("\n\t\tСПИСОК ВСЕХ ПРОЕКТИРОВЩИКОВ:\n");        //вывод всех проектировщиков на экран
           
            for (int i = 0; i < countDesigners; i++)
            {
                Console.Write("{0}. ", i + 1);
                designersList[i].printDesigner();
                
            }
        }

       
    }
}
