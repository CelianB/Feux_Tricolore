using System;
using System.Timers;

namespace Feux
{
    //Application console simulant un carrefour à N voies.
    class Feuxtricolore
    {
        public static int periode;
        public static int n, voiesaisie;
        public static int[] Tempsfeu;
        public static string[] Etat_feu;
        public static int voies, sec, valparite = 0;
        public static bool parite = false, OKimpaire = false;

        static void Main(string[] args)
        {
            Etat_feu = new string[] { "R", "V", "O" };
            Tempsfeu = new int[] { 2, 1, 2 };
            periode = 0;
            for (int k = 0; k < Tempsfeu.Length; k++) //Initialise la periode total
            {
                periode += Tempsfeu[k];
            }

            Timer t;
            t = new Timer(1000);
            t.Elapsed += T_Elapsed;//Evenement tick timer

            string s;
            do
            {
                Console.Clear();
                Console.WriteLine("Veuillez saisir un entier supérieur ou égal à 4");
                Console.Write("Nombre de voies :");
                s = Console.ReadLine();
            }
            while ((!Int32.TryParse(s, out voiesaisie)) || (Int32.Parse(s) < 4));//tq pas d'entier
            Console.WriteLine("Gestion de feu tricolore pour un carrefour à " + voiesaisie + " voies :");
            Console.WriteLine("------");
            for (int j = 1; j <= voiesaisie; j++)//Numeros des voies
            {
                Console.Write(j + "" + Espace(5));
            }
            Console.WriteLine();
            for (int i = 0; i < voiesaisie; i++)//1er ligne rouge
            {
                Ecrire(0);
                Console.Write(Espace(5));
            }
            Console.WriteLine("");
            testparite(voiesaisie);//pair ou impaire 
            while (true)
            {
                t.Start();//start timer
                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
            }
        }


        private static void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            //1sec
            //Cycle : 25,5,25

            if ((parite == false) && (OKimpaire == false))//Si impaire 
            {
                if (sec <= periode)//periode d'une voie
                {
                    if (sec == Tempsfeu[0] + Tempsfeu[1])
                    {
                        GestionVoieImpaire(1);
                    }
                    if (sec == Tempsfeu[0] + Tempsfeu[1] + Tempsfeu[2])
                    {
                        GestionVoieImpaire(2);
                    }
                }
                else
                {
                    sec = 0;
                    OKimpaire = true;
                }
            }

            if (voies <= (n - 1) / 2)//Numero de voie
            {
                if (OKimpaire == true)
                {
                    if (sec <= periode)//periode d'une voie
                    {
                        if (sec == Tempsfeu[0] + Tempsfeu[1])
                        {
                            Update(voies, 1);
                        }
                        if (sec == Tempsfeu[0] + Tempsfeu[1] + Tempsfeu[2])
                        {
                            Update(voies, 2);
                        }
                    }
                    else
                    {
                        voies++;
                        sec = 0;
                    }
                }
            }
            else
            {
                OKimpaire = false;
                voies = 0;
            }

            if (parite == true)//Si paire 
            {
                if (voies <= n - 1)//Numero de voie
                {
                    if (sec <= periode)//periode d'une voie
                    {
                        if (sec == Tempsfeu[0] + Tempsfeu[1])
                        {
                            Update(voies, 1);
                        }
                        if (sec == Tempsfeu[0] + Tempsfeu[1] + Tempsfeu[2])
                        {
                            Update(voies, 2);
                        }
                    }
                    else
                    {
                        voies++;
                        sec = 0;
                    }
                }
                else
                {
                    sec = 0; voies = 0;
                }
            }
            sec++;
        }

        public static void Update(int voie, int etat)
        {
            if (parite == true)
            {
                for (int i = 0; i < voiesaisie; i++)
                {
                    if (i == voie)
                    {
                        Ecrire(etat);
                    }
                    else if ((i == voie + n / 2) || (i == voie - n / 2))
                    {
                        Ecrire(etat);
                    }
                    else
                    {
                        Ecrire(0);
                    }
                    Console.Write(Espace(5));
                }
                Console.WriteLine();
            }
            else
            {
                for (int i = 0; i < voiesaisie; i++)
                {
                    if (i == voie + 1)
                    {
                        Ecrire(etat);
                    }
                    else if (((i != 0) && (i == voie + 1 - n / 2)) || (i == voie + 1 + n / 2))
                    {
                        Ecrire(etat);
                    }
                    else
                    {
                        Ecrire(0);
                    }
                    Console.Write(Espace(5));

                }
                Console.WriteLine();
            }
        }


        public static void GestionVoieImpaire(int etat)
        {
            for (int i = 0; i < voiesaisie; i++)
            {
                if (i == 0)
                {
                    Ecrire(etat);
                }
                else
                {
                    Ecrire(0);
                }
                Console.Write(Espace(5));
            }
            Console.WriteLine();
        }

        public static int testparite(int voiesaisie)
        {
            if (voiesaisie % 2 == 0)
            {
                valparite = 0;
                n = voiesaisie;
                parite = true;
                return n;
            }
            else
            {
                valparite = 1;
                parite = false;
                return n = voiesaisie - 1;
            }
        }

        public static string Espace(int val)
        {
            string espace = "";
            for (int o = 0; o <= val; o++)
            {
                espace += " ";
            }
            return espace;
        }
        public static void Ecrire(int couleur)
        {
            if (couleur == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(Etat_feu[couleur]);
            }
            else if (couleur == 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(Etat_feu[couleur]);
            }
            else if (couleur == 2)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(Etat_feu[couleur]);
            }
        }
    }
}

