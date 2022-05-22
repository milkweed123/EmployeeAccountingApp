using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalApp
{
    public class Patient
    {
        public string Name { get; set; }    

        public string Surname { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Illness { get; set; } 
    }
}
