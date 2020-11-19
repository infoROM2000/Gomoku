using System;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Media;

namespace Gomoku
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static Brush culoare_background = Brushes.Gray;
        static int rowNr = 19;
        static int columnNr = 19;
        static int conditie_castigare = 5;
        int[,] piese = new int[rowNr, columnNr];
        public MainWindow()
        {
            InitializeComponent();
            creare_butoane();
            valori_initiale();
        }
        void creare_butoane()
        {
            for (int i = 0; i < rowNr; i++)
                for (int j = 0; j < columnNr; j++)
                {
                    Button button = new Button()
                    {
                        Background = culoare_background,
                        Name = "b_" + i.ToString() + '_' + j.ToString()
                    };
                    button.Click += new RoutedEventHandler(muta_jucator);
                    RegisterName(button.Name, button);
                    grid.Children.Add(button);
                }
        }
        void muta_jucator(object sender, RoutedEventArgs e)
        {
            var but = sender as Button;
            String[] coordonate = but.Name.ToString().Split('_');
            int rand = Int32.Parse(coordonate[1]);
            int coloana = Int32.Parse(coordonate[2]);
            if (piese[rand, coloana] == 0)
            {
                but.Background = Brushes.Black;
                piese[rand, coloana] = 1;
                Trace.WriteLine("Ai apasat butonul: " + but.Name);
                if (Gata(rand, coloana))
                {
                    Trace.WriteLine("AI castigator!");
                    Environment.Exit(1);
                }
                muta_calculator(dificultate.SelectedItem.ToString(), algoritm.SelectedItem.ToString());
            }
            else
            {
                Trace.WriteLine("Deja ocupat!");
            }
        }
        void muta_calculator(String dificultate, String algoritm)
        {
            string antet = "System.Windows.Controls.ListBoxItem: ";
            dificultate = dificultate.Remove(0, antet.Length);
            algoritm = algoritm.Remove(0, antet.Length);
            Random rnd = new Random();
            int i = 0, j = 0;
            switch (algoritm)
            {
                case "Random*":
                    do
                    {
                        i = rnd.Next(0, rowNr);
                        j = rnd.Next(0, columnNr);
                    }
                    while (piese[i, j] != 0);
                    break;
            }
            piese[i, j] = -1;
            string nume = "b_" + i.ToString() + '_' + j.ToString();
            Trace.WriteLine("Am ales: " + i + " " + j);
            Button but = grid.FindName(nume) as Button;
            but.Background = Brushes.White;
            if (Gata(i, j))
            {
                Trace.WriteLine("AI castigator!");
                Environment.Exit(1);
            }
        }
        void valori_initiale()
        {
            dificultate.SelectedItem = dificultate.Items.GetItemAt(0);
            algoritm.SelectedItem = algoritm.Items.GetItemAt(0);
        }
        bool Gata(int i, int j)
        {
            int ci = i;
            int cj = j;
            int linie = 0;
            int culoare = piese[i, j];
            //stanga
            while (cj >= 0 && piese[ci, cj] == culoare)
            {
                linie++;
                cj--;
                if (linie == conditie_castigare)
                    return true;
            }

            //dreapta
            cj = j + 1;
            while (cj < columnNr && piese[ci, cj] == culoare)
            {
                linie++;
                cj++;
                if (linie == conditie_castigare)
                    return true;
            }
            linie = 0;
            //sus
            cj = j;
            while (ci >= 0 && piese[ci, cj] == culoare)
            {
                linie++;
                ci--;
                if (linie == conditie_castigare)
                    return true;
            }
            //jos
            ci = i + 1;
            while (ci < rowNr && piese[ci, cj] == culoare)
            {
                linie++;
                ci++;
                if (linie == conditie_castigare)
                    return true;
            }

            linie = 0;
            ci = i;
            //diagonal stanga sus
            while(ci>=0 && cj>=0 && piese[ci,cj]==culoare)
            {
                linie++;
                ci--;
                cj--;
                if (linie == conditie_castigare)
                    return true;
            }
            ci = i + 1;
            cj = j + 1;
            //diagonal dreapta jos
            while (ci < rowNr && cj < columnNr && piese[ci, cj] == culoare)
            {
                linie++;
                ci++;
                cj++;
                if (linie == conditie_castigare)
                    return true;
            }

            linie = 0;
            ci = i;
            cj = j;
            //diagonal stanga jos
            while (ci < rowNr && cj >= 0 && piese[ci, cj] == culoare)
            {
                linie++;
                ci++;
                cj--;
                if (linie == conditie_castigare)
                    return true;
            }
            ci = i - 1;
            cj = j + 1;
            //diagonal dreapta sus
            while (ci >=0 && cj < columnNr && piese[ci, cj] == culoare)
            {
                linie++;
                ci--;
                cj++;
                if (linie == conditie_castigare)
                    return true;
            }
            return false;
        }

    }
}
