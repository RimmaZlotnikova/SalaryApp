using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel;
using System.Data.SQLite;
using System.Windows.Forms;

namespace SalaryApp
{
    // возможность авторизации с проверкой логин/пароль - завести отдельную табличку с hr-ами
    //  при успехе - создавать экземпляр hr-а 
    //      как приработе разных hr-ов будет разруливаться доступ к данным и запись (блокировки - ??) мб простейший вариант - Singleton?

    class HR
    {
        // +для загрузки актуального списка сотрудников
        // +собрать список сотрудников (актуальный на тек дату)
        // +имеет возможность добавлять сотрудников в БД, устанавливать дату увольнения сотруднику, 
        // +Отображать список подчинённых
        // Не решается, что делать с подчинёнными И/ИЛИ руководителем при увольнении тех и других

        // +производить расчёт зп - с помощью Worker - передать в него ИД выбранного из списка сотрудника, 
        // +производить расчет зп для всех сотрудников
        //+ фиксировать подчинённых - с проверкой, можно ли данному соруднику иметь подчинённых 
        // Отсутствует контроль "закругления" - т.е. проверка, чтобы сотрудники не были взаимно руководителями, как частный случай
        //      Требуется решение: проверка при добавлении установлении подчинённого, он не был выше/ниже его в дереве подчинённых
        

        ApplicationContext db;

        public HR()
        {
            db = new ApplicationContext();
        }

        public decimal GetSalary( int id )
        {
            Worker worker = GetObject(id);
            return worker.GetSalary(DateTime.UtcNow, db);
        }

        public Worker GetObject(int id)
        {
            return Worker.GetRightObgById(id, db);
        }

        public Group_attribs GetByGroupId(int group_id)
        {
            Group_attribs info = db.Group_attribs.Find(group_id);
            return info;
        }

        public BindingList<Worker> getActWorkersList()
        {
            // вернём список объектов для отображения в гриде - для Источника данных
            DateTime d = DateTime.UtcNow;
            string curDateTime = d.ToString("yyyy.MM.dd hh:mm:ss");
            db.Workers.Local.Clear();
            db.Workers.SqlQuery("Select * from Workers where remove_date > @curDateTime", 
                new SQLiteParameter("@curDateTime", curDateTime)).ToList<Worker>();           
            return db.Workers.Local.ToBindingList();
        }

        public void AddWorkerToDB(string name, string recruit_date, string remove_date, int group_id, decimal base_salary)
        {
            // создадим новую запись в таблице Workers
            Worker worker = new WorkerWithInf(name, recruit_date, remove_date, group_id, base_salary); 
            db.Workers.Add(worker);
            db.SaveChanges();
        }

        public void AddInferToDB( int head_id, int inferrior_id, string add_date, string remove_date )
        {
            Worker worker = new Worker(head_id, db);
            if (worker.CheckIfInferAvail() == true)
            {
                Inferrior inferrior = new Inferrior(head_id, inferrior_id, add_date, remove_date);
                db.Inferriors.Add(inferrior);
                db.SaveChanges();
            }
            else
            {
                MessageBox.Show("Для данного вида сотрудников невозможно добавление подчинённых", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        public void ChangeWorker( int id, string remove_date )
        {
            // изменить существующую запись о сотруднике по ID
            Worker worker = db.Workers.Find(id);
            worker.Remove_date = remove_date;
            db.SaveChanges();
        }

        public BindingList<Group_attribs> getInfoList()
        {
            // вернём список объектов для отображения в гриде - для Источника данных
            db.Group_attribs.Local.Clear();
            db.Group_attribs.Load();
            return db.Group_attribs.Local.ToBindingList();
        }

        public List<Worker> getInferList(int head_id)
        {
            // вернём список объектов для отображения в гриде - для Источника данных
            // TODO завернуть в класс WorkerWithInfer .....
            var inferriors = db.Inferriors
                .SqlQuery("Select * from Inferriors where head_id == @head_id",
                new SQLiteParameter("@head_id", head_id)).ToList<Inferrior>();
            var workers = db.Workers.Local;
            var result = workers 
                .AsEnumerable()
                .Join
               (inferriors,
                     p => p.id,
                     e => e.Inferrior_id,
                     (p, e) => new Worker
                     {
                         id = p.id,
                         Name = p.Name,
                         Group_id = p.Group_id,
                         Recruit_date = p.Recruit_date,
                         Remove_date = p.Remove_date,
                         Base_salary = p.Base_salary,
                     }
                );
            return result.ToList<Worker>();
        }

        public List<Worker> GetLocalWorkers( )
        {
            return db.Workers.Local.ToList<Worker>();
        }


    }

    }
