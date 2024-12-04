using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ML;
using Microsoft.ML.Transforms.Image;

namespace FINAL_PROJPF
{
    internal class Input
    {
        [ImageType(224,224)]
        public Bitmap bitmap {  get; set; }
    }
}
