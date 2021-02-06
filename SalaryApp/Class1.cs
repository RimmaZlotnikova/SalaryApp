using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalaryApp
{
    class Worker
    {
        // АБСТРАКТНЫЙ ??? класс для обработки 1 сотрудника:
        //  считать все его парам-ры из БД по переданному ИД
        //  создать объект подходящей группы - 
        //      workerWithInf / workerWithoutInf
        //          по полю могут быть подчинённые, или нет
        //          т.е. определяющим является необходимость расчёта коэф по подчинённм сотрудникам    
        //  расчёт зп: получить базу, рассчитать коэфиициент на основе базового с учётом стажа,
        //              рассчитать коэффициент по количеству подчинённых сотрудников с учётом группы
        //  

        public int id { get; set; }
        private string name;
        private string recruit_date, remove_date;
        private int group_id;
        private decimal base_salary;

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
            set 
            { 
                if (value == 0) { group_id = 1; }
                else { group_id = 2; }                
            }
        }
        
        public decimal Base_salary
        {
            get { return base_salary; }
            set { base_salary = value; }
        }


        public Worker() {}
        
        public Worker(string name , string recruit_date, string remove_date, int group_id, decimal base_salary )
        {
            this.name = name;
            this.recruit_date = recruit_date;
            this.remove_date = remove_date;
            this.group_id = group_id;
            this.base_salary = base_salary;
        }
        
    }

    class Group_attribs
    {
        public int id { get; set; }
        private string text;
        private decimal inf_ratio;
        private decimal year_ratio, max_ratio;
        private int inf_avail;

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

    }

    class Inferrior
    {
        //[Key]
        //[Column(Order=1)]
        private int head_id { get; set; }

        //[Key]
        //[Column(Order = 2)]
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
}
