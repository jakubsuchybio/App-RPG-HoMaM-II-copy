using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using HoMM.GameComponents;
using HoMM.TileEngine;

namespace HoMM.Libraries
{
    public static class Library
    {
        #region Methods

        /// <summary>
        /// Tato metoda prevede vstupni velky rectangle na maly rectangle do minimapy
        /// </summary>
        /// <param name="toScale">vstupni velky ctverec</param>
        /// <returns>vystupni maly ctverec</returns>
        public static Rectangle Scale(Rectangle toScale)
        {
            Rectangle tmp = new Rectangle(
                (toScale.X * Minimap.Rect.Width) / Session.FrontMap.WidthInPixels,
                (toScale.Y * Minimap.Rect.Height) / Session.FrontMap.HeightInPixels,
                (toScale.Width * Minimap.Rect.Width) / Session.FrontMap.WidthInPixels + 1,
                (toScale.Height * Minimap.Rect.Height) / Session.FrontMap.HeightInPixels + 1);
            return tmp;
        }

        /// <summary>
        /// Staticka metoda.
        /// Vraci vzdalenost dvou bodu v rovine.
        /// </summary>
        /// <param name="a">Prvni bod</param>
        /// <param name="b">Druhy bod</param>
        /// <returns></returns>
        public static double VzdalenostEuklidovska(double x1, double x2, double y1, double y2)
        {
            return System.Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
        }

        #endregion
    }

    public class Halda<T> where T : IComparable
    {
        #region Promenne

        private T[] halda;
        private int k;
        public int velikostHaldy;

        #endregion

        #region Konstruktor

        /// <summary>
        /// Inicializace binomicke haldy
        /// </summary>
        /// <param name="n">Pocet maximalne pridavanych vrcholu</param>
        public Halda(int n)
        {
            this.k = 2;
            halda = new T[n];
            velikostHaldy = 0;
        }
        /// <summary>
        /// Inicializace k-regularni haldy
        /// </summary>
        /// <param name="k">Nastaveni regularity haldy</param>
        /// <param name="n">Pocet maximalne pridavanych vrcholu</param>
        public Halda(int k, int n)
        {
            this.k = k;
            halda = new T[n];
            velikostHaldy = 0;
        }

        #endregion

        #region Vstupne/Vystupni metody

        /// <summary>
        /// Prida prvek na konec haldy.
        /// Provede kontrolu pravidel haldy pro pridany prvek.
        /// Pokud prvek narusil pravidla haldy, prvek se necha "probublat" nahoru dokud se neobnovi pravidla haldy.
        /// </summary>
        /// <param name="prvek">Prvek musi mit deklarovanou metodu CompareTo z interfacu IComparable</param>
        public void PridejPrvek(T prvek)
        {
            halda[velikostHaldy] = prvek;

            // Kontrola poruseni haldy, pred pridanim prvku
            int i = velikostHaldy;
            int otec = (i - 1) / k;
            while (i != 0)
            {
                if (prvek.CompareTo(halda[otec]) < 0)
                {
                    halda[i] = halda[otec];
                    i = otec;
                    otec = (i - 1) / k;
                }
                else
                    break;
            }
            //Na pozici, kde otec je mensi nez prvek pridam prvek (muze byt i koren)
            halda[i] = prvek;
            velikostHaldy++;
        }
        /// <summary>
        /// Odebere koren z haldy a nahradi ho poslednim prvkem haldy.
        /// Provede kontrolu pravidel haldy pro novy koren.
        /// Pokud novy koren narusil pravidla haldy, prvek se necha "probublat" dolu dokud se neobnovi pravidla haldy. 
        /// </summary>
        /// <returns>Vrati koren z haldy</returns>
        public T OdeberPrvek()
        {
            if (velikostHaldy != 0)
            {
                T vystup = halda[0];
                velikostHaldy--;
                T prvek = halda[velikostHaldy];
                if (prvek.CompareTo(halda[0]) <= 0)
                    halda[0] = prvek;
                else
                {
                    //Kontrola smerem dolu
                    int i = 0;
                    int nejmensisyn = k * i + 1; ;
                    while (i < velikostHaldy)
                    {
                        for (int j = 2; (j <= k) && (k * i + j < velikostHaldy); j++)
                        {
                            if (halda[nejmensisyn].CompareTo(halda[k * i + j]) > 0)
                                nejmensisyn = k * i + j;
                        }
                        if (halda[nejmensisyn].CompareTo(prvek) < 0)
                        {
                            halda[i] = halda[nejmensisyn];
                            i = nejmensisyn;
                            if (k * i + 1 < velikostHaldy)
                                nejmensisyn = k * i + 1;
                            else
                                break;
                        }
                        else
                            break;
                    }
                    halda[i] = prvek;
                }

                return vystup;
            }
            else
                return default(T);
        }

