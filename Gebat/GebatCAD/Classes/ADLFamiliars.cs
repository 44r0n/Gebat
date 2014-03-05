
namespace GebatCAD.Classes
{
    public class ADLFamiliars : AADL
    {
        public ADLFamiliars(string connStringName)
            :base(connStringName)
        {
            this.tablename = "Familiars";
            this.idFormat.Add("Id");
        }
    }
}
