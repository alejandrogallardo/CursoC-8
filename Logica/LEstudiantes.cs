using Data;
using LinqToDB;
using Logica.Library;
using System;
using System.Collections.Generic;
using System.Drawing; // se requiere para poder user Color
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica
{
	public class LEstudiantes : Librarys
	{
		// dbt_estudiantes es la clase del modelo que contiene la infirmacion de la tabla estudiantes en DB
		private List<TextBox> listTextBox;
		private List<Label> listLabel;
		private PictureBox image;
		private Bitmap _imagBitmap;
		private DataGridView _dataGridView;
		private NumericUpDown _numericUpDown;
		private Paginador<dbt_estudiantes> _paginador;
		private string _accion = "insert";
		// private Librarys librarys;

		public LEstudiantes(List<TextBox> listTextBox, List<Label> listLabel, object[] objetos)
		{
			// Inicializacion de objetos
			this.listTextBox = listTextBox;
			this.listLabel = listLabel;
			// librarys = new Librarys();
			image = (PictureBox)objetos[0];
			_imagBitmap = (Bitmap)objetos[1];
			_dataGridView = (DataGridView)objetos[2];
			_numericUpDown = (NumericUpDown)objetos[3];
			Restablecer();
		}

		public void Registrar()
		{
			if (listTextBox[0].Text.Equals(""))
			{
				listLabel[0].Text = "El campo DPI es requerido";
				listLabel[0].ForeColor = Color.Red;
				listTextBox[0].Focus();
			}
			else
			{
				if (listTextBox[1].Text.Equals(""))
				{
					listLabel[1].Text = "El campo Nombre es requerido";
					listLabel[1].ForeColor = Color.Red;
					listTextBox[1].Focus();
				}
				else
				{
					if (listTextBox[2].Text.Equals(""))
					{
						listLabel[2].Text = "El campo Apellido es requerido";
						listLabel[2].ForeColor = Color.Red;
						listTextBox[2].Focus();
					}
					else
					{
						if (listTextBox[3].Text.Equals(""))
						{
							listLabel[3].Text = "El campo Email es requerido";
							listLabel[3].ForeColor = Color.Red;
							listTextBox[3].Focus();
						}
						else
						{
							if (textBoxEvent.comprobarFormatoEmail(listTextBox[3].Text))
							{
								var user = _dbt_Estudiantes.Where(u => u.email.Equals(listTextBox[3].Text)).ToList();
								// _dbt_Estudiantes con este objeto se obtiene toda la informacion de la tabla estudiante como coleccion de objetos
								if (user.Count.Equals(0))
								{
									Save();
								}
								else
								{
									if (user[0].id.Equals(_idEstudiante)) // actualizar registros
									{
										Save();
									}
									else
									{
										listLabel[3].Text = "Este Email ya esta registrado";
										listLabel[3].ForeColor = Color.Red;
										listTextBox[3].Focus();
									}
									
								}
							}
							else
							{
								listLabel[3].Text = "Email no valido";
								listLabel[3].ForeColor = Color.Red;
								listTextBox[3].Focus();
							}
						}
					}
				}
			}
		}
		private void Save()
		{
			BeginTransactionAsync();
			try
			{
				var imageArray = uploadimage.ImageToByte(image.Image);

				//var db = new Conexion();
				//db.Insert(new dbt_estudiantes()
				//{
				//	dpi = listTextBox[0].Text,
				//	nombre = listTextBox[1].Text,
				//	apellido = listTextBox[2].Text,
				//	email = listTextBox[3].Text,
				//});

				switch (_accion)
				{
					case "insert":
						_dbt_Estudiantes.Value(e => e.dpi, listTextBox[0].Text)
							.Value(e => e.nombre, listTextBox[1].Text)
							.Value(e => e.apellido, listTextBox[2].Text)
							.Value(e => e.email, listTextBox[3].Text)
							.Value(e => e.image, imageArray)
							.Insert();
						break;
					case "update":
						_dbt_Estudiantes.Where(u => u.id.Equals( _idEstudiante ) )
							.Set(e => e.dpi, listTextBox[0].Text)
							.Set(e => e.nombre, listTextBox[1].Text)
							.Set(e => e.apellido, listTextBox[2].Text)
							.Set(e => e.email, listTextBox[3].Text)
							.Set(e => e.image, imageArray)
							.Update();
						break;
				}

				

				CommitTransaction(); // Indica que la transaccion se ha efectuado correctamente
				Restablecer(); // Aqui se debe reestablecer
			}
			catch (Exception)
			{
				RollbackTransaction();
				//throw;
			}
			//finally
			//{
			//	Restablecer(); // Esto solo fue una prueba
			//}
		}
		private int _reg_por_pagina = 2, _num_pagina = 1;
		public void SearchEstudiante(string campo)
		{
			List<dbt_estudiantes> query = new List<dbt_estudiantes>();
			int inicio = (_num_pagina - 1) * _reg_por_pagina;
			if ( campo.Equals("") )
			{
				query = _dbt_Estudiantes.ToList();
			}
			else
			{
				query = _dbt_Estudiantes.Where( c => c.dpi.StartsWith(campo) || c.nombre.StartsWith(campo) || c.apellido.StartsWith(campo) ).ToList();
			}
			if (0 < query.Count)
			{
				_dataGridView.DataSource = query.Select( c => new { 
					c.id,
					c.dpi,
					c.nombre,
					c.apellido,
					c.email,
					c.image
				}).Skip(inicio).Take(_reg_por_pagina).ToList();
				_dataGridView.Columns[0].Visible = false;
				_dataGridView.Columns[5].Visible = false;

				_dataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
				_dataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
			}
			else
			{
				_dataGridView.DataSource = query.Select(c => new {
					c.dpi,
					c.nombre,
					c.apellido,
					c.email
				}).ToList();
			}
		}

		private int _idEstudiante = 0;
		public void GetEstudiante()
		{
			_accion = "update";
			_idEstudiante = Convert.ToInt16(_dataGridView.CurrentRow.Cells[0].Value);
			listTextBox[0].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[1].Value);
			listTextBox[1].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[2].Value);
			listTextBox[2].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[3].Value);
			listTextBox[3].Text = Convert.ToString(_dataGridView.CurrentRow.Cells[4].Value);

			try
			{
				byte[] arrayImage = (byte[])_dataGridView.CurrentRow.Cells[5].Value;
				image.Image = uploadimage.byteArrayToImage(arrayImage);
			}
			catch (Exception)
			{
				image.Image = _imagBitmap;
			}
		}

		private List<dbt_estudiantes> listEstudiante; // Coleccin de objetos de la clase estudiante
		public void Paginador(string metodo)
		{
			switch (metodo)
			{
				case "Primero":
					_num_pagina = _paginador.primero();
					break;
				case "Anterior":
					_num_pagina = _paginador.anterior();
					break;
				case "Siguiente":
					_num_pagina = _paginador.siguiente();
					break;
				case "Ultimo":
					_num_pagina = _paginador.ultimo();
					break;

			}
			SearchEstudiante("");
		}
		public void Registro_Paginas()
		{
			_num_pagina = 1;
			_reg_por_pagina = (int)_numericUpDown.Value;
			var list = _dbt_Estudiantes.ToList();
			if ( 0 < list.Count )
			{
				_paginador = new Paginador<dbt_estudiantes>(listEstudiante, listLabel[5], _reg_por_pagina);
				SearchEstudiante("");
			}
		}
		public void Eliminar()
		{
			if (_idEstudiante.Equals(0))
			{
				MessageBox.Show("Seleccione un estudiante");
			}
			else
			{
				if ( MessageBox.Show("Esta seguro de eliminar el registro?", "Eliminar registro", MessageBoxButtons.YesNo) == DialogResult.Yes )
				{
					_dbt_Estudiantes.Where(c => c.id.Equals(_idEstudiante)).Delete();
					Restablecer();
				}
			}
		}
		public void Restablecer()
		{
			_accion = "insert";
			_num_pagina = 1;
			_idEstudiante = 0;
			image.Image = _imagBitmap;
			listLabel[0].Text = "DPI";
			listLabel[1].Text = "Nombre";
			listLabel[2].Text = "Apellido";
			listLabel[3].Text = "Email";

			listLabel[0].ForeColor = Color.LightBlue;
			listLabel[1].ForeColor = Color.LightBlue;
			listLabel[2].ForeColor = Color.LightBlue;
			listLabel[3].ForeColor = Color.LightBlue;

			listTextBox[0].Text = "";
			listTextBox[1].Text = "";
			listTextBox[2].Text = "";
			listTextBox[3].Text = "";
			listEstudiante = _dbt_Estudiantes.ToList();
			if ( 0 < listEstudiante.Count )
			{
				_paginador = new Paginador<dbt_estudiantes>( listEstudiante, listLabel[5], _reg_por_pagina );
			}
			SearchEstudiante("");
		}
	}
}