        #endregion
    }

    public class Mriz
    {
        #region Pomocne tridy

        public class Vrchol
        {
            private int x, y;
            public int X
            {
                get { return x; }
                set { x = value; }

            }
            public int Y
            {
                get { return y; }
                set { y = value; }
            }
            [XmlIgnoreAttribute()]
            public List<Vrchol> sousedi;

            public bool walkable;

            public Vrchol()
            {
                sousedi = new List<Vrchol>();
            }
            public Vrchol(int x, int y)
            {
                this.x = x;
                this.y = y;
                sousedi = new List<Vrchol>();
                walkable = true;
            }

            public override string ToString()
            {
                return x + " " + y;
            }

        }

        /// <summary>
        /// Pomocna tridicka pro PathFind ve mrizi.
        /// Ke kazdemu Vrcholu si pamatuju potrebny informace pro vyhledavani cesty.
        /// </summary>
        private class PFBod : IComparable
        {
            public Vrchol bod;
            public PFBod odkud;
            public double g, h, f;

            public PFBod()
            {
                g = -1;
                h = -1;
                f = -1;
                bod = null;
                odkud = null;
            }

            public int CompareTo(object b)
            {
                PFBod pom = b as PFBod;
                if (this.f < pom.f)
                    return -1;
                else if (this.f > pom.f)
                    return 1;
                else
                    return 0;
            }
        }

        #endregion

        #region Metody

        /// <summary>
        /// Vrati list vrcholuu sousedicich s vrcholem.
        /// </summary>
        /// <param name="vrchol">Vrchol, ke kteremu hledame sousedy</param>
        /// <param name="p">Mriz, ve ktere se sousedi hledaj</param>
        /// <param name="len">Velikost strany mrizky</param>
        private static List<Vrchol> GetSousedy(Vrchol vrchol, Vrchol[,] p, int len)
        {
            List<Vrchol> sousedi = new List<Vrchol>();
            if (vrchol.X - 1 >= 0)
            {
                if (vrchol.Y - 1 >= 0)
                {
                    if (p[vrchol.X - 1, vrchol.Y - 1].walkable)
                        sousedi.Add(p[vrchol.X - 1, vrchol.Y - 1]);
                }
                if (vrchol.Y + 1 < len)
                {
                    if (p[vrchol.X - 1, vrchol.Y + 1].walkable)
                        sousedi.Add(p[vrchol.X - 1, vrchol.Y + 1]);
                }
                if (p[vrchol.X - 1, vrchol.Y].walkable)
                    sousedi.Add(p[vrchol.X - 1, vrchol.Y]);
            }
            if (vrchol.X + 1 < len)
            {
                if (vrchol.Y - 1 >= 0)
                {
                    if (p[vrchol.X + 1, vrchol.Y - 1].walkable)
                        sousedi.Add(p[vrchol.X + 1, vrchol.Y - 1]);
                }
                if (vrchol.Y + 1 < len)
                {
                    if (p[vrchol.X + 1, vrchol.Y + 1].walkable)
                        sousedi.Add(p[vrchol.X + 1, vrchol.Y + 1]);
                }
                if (p[vrchol.X + 1, vrchol.Y].walkable)
                    sousedi.Add(p[vrchol.X + 1, vrchol.Y]);
            }
            if (vrchol.Y - 1 >= 0)
                if (p[vrchol.X, vrchol.Y - 1].walkable)
                    sousedi.Add(p[vrchol.X, vrchol.Y - 1]);
            if (vrchol.Y + 1 < len)
                if (p[vrchol.X, vrchol.Y + 1].walkable)
                    sousedi.Add(p[vrchol.X, vrchol.Y + 1]);
            return sousedi;
        }

        /// <summary>
        /// Vraci vzdalenost mezi dvema "sousednimi" body. 
        /// Jsou-li diagonalne vraci 1,41. 
        /// Jsou-li jinak(treba i teleport) vraci 1.
        /// </summary>
        /// <param name="a">Prvni vrchol</param>
        /// <param name="b">Druhy vrchol</param>
        /// <returns>Vzdalenost mezi nimi, bud 1 nebo 1.4</returns>
        private static double GetVzdalenost(Vrchol a, Vrchol b)
        {
            int x = Math.Abs(a.X - b.X);
            int y = Math.Abs(a.Y - b.Y);
            if (x == 1 & y == 1)
                return 1.41;
            else
                return 1;
        }

