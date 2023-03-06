using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClasses
{
    public class Bilmodel
    {
        public Bilmodel(string mærke, string model, int startår, int id)
        {
            Mærke = mærke;
            Model = model;
            Startår = startår;
            Id = id;
        }
        public string Mærke { get; set; }
        public string Model { get; set; }
        public int Startår { get; set; }
        public int Slutår { get; set; }
        public int Standardpris { get; set; }
        public int Id { get; }
    }
}
