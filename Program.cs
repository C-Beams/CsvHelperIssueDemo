using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Newtonsoft.Json;

namespace CsvHelperIssueDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var csvSource = Encoding.Default.GetBytes(Properties.Resources.sample_data);

            try
            {
                Console.WriteLine("Calling GetRecords with ToList");

                var listRecords = ReturnRecordsAsList(csvSource);

                Console.WriteLine("Calling ToArray() on result List");

                var listRecsArray = listRecords.ToArray();

                Console.WriteLine($"Result:\r\n" +
                                  $"{JsonConvert.SerializeObject(listRecsArray, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            try
            {
                Console.WriteLine("Calling GetRecords without ToList");

                var iEnumerableRecords = ReturnRecordsAsIEnumerable(csvSource);

                Console.WriteLine("Calling ToArray() on result IEnumerable");

                var iEnumRecsArray = iEnumerableRecords.ToArray();

                Console.WriteLine($"Result:\r\n" +
                                  $"{JsonConvert.SerializeObject(iEnumRecsArray, Formatting.Indented)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }

        public static List<SampleModel> ReturnRecordsAsList(byte[] source)
        {
            var memStream = new MemoryStream(source);

            using var reader = new StreamReader(memStream);

            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = true;

                return csv.GetRecords<SampleModel>().ToList();
            }
        }

        public static IEnumerable<SampleModel> ReturnRecordsAsIEnumerable(byte[] source)
        {
            var memStream = new MemoryStream(source);

            using var reader = new StreamReader(memStream);

            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.HasHeaderRecord = true;

                return csv.GetRecords<SampleModel>();
            }
        }
    }
}
