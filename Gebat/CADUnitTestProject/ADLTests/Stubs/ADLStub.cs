using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GebatCAD.Classes;

namespace CADUnitTestProject.ADLTests
{
    public class ADLStub : ADL
    {
        public ADLStub(string connStringName, string tableName, params string[] ids)
            :base(connStringName,tableName,ids)
        {
            this.sqlConnectionString = "Server=tacas;Database=test;Uid=root;";
            this.sqlprovider = "MySql.Data.MySqlClient";
        }
    }
}
