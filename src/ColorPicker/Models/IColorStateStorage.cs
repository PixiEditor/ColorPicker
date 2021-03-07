using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPicker.Models
{
    public interface IColorStateStorage
    {
        public ColorState ColorState { get; set; }
    }
}
