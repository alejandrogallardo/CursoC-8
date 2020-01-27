using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logica.Library
{
	public class Paginador <T>
	{
		private List<T> _dataList;
		private Label _label;
		// Static para que mantengan sus registros
		private static int maxReg, _reg_por_pagina, pageCount, numPagina = 1;

		// Metodo contructor de la clase
		public Paginador(List<T> dataList, Label label, int reg_por_pagina)
		{
			// Inicializacion de objetos
			_dataList = dataList;
			_label = label;
			_reg_por_pagina = reg_por_pagina;

			cargarDatos();
		}

		private void cargarDatos()
		{
			numPagina = 1;
			maxReg = _dataList.Count;
			pageCount = ( maxReg / _reg_por_pagina );
			// Ajuste el numero de la pagina si la ultima pagina contiene una parte de la pagina
			if ( (maxReg % _reg_por_pagina) > 0 )
			{
				pageCount += 1;
			}
			_label.Text = $"Paginas 1 / { pageCount }";
		}
		public int primero()
		{
			numPagina = 1;
			_label.Text = $"Paginas { numPagina} / {pageCount }";
			return numPagina;
		}
		public int anterior()
		{
			if (numPagina > 1)
			{
				numPagina -= 1;
				_label.Text = $"Paginas { numPagina} / {pageCount }";
			}
			return numPagina;
		}
		public int siguiente()
		{

			if (numPagina == pageCount)
				numPagina -= 1;
			if (numPagina < pageCount)
			{
				numPagina += 1;
				_label.Text = $"Paginas { numPagina} / {pageCount }";
			}
			
			return numPagina;
		}
		public int ultimo()
		{
			numPagina = pageCount;
			_label.Text = $"Paginas { numPagina} / {pageCount }";
			return numPagina;
		}
	}
}
