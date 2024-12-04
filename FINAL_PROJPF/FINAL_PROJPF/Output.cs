using Microsoft.ML.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FINAL_PROJPF
{
    internal class Output
    {
        [ColumnName("sequential_3")]
        public float[] labels {  get; set; }
    }
}
