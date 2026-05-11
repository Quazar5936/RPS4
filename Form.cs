using ClosedXML.Excel;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Spreadsheet;
using OpenTK.Graphics.OpenGL;
using ScottPlot;
using ScottPlot.MultiplotLayouts;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace KR33
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            visualData = new();
            dataGridView.DataSource = visualData;
            dataGridView.AutoGenerateColumns = true;
            A = 0; Step = 0;
            Greeting();
        }

        private void Greeting()
        {
            if (Properties.Settings.Default.ShowGreeting)
            {
                GreetingDialog greeting = new();
                greeting.Show();
            }
        }
        private bool CheckInputDataForChart(decimal leftBound, decimal rightBound, decimal a, decimal step)
        {
            if (a == 0)
            {
                MessageBox.Show("Ошибка", "Коэффициент a не может быть равен нулю", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (leftBound > rightBound)
            {
                MessageBox.Show("Ошибка", "Левая граница должна быть меньше правой", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (step <= 0)
            {
                MessageBox.Show("Ошибка", "Шаг не может быть отрицательным или равным нулю", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        static public decimal FormulaCalc(decimal x, decimal a)
        {
            return DecimalMath.DecimalEx.Sqrt(((a - x) * DecimalMath.DecimalEx.Pow((8 * a + x), 2)) / (27 * a));
        }

        private bool CountY(BindingList<VisualData> AllVisualData, decimal LeftBound, decimal RightBound, decimal Anew, decimal NewStep)
        {
            decimal y = 0;
            bool showWarning = false;

            for (decimal x = LeftBound; x <= RightBound; x += NewStep)
            {
                try
                {
                    y = FormulaCalc(x, Anew);
                }
                catch (ArgumentException)
                {
                    showWarning = true;
                    continue;
                }
                catch (Exception)
                {
                    MessageBox.Show("Неизвестная ошибка", "Аварийное завершение построения графика, введите другие данные", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                VisualData newData = new(x, y, -y);
                AllVisualData.Add(newData);
            }

            if (AllVisualData.Count() == 0)
            {
                MessageBox.Show("Требуется смена границ", "Данный диапазон не позволяет построить график, пожалуйста, введите другие границы", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            if (showWarning)
            {
                MessageBox.Show("Не все точки будут прорисованы", "График не будет полностью прорисован", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return true;
        }

        private void BuildChart()
        {
            formsPlot.Plot.Clear();

            dataGridView.Columns[0].Width = 100;

            formsPlot.Plot.Add.Scatter(visualData.Select(px => px.X).ToArray(), visualData.Select(py => py.Y).ToArray(), ScottPlot.Colors.Blue);
            formsPlot.Plot.Add.Scatter(visualData.Select(px => px.X).ToArray(), visualData.Select(pminusy => pminusy.minusY).ToArray(), ScottPlot.Colors.Blue);

            formsPlot.Plot.Grid.MajorLineColor = ScottPlot.Colors.Gray.WithAlpha(0.2);
            formsPlot.Plot.Axes.AutoScale();

            dataGridView.Refresh();
            formsPlot.Refresh();
        }

        private void BuildChartButton_Click(object sender, EventArgs e)
        {
            decimal leftBound = 0, rightBound = 0, a = 0, step = 0;
            BindingList<VisualData> allVisualData = new();

            if (!decimal.TryParse(LeftBoundTextBox.Text.Trim(), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out leftBound))
            {
                MessageBox.Show("Введите корректное число.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(RightBoundTextBox.Text.Trim(), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out rightBound))
            {
                MessageBox.Show("Введите корректное число.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(ATextBox.Text.Trim(), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out a))
            {
                MessageBox.Show("Введите корректное число.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!decimal.TryParse(StepTextBox.Text.Trim(), NumberStyles.Float | NumberStyles.AllowThousands, CultureInfo.CurrentCulture, out step))
            {
                MessageBox.Show("Введите корректное число.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!CheckInputDataForChart(leftBound, rightBound, a, step))
            {
                return;
            }

            if (!CountY(allVisualData, leftBound, rightBound, a, step))
            {
                return;
            }
            dataGridView.DataSource = null;
            visualData = allVisualData;
            dataGridView.DataSource = visualData;
            A = a;
            Step = step;
            BuildChart();
        }

        private void ExcelLoadButton_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Excel Files|*.xlsx;*.xls";
                openFileDialog.Title = "Открыть Excel файл";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        if (!LoadFromExcel(openFileDialog.FileName))
                        {
                            MessageBox.Show("Некорректное содержимое файла, выберите пожалуйста другой файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        BuildChart();
                        LeftBoundTextBox.Text = visualData[0].X.ToString();
                        RightBoundTextBox.Text = visualData[visualData.Count() - 1].X.ToString();
                        ATextBox.Text = A.ToString();
                        StepTextBox.Text = Step.ToString();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при загрузке:\n{ex.Message}", "Ошибка",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }


                }
            }
        }

        private bool LoadFromExcel(string fileName)
        {
            using (var workbook = new XLWorkbook(fileName))
            {
                var worksheet = workbook.Worksheet(1);
                BindingList<VisualData> excelVisualData = new();
                decimal a = 0, step = 0;

                
                var range = worksheet.RangeUsed();
                if (range == null) return false;

                int rowCount = range.RowCount();
                int colCount = range.ColumnCount();

                if (colCount != 5) return false;

                if (worksheet.Cell(1, ColX).Value.ToString() != "X" || worksheet.Cell(1, ColY).Value.ToString() != "Y" ||
                    worksheet.Cell(1, ColminusY).Value.ToString() != "minusY" || worksheet.Cell(1, ColA).Value.ToString() != "A"
                    || worksheet.Cell(1, ColStep).Value.ToString() != "step")
                {
                    return false;
                }

                try
                {
                    for (int row = 2; row <= rowCount; row++)
                    {
                       
                        for (int col = 1; col < colCount - 1; col++)
                        {
                            if (worksheet.Cell(row, col).IsEmpty())
                            {
                                return false;
                            }
                        }

                        
                        decimal X = worksheet.Cell(row, ColX).GetValue<decimal>();
                        decimal Y = worksheet.Cell(row, ColY).GetValue<decimal>();
                        decimal minusY = worksheet.Cell(row, ColminusY).GetValue<decimal>();
                        VisualData rowData = new(X, Y, minusY);
                        excelVisualData.Add(rowData);
                    }

                    a = worksheet.Cell(2, ColA).GetValue<decimal>();
                    step = worksheet.Cell(2, ColStep).GetValue<decimal>();
                }
                catch (Exception)
                {
                    return false;
                }

                if (!CheckInputDataForChart(excelVisualData[0].X, excelVisualData[excelVisualData.Count() - 1].X, a, step))
                {
                    return false;
                }

                BindingList<VisualData> checkVisualData = new();

                if (!CountY(checkVisualData, excelVisualData[0].X, excelVisualData[excelVisualData.Count() - 1].X, a, step))
                {
                    return false;
                }

                if (excelVisualData.Count != checkVisualData.Count)
                {
                    return false;
                }

                for (int i = 0; i < checkVisualData.Count; i++)
                {
                    if ((Math.Abs(excelVisualData[i].X - checkVisualData[i].X) > Epsilon) || (Math.Abs(excelVisualData[i].Y - checkVisualData[i].Y) > Epsilon))
                    {
                        return false;
                    }
                }

                dataGridView.DataSource = null;
                visualData = excelVisualData;
                dataGridView.DataSource = visualData;
                A = a;
                Step = step;
            }

            return true;
        }

        private void ExcelSaveButton_Click(object sender, EventArgs e)
        {
            using (var saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "Excel Files|*.xlsx";
                saveDialog.Title = "Сохранить в Excel";
                saveDialog.FileName = $"Data_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    SaveToExcel(saveDialog.FileName);
                }
            }
        }

        private void SaveToExcel(string fileName)
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Data");

               
                for (int i = 0; i < dataGridView.Columns.Count; i++)
                {
                    worksheet.Cell(1, i + 1).Value = dataGridView.Columns[i].HeaderText;
                    worksheet.Cell(1, i + 1).Style.Font.Bold = true;
                }

                worksheet.Cell(1, ColA).Value = "A";
                worksheet.Cell(1, ColA).Style.Font.Bold = true;
                worksheet.Cell(1, ColStep).Value = "step";
                worksheet.Cell(1, ColStep).Style.Font.Bold = true;

              
                for (int i = 0; i < dataGridView.Rows.Count; i++)
                {
                    if (dataGridView.Rows[i].IsNewRow) continue;

                    for (int j = 0; j < dataGridView.Columns.Count; j++)
                    {
                        worksheet.Cell(i + 2, j + 1).Value = Convert.ToDecimal(dataGridView.Rows[i].Cells[j].Value);
                    }
                }
                worksheet.Cell(2, ColA).Value = A;
                worksheet.Cell(2, ColStep).Value = Step;

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(fileName);
            }
        }

        private void AuthorButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Контрольная работа 3, автор: Заиграев С.С., студент группы 444. Вариант 9. \nПрограмма строит график кубики Чирнгауза, позволяя загрузить данные из таблицы Excel или сохранить данные в таблицу Excel", "Приветствие");
        }
    }
}
