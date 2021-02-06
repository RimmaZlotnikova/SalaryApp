
namespace SalaryApp
{
    partial class fHR
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnAdd = new System.Windows.Forms.Button();
            this.dgvWorkers = new System.Windows.Forms.DataGridView();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSalaryCalc = new System.Windows.Forms.Button();
            this.btnInfo = new System.Windows.Forms.Button();
            this.dgvInf = new System.Windows.Forms.DataGridView();
            this.lblWorkers = new System.Windows.Forms.Label();
            this.lblInf = new System.Windows.Forms.Label();
            this.btnAddInf = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInf)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(727, 45);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(175, 37);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Добавить сотрудника";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // dgvWorkers
            // 
            this.dgvWorkers.AllowUserToAddRows = false;
            this.dgvWorkers.AllowUserToDeleteRows = false;
            this.dgvWorkers.AllowUserToOrderColumns = true;
            this.dgvWorkers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWorkers.Location = new System.Drawing.Point(30, 34);
            this.dgvWorkers.Name = "dgvWorkers";
            this.dgvWorkers.ReadOnly = true;
            this.dgvWorkers.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWorkers.Size = new System.Drawing.Size(639, 355);
            this.dgvWorkers.TabIndex = 9;
            this.dgvWorkers.SelectionChanged += new System.EventHandler(this.dgvWorkers_SelectionChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(727, 110);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(175, 37);
            this.btnDelete.TabIndex = 10;
            this.btnDelete.Text = "Увольнение сотрудника";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSalaryCalc
            // 
            this.btnSalaryCalc.Location = new System.Drawing.Point(727, 174);
            this.btnSalaryCalc.Name = "btnSalaryCalc";
            this.btnSalaryCalc.Size = new System.Drawing.Size(175, 37);
            this.btnSalaryCalc.TabIndex = 11;
            this.btnSalaryCalc.Text = "Расчёт з/п сотрудника";
            this.btnSalaryCalc.UseVisualStyleBackColor = true;
            // 
            // btnInfo
            // 
            this.btnInfo.Location = new System.Drawing.Point(727, 236);
            this.btnInfo.Name = "btnInfo";
            this.btnInfo.Size = new System.Drawing.Size(175, 37);
            this.btnInfo.TabIndex = 12;
            this.btnInfo.Text = "Справочная информация";
            this.btnInfo.UseVisualStyleBackColor = true;
            this.btnInfo.Click += new System.EventHandler(this.btnInfo_Click);
            // 
            // dgvInf
            // 
            this.dgvInf.AllowUserToDeleteRows = false;
            this.dgvInf.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInf.Location = new System.Drawing.Point(30, 424);
            this.dgvInf.Name = "dgvInf";
            this.dgvInf.ReadOnly = true;
            this.dgvInf.Size = new System.Drawing.Size(639, 154);
            this.dgvInf.TabIndex = 14;
            // 
            // lblWorkers
            // 
            this.lblWorkers.AutoSize = true;
            this.lblWorkers.Location = new System.Drawing.Point(31, 16);
            this.lblWorkers.Name = "lblWorkers";
            this.lblWorkers.Size = new System.Drawing.Size(87, 13);
            this.lblWorkers.TabIndex = 15;
            this.lblWorkers.Text = "Все сотрудники";
            // 
            // lblInf
            // 
            this.lblInf.AutoSize = true;
            this.lblInf.Location = new System.Drawing.Point(31, 404);
            this.lblInf.Name = "lblInf";
            this.lblInf.Size = new System.Drawing.Size(215, 13);
            this.lblInf.TabIndex = 16;
            this.lblInf.Text = "Подчинённые сотрудники (для текущего)";
            // 
            // btnAddInf
            // 
            this.btnAddInf.Location = new System.Drawing.Point(727, 297);
            this.btnAddInf.Name = "btnAddInf";
            this.btnAddInf.Size = new System.Drawing.Size(175, 39);
            this.btnAddInf.TabIndex = 17;
            this.btnAddInf.Text = "Добавить подчинённого";
            this.btnAddInf.UseVisualStyleBackColor = true;
            this.btnAddInf.Click += new System.EventHandler(this.btnAddInf_Click);
            // 
            // fHR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 591);
            this.Controls.Add(this.btnAddInf);
            this.Controls.Add(this.lblInf);
            this.Controls.Add(this.lblWorkers);
            this.Controls.Add(this.dgvInf);
            this.Controls.Add(this.btnInfo);
            this.Controls.Add(this.btnSalaryCalc);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dgvWorkers);
            this.Controls.Add(this.btnAdd);
            this.Name = "fHR";
            this.Text = "Список сотрудников";
            ((System.ComponentModel.ISupportInitialize)(this.dgvWorkers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInf)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dgvWorkers;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSalaryCalc;
        private System.Windows.Forms.Button btnInfo;
        private System.Windows.Forms.DataGridView dgvInf;
        private System.Windows.Forms.Label lblWorkers;
        private System.Windows.Forms.Label lblInf;
        private System.Windows.Forms.Button btnAddInf;
    }
}

