using DevExpress.Export.Xl;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraPrinting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WareHousingMaster.view.common
{
    static class Common
    {

        public static Dictionary<int, string> _dicRateKbn = new Dictionary<int, string>()
                                                            {
                                                                {11, "위탁1"},
                                                                {12, "위탁2"},
                                                                {13, "위탁3"},
                                                                {14, "위탁4"},
                                                                {21, "현매1"},
                                                                {22, "현매2"},
                                                                {23, "현매3"},
                                                                {24, "현매4"},
                                                            };

    public static int _FONTSIZE { get; set; }
        public static bool _ISDARKMODE { get; set; }

        static public int GetWidth(LookUpEdit editor)
        {
            if (editor == null)
                return 0;
            return editor.Width;
        }

        static public void setGridViewNo(GridView gv)
        {
            int rowHandle;
            ArrayList rows = new ArrayList();
            for (int i = 0; i < gv.DataRowCount; i++)
            {
                rowHandle = gv.GetVisibleRowHandle(i);
                rows.Add(gv.GetDataRow(rowHandle));
            }

            for (int i = 0; i < rows.Count; i++)
            {
                DataRow row = rows[i] as DataRow;
                row["NO"] = i + 1;
            }
        }

        static public void gridViewButtonChecked(GridView gvList, DataTable dt)
        {
            int rowhandle = gvList.FocusedRowHandle;
            int topRowIndex = gvList.TopRowIndex;
            gvList.FocusedRowHandle = -2147483646;
            gvList.FocusedRowHandle = rowhandle;

            try
            {
                gvList.BeginUpdate();
                foreach (DataRow row in dt.Rows)
                {
                    row.BeginEdit();
                    row["CHECK"] = false;
                    row.EndEdit();
                }

                ArrayList rows = new ArrayList();
                for (int i = 0; i < gvList.DataRowCount; i++)
                {
                    int rowHandle = gvList.GetVisibleRowHandle(i);
                    rows.Add(gvList.GetDataRow(rowHandle));
                }

                for (int i = 0; i < rows.Count; i++)
                {
                    DataRow row = rows[i] as DataRow;
                    // Change the field value.
                    row["CHECK"] = true;
                }
            }
            finally
            {
                gvList.EndUpdate();
            }
        }

        static public void gridViewButtonUnchecked(GridView gvList, DataTable dt)
        {
            int rowhandle = gvList.FocusedRowHandle;
            int topRowIndex = gvList.TopRowIndex;
            gvList.FocusedRowHandle = -2147483646;
            gvList.FocusedRowHandle = rowhandle;

            gvList.BeginDataUpdate();

            foreach (DataRow row in dt.Rows)
            {
                row.BeginEdit();
                row["CHECK"] = false;
                row.EndEdit();
            }
            gvList.EndDataUpdate();
        }

        static public void exportFile(GridView gvList)
        {
            using (SaveFileDialog form = new SaveFileDialog())
            {
                form.Filter = "Excel 통합문서|*.xlsx";
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        XlsxExportOptionsEx options = new XlsxExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG };
                        gvList.ExportToXlsx(form.FileName, options);

                        if (Dangol.MessageYN("엑셀 내보내기를 완료 했습니다.\r\n해당 엑셀파일을 확인하시겠습니까?") == DialogResult.Yes)
                        {
                            Process.Start(form.FileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        Dangol.Error(ex.Message);
                    }
                }
            }
        }

        static public void exportFileLanguage(GridView gvList, Dictionary<string, string[]> dicColName, int language)// 1:ko-kr, 2:en-us
        {
            using (SaveFileDialog form = new SaveFileDialog())
            {
                form.Filter = "Excel 통합문서|*.xlsx";
                if (form.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        ChangeGridColumNmLanguage(gvList, dicColName, language);
                        XlsxExportOptionsEx options = new XlsxExportOptionsEx() { ExportType = DevExpress.Export.ExportType.WYSIWYG };
                        gvList.ExportToXlsx(form.FileName, options);


                        if (Dangol.MessageYN("엑셀 내보내기를 완료 했습니다.\r\n해당 엑셀파일을 확인하시겠습니까?") == DialogResult.Yes)
                        {
                            Process.Start(form.FileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        Dangol.Error(ex.Message);
                    }
                    finally
                    {
                        ChangeGridColumNmLanguage(gvList, dicColName, 1);
                    }
                }
            }
        }

        static public void ChangeGridColumNmLanguage(GridView gvList, Dictionary<string, string[]> dicColName, int language)// 1:ko-kr, 2:en-us
        {
            string fieldNm;
            foreach (GridColumn gc in gvList.Columns)
            {
                fieldNm = gc.FieldName;

                if (dicColName.ContainsKey(fieldNm))
                    gc.Caption = dicColName[fieldNm][language];
            }
        }

        static public void ExportDataTable(DataTable dt, string fileOutputPath, string sheetName, List<DataColumn> cols)
        {
            try
            {
                IXlExporter exporter = XlExport.CreateExporter(XlDocumentFormat.Xlsx);
                using (FileStream stream = new FileStream(fileOutputPath, FileMode.Create, FileAccess.Write))
                using (IXlDocument document = exporter.CreateDocument(stream))
                {
                    XlCellFormatting formatting = new XlCellFormatting();
                    formatting.Fill = XlFill.SolidFill(XlColor.FromArgb(0x00, 0x00, 0x00));
                    formatting.Font = new XlFont();
                    formatting.Font.SchemeStyle = XlFontSchemeStyles.None;
                    formatting.Font.Size = 9;
                    formatting.Font.Color = Color.White;
                    formatting.Font.Bold = true;

                    // Worksheet
                    using (IXlSheet sheet = document.CreateSheet())
                    {
                        sheet.Name = sheetName;

                        // Create the columns. It could be a subset from the DataTable.
                        foreach (DataColumn col in cols)
                        {
                            using (IXlColumn column = sheet.CreateColumn())
                            {
                                if (col.ColumnName.Equals("NO"))
                                    column.WidthInPixels = 50;
                                else if (col.ColumnName.Equals("BARCODE"))
                                    column.WidthInPixels = 100;
                                else if (col.DataType.Name.Equals("String"))
                                    column.WidthInPixels = 150;
                                else if (col.DataType.Name.Equals("Int32") || col.GetType().Equals("Int64"))
                                    column.WidthInPixels = 100;
                                else
                                    column.WidthInPixels = 100;


                            }
                        }

                        // Header row.
                        using (IXlRow row = sheet.CreateRow())
                        {
                            foreach (DataColumn col in cols)
                            {
                                using (IXlCell cell = row.CreateCell())
                                {
                                    cell.Value = col.Caption ?? col.ColumnName;
                                    //??? cell.Formatting.Font.Bold = true;  //to be fixed
                                    cell.ApplyFormatting(formatting);
                                }
                            }
                        }

                        // Rows with the data
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            using (IXlRow row = sheet.CreateRow())
                            {
                                foreach (DataColumn col in cols)
                                {
                                    using (IXlCell cell = row.CreateCell())
                                    {
                                        object o = dataRow[col.ColumnName];
                                        cell.Value = XlVariantValue.FromObject(o);

                                        if (col.DataType.Name.Equals("Int32") || col.DataType.Name.Equals("Int64"))
                                            cell.Formatting = XlNumberFormat.NumberWithThousandSeparator;
                                    }
                                }
                            }
                        }
                    }
                }

                if (Dangol.MessageYN("엑셀 내보내기를 완료 했습니다.\r\n해당 엑셀파일을 확인하시겠습니까?") == DialogResult.Yes)
                {
                    Process.Start(fileOutputPath);
                }
            }
            catch (Exception ex)
            {
                Dangol.Error(ex.Message);
            }
        }


        static public string addComma(string data)
        {
            int value = ConvertUtil.ToInt32(data);
            string numberWithComma = string.Format("{0:#,##0}", value);

            return numberWithComma;
        }

        static public string addComma(int data)
        {
            string numberWithComma = string.Format("{0:#,##0}", data);
            return numberWithComma;
        }

        static public string addComma(long data)
        {
            string numberWithComma = string.Format("{0:#,##0}", data);
            return numberWithComma;
        }




        [STAThreadAttribute]
        static public void copy(DataRow[] rows, string Column)
        {
            StringBuilder stBuilder = new StringBuilder();

            foreach (DataRow row in rows)
                stBuilder.AppendLine(ConvertUtil.ToString(row[Column]));

            // 클립보드에 text 저장하기
            Clipboard.SetText(stBuilder.ToString());

            // 클립보드에서 text 가져오기
            //var text = Clipboard.GetText();
            // 콘솔 출력

        }
    }
}
