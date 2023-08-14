using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using WareHousingMaster.view.common;
using Excel = Microsoft.Office.Interop.Excel;

namespace ImportExcel
{
    public class ExcelUtil
    {
        public static DataTable getDataTableFromExcel(string fileName, string filePath)
        {
            DataTable dt = new DataTable();
            Dictionary<string, int> dicColNm = new Dictionary<string, int>();

            Excel.Application excelApp = null;
            Excel.Workbook workBook = null;
            Excel.Worksheet workSheet = null;

            try
            {
                excelApp = new Excel.Application(); // 엑셀 어플리케이션 생성 
                workBook = excelApp.Workbooks.Open(filePath); // 워크북 열기 
                workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet; // 엑셀 첫번째 워크시트 가져오기 
                Excel.Range range = workSheet.UsedRange; // 사용중인 셀 범위를 가져오기 

                for (int column = 1;column <= range.Columns.Count; column++) // 가져온 열 만큼 반복 
                {
                    string str = ConvertUtil.ToString((range.Cells[1, column] as Excel.Range).Value2); // 셀 데이터 가져옴 

                    if (dicColNm.ContainsKey(str))
                    {
                        dicColNm[str]++;
                        dt.Columns.Add(new DataColumn($"{str}_{dicColNm[str]}", typeof(string)));
                    }
                    else
                    {
                        dicColNm.Add(str, 0);
                        dt.Columns.Add(new DataColumn(str, typeof(string)));
                    }       
                }
               


                for (int row = 2; row <= range.Rows.Count; row++) // 가져온 행 만큼 반복 
                {
                    DataRow dr = dt.NewRow();

                    for (int column = 1; column <= range.Columns.Count; column++) // 가져온 열 만큼 반복 
                    {
                        string str = ConvertUtil.ToString((range.Cells[row, column] as Excel.Range).Value2); // 셀 데이터 가져옴 
                        dr[column - 1] = str;
                        //Console.Write(str + " ");
                    }
                    dt.Rows.Add(dr);
                    //Console.WriteLine();
                }


                workBook.Close(true); // 워크북 닫기 
                excelApp.Quit(); // 엑셀 어플리케이션 종료 
            }
            finally
            {
                ReleaseObject(workSheet); ReleaseObject(workBook); ReleaseObject(excelApp);
            }

            return dt;
        } 
        
        /// <summary> 
        /// 액셀 객체 해제 메소드 
        /// </summary> 
        /// <param name="obj">
        /// </param> 


        static void ReleaseObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj); // 액셀 객체 해제
                    obj = null;
                }
            }
            catch (Exception ex)
            {
                obj = null; throw ex;
            }
            finally
            {
                GC.Collect(); // 가비지 수집 
            }
        }
    }
}
