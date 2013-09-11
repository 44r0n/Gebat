//
//  ENFoodTest.cs
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
using NUnit.Framework;
using GebatEN.Classes;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Configuration;
using SqlManager;
using GebatCAD.Classes;

namespace Test
{
	[TestFixture]
	public class TestENFood
	{
		private string scriptfilename = "PreparacionTest.sql";

		private void InitBD()
		{
			string connString = ConfigurationManager.ConnectionStrings["GebatDataConnectionString"].ConnectionString;
			connString += "Password=root";
			string provider = ConfigurationManager.ConnectionStrings ["GebatDataConnectionString"].ProviderName;
			ISql manager = FactorySql.Create(provider);
			FileInfo file = new FileInfo(scriptfilename);
			StreamReader lector = file.OpenText ();
			string script = lector.ReadToEnd();
			lector.Close ();
			DbConnection conn = manager.Connection (connString);
			conn.Open();
			DbCommand comando = manager.Command(script, conn);
			comando.ExecuteNonQuery();
			conn.Close();
		}

		[SetUp]
		public void InnitTest()
		{
			ACAD.Password = "root";
			this.InitBD();
		}

		[Test]
		public void Save()
		{
			AEN food = new ENFood("Peres");
			food.Save();
		}

		[Test]
		[ExpectedException(typeof(NullReferenceException))]
		public void SaveNullName()
		{
			AEN food = new ENFood(null);
			food.Save();
		}

		[Test]
		public void ReadSave()
		{
			List<int> ids = new List<int>();
			ids.Add(4);
			ENFood food = (ENFood)new ENFood("").Read(ids);
			food.Quantity = 2;
			food.Save();
		}

		[Test]
		public void ReadDelete()
		{
			List<int> ids = new List<int>();
			ids.Add(2);
			ENFood food = (ENFood)new ENFood("").Read(ids);
			food.Delete();
		}
	}
}


