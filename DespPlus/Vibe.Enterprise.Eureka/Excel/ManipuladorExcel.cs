//using DocumentFormat.OpenXml;
//using DocumentFormat.OpenXml.Packaging;
//using DocumentFormat.OpenXml.Spreadsheet;
//using System;
//using System.IO;
//using System.Linq;
//using System.Text;
//using PowerDev.Enterprise.Eureka.Extensoes;

//namespace PowerDev.Enterprise.Eureka.Excel
//{
//    public class ManipuladorExcel
//    {
//        private SpreadsheetDocument Documento { get; set; }
//        public ManipuladorExcel(string caminhoArquivo)
//        {
//            var documento = SpreadsheetDocument.Open(caminhoArquivo, true);
//            Documento = (SpreadsheetDocument)documento.Clone();
//            documento.Close();
//        }
//        public ManipuladorExcel(byte[] arquivo)
//        {
//            var stream = new MemoryStream(arquivo);
//            Documento = SpreadsheetDocument.Open(stream, true);
//        }
//        public void FecharDocumento()
//        {
//            Documento.Close();
//        }
//        public void EscreverNaCelula(int linha, int coluna, object valorCelula, string nomePlanilha = "")
//        {
//            if (Documento == null)
//                throw new Exception("Você precisa preparar um documento antes de editar uma célula");

//            SharedStringTablePart shareStringPart;
//            if (Documento.WorkbookPart.GetPartsOfType<SharedStringTablePart>().Count() > 0)
//                shareStringPart = Documento.WorkbookPart.GetPartsOfType<SharedStringTablePart>().First();
//            else
//                shareStringPart = Documento.WorkbookPart.AddNewPart<SharedStringTablePart>();

//            var indiceTabela = InserirItemStringCompartilhada(valorCelula.ToString(), shareStringPart);
//            var pastaTrabalho = nomePlanilha.LimpoNuloBranco() ? Documento.WorkbookPart.WorksheetParts.FirstOrDefault()
//                : (WorksheetPart)Documento.WorkbookPart.GetPartById(Documento.WorkbookPart.Workbook.Descendants<Sheet>().First(s => nomePlanilha.Equals(s.Name)).Id);

//            Cell cell = InserirOuObterCelula(linha, coluna, pastaTrabalho.Worksheet);

//            var tipoValor = valorCelula.GetType();

//            switch (Type.GetTypeCode(tipoValor))
//            {
//                case TypeCode.String:
//                    cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
//                    break;
//                case TypeCode.Int16:
//                case TypeCode.Int32:
//                    cell.DataType = new EnumValue<CellValues>(CellValues.Number);
//                    break;
//                case TypeCode.Decimal:
//                case TypeCode.Double:
//                    cell.DataType = new EnumValue<CellValues>(CellValues.Number);
//                    cell.StyleIndex = 43;
//                    break;
//                case TypeCode.DateTime:
//                    cell.DataType = new EnumValue<CellValues>(CellValues.Date);
//                    break;
//                case TypeCode.Boolean:
//                    cell.DataType = new EnumValue<CellValues>(CellValues.Boolean);
//                    break;
//                default:
//                    cell.DataType = new EnumValue<CellValues>(CellValues.SharedString);
//                    break;
//            }
//            cell.CellValue = new CellValue(indiceTabela.ToString());

//            pastaTrabalho.Worksheet.Save();
//        }
//        public byte[] ObterBytesDocumento()
//        {
//            using (var ms = new MemoryStream())
//            {
//                Documento.Clone(ms);
//                FecharDocumento();
//                return ms.ToArray();
//            }
//        }
//        private static Cell InserirOuObterCelula(int linha, int coluna, Worksheet pastaTrabalho)
//        {
//            var indiceLinha = (uint)linha;
//            var nomeColuna = NumeroColunaParaString(coluna);
//            var referenciaCelula = $"{nomeColuna}{linha}";

//            SheetData sheetData = pastaTrabalho.GetFirstChild<SheetData>();

//            // If the worksheet does not contain a row with the specified row index, insert one.
//            Row row;
//            if (sheetData.Elements<Row>().Where(r => r.RowIndex == indiceLinha).Count() != 0)
//                row = sheetData.Elements<Row>().Where(r => r.RowIndex == indiceLinha).First();
//            else
//            {
//                row = new Row() { RowIndex = indiceLinha };
//                sheetData.Append(row);
//            }

