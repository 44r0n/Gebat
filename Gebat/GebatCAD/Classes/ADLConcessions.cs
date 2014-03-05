
namespace GebatCAD.Classes
{
    public class ADLConcessions : AADL
    {
        public ADLConcessions(string connStringName)
            : base(connStringName)
        {
            this.tablename = "Concessions";
            this.idFormat.Add("Id");
        }
    }
}
