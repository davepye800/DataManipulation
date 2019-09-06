using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FileProcessing.Model;
using FileProcessing.Interface;

namespace FileProcessing.File
{
    public class ClaimsFile : IFile
    {
        private StringBuilder output;
        private List<ClaimsData> dataList;

        public ClaimsFile()
        {
            output = new StringBuilder();
            dataList = new List<ClaimsData>();
        }

        public string Process(string data)
        {
            string[] dataRows = data.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string row in dataRows.Skip(1))
                {
                    string[] columns = row.Split(',');
                    ClaimsData d = new ClaimsData();
                    d.Product = columns[0];
                    d.OriginYear = Convert.ToInt16(columns[1]);
                    d.DevlopmentYear = Convert.ToInt16(columns[2]);
                    d.IncrementalValue = Convert.ToDouble(columns[3]);
                    dataList.Add(d);
                }
                dataList = dataList.OrderBy(y => y.OriginYear).ToList();

            //create header values
            int startYear = dataList[0].OriginYear;
            int duration = (dataList[dataList.Count - 1].OriginYear + 1) - dataList[0].OriginYear;

            output.Append(startYear);
            output.Append(",");
            output.Append(duration);
            output.Append(Environment.NewLine);

            //cycle through products
            foreach (string product in dataList.Select(d => d.Product).Distinct())
            {
                output.Append(product);
                output.Append(",");

                //cycle through origin years
                for (int o = 0; o < duration; o++)
                {
                    double cumulativeResult = 0;
                    int originYear = startYear + o;

                    //cycle through development years
                    for (int d = 0; d < duration - o; d++)
                    {
                        int developmentYear = originYear + d;
                        var developmentYearResult = Convert.ToDouble(dataList.Where(y => y.OriginYear == originYear
                                                    && y.DevlopmentYear == developmentYear
                                                    && y.Product == product).Select(a => a.IncrementalValue).FirstOrDefault());
                        cumulativeResult += developmentYearResult;
                        output.Append(cumulativeResult);
                        output.Append(",");
                    }
                }
                output.Append(Environment.NewLine);
            }
            return output.ToString();
        }
    }
}
