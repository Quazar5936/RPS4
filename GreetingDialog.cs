using System.Drawing;
using System.Windows.Forms;
namespace KR33
{
    public class GreetingDialog : Form
    {
        public GreetingDialog()
        {
            
            Text = "Приветствие";
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            TopMost = true;

            
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Padding = new Padding(20, 20, 20, 15);
            MinimumSize = new Size(400, 200); 

            
            var lbl = new Label
            {
                Text = "Контрольная работа 3, автор: Заиграев С.С., студент группы 444. Вариант 9.\n" +
                       "Программа строит график кубики Чирнгауза, позволяя загрузить данные из таблицы Excel или сохранить данные в таблицу Excel",
                AutoSize = true,
                MaximumSize = new Size(360, 0), 
                Margin = new Padding(0, 0, 0, 20), 
                TextAlign = ContentAlignment.MiddleLeft
            };

            
            var btnOk = new Button
            {
                Text = "Ок",
                Size = new Size(90, 30),
                Margin = new Padding(5, 0, 0, 0) 
            };

            var btnDontShow = new Button
            {
                Text = "Больше не показывать",
                Size = new Size(200, 30),
                Margin = new Padding(5, 0, 0, 0)
            };

            
            btnOk.Click += (s, e) => { DialogResult = DialogResult.OK; Close(); };
            btnDontShow.Click += (s, e) =>
            {
                Properties.Settings.Default.ShowGreeting = false;
                Properties.Settings.Default.Save();
                DialogResult = DialogResult.OK;
                Close();
            };

            AcceptButton = btnOk; 

            
            var btnPanel = new FlowLayoutPanel
            {
                AutoSize = true,
                FlowDirection = FlowDirection.RightToLeft,
                Dock = DockStyle.Bottom, 
                Padding = new Padding(0, 10, 0, 0)
            };
            btnPanel.Controls.Add(btnOk);
            btnPanel.Controls.Add(btnDontShow);

            
            Controls.Add(lbl);
            Controls.Add(btnPanel);
        }
    }
}