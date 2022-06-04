using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading.Tasks;
using KaveNegar.Project.Domain;
using EntityFramework.BulkInsert.Extensions;

namespace KaveNegar.Infrastructure
{
    public class UploadRepository
    {
        private IEnumerable<string> Phones { get; set; }

        private readonly RedisRepository _redisRepository;
        public UploadRepository()
        {
            _redisRepository = new RedisRepository();
        }

        public async Task ReadingData(string filePath)
        {
            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filePath);
            Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
            Excel.Range xlRange = xlWorksheet.UsedRange;

            var firstColumn = xlRange.Columns[1];
            var myvalues = (Array)firstColumn.Cells.Value;
            string[] strArray = myvalues.OfType<object>().Select(o => o.ToString()).ToArray();

            Phones = strArray.Distinct();
        }

        public async Task CachingData()
        {
            _redisRepository.Set("MyData", Phones);
        }

        public async Task SavingData()
        {
            var data = _redisRepository.Get<IEnumerable<string>>("MyData");

            var list = data.Select(s => new Numbers { Number = s });

            using (var context = new ContextRepository())
            {
                await context.BulkInsertAsync(list);
            }

            _redisRepository.RemoveStrings("MyData");
        }
    }
}
