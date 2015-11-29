using System;

namespace OrbitalDefense
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (OrbitalDefenseMain game = new OrbitalDefenseMain())
            {
                game.Run();
            }
        }
    }
#endif
}

