using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Web;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.comboBox1.Items.Add(@"https://lenta.ru/rss/news/"); //Новости "Ленты" для примера
            this.comboBox1.SelectedIndex = 0;
            this.flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;//Статьи располагаются сверху вниз
            this.label1.Text = "Настройки";
            this.checkBox1.Enabled = this.checkBox1.Visible = false; //Изначально, чек бокс не виден
                                                                     //Отобоазится при выборе остальных настроек
            this.comboBox2.Items.Add("Частота обновления ленты");
            this.comboBox2.Items.Add("Отображение заголовка");
            this.comboBox2.Items.Add("Отображение описания");
            this.comboBox2.Items.Add("Отображение даты публикации");
            this.comboBox2.SelectedIndex = 0;
            this.checkBox1.Text = "";
            this.button1.Text = "Добавить ленту";
            this.timer = new Timer();
            this.timer.Interval = Settings.Frequency;
            this.timer.Tick += new System.EventHandler(this.update);//По истечении таймера, обновляем ленту
            this.timer.Start();
            this.maskedTextBox1.Text = 
                (Settings.Frequency / (60 * 1000) < 10 ? "0" + Settings.Frequency / (60 * 1000) 
                : Settings.Frequency / (60 * 1000) + "" ) +
                ":" +
                (((Settings.Frequency % (60 * 1000)))/1000 < 10 ? "0" + (Settings.Frequency % (60 * 1000)) / 100 : 
                ((Settings.Frequency % (60 * 1000))) / 1000 + "");//Сложная формула для получения времени из милисекунд в формат MM:SS
            this.update();
        }
        

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.update();
        }

        private void update(object sender = null, EventArgs e = null)
        {
            this.flowLayoutPanel1.Controls.Clear(); //Очищаем контейнер от старых статей
            this.reader.getNewArticles(this.comboBox1.Text); //Получаем текущую ленту
            Article article;
            foreach (var art in this.reader.articles)
            {
                article = new Article();
                article.setSize(Form1.layoutWidth, 50);//Устанавливаем ширину статьи по размеру контейнера
                article.Link = art.link;
                article.Description.Text = art.description + Environment.NewLine;
                article.PubDate.Text = art.pubDate + Environment.NewLine + Environment.NewLine;
                article.Title.Text = art.title + Environment.NewLine + Environment.NewLine;
                article.add(this.flowLayoutPanel1);
                article.visibleOfTitle = Settings.IsShowTitle;
                article.visibleOfDescription = Settings.IsShowDescription;
                article.visibleOfPubDate = Settings.IsShowPubDate;

            }
            
        }
        
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {
                case (int)NamesOfSettings.FrequencySetting:
                    {
                        this.switchTextAndCheckBoxes(false);
                        break;
                    }
                case (int)NamesOfSettings.TitleSetting:
                    {
                        this.switchTextAndCheckBoxes(true); //true - checkBox; false - textBox
                        this.checkBox1.Checked = Settings.IsShowTitle;
                        break;
                    }
                case (int)NamesOfSettings.DescriptionSetting:
                    {
                        this.switchTextAndCheckBoxes(true); //true - checkBox; false - textBox
                        this.checkBox1.Checked = Settings.IsShowDescription;
                        break;
                    }
                case (int)NamesOfSettings.PubDateSetting:
                    {
                        this.switchTextAndCheckBoxes(true); //true - checkBox; false - textBox
                        this.checkBox1.Checked = Settings.IsShowPubDate;
                        break;
                    }
            }
        }
        private void checkBox_CheckIsChanged(object sender, EventArgs e)
        {
            switch (this.comboBox2.SelectedIndex)
            {
                case (int)NamesOfSettings.TitleSetting:
                    {
                        if (Settings.IsShowTitle == this.checkBox1.Checked) return;
                        Settings.IsShowTitle = this.checkBox1.Checked;
                        break;
                    }
                case (int)NamesOfSettings.DescriptionSetting:
                    {
                        if (Settings.IsShowDescription == this.checkBox1.Checked) return;
                        Settings.IsShowDescription = this.checkBox1.Checked;
                        break;
                    }
                case (int)NamesOfSettings.PubDateSetting:
                    {
                        if (Settings.IsShowPubDate == this.checkBox1.Checked) return;
                        Settings.IsShowPubDate = this.checkBox1.Checked;
                        break;
                    }

            }
            this.update();
        }
        private void switchTextAndCheckBoxes(bool isCheckBox)//Изменяет отображаемость чекбокса на текст бокс и наоборот
        {
            this.checkBox1.Visible = this.checkBox1.Enabled = isCheckBox;
            this.maskedTextBox1.Enabled = this.maskedTextBox1.Visible = !isCheckBox;
            this.maskedTextBox1.Enabled = !isCheckBox;
        }
        
        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            string time = this.maskedTextBox1.Text.Split(' ')[0];//Так так текст хранится под маской, можно быть увереным, что формат верен
            string[] splitedTime = time.Split(':');
            Settings.Frequency = Int32.Parse(splitedTime[0]) * 1000 * 60 + Int32.Parse(splitedTime[1]) * 1000;//Перевод из MM:SS в милисекунды
            Console.WriteLine(this.maskedTextBox1.Text);
        }
        private void saveSettings(object sender, EventArgs e)
        {
            Settings.save();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.textBox2.Text != "" && !this.comboBox1.Items.Contains(this.textBox2.Text))//Если введенная лента не пуста и еще не содержится в списке
                this.comboBox1.Items.Add(this.textBox2.Text);
        }
    }

    enum NamesOfSettings : int { FrequencySetting = 0, TitleSetting, DescriptionSetting, PubDateSetting };
    //Енам для имен настроек
}
