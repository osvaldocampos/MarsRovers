using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRovers.Models
{
    public class NavigationCommandModel
    {
        public int X_Position { get; set; }
        public int Y_Position { get; set; }
        public char Z_Position { get; set; }
        public string Instructions { get; set; }

        public NavigationCommandModel() { }
    }
}
