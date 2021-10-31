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
    public partial class Form5 : Form
    {
        int id = 0;

        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            this.dataGridView1.DataSource = Logica.ListarPaquete();
            this.dataGridView1.Refresh();
            loadCombox(this.comboBox1);
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

        private void loadCombox(ComboBox cmb)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            cmb.DataSource = Logica.ObtenerListaDeMunicipios();
            cmb.DisplayMember = "nombre_municipio";
            cmb.ValueMember = "MunicipioId";
        }

        private void resetForm()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            LogicaConsultas Logica = new LogicaConsultas();
            string NombreDestinatario = null;
            string DirrecionDestino = null; 
            if (textBox2.Text != "")
                NombreDestinatario = textBox2.Text;
            if (textBox3.Text != "")
                DirrecionDestino = textBox3.Text;
            this.dataGridView1.DataSource = Logica.BuscarPaquete(NombreDestinatario,DirrecionDestino,  comboBox1.Text);
            this.dataGridView1.Refresh();
            DesactivarEdicion();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
                {
                    LogicaConsultas Logica = new LogicaConsultas(); string resp;
                    resp = Logica.GuardarPaquete(((int)comboBox1.SelectedValue).ToString(),  textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
                    if (resp.ToUpper().Contains("ÉXITO"))
                    {
                        MessageBox.Show("Se ha grabado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridView1.DataSource = Logica.ListarPaquete();
                        dataGridView1.Refresh();
                        resetForm();
                        DesactivarEdicion();
                    }
                    else
                        MessageBox.Show(resp, "Error al grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                    MessageBox.Show("Se necesita llenar todos los campos", "Error al grabar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                LogicaConsultas Logica = new LogicaConsultas(); string resp;
                resp = Logica.ModificarRegistroPaquete(id, ((int)comboBox1.SelectedValue).ToString(), textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text);
                if (resp.ToUpper().Contains("ÉXITO"))
                {
                    MessageBox.Show("Se ha grabado con éxito", resp, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dataGridView1.DataSource = Logica.ListarPaquete();
                    dataGridView1.Refresh();
                    resetForm();
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
                    textBox1.Text = selectedRow.Cells[2].Value.ToString();
                    textBox2.Text = selectedRow.Cells[4].Value.ToString();
                    textBox3.Text = selectedRow.Cells[5].Value.ToString();
                    textBox4.Text = selectedRow.Cells[3].Value.ToString().Replace(',', '.');

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
