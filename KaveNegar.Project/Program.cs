using KaveNegar.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KaveNegar.Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var uploadRepository = new UploadRepository();
            uploadRepository.ReadingData(@"C:\Users\orouji\Desktop\KaveNegar\RandomNumber.xlsx").GetAwaiter().GetResult();

            uploadRepository.CachingData().GetAwaiter().GetResult();

            uploadRepository.SavingData().GetAwaiter().GetResult();
            Console.ReadKey();
        }
    }
}
