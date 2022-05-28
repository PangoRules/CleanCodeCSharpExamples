using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode
{
    class Program
    {
        static void Main(string[] args)
        {
            var outputParameters = new OutputParameters.OutputParameters();

            outputParameters.DisplayCustomers();

            string message = "Hello World!!";

            Console.WriteLine(message);
        }
    }
}
