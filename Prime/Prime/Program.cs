using System;

namespace Prime
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Prime game = new Prime())
            {
                game.Run();
            }
        }
    }
#endif
}

