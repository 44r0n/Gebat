//
//  Test.cs
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
using GebatCAD.Classes;
using GebatCAD.Exceptions;
using System.Data;
using SqlManager;
using System.Configuration;
using System.IO;
using System.Data.Common;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Test
{
	[TestFixture()]
	public class CADTypeTest
	{
		private DataTable tableFormat
		{
			get
			{
				DataTable expected = new DataTable();
				expected.Columns.Add("Id", typeof(int));
				expected.Columns.Add("Name", typeof(string));
				return expected;
			}
		}

		private string scriptfilename = "PreparacionTest.sql";

		private ISql fooCon
		{
			get
			{
				StubIsql ret = new StubIsql ();
				ret.Conn = "Server=tacas;Database=test;Uid=root;";
				return ret;
			}
		}

		private void setFailConn()
		{
			ISql conn = fooCon;
			FactorySql factory = new FactorySql();
			factory.SetManager(conn);
		}

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


		private void ResetConn()
		{
			FactorySql factory = new FactorySql();
			factory.ResetManager();
		}

		public void SetPasswd()
		{
			ACAD.Password = "root";
		}

		[SetUp]
		public void InnitTest()
		{
			ResetConn();
			this.SetPasswd ();
			this.InitBD();
		}

		[Test]
		public void TestSelectOne ()
		{
			string expected = "Kg";
			ACAD food = new CADType("GebatDataConnectionString");
			List<object> ids = new List<object>();
			ids.Add((int)1);
			DataRow actual = food.Select(ids);
			Assert.AreEqual(actual["Name"].ToString(), expected);
		}

		[Test]
		public void TestCount()
		{
			int expected = 3;
			ACAD food = new CADType("GebatDataConnectionString");
			int actual = food.Count();
			Assert.AreEqual(expected, actual);
		}

		[Test]
		[ExpectedException(typeof(MySqlException))]
		public void TestCountConnFail()
		{
			setFailConn();
			ACAD food = new CADType("GebatDataConnectionString");

			food.Count();
		}

		[Test]
		[ExpectedException(typeof(MySqlException))]
		public void TestLastConnFail()
		{
			setFailConn();
			ACAD food = new CADType("GebatDataConnectionString");

			food.Last();
		}

		[Test]
		public void TestLast()
		{

			ACAD food = new CADType("GebatDataConnectionString");
			DataRow actual = food.Last();
			DataRow expected = food.GetVoidRow;
			expected ["Id"] = 4;
			expected ["Name"] = "Paquetes";
			Assert.AreEqual(expected["Id"], actual["Id"]);
			Assert.AreEqual(expected["Name"], actual["Name"]);
		}

		[Test]
		public void SelectAll()
		{
			ACAD food = new CADType("GebatDataConnectionString");
			DataTable actual = food.SelectAll();
			DataTable expected = this.tableFormat;
			DataRow row = expected.NewRow();
			row["Id"] = 1;
			row["Name"] = "Kg";
			expected.Rows.Add(row);
			DataRow row2 = expected.NewRow();
			row2["Id"] = 2;
			row2["Name"] = "Litros";
			expected.Rows.Add(row2);
			DataRow row3 = expected.NewRow();
			row3["Id"] = 4;
			row3["Name"] = "Paquetes";
			expected.Rows.Add(row3);
			for (int i = 0; i < expected.Rows.Count; i++)
			{
				Assert.AreEqual(expected.Rows[i]["Id"], actual.Rows[i]["Id"]);
				Assert.AreEqual(expected.Rows[i]["Name"], actual.Rows[i]["Name"]);
			}
		}



		[Test]
		[ExpectedException(typeof(MySqlException))]
		public void SelectAllFailConn()
		{
			setFailConn();
			ACAD food = new CADType("GebatDataConnectionString");

			food.SelectAll();
		}

		[Test]
		public void Select()
		{
			ACAD food = new CADType("GebatDataConnectionString");
			List<object> ids = new List<object>();
			ids.Add(1);
			DataRow actual = food.Select(ids);
			DataTable table = tableFormat;
			DataRow expected = table.NewRow();
			expected["Id"] = 1;
			expected["Name"] = "Kg";

			Assert.AreEqual(expected["Id"], actual["Id"]);
			Assert.AreEqual(expected["Name"], actual["Name"]);
		}

		[Test]
		[ExpectedException(typeof(NullReferenceException))]
		public void SelectVoidList()
		{
			ACAD food = new CADType("GebatDataConnectionString");
			food.Select(null);
		}

		[Test]
		[ExpectedException(typeof(InvalidNumberIdException))]
		public void SelectInvalidNumberId()
		{
			List<object> ids = new List<object>();
			ids.Add("hola");
			ids.Add(3);
			ACAD food = new CADType("GebatDataConnectionString");
			food.Select(ids);
		}

		[Test]
		[ExpectedException(typeof(MySqlException))]
		public void SelectConnFail()
		{
			setFailConn();

			ACAD food = new CADType("GebatDataConnectionString");
			List<object> ids = new List<object>();
			ids.Add(2);
			food.Select(ids);

		}

		[Test]
		public void SelectWhere()
		{
			ACAD food = new CADType("GebatDataConnectionString");
			DataTable expected = tableFormat;
			DataRow row = expected.NewRow();
			row["Id"] = 1;
			row["Name"] = "Kg";
			expected.Rows.Add(row);
			DataTable actual = food.SelectWhere("Name = 'Kg'");

			for (int i = 0; i < expected.Rows.Count; i++)
			{
				Assert.AreEqual(expected.Rows[i]["Id"], actual.Rows[i]["Id"]);
				Assert.AreEqual(expected.Rows[i]["Name"], actual.Rows[i]["Name"]);
			}
		}

		[Test]
		[ExpectedException(typeof(InvalidStartRecordException))]
		public void SelectWhereInvalidStart()
		{
			ACAD food = new CADType("GebatDataConnectionString");
			food.SelectWhere("Name = 'Kg'", -3);
		}

		[Test]
		[ExpectedException(typeof(MySqlException))]
		public void SelectWjereInvalidStatement()
		{
			ACAD food = new CADType("GebatDataConnectionString");
			food.SelectWhere("Name = ; ");
		}

		[Test]
		[ExpectedException(typeof(MySqlException))]
		public void SelectWhereFailConn()
		{
			setFailConn();

			ACAD food = new CADType("GebatDataConnectionString");
			food.SelectWhere("Name = 'Kg'");
		}

		[Test]
		public void Insert()
		{
			ACAD food = new CADType("GebatDataConnectionString");
			DataRow ins = food.GetVoidRow;
			ins["Name"] = "Cajas";
			DataRow expected = food.GetVoidRow;
			expected ["Id"] = 5;
			expected ["Name"] = "Cajas";
			DataRow actual = food.Insert(ins);
			Assert.AreEqual(expected["Id"], actual["Id"]);
			Assert.AreEqual(expected["Name"], actual["Name"]);
		}

		[Test]
		[ExpectedException(typeof(NullReferenceException))]
		public void InsertNullRow()
		{
			ACAD food = new CADType("GebatDataConnectionString");
			DataRow ins = null;
			food.Insert(ins);
		}

		[Test]
		[ExpectedException(typeof(MySqlException))]
		public void InsertFailCOnn()
		{
			setFailConn();
			ACAD food = new CADType("GebatDataConnectionString");
			DataRow ins = food.GetVoidRow;
			ins["Name"] = "Cajas";
			food.Insert(ins);
		}


		[Test]
		public void Update()
		{
			ACAD food = new CADType("GebatDataConnectionString");
			DataRow mod = food.GetVoidRow;
			mod["Id"] = 1;
			mod["Name"] = "Cajas";
			food.Update(mod);
		}

		[Test]
		[ExpectedException(typeof(NullReferenceException))]
		public void UpdateNullRow()
		{
			ACAD food = new CADType("GebatDataConnectionString");
			DataRow mod = null;
			food.Update(mod);
		}

		[Test]
		[ExpectedException(typeof(MySqlException))]
		public void UpdateFailConn()
		{
			setFailConn();
			ACAD food = new CADType("GebatDataConnectionString");
			DataRow ins = food.GetVoidRow;
			ins["Id"] = 1;
			ins["Name"] = "Cajas";
			food.Update(ins);
		}

		[Test]
		public void Delete()
		{
			ACAD food = new CADType("GebatDataConnectionString");
			DataRow del = food.GetVoidRow;
			del["Id"] = 1;
			del["Name"] = "Kg";
			food.Delete(del);
		}

		[Test]
		[ExpectedException(typeof(NullReferenceException))]
		public void DeleteNullRow()
		{
			ACAD food = new CADType("GebatDataConnectionString");
			food.Delete(null);
		}

		[Test]
		[ExpectedException(typeof(MySqlException))]
		public void DeleteWrongRow()
		{
			ACAD food = new CADType("GebatDataConnectionString");
			DataRow del = food.GetVoidRow;
			del["Name"] = new ENType("taca");
			food.Delete(del);
		}

		[Test]
		[ExpectedException(typeof(MySqlException))]
		public void DeleteFailConn()
		{
			setFailConn();
			ACAD food = new CADType("GebatDataConnectionString");
			DataRow del = food.GetVoidRow;
			del["Id"] = 1;
			del["Name"] = "cajas";
			food.Delete(del);
		}
	}
}

