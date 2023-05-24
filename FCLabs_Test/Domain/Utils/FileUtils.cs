using ClosedXML.Excel;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Models.UserModels.ListUser;
using Entities.Entities;
using Entities.Enums;
using iTextSharp.text.pdf;
using Bold = DocumentFormat.OpenXml.Wordprocessing.Bold;
using Document = DocumentFormat.OpenXml.Wordprocessing.Document;
using Paragraph = DocumentFormat.OpenXml.Wordprocessing.Paragraph;
using Run = DocumentFormat.OpenXml.Wordprocessing.Run;
using RunProperties = DocumentFormat.OpenXml.Wordprocessing.RunProperties;
using Table = DocumentFormat.OpenXml.Wordprocessing.Table;
using Text = DocumentFormat.OpenXml.Wordprocessing.Text;

namespace Domain.Utils
{
    public static class FileUtils
    {
        public static ExportListUsersResult ExportListUsers(List<User> users, ExportEnum exportFormat)
        {
            switch (exportFormat)
            {
                case ExportEnum.EXCEL:
                    return ExportListUsersExcel(users);

                case ExportEnum.PDF:
                    return ExportListUsersPdf(users);

                case ExportEnum.WORD:
                    return ExportListUsersWord(users);

                default:
                    return null;
            }
        }

        public static ExportListUsersResult ExportListUsersExcel(List<User> users)
        {
            var stream = new MemoryStream();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("List Users");

                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Status";
                worksheet.Cell(1, 3).Value = "CPF";
                worksheet.Cell(1, 4).Value = "Name";
                worksheet.Cell(1, 5).Value = "Login";
                worksheet.Cell(1, 6).Value = "Email";
                worksheet.Cell(1, 7).Value = "Phone";
                worksheet.Cell(1, 8).Value = "BirthDate";
                worksheet.Cell(1, 9).Value = "MotherName";
                worksheet.Cell(1, 10).Value = "InclusionDate";
                worksheet.Cell(1, 11).Value = "LastChangeDate";

                int row = 2;
                foreach (var user in users)
                {
                    worksheet.Cell(row, 1).Value = user.Id.ToString();
                    worksheet.Cell(row, 2).Value = user.Status.ToString();
                    worksheet.Cell(row, 3).Value = user.CPF;
                    worksheet.Cell(row, 4).Value = user.Name;
                    worksheet.Cell(row, 5).Value = user.Login;
                    worksheet.Cell(row, 6).Value = user.Email;
                    worksheet.Cell(row, 7).Value = user.Phone;
                    worksheet.Cell(row, 8).Value = user.BirthDate.ToString("dd/MM/yyyy");
                    worksheet.Cell(row, 9).Value = user.MotherName;
                    worksheet.Cell(row, 10).Value = user.InclusionDate.ToString("dd/MM/yyyy");
                    worksheet.Cell(row, 11).Value = user.LastChangeDate.ToString("dd/MM/yyyy");
                    row++;
                }

                workbook.SaveAs(stream);
            }

            stream.Position = 0;
            return new ExportListUsersResult
            {
                Stream = stream,
                Headers = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                FileName = "Users.xlsx"
            };
        }

        private static ExportListUsersResult ExportListUsersPdf(List<User> users)
        {
            var stream = new MemoryStream();
            var document = new iTextSharp.text.Document();
            PdfWriter writer = PdfWriter.GetInstance(document, stream);
            writer.CloseStream = false;
            document.Open();

            PdfPTable table = new PdfPTable(11);
            table.AddCell("Id");
            table.AddCell("Status");
            table.AddCell("CPF");
            table.AddCell("Name");
            table.AddCell("Login");
            table.AddCell("Email");
            table.AddCell("Phone");
            table.AddCell("BirthDate");
            table.AddCell("MotherName");
            table.AddCell("InclusionDate");
            table.AddCell("LastChangeDate");

            foreach (var user in users)
            {
                table.AddCell(user.Id.ToString());
                table.AddCell(user.Status.ToString());
                table.AddCell(user.CPF);
                table.AddCell(user.Name);
                table.AddCell(user.Login);
                table.AddCell(user.Email);
                table.AddCell(user.Phone);
                table.AddCell(user.BirthDate.ToString("dd/MM/yyyy"));
                table.AddCell(user.MotherName);
                table.AddCell(user.InclusionDate.ToString("dd/MM/yyyy"));
                table.AddCell(user.LastChangeDate.ToString("dd/MM/yyyy"));
            }

            document.Add(table);
            document.Close();

            stream.Position = 0;
            return new ExportListUsersResult
            {
                Stream = stream,
                Headers = "application/pdf",
                FileName = "Users.pdf"
            };
        }

        private static ExportListUsersResult ExportListUsersWord(List<User> users)
        {
            users.Add(new User());

            var stream = new MemoryStream();

            using (var wordDocument = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document))
            {
                var mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();

                var body = mainPart.Document.AppendChild(new Body());
                var heading = body.AppendChild(new Paragraph());
                var headingRun = heading.AppendChild(new Run());

                headingRun.AppendChild(new Text("Lista de Usuários"));
                headingRun.RunProperties = new RunProperties(new Bold());

                var table = new Table();
                var headerRow = new TableRow();

                headerRow.Append(
                    new TableCell(new Paragraph(new Run(new Text("Id")))),
                    new TableCell(new Paragraph(new Run(new Text("Status")))),
                    new TableCell(new Paragraph(new Run(new Text("CPF")))),
                    new TableCell(new Paragraph(new Run(new Text("Name")))),
                    new TableCell(new Paragraph(new Run(new Text("Login")))),
                    new TableCell(new Paragraph(new Run(new Text("Email")))),
                    new TableCell(new Paragraph(new Run(new Text("Phone")))),
                    new TableCell(new Paragraph(new Run(new Text("BirthDate")))),
                    new TableCell(new Paragraph(new Run(new Text("MotherName")))),
                    new TableCell(new Paragraph(new Run(new Text("InclusionDate")))),
                    new TableCell(new Paragraph(new Run(new Text("LastChangeDate"))))
                );
                table.Append(headerRow);

                foreach (var user in users)
                {
                    var dataRow = new TableRow();
                    dataRow.Append(
                        new TableCell(new Paragraph(new Run(new Text(user.Id.ToString())))),
                        new TableCell(new Paragraph(new Run(new Text(user.Status.ToString())))),
                        new TableCell(new Paragraph(new Run(new Text(user.CPF)))),
                        new TableCell(new Paragraph(new Run(new Text(user.Name)))),
                        new TableCell(new Paragraph(new Run(new Text(user.Login)))),
                        new TableCell(new Paragraph(new Run(new Text(user.Email)))),
                        new TableCell(new Paragraph(new Run(new Text(user.Phone)))),
                        new TableCell(new Paragraph(new Run(new Text(user.BirthDate.ToString("dd/MM/yyyy"))))),
                        new TableCell(new Paragraph(new Run(new Text(user.MotherName)))),
                        new TableCell(new Paragraph(new Run(new Text(user.InclusionDate.ToString("dd/MM/yyyy"))))),
                        new TableCell(new Paragraph(new Run(new Text(user.LastChangeDate.ToString("dd/MM/yyyy")))))
                    );
                    table.Append(dataRow);
                }

                body.Append(table);
            }

            stream.Position = 0;
            return new ExportListUsersResult
            {
                Stream = stream,
                Headers = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                FileName = "Users.docx"
            };
        }
    }
}