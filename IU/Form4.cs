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
    public partial class Form4 : Form
    {
        int id = 0;
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            this.dataGridView1.DataSource = Logica.ObtenerListaDeMunicipios();
            this.dataGridView1.Refresh();
            loadCombox(this.comboBox1);
            DesactivarEdicion();
        }
        private void DesactivarEdicion()
        {
            button3.Enabled = false;
            button4.Enabled = false;
        }
        private void ActivarEdicion()
        {
            button3.Enabled = true;
            button4.Enabled = true;
        }
        private void loadCombox(ComboBox cmb)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            cmb.DataSource = Logica.ListarDepartamentos();
            cmb.DisplayMember = "nombre_departamento";
            cmb.ValueMember = "DepartamentoId";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            string NombreMunicipio = null;
            if (textBox1.Text != "")
                NombreMunicipio = textBox1.Text;
            this.dataGridView1.DataSource = Logica.BuscarMunicipio(NombreMunicipio, comboBox1.Text);
            this.dataGridView1.Refresh();
            DesactivarEdicion();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                LogicaConsultas Logica = new LogicaConsultas(); string resp;
                resp = Logica.GuardarNuevoMunicipio(textBox1.Text, ((int)comboBox1.SelectedValue).ToString());
                if (resp.ToUpper().Contains("ÉXITO"))
                {
                    MessageBox.Show("Se ha grabado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Logica.ObtenerListaDeMunicipios();
                    dataGridView1.Refresh();
                    textBox1.Text = "";
                    DesactivarEdicion();
                }
                else
                    MessageBox.Show(resp, "Error al grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Se necesita llenar todos los campos", "Error al grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    id = Convert.ToInt32(selectedRow.Cells[0].Value.ToString());
                    textBox1.Text = selectedRow.Cells[1].Value.ToString();
                    comboBox1.SelectedIndex = comboBox1.FindStringExact(selectedRow.Cells[2].Value.ToString());
                    ActivarEdicion();
                }
                else if (button3.Enabled)
                {
                    DesactivarEdicion();
                    textBox1.Text = "";
                    id = 0;
                }
            }
            else if (button3.Enabled)
            {
                DesactivarEdicion();
                textBox1.Text = "";
                id = 0;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                LogicaConsultas Logica = new LogicaConsultas(); string resp;
                resp = Logica.ModificarRegistroMunicipio(id, textBox1.Text, ((int)comboBox1.SelectedValue));
                if (resp.ToUpper().Contains("ÉXITO"))
                {
                    MessageBox.Show("Se ha grabado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Logica.ObtenerListaDeMunicipios();
                    dataGridView1.Refresh();
                    textBox1.Text = "";
                    DesactivarEdicion();
                    id = 0;
                }
                else
                    MessageBox.Show(resp, "Error al grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("Se necesita llenar todos los campos", "Error al grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas(); string resp;
            resp = Logica.EliminarRegistroMunicipio(id);
            if (resp.ToUpper().Contains("ÉXITO"))
            {
                MessageBox.Show("Se ha eliminado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                dataGridView1.DataSource = Logica.ObtenerListaDeMunicipios();
                dataGridView1.Refresh();
                textBox1.Text = "";
                DesactivarEdicion();
                id = 0;
            }
            else
                MessageBox.Show(resp, "Error al eliminar", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
