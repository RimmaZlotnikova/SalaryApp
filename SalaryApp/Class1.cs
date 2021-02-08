using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SQLite;

namespace SalaryApp
{

    class Worker
    {
        // класс для обработки 1 сотрудника:
        //  считать все его парам-ры из БД по переданному ИД
        //  создать объект подходящей группы - 
        //      workerWithInf / workerWithoutInf
        //          по полю могут быть подчинённые, или нет
        //          т.е. определяющим является необходимость расчёта коэф по подчинённм сотрудникам    
        //  расчёт зп: получить базу, рассчитать коэфиициент на основе базового с учётом стажа,
        //              рассчитать коэффициент по количеству подчинённых сотрудников с учётом группы
        //  Считаю неправильным по логике решением иметь Базовый класс, создавать его экземпляры - например, для работы с БД
        //          и в то же время иметь наследующие классы, тоже с созданием объектов - для расчёта з/п по-разному
        //  Т.е. родитель решает одни вопросы, а наследники уже по-разному другие.
        //  TODO
        //      при редактировании/добавлении сотрудников необходимо добавить контроль за вводимыми данными: датой, группой - подбрасывать из связанной таблицы
        //      Не везде расчёт з/п дописан с возможностью HR-ом указать дату...
        public int id { get; set; }
        protected string name;
        protected string recruit_date, remove_date;
        protected int group_id;
        protected decimal base_salary;
        protected ApplicationContext db;
        protected Group_attribs info;

        public string Name
        {
            get { return name; }
            set{ name = value; }
        }
        
        public string Recruit_date
        {
            get { return recruit_date; }
            set { recruit_date = value; }
        }
        public string Remove_date
        {
            get { return remove_date; }
            set { remove_date = value; }
        }
        
        public int Group_id
        {
            get { return group_id; }
            set { group_id = value; }
        }
        
        public decimal Base_salary
        {
            get { return base_salary; }
            set { base_salary = value; }
        }

        public Worker() { }

        public Worker(string name, string recruit_date, string remove_date, int group_id, decimal base_salary)
        {
            this.name = name;
            this.recruit_date = recruit_date;
            this.remove_date = remove_date;
            this.group_id = group_id;
            this.base_salary = base_salary;
        }

        public Worker(int id, ApplicationContext db)
        {
            Worker worker = db.Workers.Find(id);

            this.id = worker.id;
            this.name = worker.name;
            this.recruit_date = worker.recruit_date;
            this.remove_date = worker.remove_date;
            this.group_id = worker.group_id;
            this.base_salary = worker.base_salary;
        }

        public Worker(Worker worker, Group_attribs info)
        {
            this.id = worker.id;
            this.name = worker.name;
            this.recruit_date = worker.recruit_date;
            this.remove_date = worker.remove_date;
            this.group_id = worker.group_id;
            this.base_salary = worker.base_salary;

            this.info = info;
        }

        public static Worker GetRightObgById(int id, ApplicationContext db)
        {
            Worker worker = new Worker(id, db);  
            Group_attribs info = new Group_attribs(worker.group_id);

            if (info.InfAvail() == false)
            {
                return new WorkerWithoutInf(worker, info);
            }
            else
            {
                return new WorkerWithInf(worker, info);
            }
        }

        public bool CheckIfInferAvail( )
        {
            if (info == null)
            {
                this.info = new Group_attribs(this.group_id);
            }
            return info.InfAvail();
        }

        public virtual decimal GetSalary(DateTime datetime, ApplicationContext db) 
        {
            // расчёт зп без учёта подчинённых - тк не положены по группе 
            decimal yearsRatio = GetYearsRatio(datetime); // [ year_ratio; max_ratio ]
            return (this.base_salary + this.base_salary / yearsRatio);
        }

        public decimal GetYearsRatio(DateTime datetime)
        {
            // в зависимости от стажа на данной должности рассчитаем yearsRatio:
            // 1 получить из info year_ratio; max_ratio
            // 2 получить количество отработанных полных лет НА ДАТУ!
            if ( info == null )
            {
                this.info = new Group_attribs(this.Group_id);
            }
            var year_base = info.Year_ratio;
            var max_ratio = info.Max_ratio;
            var years = GetWorkerYears(datetime);
            return Utils.CalcFromBaseToMax(years, year_base, max_ratio);
        }
        public int GetWorkerYears( DateTime datetime )
        {
            //  от переданной даты!!
            // посчитаем полное количество отработанных лет 
            DateTime start = Convert.ToDateTime(this.recruit_date);
            int result = datetime.Year - start.Year;
            if (start > datetime.AddYears(-result)) result--;
            return result;
        }

    }

    class WorkerWithoutInf : Worker
    {
        public WorkerWithoutInf() : base() { }
        public WorkerWithoutInf(string name, string recruit_date, string remove_date, int group_id, decimal base_salary)
       : base(name, recruit_date, remove_date, group_id, base_salary) { }
        public WorkerWithoutInf(Worker worker, Group_attribs info) : base(worker, info) { }
        public WorkerWithoutInf(int id, ApplicationContext db) : base(id, db) { }
        
    }

    class WorkerWithInf : Worker
    {
        public WorkerWithInf() : base() { }
        public WorkerWithInf(string name, string recruit_date, string remove_date, int group_id, decimal base_salary)
       : base(name, recruit_date, remove_date, group_id, base_salary) { }
        public WorkerWithInf( Worker worker, Group_attribs info) : base(worker, info) { }
        public WorkerWithInf(int id, ApplicationContext db) : base(id, db) { }