        /// <summary>
        /// Pomoci algoritmu A* hleda cestu z bodu start do bodu cil ve mrizce p
        /// Pouzita heuristika: Euklidovska
        /// </summary>
        /// <param name="start">Startovni bod</param>
        /// <param name="cil">Cilovy bod</param>
        /// <param name="p">Odkaz na mrizku</param>
        /// <param name="len">Velikost strany mrizky</param>
        /// <returns>Vraci list bodu, jako cestu ze startu do cile</returns>
        public static List<Vrchol> PathFind(Vrchol start, Vrchol cil, Vrchol[,] p, int len)
        {
            List<PFBod> closedset = new List<PFBod>();
            Halda<PFBod> openset = new Halda<PFBod>(2, len * len);
            PFBod[,] PFMriz = new PFBod[len, len];

            //Nadefinovani a pridani prvniho PFBodu
            {
                int x = start.X;
                int y = start.Y;
                if (PFMriz[x, y] == null)
                    PFMriz[x, y] = new PFBod();
                PFMriz[x, y].g = 0;
                PFMriz[x, y].h = Library.VzdalenostEuklidovska((double)start.X, (double)cil.X, (double)start.Y, (double)cil.Y);
                PFMriz[x, y].f = PFMriz[x, y].h;
                PFMriz[x, y].bod = start;
                openset.PridejPrvek(PFMriz[x, y]);
            }

            //Dokud mame vrcholy v halde ke zpracovani, tak zpracovavame
            while (openset.velikostHaldy > 0)
            {
                PFBod tmp = openset.OdeberPrvek();

                //Pokud jsme dosli do cile, zrekonstruuje cestu ze do startu a returnne ji
                if (tmp.bod == cil)
                {
                    List<Vrchol> pom = new List<Vrchol>();
                    do
                    {
                        pom.Add(tmp.bod);
                        tmp = tmp.odkud;
                    } while (tmp != null);
                    return pom;
                }

                //Reprezentuje uz zpracovane vrcholy
                closedset.Add(tmp);

                //Zjistim si sousedy kolem, pripadne pridam nejaky na ktery se muzu teleportovat z tmp.bodu
                List<Vrchol> sousedi;
                if (tmp.bod.sousedi == null)
                    sousedi = GetSousedy(tmp.bod, p, len);
                else
                {
                    sousedi = GetSousedy(tmp.bod, p, len);
                    foreach (Vrchol item in tmp.bod.sousedi)
                        sousedi.Add(item);
                }

                foreach (Vrchol item in sousedi)
                {
                    int x = item.X;
                    int y = item.Y;
                    //Preskocim vrcholy uz zpracovane
                    if (closedset.Contains(PFMriz[x, y]))
                        continue;

                    //Zjistim si vzdalenost do bodu item od pocatku pres bod tmp
                    double pom_g = tmp.g + GetVzdalenost(tmp.bod, item);

                    //Pokud jsem vrchol jeste nenavstivil, vytvorim ho, nadefinuju a vlozim ke zpracovani
                    if (PFMriz[x, y] == null)
                    {
                        PFMriz[x, y] = new PFBod();
                        PFMriz[x, y].g = pom_g;
                        PFMriz[x, y].h = Library.VzdalenostEuklidovska((double)item.X, (double)cil.X, (double)item.Y, (double)cil.Y);
                        PFMriz[x, y].f = PFMriz[x, y].g + PFMriz[x, y].h;
                        PFMriz[x, y].bod = item;
                        PFMriz[x, y].odkud = tmp;
                        openset.PridejPrvek(PFMriz[x, y]);
                    }
                    //Pokud uz jsem vrchol navstivil, ale nasel jsem lepsi cestu, tak starou nahradim novou
                    else if (pom_g < PFMriz[x, y].g)
                    {
                        PFMriz[x, y].odkud = tmp;
                        PFMriz[x, y].g = pom_g;
                        PFMriz[x, y].h = Library.VzdalenostEuklidovska((double)item.X, (double)cil.X, (double)item.Y, (double)cil.Y);
                        PFMriz[x, y].f = PFMriz[x, y].g + PFMriz[x, y].h;
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
