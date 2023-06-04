using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pry_Buscador_Franco
{
    internal class ClsBuscador
    {
        public OleDbConnection BDConexion = new OleDbConnection();
        public OleDbDataReader BDLector;
        public OleDbCommand BDComando = new OleDbCommand();

        public string ListarTablas(ComboBox cmbTablas, DataGridView dgv, Label lblBase)
        {
            dgv.DataSource = null;
            cmbTablas.SelectedIndex = -1;
            string Conexion = "";
            using (OpenFileDialog OpenFileDialog = new OpenFileDialog())
            {
                if (OpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string archivo = OpenFileDialog.FileName;
                    lblBase.Text = Path.GetFileName(archivo);

                    if (Path.GetExtension(archivo) == ".accdb")
                    {
                        Conexion = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + archivo + ";Persist Security Info=False;";
                    }
                    else
                    {
                        Conexion = "Provider = Microsoft.Jet.OLEDB.4.0; Data Source =" + archivo + ";";
                    }

                    BDConexion.ConnectionString = Conexion;

                    cmbTablas.Items.Clear();

                    try
                    {
                        BDConexion.Open();

                        DataTable tablas = BDConexion.GetSchema("Tables");

                        foreach (DataRow tabla in tablas.Rows)
                        {
                            if (tabla[3].ToString() == "TABLE")
                            {
                                cmbTablas.Items.Add(tabla[2].ToString());
                            }
                        }
                        BDConexion.Close();
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.Message);
                    }
                }
            }
            return Conexion;
        }

        public void MostrarTablas(ComboBox cmbTablas, string Conexion, DataGridView dgv)
        {
            if (cmbTablas.SelectedIndex != -1)
            {
                BDConexion.ConnectionString = Conexion;

                try
                {
                    BDComando.Connection = BDConexion;
                    BDComando.CommandText = cmbTablas.Text;
                    BDComando.CommandType = CommandType.TableDirect;
                    BDComando.Connection.Open();

                    BDLector = BDComando.ExecuteReader();

                    DataTable tabla = new DataTable();
                    tabla.Load(BDLector);

                    dgv.DataSource = tabla;

                    BDComando.Connection.Close();
                }
                catch (Exception error)
                {
                    MessageBox.Show(error.Message);
                }
            }
        }
    }
}
