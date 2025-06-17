using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfApp.Models
{
    public sealed class SelectedImage
    {
        public ImageSource ImageSource { get; set; }
        public byte[] ImageBytes { get; set; }
        public string FileName { get; set; }
    }
}
