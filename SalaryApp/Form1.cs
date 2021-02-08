using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Entity;

namespace SalaryApp
{

   // HR hr;

    public partial class fHR : Form
    {
        // ApplicationContext db;
        HR hr;

        public fHR()
        {
            InitializeComponent();
            hr = new HR();
            dgvWorkers.DataSource = hr.getActWorkersList();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            WorkerF workerF = new WorkerF();
            DialogResult result = workerF.ShowDialog(this);

            if (result == DialogResult.Cancel)
                return;

            string name = workerF.textBox1.Text;
            string recruit_date = workerF.textBox2.Text;
            string remove_date = "9999.12.31 00:00:00";
            int v = Convert.ToInt32(workerF.textBox3.Text);
            int group_id = v;
            decimal base_salary = Convert.ToDecimal(workerF.textBox4.Text);

            hr.AddWorkerToDB(name, recruit_date, remove_date, group_id, base_salary);

        }
        private void btnAddInf_Click(object sender, EventArgs e)
        {
            if (dgvWorkers.CurrentRow != null && dgvWorkers.SelectedRows.Count == 2)
            {
                //var i = dgvWorkers.CurrentRow.Index; // <-- строка, ктр в фокусе - думаю, будет подчинённый
                DateTime d = DateTime.UtcNow;
                string curDateTime = d.ToString("yyyy.MM.dd hh:mm:ss");

                hr.AddInferToDB(
                    Convert.ToInt32(dgvWorkers.SelectedRows[1].Cells[0].Value),     // head_id
                    Convert.ToInt32(dgvWorkers.CurrentRow.Cells[0].Value),          // inferrior_id 
                    DateTime.UtcNow.ToString("yyyy.MM.dd hh:mm:ss"),                // add_date
                    "9999.12.31 00:00:00"                                           // remove_date
                    );
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvWorkers.CurrentRow != null)
            {
                WorkerF workerF = new WorkerF();
                workerF.label1.Text = "Дата увольнения:";
                workerF.textBox1.Text = Convert.ToString(dgvWorkers.CurrentRow.Cells[3].Value);
                Utils.InactiveElem(workerF);

                DialogResult result = workerF.ShowDialog(this);
                if (result == DialogResult.Cancel)
                    return;
                hr.ChangeWorker(Convert.ToInt32(dgvWorkers.CurrentRow.Cells[0].Value), workerF.textBox1.Text);

                dgvWorkers.Rows.RemoveAt(dgvWorkers.SelectedRows[0].Index);
            }
            else 
            {
                MessageBox.Show("Не выбран сотрудник",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }

        private void btnInfo_Click(object sender, EventArgs e)
        {
            GroupInfoForm infoF = new GroupInfoForm();
            infoF.dgvInfo.DataSource = hr.getInfoList();
            infoF.ShowDialog();
        }

        private void dgvWorkers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvWorkers.CurrentRow != null)
            {
                dgvInf.DataSource = hr.getInferList(Convert.ToInt32(dgvWorkers.CurrentRow.Cells[0].Value));
                lblSalaryResult.Text=  "";
            }
        }

        private void btnSalaryCalc_Click(object sender, EventArgs e)
        {
            if (dgvWorkers.CurrentRow != null)
            {
                string msg = " З/П текущего сотрудника: " 
                    + hr.GetSalary(Convert.ToInt32(dgvWorkers.CurrentRow.Cells[0].Value));
                lblSalaryResult.Text = msg;
            }
            else
            {
                MessageBox.Show("Не выбран сотрудник для расчёта его з/п", 
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }
    }
}
