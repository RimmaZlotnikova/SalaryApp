using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalaryApp
{
    public partial class WorkerF : Form
    {
        // TODO Здесь необходимо добавить Combobox, чтобы пользователь выбирал Группу, а не вводил её
        public WorkerF()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            
        }

    }
}
