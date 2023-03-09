using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClasses
{
    public class Bilmodel
    {
        public Bilmodel(string mærke, string model, int startår, int slutår, int standardpris, int forsikringssum, int id = -1)
        {
            Mærke = mærke;
            Model = model;
            Startår = startår;
            Slutår = slutår;
            Standardpris = standardpris;
            Forsikringssum = forsikringssum;
            Id = id;
        }
        public string Mærke { get; set; }
        public string Model { get; set; }
        public int Startår { get; set; }
        public int Slutår { get; set; }
        public string FuldBil
        {
            get
            {
                return Mærke + " " + Model + "(" + Startår + "-" + Slutår + ")";
            }
        }
        public int Standardpris { get; set; }
        public int Forsikringssum { get; set; }
        public int Id { get; }
    }
}
