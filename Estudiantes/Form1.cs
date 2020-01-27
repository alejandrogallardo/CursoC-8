using Logica;
using Logica.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Estudiantes
{
	public partial class Form1 : Form
	{
		private LEstudiantes estudiantes;
		// private Librarys librarys;
		public Form1()
		{
			InitializeComponent();
			// librarys = new Librarys();
			// Coleccion de objetos de la clase text box
			var listTextBox = new List<TextBox>();
			listTextBox.Add(textBoxDPI);
			listTextBox.Add(textBoxNombre);
			listTextBox.Add(textBoxApellido);
			listTextBox.Add(textBoxEmail);

			var listLabel = new List<Label>();
			listLabel.Add(labelDPI);
			listLabel.Add(labelNombre);
			listLabel.Add(labelApellido);
			listLabel.Add(labelEmail);
			listLabel.Add(labelGuardando);
			listLabel.Add(labelPaginas);

			Object[] objetos = { pictureBoxImage, Properties.Resources.boy, dataGridView, numericUpDown1, numericUpDown1 }; // Resetear con la imagen por defecto el formulario

			estudiantes = new LEstudiantes(listTextBox, listLabel, objetos);
		}

		private void pictureBoxImage_Click(object sender, EventArgs e)
		{
			estudiantes.uploadimage.CargarImagen(pictureBoxImage);
		}

		private void textBoxDPI_TextChanged(object sender, EventArgs e)
		{
			if ( textBoxDPI.Text.Equals("") )
			{
				labelDPI.ForeColor = Color.LightSlateGray;
			}
			else
			{
				labelDPI.ForeColor = Color.Green;
				labelDPI.Text = "DPI";
			}
		}

		private void textBoxDPI_KeyPress(object sender, KeyPressEventArgs e)
		{
			estudiantes.textBoxEvent.numberKeyPress(e);
		}

		private void textBoxNombre_TextChanged(object sender, EventArgs e)
		{
			if (textBoxNombre.Text.Equals(""))
			{
				labelNombre.ForeColor = Color.LightSlateGray;
			}
			else
			{
				labelNombre.ForeColor = Color.Green;
				labelNombre.Text = "Nombre";
			}
		}

		private void textBoxNombre_KeyPress(object sender, KeyPressEventArgs e)
		{
			estudiantes.textBoxEvent.textKeyPress(e);
		}

		private void textBoxApellido_TextChanged(object sender, EventArgs e)
		{
			if (textBoxApellido.Text.Equals(""))
			{
				labelApellido.ForeColor = Color.LightSlateGray;
			}
			else
			{
				labelApellido.ForeColor = Color.Green;
				labelApellido.Text = "Apellido";
			}
		}

		private void textBoxApellido_KeyPress(object sender, KeyPressEventArgs e)
		{
			estudiantes.textBoxEvent.textKeyPress(e);
		}

		private void textBoxEmail_TextChanged(object sender, EventArgs e)
		{
			if (textBoxEmail.Text.Equals(""))
			{
				labelEmail.ForeColor = Color.LightSlateGray;
			}
			else
			{
				labelEmail.ForeColor = Color.Green;
				labelEmail.Text = "Email";
			}
		}

		private void buttonAgregar_Click(object sender, EventArgs e)
		{
			estudiantes.Registrar();
		}

		private void textBoxBuscar_TextChanged(object sender, EventArgs e)
		{
			estudiantes.SearchEstudiante(textBoxBuscar.Text);
		}

		private void buttonPrimero_Click(object sender, EventArgs e)
		{
			estudiantes.Paginador("Primero");
		}

		private void buttonAnterior_Click(object sender, EventArgs e)
		{
			estudiantes.Paginador("Anterior");
		}

		private void buttonSiguiente_Click(object sender, EventArgs e)
		{
			estudiantes.Paginador("Siguiente");
		}

		private void buttonUltimo_Click(object sender, EventArgs e)
		{
			estudiantes.Paginador("Ultimo");
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			estudiantes.Registro_Paginas();
		}

		private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
		{
			if (dataGridView.Rows.Count != 0)
				estudiantes.GetEstudiante();

			
		}

		private void dataGridView_KeyUp(object sender, KeyEventArgs e)
		{
			if (dataGridView.Rows.Count != 0)
				estudiantes.GetEstudiante();
		}

		private void buttonBorrar_Click(object sender, EventArgs e)
		{
			estudiantes.Eliminar();
		}

		private void buttonCancelar_Click(object sender, EventArgs e)
		{
			estudiantes.Restablecer();
		}
	}
}
