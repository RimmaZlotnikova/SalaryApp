using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel;
using System.Data.SQLite;
//using System.Data.SqlClient;

namespace SalaryApp
{
    // возможность авторизации? с проверкой логин/пароль - завести отдельную табличку с hr-ами
    //  при успехе - создавать экземпляр hr-а
    //  как приработе разных hr-ов будет разруливаться доступ к данным и запись (блокировки - ??) мб простейший вариант - Singleton?

    class HR
    {
        // +для загрузки актуального списка сотрудников
        // +собрать список сотрудников (актуальный на тек дату)
        // +имеет возможность добавлять сотрудников в БД, устанавливать дату увольнения сотруднику, 
        // Отображать список подчинённых
        // что делать с подчинёнными И/ИЛИ руководителем при увольнении тех и других?

        // производить расчёт зп - с помощью Worker - передать в него ИД выбранного из списка сотрудника, 
        // производить расчет зп для всех сотрудников
        // добавлять группы 
        // фиксировать подчинённых - с проверкой, можно ли данному соруднику иметь подчинённых (контроль "закругления"?)
        
        ApplicationContext db;

        public HR() 
        {
            db = new ApplicationContext();
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
            Worker worker = new Worker(name, recruit_date, remove_date, group_id, base_salary); 
            db.Workers.Add(worker);
            db.SaveChanges();
        }

        public void AddInferToDB( int head_id, int inferrior_id, string add_date, string remove_date )
        {
            Inferrior inferrior = new Inferrior(head_id, inferrior_id, add_date, remove_date );
            db.Inferriors.Add(inferrior);
            db.SaveChanges();
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

        public void addInfIfAvail(int head_id)
        {
            //если для переданного head доступна опция "Подчинённые" - определяется по GROUP_ID, 
            // то 
        }

    }

    }
