using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SampleApp
{
    using Beerdriven.Mobile.Gaming;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        static void Main()
        {
            using (var game = new SampleGame())
            {
                game.Run();
            }
        }
    }
}