//            // If there is not a cell with the specified column name, insert one.  
//            if (row.Elements<Cell>().Where(c => c.CellReference.Value == nomeColuna + indiceLinha).Count() > 0)
//                return row.Elements<Cell>().Where(c => c.CellReference.Value == referenciaCelula).First();
//            else
//            {
//                // Cells must be in sequential order according to CellReference. Determine where to insert the new cell.
//                Cell refCell = null;
//                foreach (Cell cell in row.Elements<Cell>())
//                {
//                    if (string.Compare(cell.CellReference.Value, referenciaCelula, true) > 0)
//                    {
//                        refCell = cell;
//                        break;
//                    }
//                }

//                Cell newCell = new Cell() { CellReference = referenciaCelula };
//                row.InsertBefore(newCell, refCell);

//                pastaTrabalho.Save();
//                return newCell;
//            }
//        }
//        private int InserirItemStringCompartilhada(string texto, SharedStringTablePart shareStringPart)
//        {
//            // If the part does not contain a SharedStringTable, create one.
//            if (shareStringPart.SharedStringTable == null)
//            {
//                shareStringPart.SharedStringTable = new SharedStringTable();
//            }

//            int i = 0;

//            // Iterate through all the items in the SharedStringTable. If the text already exists, return its index.
//            foreach (SharedStringItem item in shareStringPart.SharedStringTable.Elements<SharedStringItem>())
//            {
//                if (item.InnerText == texto)
//                {
//                    return i;
//                }

//                i++;
//            }

//            // The text does not exist in the part. Create the SharedStringItem and return its index.
//            shareStringPart.SharedStringTable.AppendChild(new SharedStringItem(new DocumentFormat.OpenXml.Spreadsheet.Text(texto)));
//            shareStringPart.SharedStringTable.Save();

//            return i;
//        }
//        public int ObterNumeroLinhas(string nomePlanilha)
//        {
//            var planilha = Documento.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == nomePlanilha).FirstOrDefault();
//            var pastaTrabalho = (WorksheetPart)Documento.WorkbookPart.GetPartById(planilha.Id);
//            return ObterNumeroLinhas(pastaTrabalho.Worksheet.Elements<SheetData>().FirstOrDefault());
//        }
//        public int ObterNumeroLinhas()
//        {
//            var planilha = Documento.WorkbookPart.Workbook.Descendants<Sheet>().FirstOrDefault();
//            var pastaTrabalho = (WorksheetPart)Documento.WorkbookPart.GetPartById(planilha.Id);
//            return ObterNumeroLinhas(pastaTrabalho.Worksheet.Elements<SheetData>().FirstOrDefault());
//        }
//        private int ObterNumeroLinhas(SheetData dadosPlanilha)
//        {
//            return dadosPlanilha.Elements<Row>().Count();
//        }
//        public object LerCelula<T>(int linha, int coluna)
//        {
//            var planilha = Documento.WorkbookPart.Workbook.Descendants<Sheet>().FirstOrDefault();
//            return LerCelula<T>(linha, coluna, planilha);
//        }
//        public object LerCelula<T>(int linha, int coluna, string nomePlanilha)
//        {
//            var planilha = Documento.WorkbookPart.Workbook.Descendants<Sheet>().Where(s => s.Name == nomePlanilha).FirstOrDefault();
//            return LerCelula<T>(linha, coluna, planilha);
//        }
//        private object LerCelula<T>(int linha, int coluna, Sheet planilha)
//        {
//            var pastaTrabalho = (WorksheetPart)Documento.WorkbookPart.GetPartById(planilha.Id);
//            var celula = pastaTrabalho.Worksheet.Descendants<Cell>().Where(c => c.CellReference == $"{NumeroColunaParaString(coluna)}{linha}").FirstOrDefault();

//            object value = null;

//            if (celula != null)
//            {
//                value = celula.InnerText;

//                if (celula.DataType != null)
//                {
//                    switch (celula.DataType.Value)
//                    {
//                        case CellValues.SharedString:
//                            var stringTable = Documento.WorkbookPart.GetPartsOfType<SharedStringTablePart>().FirstOrDefault();

//                            if (stringTable != null)
//                                return stringTable.SharedStringTable.ElementAt(int.Parse((string)value)).InnerText;
//                            break;

//                        case CellValues.Boolean:
//                            switch (value)
//                            {
//                                case "0":
//                                    return false;
//                                default:
//                                    return true;
//                            }
//                    }
//                }
//            }

//            return value;
//        }
//        private static string NumeroColunaParaString(int coluna)
//        {
//            int dividendo = coluna;
//            int modulo;
//            var sb = new StringBuilder();
//            while (dividendo > 0)
//            {
//                modulo = (dividendo - 1) % 26;
//                sb.Insert(0, Convert.ToChar(65 + modulo));
//                dividendo = (dividendo - modulo) / 26;
//            }

//            return sb.ToString();
//        }
//    }
//}