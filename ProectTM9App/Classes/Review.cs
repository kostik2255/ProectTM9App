using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProectTM9App.Classes
{
    public class Review 
    {
        public string FullName { get; set; }
        public string Company { get; set; }
        public string Position { get; set; }
        public string Feedback { get; set; }

        public override string ToString()
        {
            return $"{FullName}, {Position}, {Company}: {Feedback}";
        }
    }
}
