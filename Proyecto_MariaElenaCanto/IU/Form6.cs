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
    public partial class Form6 : Form
    {
        int id = 0;
        public Form6()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            this.dataGridView1.DataSource = Logica.BuscarEnvio(comboBox1.Text, comboBox2.Text, monthCalendar1.SelectionStart.ToString("yyyy-MM-dd"));
            this.dataGridView1.Refresh();
            DesactivarEdicion();
        }
        private void loadComboxMuni(ComboBox cmb)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            cmb.DataSource = Logica.ListarClientes();
            cmb.DisplayMember = "nombre_completo";
            cmb.ValueMember = "ClienteId";
        }

        private void loadComboxPaquete(ComboBox cmb)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            cmb.DataSource = Logica.ListarPaquete();
            cmb.DisplayMember = "nombre_destinatario";
            cmb.ValueMember = "PaqueteId";
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            this.dataGridView1.DataSource = Logica.ListarEnvios();
            this.dataGridView1.Refresh();
            loadComboxMuni(this.comboBox1);
            loadComboxPaquete(this.comboBox2);
            DesactivarEdicion();
            textBox1.Select();
            textBox1.Focus();
        }
        private void DesactivarEdicion()
        {
            button3.Enabled = false;
        }

        private void ActivarEdicion()
        {
            button3.Enabled = true;
        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text != "")
            {
                LogicaConsultas Logica = new LogicaConsultas(); string resp;
                resp = Logica.GuardarEnvio(((int)comboBox1.SelectedValue).ToString(), ((int)comboBox2.SelectedValue).ToString(),monthCalendar1.SelectionStart.ToString("yyyy-MM-dd"), textBox1.Text, (Convert.ToInt32(checkBox1.Checked)).ToString());
                if (resp.ToUpper().Contains("ÉXITO"))
                {
                    MessageBox.Show("Se ha grabado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Logica.ListarEnvios();
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                LogicaConsultas Logica = new LogicaConsultas(); string resp;
                resp = Logica.ModificarRegistroEnvio(id, ((int)comboBox1.SelectedValue).ToString(), ((int)comboBox2.SelectedValue).ToString(), monthCalendar1.SelectionStart.ToString("yyyy-MM-dd"), textBox1.Text, (Convert.ToInt32(checkBox1.Checked)).ToString());
                if (resp.ToUpper().Contains("ÉXITO"))
                {
                    MessageBox.Show("Se ha grabado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Logica.ListarEnvios();
                    dataGridView1.Refresh();
                    textBox1.Text = "";
                    checkBox1.Checked = false;
                    DesactivarEdicion();
                    id = 0;
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
                    comboBox1.SelectedIndex = comboBox1.FindStringExact(selectedRow.Cells[1].Value.ToString());
                    comboBox2.SelectedIndex = comboBox2.FindStringExact(selectedRow.Cells[2].Value.ToString());
                    monthCalendar1.SelectionStart = (DateTime)selectedRow.Cells[3].Value;
                    textBox1.Text = selectedRow.Cells[4].Value.ToString().Replace(',', '.');
                    checkBox1.Checked = (Boolean)selectedRow.Cells[5].Value;
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
    }
}
