using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Data;
using System.Net.Mail;
using System.Net;
using CadeDateACcess;
using System.IO;
using OfficeOpenXml;
namespace CafeDateAccess
{
    public class ClsSettings
    {
        public static string ConnectionString = "Data Source=DBCafe.db;";
        public static void CreateTheErrorAtEventLog(string Message)
        {
            string logName = "Application";

            // اسم المصدر الذي سيتم عرضه في سجل الأحداث
            string source = "Restaurant,Cafe";

            // تحقق مما إذا كان المصدر موجودًا، وإذا لم يكن موجودًا، قم بإنشائه
            if (!EventLog.SourceExists(source))
            {
                EventLog.CreateEventSource(source, logName);
            }

            // إنشاء حدث جديد في السجل
            using (EventLog eventLog = new EventLog(logName))
            {
                eventLog.Source = source;
                eventLog.WriteEntry(Message, EventLogEntryType.Error);
            }
        }
        public static void ShowMessagboxForSuccessAdding()
        {
            MessageBox.Show("تم الاضافه بنجاح", "عمليه ناجحه", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void ShowMessagboxForFalireOperations()
        {
            MessageBox.Show("لم تتم العمليه", "عمليه فاشله", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void ShowMessagboxForSuccessUPdating()
        {
            MessageBox.Show("تم التعديل بنجاح", "عمليه ناجحه", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void ShowMessagboxForUnCompeleteDetails()
        {
            MessageBox.Show("معلومات مفقوده او غير صحيحه", "عمليه فاشله", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static void ShowSuccessMessageBoxForRecover()
        {
            MessageBox.Show("تم استرجاع الفاتوره", "عمليه ناجحه", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        public static async Task SendEmailWithTodaySalesAsync(DataTable dt, string Subject)
        {
            try
            {
                string fromAddress = "2saadmahmoud312@gmail.com";
                string fromName = "Your Cafe Program";
                string appPassword = "jrpp zohe wxbg zymv";
                string toAddress = ClsUser.GetUserName();
                string subject = Subject;
                string body = GenerateHtmlTable(dt, Subject);

                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress, appPassword)
                };

                MailAddress from = new MailAddress(fromAddress, fromName);
                MailMessage message = new MailMessage
                {
                    From = from,
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                message.To.Add(toAddress);

                // استخدام النسخة غير المتزامنة لإرسال البريد الإلكتروني
                smtp.Send(message);
                Console.WriteLine("Email sent successfully!");
            }
            catch (Exception ex)
            {
                ClsSettings.CreateTheErrorAtEventLog(ex.Message);
            }
        }
        private static string GenerateHtmlTable(DataTable dt, string Subject)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<html><body>");
            html.Append("<meta charset='UTF-8'/>");
            html.Append($"<h2>{Subject}</h2>");
            html.Append("<table border='1' cellspacing='0' cellpadding='5'>");

            // إضافة رؤوس الأعمدة
            html.Append("<tr>");
            foreach (DataColumn column in dt.Columns)
            {
                html.Append($"<th>{column.ColumnName}</th>");
            }
            html.Append("</tr>");

            // إضافة الصفوف
            foreach (DataRow row in dt.Rows)
            {
                html.Append("<tr>");
                foreach (DataColumn column in dt.Columns)
                {
                    html.Append($"<td>{row[column]}</td>");
                }
                html.Append("</tr>");
            }

            // حساب إجمالي المبيعات
            double totalSales = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (row["السعر الاجمالي"] != DBNull.Value)
                {
                    totalSales += Convert.ToDouble(row["السعر الاجمالي"]);
                }
            }

            // إضافة إجمالي المبيعات في صف جديد
            html.Append("<tr>");
            html.Append("<td colspan='");
            html.Append(dt.Columns.Count);
            html.Append("' style='text-align:right; font-weight:bold;'>");
            html.Append($"إجمالي المبيعات: {totalSales:C} ");
            html.Append("</td>");
            html.Append("</tr>");

            html.Append("</table>");
            html.Append("</body></html>");

            return html.ToString();
        }
        public static void SendEmail(string Subject, string Body, string EmailToSend)
        {
            try
            {
                // إعدادات البريد الإلكتروني
                string fromAddress = "2saadmahmoud312@gmail.com"; // بريدك الإلكتروني
                string appPassword = "jrpp zohe wxbg zymv"; // كلمة مرور التطبيق أو كلمة المرور العادية إذا كان "الوصول للتطبيقات الأقل أمانًا" مفعلاً
                string toAddress = EmailToSend; // البريد الإلكتروني للمستلم
                string subject = Subject;
                string fromName = "Your Restaurant Program";
                string body = Body;

                // تكوين SMTP
                SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress, appPassword)
                };

                MailAddress from = new MailAddress(fromAddress, fromName);
                MailMessage message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    From = from,
                    Body = body
                };

                // إرسال الرسالة
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                ClsSettings.CreateTheErrorAtEventLog(ex.Message);
            }
        }
        public static void SendToExcelSheet(DataTable dt, string filePath,string SheetName)
        {
            if (dt == null || string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("DataTable or file path cannot be null or empty.");
            }
           try
            {
                using (var package = new ExcelPackage())
                {
                    // إضافة ورقة عمل جديدة
                    var worksheet = package.Workbook.Worksheets.Add($"{SheetName}");

                    // إضافة بيانات الجدول إلى ورقة العمل
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        worksheet.Cells[1, i + 1].Value = dt.Columns[i].ColumnName;
                    }

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            worksheet.Cells[i + 2, j + 1].Value = dt.Rows[i][j];
                        }
                    }

                    // حفظ الملف إلى المسار المحدد
                    FileInfo fileInfo = new FileInfo(filePath);
                    package.SaveAs(fileInfo);
                }
                     
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}

