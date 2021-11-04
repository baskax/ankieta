using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace ankieta
{
    public partial class Form1 : Form
    {
        private string[] pytania;
        private string[] odpowiedzi;
        private int aktualnePytanie = 0;
        public Form1()
        {
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fileLocation = Path.Combine(executableLocation, "plik.txt");
            int count = 0;
            foreach (string line in File.ReadLines(fileLocation))
            {
                if (line == "---") break;
                count++;
            }
            pytania = new string[count];
            odpowiedzi = new string[count];
            for (int i = 0; i < count; i++)
            {
                pytania[i] = File.ReadLines(fileLocation).Skip(i).Take(1).First();
            }

            InitializeComponent();
            Pytanie();
        }

        private void Pytanie()
        {
            if (aktualnePytanie == pytania.Length - 1)
            {
                buttonNext.Text = "Koniec";
            }
            else
            {
                buttonNext.Text = "Dalej";
            }
            if (aktualnePytanie == 0)
            {
                buttonBack.Enabled = false;
            }
            else
            {
                buttonBack.Enabled = true;
            }
            string[] pytanie = pytania[aktualnePytanie].Split(';');
            groupBoxPytanie.Text = $"Pytanie {aktualnePytanie + 1} - {pytanie[0]}";
            radioButton1.Text = pytanie[1];
            radioButton2.Text = pytanie[2];
            radioButton3.Text = pytanie[3];

            switch (odpowiedzi[aktualnePytanie])
            {
                case "a":
                    radioButton1.Checked = true;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    break;
                case "b":
                    radioButton1.Checked = false;
                    radioButton2.Checked = true;
                    radioButton3.Checked = false;
                    break;
                case "c":
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = true;
                    break;
                default:
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                    radioButton3.Checked = false;
                    break;
            }


        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                odpowiedzi[aktualnePytanie] = "a";
            }
            else if (radioButton2.Checked == true)
            {
                odpowiedzi[aktualnePytanie] = "b";
            }
            else if (radioButton3.Checked == true)
            {
                odpowiedzi[aktualnePytanie] = "c";
            }

            if (aktualnePytanie == pytania.Length - 1)
            {
                Podsumowanie();
            }
            else
            {
                aktualnePytanie += 1;
                Pytanie();
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            aktualnePytanie -= 1;
            Pytanie();
        }

        private void Podsumowanie()
        {
            string odpowiedzi = String.Join("", this.odpowiedzi.ToArray());
            string executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fileLocation = Path.Combine(executableLocation, "plik.txt");
            string[] klucz = File.ReadLines(fileLocation).Last().Split(';');
            int[] odpowiedzicount = { odpowiedzi.Split('a').Length - 1, odpowiedzi.Split('b').Length - 1, odpowiedzi.Split('c').Length - 1, odpowiedzi.Split('d').Length - 1 }; // policz ile jest w odpowiedziach odpowiednio a b c d


            var indeksymaksow = odpowiedzicount.Select((n, i) => new { n, i })
                                    .GroupBy(x => x.n, x => x.i)
                                    .OrderByDescending(x => x.Key)
                                    .Take(1)
                                    .SelectMany(x => x)
                                    .ToArray();

            foreach (var i in indeksymaksow)
            {
                var form = new Form2(klucz[i]);
                form.Show();
            }
        }
    }
}
