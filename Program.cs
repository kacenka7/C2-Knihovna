
internal class Program
{
        public static void NovaKniha (List<Kniha> knihovna, string[] vstupKniha)
    {
        Kniha novaKniha = new Kniha(vstupKniha);
        knihovna.Add(novaKniha);
        Console.WriteLine("Kniha byla přidána do knihovny.");
        novaKniha.VypisKnihu();
    }

    public static void PrumernyPocetStran( List<Kniha> knihovna)
    {
        var prumerStran = knihovna.Average(k => k.Pages);
                    Console.WriteLine($"Průměrný počet stran je: {prumerStran}");

                    var seskupene = knihovna.GroupBy(k => k.Autor);
                    foreach (var skupina in seskupene)
                    {
                        foreach (var kniha in skupina)
                        {
                            Console.WriteLine($"  - {kniha.Nazev} ({kniha.DatumVydani.Year}, {kniha.Autor})");
                        }
                    }

                    var unikatniSlova = knihovna
                        .SelectMany(k => k.Nazev
                            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                        .Select(slovo => slovo.Trim(new[] { '.', ',', '!', '?', ':', ';', '-', '„', '“', '"' }))
                        .Where(slovo => !string.IsNullOrWhiteSpace(slovo))
                        .Select(slovo => slovo.ToLower())
                        .Distinct();
                    Console.WriteLine($"Počet unikátních slov v názvech knih: {unikatniSlova.Count()}");
    }

    public static void NajdiKnihu(List<Kniha> knihovna, string[] rozdelene)
    {
        if (rozdelene.Length == 2)
        {
            string klicoveSlovo = rozdelene[1].Trim().ToLower();
            if (klicoveSlovo.Length > 0)
            {
                var find = knihovna.Where(k => k.Nazev.ToLower().Contains(klicoveSlovo));
                if (find.Any())
                {
                    foreach (var k in find)
                    {
                        k.VypisKnihu();
                    }
                }
                else
                {
                    Console.WriteLine("Nebyla nalezená shoda");
                }
            }
            else
            {
                Console.WriteLine("Nebylo zadáno klíčové slovo");
            }

        }

        else
    {
        Console.WriteLine("Nebylo zadáno klíčové slovo. Použijte formát: FIND;klíčové_slovo");
    }
    }

    public static void PridejKnihu(List<Kniha> knihovna, string[] rozdelene)
    {
        if (rozdelene.Length == 2)
        {
            string[] vstupKniha = rozdelene[1].Trim().Split(';');
            if (vstupKniha.LongLength == 4)
            {
                try
                {

                    NovaKniha(knihovna, vstupKniha);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine("Kniha nebyla přidána: " + ex.Message);
                }
                catch (Exception)
                {
                    Console.WriteLine("Kniha nebyla přidána kvůli neočekávané chybě.");
                }
            }
            else
            {
                Console.WriteLine("Nebyly zadány všechny údaje o knize.");
            }
        }
        else
        {
            Console.WriteLine("Nebyly zadány údaje o knize.");
        }
    }

    private static void Main(string[] args)
    {
        Kniha harryPotter1 = new Kniha("Harry Potter a Kámen mudrců", "J. K. Rowlingová", "2021-05-24", 336);
        Kniha harryPotter2 = new Kniha("Harry Potter a Tajemná komnata", "J. K. Rowlingová", "2024-10-10", 368);
        Kniha dvory1 = new Kniha("Dvůr trnů a růží", "S. J. Maas", "2024-08-26", 440);
        Kniha dvory2 = new Kniha("Dvůr mlhy a hněvu", "S. J. Maas", "2024-09-30", 664);

        List<Kniha> knihovna = new List<Kniha>() { harryPotter1, harryPotter2, dvory1, dvory2 };

        while (true)
        {
            Console.WriteLine("Pro vložení nové knihy zadej ADD, Pro vypsání všech knih dle data vydání zadej LIST, pro výpis statistiky zadej STATS, pro vyhledání konkrétní knihy zadej FIND nebo pro ukončení programu zadej END");
            string volba = Console.ReadLine();
            string[] rozdelene = volba.Split(';', 2);
            string akce = rozdelene[0].Trim().ToUpper();


            switch (akce)
            {
                case "ADD":
                    PridejKnihu(knihovna, rozdelene);
                    break;
                case "LIST":
                    var serazeneKnihy = knihovna.OrderBy(k => k.DatumVydani);
                    foreach (var k in serazeneKnihy)
                    {
                        k.VypisKnihu();
                    }
                    break;
                case "STATS":
                    PrumernyPocetStran(knihovna);
                    break;
                case "FIND":
                    NajdiKnihu(knihovna, rozdelene);
                    break;
                case "END":
                    Console.WriteLine("Program se ukončí");
                    return;
                default:
                    Console.WriteLine("Neznámý příkaz. Zkus to znovu.");
                    break;
            }

        }



    }
}