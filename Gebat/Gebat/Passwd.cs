//
//  Passwd.cs
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
using GebatCAD.Classes;
using Gtk;

namespace Gebat
{
	public partial class Passwd : Gtk.Window
	{
		public Passwd () : 
				base(Gtk.WindowType.Toplevel)
		{
			this.Build ();

		}

		protected void connect (object sender, EventArgs e)
		{
			ACAD.Password = entry6.Text;
			if (ACAD.AttemptConnection())
			{
				button3.Sensitive = true;
				label8.Text = "Conexión establecida.";
			} 
			else
			{
				button3.Sensitive = false;
				label8.Text = "No se pudo establecer la conexión.";
			}
		}

		protected void exit (object sender, EventArgs e)
		{
			Application.Quit ();
		}

		protected void accept (object sender, EventArgs e)
		{
			MainWindow win = new MainWindow ();
			win.Show ();
			this.Destroy ();
		}
	}
}

