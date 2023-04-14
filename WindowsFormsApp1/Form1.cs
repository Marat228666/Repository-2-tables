using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Web;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using WindowsFormsApp1.Repository.RepositoryPovider;
using WindowsFormsApp1.Repository.RepositoryProvider;
using WindowsFormsApp1.Repository.RepositoryProduct;
using WindowsFormsApp1.models;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        int UpdId;
        bool read;
        IProviderRepository providerRep;
        IProductRepository productRep;
        public Form1()
        {

            
            providerRep = new ProviderRepository("localhost", "SHOP", "root", "root");
            productRep = new ProductRepository("localhost", "SHOP", "root", "root");
            InitializeComponent();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            boxesandlabels();
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text)|| String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(textBox3.Text) ||  (tabControl1.SelectedIndex == 1 & String.IsNullOrEmpty(textBox4.Text)))
            {
               
                  button2.Enabled= false;
                  button3.Enabled= false;
            }
            else
            {
                button2.Enabled = true;
                button3.Enabled = true;

            }
        }
        private void boxesandlabels()
        {
            if (read)
            {
                switch (tabControl1.SelectedIndex)
                {
                    case 0:
                        {

                            label1.Text = "name";
                            label2.Text = "second name";
                            label3.Text = "number";
                            label1.Visible = true;
                            textBox1.Visible = true;
                            label2.Visible = true;
                            textBox2.Visible = true;
                            label3.Visible = true;
                            textBox3.Visible = true;

                            break;
                        }
                    case 1:
                        {
                            label1.Text = "name";
                            label2.Text = "price";
                            label3.Text = "weight";
                            label4.Text = "provider id";
                            label1.Visible = true;
                            textBox1.Visible = true;
                            label2.Visible = true;
                            textBox2.Visible = true;
                            label3.Visible = true;
                            textBox3.Visible = true;
                            label4.Visible = true;
                            textBox4.Visible = true;

                            break;
                        }
                }
            }

        }
            private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            switch(tabControl1.SelectedIndex)
            {
                case 0:
                    {
                        UpdId = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                        label5.Text = $"UpdId={UpdId}";
                        if (UpdId>0)
                        {
                            button3.Enabled = true;
                        }
                        textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                        textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                        textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                        break;
                    }
                case 1:
                    {
                        UpdId = (int)dataGridView2.SelectedRows[0].Cells[0].Value;
                        label5.Text = $"UpdId={UpdId}";
                        if (UpdId > 0)
                        {
                            button3.Enabled = true;
                        }
                        textBox1.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                        textBox2.Text = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                        textBox3.Text = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                        textBox4.Text = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();

                        break;
                    }
            }
            label5.Text = $"UpdId={UpdId}";
            
        }
        private void RefreshTable()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = providerRep.GetAll();
            dataGridView2.Columns.Clear();
            dataGridView2.DataSource = productRep.GetAll();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RefreshTable();
            read = true;
            boxesandlabels();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int rowsaffected = 0;
            switch(tabControl1.SelectedIndex)
            {
                case 0:
                    {
                        rowsaffected = providerRep.insert(new provider { name = textBox1.Text, second_name = textBox2.Text, number = int.Parse(textBox3.Text) }); 
                        break;
                    }
                case 1:
                    {
                        rowsaffected = productRep.insert(new product { name = textBox1.Text, price = int.Parse(textBox2.Text), weight = int.Parse(textBox3.Text), provider_id=int.Parse(textBox4.Text) });
                        break;
                    }
            }
            MessageBox.Show($"Rows affected: {rowsaffected}");
            RefreshTable();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int rowsaffected=0;
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    {
                        rowsaffected = providerRep.update(UpdId, new provider { name = textBox1.Text, second_name = textBox2.Text, number = int.Parse(textBox3.Text) });
                        break;
                    }
                case 1:
                    {
                        rowsaffected = productRep.update(UpdId, new product { name = textBox1.Text, price = int.Parse(textBox2.Text), weight = int.Parse(textBox3.Text), provider_id = int.Parse(textBox4.Text) });
                        break;
                    }
            }
            MessageBox.Show($"Rows affected: {rowsaffected}");
            RefreshTable();
            UpdId = 0;
            button3.Enabled = false;

        }
    }
}
