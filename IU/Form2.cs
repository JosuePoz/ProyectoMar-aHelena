using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;

namespace IU
{
    public partial class Form2 : Form
    {
        int idSelected = 0;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            dataGridView1.DataSource = Logica.ListarClientes();
            dataGridView1.Refresh();
            activedFormEdicion();
        }

        private void Buscar_Click(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            this.dataGridView1.DataSource = Logica.BuscarCliente(textBox11.Text);
            this.dataGridView1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validateFormCrear())
            {
                LogicaConsultas Logica = new LogicaConsultas(); string resp;
                resp = Logica.GuardarCliente(textBox1.Text, textBox2.Text, textBox3.Text, textBox5.Text, textBox4.Text);
                if (resp.ToUpper().Contains("ÉXITO"))
                {
                    MessageBox.Show("Se ha grabado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Logica.ListarClientes();
                    dataGridView1.Refresh();
                    resetFormCrear();
                }
                else
                    MessageBox.Show(resp, "Error al grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("llenar todos los campos", "Error al grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void resetFormCrear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";

        }
        
        private bool validateFormCrear()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                return false;
            return true;
        }
       
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            if (index != -1)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[index];
                string valor = selectedRow.Cells[0].Value.ToString();
                if (valor != "")
                {
                    idSelected = Convert.ToInt32(selectedRow.Cells[0].Value.ToString());
                    textBox1.Text = selectedRow.Cells[1].Value.ToString();
                    textBox2.Text = selectedRow.Cells[3].Value.ToString();
                    textBox3.Text = selectedRow.Cells[5].Value.ToString();
                    textBox4.Text = selectedRow.Cells[4].Value.ToString();
                    textBox5.Text = selectedRow.Cells[2].Value.ToString();
                    if(!textBox4.Enabled)
                        activedFormEdicion();
                }
                else if (textBox4.Enabled)
                {
                    activedFormEdicion();
                    resetFormEdicion();
                    idSelected = 0;
                }
            }
            else if (textBox4.Enabled)
            {
                activedFormEdicion();
                resetFormEdicion();
                idSelected = 0;
            }
        }

        
        public void activedFormEdicion()
        {
           // textBox1.Enabled = !textBox1.Enabled;
            //textBox2.Enabled = !textBox2.Enabled;
           // textBox3.Enabled = !textBox3.Enabled;
           // textBox4.Enabled = !textBox4.Enabled;
            //textBox5.Enabled = !textBox5.Enabled;
            //button2.Enabled = !button2.Enabled;
            //button3.Enabled = !button3.Enabled;
        }
        
        public void resetFormEdicion()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas(); string resp;
            resp = Logica.EliminarCliente(idSelected);
            if (resp.ToUpper().Contains("ÉXITO"))
            {
                MessageBox.Show("Se ha eliminado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = Logica.ListarClientes();
                dataGridView1.Refresh();
                resetFormEdicion();
                activedFormEdicion();
                idSelected = 0;
            }
            else
                MessageBox.Show(resp, "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            if (validateFormEdicion())
            {
                LogicaConsultas Logica = new LogicaConsultas(); string resp;
                resp = Logica.EditarCliente(idSelected ,textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text);
                if (resp.ToUpper().Contains("ÉXITO"))
                {
                    MessageBox.Show("Se ha editado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Logica.ListarClientes();
                    dataGridView1.Refresh();
                    resetFormEdicion();
                    activedFormEdicion();
                    idSelected = 0;
                }
                else
                    MessageBox.Show(resp, "Error al editar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Se necesita llenar todos los campos", "Error al editar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool validateFormEdicion()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
                return false;
            return true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
