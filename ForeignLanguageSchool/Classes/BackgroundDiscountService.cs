using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ForeignLanguageSchool
{
    public partial class Service
    {
        public Brush GetBackgroundColorDiscountService
        {
            get
            {
                if (Discount != 0)
                {
                    var brushConverter = new BrushConverter();
                    return (Brush)brushConverter.ConvertFrom("#e7fabf");
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
