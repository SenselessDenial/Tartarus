using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tartarus
{
    class SelectionMatrix : Component
    {
        private List<Button> matrix;

        public int Width;
        public int XSpacing;
        public int YSpacing;


        public SelectionMatrix(int width, int xSpacing, int ySpacing) 
            : base() 
        {
            matrix = new List<Button>();
            Width = width;
            XSpacing = xSpacing;
            YSpacing = ySpacing;
        }

























    }
}
