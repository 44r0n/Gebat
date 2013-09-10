//
//  ISql.cs
//
//  Author:
//       Aarón Sánchez Navarro <aaron.sn.1988@gmail.com>
//
//  Copyright (c) 2013 GNU
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;

namespace SqlManager
{
	public interface ISql
	{
		/// <summary>
		/// Devuelve una coexión a la base de datos, dependiendo del proveedor y la cadena de conexión.
		/// </summary>
		/// <param name="sqlConnectionString">Cadena de conexión.</param>
		DbConnection Connection (string sqlConnectionString);

		/// <summary>
		/// Devuelve un objeto de tipo command con la sqlQuery a ejecutar y la conexión a la base de datos.
		/// </summary>
		/// <param name="sqlQuery">Sqlquery a ejecutar.</param>
		/// <param name="connection">Conexión en la que se ejecutará el comando.</param>
		DbCommand Command (string sqlQuery, DbConnection connection);

		/// <summary>
		/// Devuelve un DbAdapter con la sqlQuery pasada por parámetro a la conexión indicada.
		/// </summary>
		/// <param name="sqlQuery">sqlQuery a ejecutar.</param>
		/// <param name="connection">Connexión a la base de datos.</param>
		DbDataAdapter Adapter(string sqlQuery, DbConnection connection);

		/// <summary>
		/// Devuelve un DbCommandBuilder con el adaptador pasado por parámetro.
		/// </summary>
		/// <param name="adapter">DbDataAdapter para construir el CommandBuilder.</param>
		DbCommandBuilder Builder (DbDataAdapter adapter);
	}
}


