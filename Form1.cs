using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ejercicio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: esta línea de código carga datos en la tabla 'dataSetAutos.Autos' Puede moverla o quitarla según sea necesario.
            this.autosTableAdapter.Fill(this.dataSetAutos.Autos);

        }

        public void Limpiar() {
            idAutoBox.Text = String.Empty;
            marcaBox.Text = String.Empty;

            marcaBox.Focus();
        }

        public List<DataSetAutos.AutosRow> Filtro(int id) { 

            return this.autosTableAdapter.GetData().Where(auto => auto.id == id).ToList();

        }

        private void btn_agregar_Click(object sender, EventArgs e)
        {
            //Insertar en variables los datos del formulario
            var autos = this.autosTableAdapter;
            string marcaForm = marcaBox.Text;
            DateTime creacion = DateTime.Now;

            marcaForm = marcaForm.ToLower();

            //Hacer filtro
            var filtro = autos.GetData().Where(auto => auto.marca == marcaForm).ToList();
            //{Suzuki onichan}
            if (filtro.Count > 0)
            {
                MessageBox.Show("Auto ya registrado, Ingrese uno nuevo");
                Limpiar();

            }
            else
            {
                //Hacemos la consulta al DataSet 
                
                autos.Insert(marcaForm, creacion);

                //Actualizamos la tabla una vez ejecutada la consulta
                autos.Fill(this.dataSetAutos.Autos);
                Limpiar();
            }
        }

        private void btn_limpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            var autos = this.autosTableAdapter;
            var idForm = int.Parse(idAutoBox.Text);

            
            var filtro = Filtro(idForm);

            if (filtro.Count > 0)
            {
                var idAuto = filtro[0].id;
                var marca = filtro[0].marca;
                var fecha = filtro[0].fecha_creacion;

                autos.Delete(idAuto, marca, fecha);

                autos.Fill(this.dataSetAutos.Autos);
                Limpiar();
            }
            else {

                MessageBox.Show("El auto no existe");
                Limpiar();
            }
        }

        private void btn_actualizar_Click(object sender, EventArgs e)
        {
            var autos = this.autosTableAdapter;
            var idForm = int.Parse(idAutoBox.Text);
            var marcaForm = marcaBox.Text;
            var fechaForm = DateTime.Now;

            var filtro = Filtro(idForm);

            var idOriginal = filtro[0].id;
            var marcaOriginal = filtro[0].marca;
            var fechaOriginal = filtro[0].fecha_creacion;
            autos.Update(marcaForm, fechaForm, idOriginal, marcaOriginal, fechaOriginal);

            autos.Fill(this.dataSetAutos.Autos);
            Limpiar();
        }
    }
}
