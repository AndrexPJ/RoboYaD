using System;

namespace RoboYaDBL
{
    class ConsoleTimeLogger
    {
        public static void Log(string info)
        {
            Console.WriteLine("{0}: {1}", DateTime.Now, info);
        }
    }
}