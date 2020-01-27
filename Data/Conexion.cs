using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
	public class Conexion : DataConnection
	{
		public Conexion() : base("SQS") { }
		public ITable<dbt_estudiantes> _dbt_Estudiantes { get { return GetTable<dbt_estudiantes>(); } }
		
	}
}
