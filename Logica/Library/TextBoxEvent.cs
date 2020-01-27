using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // para poder usar la validacion de Email
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica.Library
{
	public class TextBoxEvent
	{
		public void textKeyPress(KeyPressEventArgs e)
		{
			// condicion que solo nos perminte ingresar datos de tipo texto
			if (char.IsLetter(e.KeyChar)) { e.Handled = false; }
			// condicion que no permite dar salto de linea cuando se oprime Enter
			else if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; }
			// condicion que nos permite utilizar la tecla backspace
			else if (char.IsControl(e.KeyChar)) { e.Handled = false; }
			// condicion que nos permite utilizar la techa de espacio
			else if (char.IsSeparator(e.KeyChar)) { e.Handled = false; }
			else { e.Handled = true; }
		}

		public void numberKeyPress(KeyPressEventArgs e)
		{
			// condicion que solo nos perminte ingresar datos de tipo numerico
			if (char.IsDigit(e.KeyChar)) { e.Handled = false; }
			// condicion que no permite dar salto de linea cuando se oprime Enter
			else if (e.KeyChar == Convert.ToChar(Keys.Enter)) { e.Handled = true; }
			// Condicion que no permite ingresar datos de tipo texto
			else if (char.IsLetter(e.KeyChar)) { e.Handled = true; }
			// condicion que nos permite utilizar la tecla backspace
			else if (char.IsControl(e.KeyChar)) { e.Handled = false; }
			// condicion que nos permite utilizar la techa de espacio
			else if (char.IsSeparator(e.KeyChar)) { e.Handled = false; }
			else { e.Handled = true; }
		}

		public bool comprobarFormatoEmail(string email)
		{
			return new EmailAddressAttribute().IsValid(email);
		}
	}
}
