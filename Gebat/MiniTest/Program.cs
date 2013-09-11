//
//  Program.cs
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
using GebatEN.Classes;
using GebatCAD.Classes;

namespace MiniTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Escribe la contraseña de la base de datos.");
			ACAD.Password = Console.ReadLine ();
			/*ENFood food = new ENFood ("Patates", 2);
			Console.WriteLine ("Antes de guardar " + food.Id [0]);
			food.Save ();
			Console.WriteLine ("Despues de guardar " + food.Id [0]);*/
			ACAD food = new CADFood ();
			Console.WriteLine ("Hay " + food.Count ());

		}
	}
}
