namespace DataViewExample
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.SortTextBox = new System.Windows.Forms.TextBox();
            this.FilterTextBox = new System.Windows.Forms.TextBox();
            this.SetDataViewPropertiesButton = new System.Windows.Forms.Button();
            this.AddRowButton = new System.Windows.Forms.Button();
            this.nortwndDataSet1 = new DataViewExample.nortwndDataSet();
            this.customersTableAdapter1 = new DataViewExample.nortwndDataSetTableAdapters.CustomersTableAdapter();
            this.ordersTableAdapter1 = new DataViewExample.nortwndDataSetTableAdapters.OrdersTableAdapter();
            this.nortwndDataSet1BindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.GetOrdersButton = new System.Windows.Forms.Button();
            this.OrdersGrid = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nortwndDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nortwndDataSet1BindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrdersGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(378, 232);
            this.dataGridView1.TabIndex = 0;
            // 
            // SortTextBox
            // 
            this.SortTextBox.Location = new System.Drawing.Point(396, 251);
            this.SortTextBox.Name = "SortTextBox";
            this.SortTextBox.Size = new System.Drawing.Size(301, 20);
            this.SortTextBox.TabIndex = 1;
            this.SortTextBox.Text = "CustomerID";
            // 
            // FilterTextBox
            // 
            this.FilterTextBox.Location = new System.Drawing.Point(12, 250);
            this.FilterTextBox.Name = "FilterTextBox";
            this.FilterTextBox.Size = new System.Drawing.Size(378, 20);
            this.FilterTextBox.TabIndex = 2;
            this.FilterTextBox.Text = "City = \'London\'";
            // 
            // SetDataViewPropertiesButton
            // 
            this.SetDataViewPropertiesButton.Location = new System.Drawing.Point(12, 276);
            this.SetDataViewPropertiesButton.Name = "SetDataViewPropertiesButton";
            this.SetDataViewPropertiesButton.Size = new System.Drawing.Size(130, 23);
            this.SetDataViewPropertiesButton.TabIndex = 3;
            this.SetDataViewPropertiesButton.Text = "SetDataViewProperties";
            this.SetDataViewPropertiesButton.UseVisualStyleBackColor = true;
            this.SetDataViewPropertiesButton.Click += new System.EventHandler(this.SetDataViewPropertiesButton_Click);
            // 
            // AddRowButton
            // 
            this.AddRowButton.Location = new System.Drawing.Point(323, 276);
            this.AddRowButton.Name = "AddRowButton";
            this.AddRowButton.Size = new System.Drawing.Size(67, 23);
            this.AddRowButton.TabIndex = 4;
            this.AddRowButton.Text = "Add Row";
            this.AddRowButton.UseVisualStyleBackColor = true;
            this.AddRowButton.Click += new System.EventHandler(this.AddRowButton_Click);
            // 
            // nortwndDataSet1
            // 
            this.nortwndDataSet1.DataSetName = "nortwndDataSet";
            this.nortwndDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // customersTableAdapter1
            // 
            this.customersTableAdapter1.ClearBeforeFill = true;
            // 
            // ordersTableAdapter1
            // 
            this.ordersTableAdapter1.ClearBeforeFill = true;
            // 
            // nortwndDataSet1BindingSource
            // 
            this.nortwndDataSet1BindingSource.DataSource = this.nortwndDataSet1;
            this.nortwndDataSet1BindingSource.Position = 0;
            // 
            // GetOrdersButton
            // 
            this.GetOrdersButton.Location = new System.Drawing.Point(703, 251);
            this.GetOrdersButton.Name = "GetOrdersButton";
            this.GetOrdersButton.Size = new System.Drawing.Size(85, 20);
            this.GetOrdersButton.TabIndex = 5;
            this.GetOrdersButton.Text = "Get Orders";
            this.GetOrdersButton.UseVisualStyleBackColor = true;
            this.GetOrdersButton.Click += new System.EventHandler(this.GetOrdersButton_Click);
            // 
            // OrdersGrid
            // 
            this.OrdersGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.OrdersGrid.Location = new System.Drawing.Point(396, 12);
            this.OrdersGrid.Name = "OrdersGrid";
            this.OrdersGrid.Size = new System.Drawing.Size(392, 232);
            this.OrdersGrid.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.OrdersGrid);
            this.Controls.Add(this.GetOrdersButton);
            this.Controls.Add(this.AddRowButton);
            this.Controls.Add(this.SetDataViewPropertiesButton);
            this.Controls.Add(this.FilterTextBox);
            this.Controls.Add(this.SortTextBox);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nortwndDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nortwndDataSet1BindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OrdersGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox SortTextBox;
        private System.Windows.Forms.TextBox FilterTextBox;
        private System.Windows.Forms.Button SetDataViewPropertiesButton;
        private System.Windows.Forms.Button AddRowButton;
        private nortwndDataSet nortwndDataSet1;
        private nortwndDataSetTableAdapters.CustomersTableAdapter customersTableAdapter1;
        private nortwndDataSetTableAdapters.OrdersTableAdapter ordersTableAdapter1;
        private System.Windows.Forms.BindingSource nortwndDataSet1BindingSource;
        private System.Windows.Forms.Button GetOrdersButton;
        private System.Windows.Forms.DataGridView OrdersGrid;
    }
}

