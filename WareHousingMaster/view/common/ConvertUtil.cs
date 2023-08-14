using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WareHousingMaster.view.common
{
    static class ConvertUtil
    {
        static Regex r = new Regex(@"[0-9]*\.[0-9]+");
        private static bool IsEmpty(object o)
        {
            if (o == null || o == DBNull.Value || o.ToString().Equals(string.Empty)) return true;
            //else if (o is double && Double.IsNaN((double)o)) return true;
            return false;
        }
        public static string ToDateTimeNull(object o)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            if (string.IsNullOrEmpty(ToString(o)))
                return null;
            else
            {
                if (ToString(o).Length == 8)
                    return ToDateTimeNull_S(o);
                else
                {
                    if (ToInt64(o) < 1)
                        return "";
                    else
                        return start.AddMilliseconds(ToInt64(o)).ToLocalTime().ToString("yyyy-MM-dd");
                }
            }    
        }

        public static string ToDateTimeNull_S(object o)
        {
            if (string.IsNullOrEmpty(ToString(o)))
                return null;
            else
            {
                string date = ToString(o);
                return DateTime.ParseExact(date, "yyyyMMdd", null).ToString("yyyy-MM-dd");
            }
        }

        public static string ToDateTimeNullWithTime(object o)
        {
            DateTime start = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            if (string.IsNullOrEmpty(ConvertUtil.ToString(o)))
                return null;
            else
                return start.AddMilliseconds(ConvertUtil.ToInt64(o)).ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string ToDateTime(object o, string format)
        {
            if (IsEmpty(o) || o == null) return string.Empty;
            else
            {
                DateTime newDate;
                if (DateTime.TryParse(o.ToString(), out newDate))
                {
                    return newDate.ToString(format);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public static DateTime ToDateTime(object o)
        {
            if (IsEmpty(o) || o == null) return DateTime.Now;
            else
            {
                DateTime newDate;
                if (DateTime.TryParse(o.ToString(), out newDate))
                {
                    return newDate;
                }
                else
                {
                    try
                    {
                        double oleTime = Convert.ToDouble(o);
                        if (oleTime < -657435 || oleTime > 2958466)
                        {
                            // OLE시간 아님
                            string timeString = o.ToString();
                            if (timeString.Length == 14)
                            {
                                int year = Convert.ToInt32(timeString.Substring(0, 4));
                                int month = Convert.ToInt32(timeString.Substring(4, 2));
                                int day = Convert.ToInt32(timeString.Substring(6, 2));
                                int hour = Convert.ToInt32(timeString.Substring(8, 2));
                                int min = Convert.ToInt32(timeString.Substring(10, 2));
                                int sec = Convert.ToInt32(timeString.Substring(12, 2));
                                var time = new DateTime(year, month, day, hour, min, sec);
                                return time;
                            }
                            else if (timeString.Length == 8)
                            {
                                int year = Convert.ToInt32(timeString.Substring(0, 4));
                                int month = Convert.ToInt32(timeString.Substring(4, 2));
                                int day = Convert.ToInt32(timeString.Substring(6, 2));
                                return new DateTime(year, month, day);
                            }
                            else
                            {
                                throw new Exception("날짜 형식이 잘못되었습니다.");
                            }
                        }
                        else
                        {
                            return DateTime.FromOADate(oleTime);
                        }
                    }
                    catch (System.InvalidCastException ex)
                    {
                        // 시스템 설정에서 사용자가 날짜 표시 형식을 임의로 변경한 경우 
                        // : 제어판-국가 또는 지역-[추가 설정]-날짜 탭에서 간단한 날짜(ToShortDateString), 자세한 날짜(ToLongDateString)를 변경하면 DateTime 형식 전체에 영향을 줌
                        DateTime? dt = o as DateTime?;
                        if (dt == null)
                        {
                            throw new Exception($"날짜 형식이 잘못되었습니다. \n{ex.Message}");
                        }
                        DateTime dtAdj = new DateTime(dt.Value.Year, dt.Value.Month, dt.Value.Day, dt.Value.Hour, dt.Value.Minute, dt.Value.Second, dt.Value.Millisecond);
                        return dtAdj;
                    }

                }
            }
        }

        public static DateTime ToDateTime(DateEdit dateEdit, int type = 0)  //0:기본, 1:00, 2: 59
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            if (dateEdit.EditValue != null && !string.IsNullOrEmpty(dateEdit.EditValue.ToString()))
            {
                if(type == 0)
                    dt = Convert.ToDateTime(dateEdit.EditValue);
                else if(type == 1)
                {
                    string date = dateEdit.Text.Trim();
                    if(date.Length == 10)
                        dt = Convert.ToDateTime($"{date} 00:00:00");
                    else
                    {
                        date = date.Substring(0, 10);
                        dt = Convert.ToDateTime($"{date} 00:00:00");
                    }
                }
                else if (type == 2)
                {
                    string date = dateEdit.Text.Trim();
                    if (date.Length == 10)
                        dt = Convert.ToDateTime($"{date} 23:59:59");
                    else
                    {
                        date = date.Substring(0, 10);
                        dt = Convert.ToDateTime($"{date} 23:59:59");
                    }
                }
            }

            return dt;
        }

        public static Int16 ToInt16(object o)
        {
            if (IsEmpty(o) || o == null) return (Int16)0;
            else return Convert.ToInt16(o);
        }

        public static Int32 ToInt32(object o)
        {
            if (IsEmpty(o) || o == null) return 0;
            else return Convert.ToInt32(o);
        }

        public static Int64? ToInt64OrNull(object o)
        {
            if (IsEmpty(o) || o == null) return null;
            else return Convert.ToInt64(o);
        }

        public static Int64 ToInt64(object o)
        {
            if (IsEmpty(o) || o == null) return 0;
            else return Convert.ToInt64(o);
        }

        public static double ToDouble(object o)
        {
            if (IsEmpty(o) || o == null) return 0;
            else return Convert.ToDouble(o);
        }

        public static decimal ToDecimal(object o)
        {
            if (IsEmpty(o) || o == null) return 0;
            else if (o is double || o is float)
            {
                double v = Convert.ToDouble(o);

                if (v > (double)decimal.MaxValue)
                {
                    return decimal.MaxValue;
                }
                else if (v < (double)decimal.MinValue)
                {
                    return decimal.MinValue;
                }
                else
                {
                    try
                    {
                        return decimal.Parse(o.ToString(), System.Globalization.NumberStyles.Float);
                    }
                    catch (FormatException fx)
                    {
                        return decimal.Parse(o.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                    }
                    //지수 표기법 데이터 처리를 위해 변경 (2018-09-12 변지환)
                    //return Convert.ToDecimal(o);
                }
            }
            else
            {
                try
                {
                    return decimal.Parse(o.ToString(), System.Globalization.NumberStyles.Float);
                }
                catch (FormatException fx)
                {
                    return decimal.Parse(o.ToString(), System.Globalization.CultureInfo.InvariantCulture);
                }
            }
        }

        public static string ToString(object o)
        {
            if (IsEmpty(o) || o == null) return string.Empty;
            else return Convert.ToString(o);
        }

        public static float ToFloat(object o)
        {
            if (IsEmpty(o)) return 0;
            else return (float)Convert.ToDouble(o);
        }

        public static bool ToBoolean(object o)
        {
            if (IsEmpty(o)) return false;
            else if (o is string && o.Equals("0"))
                return false;
            else if (o is string && o.Equals("1"))
                return true;
            else return (bool)Convert.ToBoolean(o);
        }

        public static int ParseInt32(object o)
        {
            if (IsEmpty(o) || o == null) return 0;
            else
            {
                string data = Regex.Replace(ToString(o), @"\D", "");
                return ToInt32(data);
            }
        }

        public static double ParseDouble(object o)
        {
            if (IsEmpty(o) || o == null) return 0.0;
            else
            {
                Match m = r.Match(ToString(o));
                return ToDouble(m.Value);
            }
        }

        public static object ToType(object value, Type type)
        {
            if (type == typeof(Int16))
            {
                return ConvertUtil.ToInt16(value);
            }
            if (type == typeof(Int32))
            {
                return ConvertUtil.ToInt32(value);
            }
            if (type == typeof(Int64))
            {
                return ConvertUtil.ToInt64(value);
            }
            if (type == typeof(double))
            {
                return ConvertUtil.ToDouble(value);
            }
            if (type == typeof(decimal))
            {
                return ConvertUtil.ToDecimal(value);
            }
            if (type == typeof(bool))
            {
                return ConvertUtil.ToBoolean(value);
            }
            if (type == typeof(DateTime))
            {
                return ConvertUtil.ToDateTime(value);
            }
            else
                return value;
        }


    }
}
