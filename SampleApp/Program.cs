namespace SampleApp
{
    using System;

    internal static class Program
    {
        /// <summary>
        ///   The main entry point for the application.
        /// </summary>
        [MTAThread]
        private static void Main()
        {
            using (var game = new SampleGame())
            {
                game.Run();
            }
        }
    }
}