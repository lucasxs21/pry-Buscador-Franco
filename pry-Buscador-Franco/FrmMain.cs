using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pry_Buscador_Franco
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }
        public string Conexion;

        private void btnAbrir_Click(object sender, EventArgs e)
        {
            ClsBuscador BaseDeDatos = new ClsBuscador();
            Conexion = BaseDeDatos.ListarTablas(cmbTablas, dgv, lblBase);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ClsBuscador clsBaseDeDatos = new ClsBuscador();
            clsBaseDeDatos.MostrarTablas(cmbTablas, Conexion, dgv);
        }
    }
}