        public override decimal GetSalary(DateTime datetime, ApplicationContext db)
        {
            // для данного класса зп зависит от подчинённых ещё
            // 1 рассчитаем коэф за стаж на должности 
            // 2 рассчитаем надбавку за каждого подчинённого первого уровня
            // 3 Формула: базовая_ставка * коэф_за_стаж  + сумма_зп_подчинённых * коэф_за_подчин 
            decimal yearsRatio = GetYearsRatio(datetime); // [ year_ratio; max_ratio ]
            decimal inferBonus = GetInferBonus(datetime, db);
            return (this.base_salary + this.base_salary * yearsRatio) + inferBonus;
        }

        public decimal GetInferBonus(DateTime datetime, ApplicationContext db)
        {
            // 2 рассчитаем надбавку за каждого подчинённого первого уровня:
            //      получить лист подчинённых, для каждого получить размер зп (у каждого могут быть свои подчинённые..)    
            //      просуммировать все зп подчинённых 
            //      домножить сумму на коэф inf_ratio
            List<Inferrior> infers = GetInferList(datetime, db);

            if (infers.Count != 0 )
            {
                decimal sumInferSalary = 0;
                foreach (Inferrior inf in infers)
                {
                    Worker worker = Worker.GetRightObgById(inf.Inferrior_id, db);
                    sumInferSalary += worker.GetSalary(datetime, db);
                }
                return sumInferSalary * this.info.Inf_ratio;
            }
            else
            {
                return 0;
            }
        }

        public List<Inferrior> GetInferList(DateTime datetime, ApplicationContext db) // TODO с датой
        {
            return db.Inferriors
                .SqlQuery("Select * from Inferriors where head_id == @head_id",
                new SQLiteParameter("@head_id", this.id)).ToList<Inferrior>();
        }
    }

    class Group_attribs
    {
        public int id { get; set; }
        private string text;
        private decimal inf_ratio;
        private decimal year_ratio, max_ratio;
        private int inf_avail;
        private ApplicationContext dbInfo;

        public string Text
        {
            get { return text; }
            set { text = value; }
        }
        public decimal Year_ratio
        {
            get { return year_ratio; }
            set { year_ratio = value; }
        }
        public decimal Max_ratio
        {
            get { return max_ratio; }
            set { max_ratio = value; }
        }
        public decimal Inf_ratio
        {
            get { return inf_ratio; }
            set { inf_ratio = value; }
        }
        public int Inf_avail
        {
            get { return inf_avail; }
            set { inf_avail = value; }
        }

        public Group_attribs() { }

        public Group_attribs(string text, int inf_avail, decimal inf_ratio, decimal year_ratio, decimal max_ratio)
        {
            this.text = text;
            this.year_ratio = year_ratio;
            this.max_ratio = max_ratio;
            this.inf_ratio = inf_ratio;
            this.inf_avail = inf_avail;
        }
        public Group_attribs(int group_id) // мб стоит передать объект  ApplicationContext - ?
        {
            dbInfo = new ApplicationContext();    
            Group_attribs info = dbInfo.Group_attribs.Find(group_id);
            this.text = info.text;
            this.year_ratio = info.year_ratio;
            this.max_ratio = info.max_ratio;
            this.inf_ratio = info.inf_ratio;
            this.inf_avail = info.inf_avail;
        }

        public bool InfAvail()
        {
            return (this.Inf_avail == 1);
        }

    }

    class Inferrior
    {
        private int head_id { get; set; }

        private int inferrior_id { get; set; }
        private string add_date, remove_date;

        [Key]
        [Column(Order = 1)]
        public int Head_id
        {
            get { return head_id; }
            set { head_id = value; }
        }
        [Key]
        [Column(Order = 2)]
        public int Inferrior_id
        {
            get { return inferrior_id; }
            set { inferrior_id = value; }
        }
        public string Add_date
        {
            get { return add_date; }
            set { add_date = value; }
        }
        public string Remove_date
        {
            get { return remove_date; }
            set { remove_date = value; }
        }


        public Inferrior() { }

        public Inferrior(int head_id, int inferrior_id, string add_date, string remove_date)
        {
            this.head_id = head_id;
            this.inferrior_id = inferrior_id;
            this.add_date = add_date;
            this.remove_date = remove_date;
        }

    }

    // TODO
    // создать класс Test для модульного тестирования API данного проекта:

    // например, для метода CalcFromBaseToMax можно создать метод
    // public static void  calcFrom1To10_5( ) { } с логикой: 
    // если 5 лет, за год 1, максимально 10 - то результат будет 5
    //  с информированием , пройден тест, или нет - с логированием шагов. И далее :
    // если 9 лет, за год 1, максимально 10 - то результат будет 9
    // если 10 лет, за год 1, максимально 10 - то результат будет 10
    // если 25 лет, за год 1, максимально 10 - то результат будет 10
    //
    // для работы с Сотрудниками, например, для проверки, сохраняются ли в БД данные, метод
    // public static void addNewWorker( ) { } с логикой:
    // создать объект NAME1, с recruit_date = curDateTime = DateTime.UtcNow;
    // добавить в DbSet, сохранить изменения.
    // выполнить SqlSelect по NAME1 and curDateTime
    //
    // ETC...
}
