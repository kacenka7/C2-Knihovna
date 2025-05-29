
internal class Program
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime PublishedDate { get; set; }

        private int _pages;
        public int Pages
        {
            get
            {
                return _pages;
            }
            private set
            {
                if (value < 1)
                {
                    Console.WriteLine("Počet stránek nemůže být záporný nebo nula ");
                }
                else
                {
                    _pages = value;
                }
            }
        }
        public Book(string title, string author, string publishedDate, int pages)
        {
            Title = title;
            Author = author;
            PublishedDate = DateTime.Parse(publishedDate);
            Pages = pages;
        }

        public Book(string[] vstup)
        {
            try
            {
                if (vstup[0].Trim().Length > 0)
                {
                    Title = vstup[0].Trim();
                }
                else
                {
                    throw new ArgumentException("Název knihy je povinný.");
                }

                if (vstup[1].Trim().Length > 0)
                {
                    Author = vstup[1].Trim();
                }
                else
                {
                    throw new ArgumentException("Autor je povinný.");
                }

                if (DateTime.TryParse(vstup[2].Trim(), out DateTime datum))
                {
                    PublishedDate = datum;
                }
                else
                {
                    throw new ArgumentException("Neplatný formát datumu.");
                }

                if (int.TryParse(vstup[3].Trim(), out int stranky) && stranky > 0)
                {
                    Pages = stranky;
                }
                else
                {
                    throw new ArgumentException("Neplatný počet stran.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Chyba při vytváření knihy: " + ex.Message);
                throw;
            }

            
        }
        public void Kniha()
        {
            Console.WriteLine($"Název:{Title}, Autor:{Author}, Rok Vydání: {PublishedDate}, Počet stran: {Pages}");
        }
    }
    private static void Main(string[] args)
    {
        Book harryPotter1 = new Book("Harry Potter a Kámen mudrců", "J. K. Rowlingová", "2021-05-24", 336);
        Book harryPotter2 = new Book("Harry Potter a Tajemná komnata", "J. K. Rowlingová", "2024-10-10", 368);
        Book dvory1 = new Book("Dvůr trnů a růží", "S. J. Maas", "2024-08-26", 440);
        Book dvory2 = new Book("Dvůr mlhy a hněvu", "S. J. Maas", "2024-09-30", 664);

        List<Book> knihovna = new List<Book>() { harryPotter1, harryPotter2, dvory1, dvory2 };

        while (true)
        {
            Console.WriteLine("Pro vložení nové knihy zadej ADD, Pro vypsání všech knih dle data vydání zadej LIST, pro výpis statistiky zadej STATS, pro vyhledání konkrétní knihy zadej FIND nebo pro ukončení programu zadej END");
            string volba = Console.ReadLine();
            string[] rozdelene = volba.Split(';', 2);
            string akce = rozdelene[0].Trim().ToUpper();
            

            switch (akce)
            {
                case "ADD":
                    if (rozdelene.Length == 2)
                    {
                        string[] vstupKniha = rozdelene[1].Trim().Split(';');
                        if (vstupKniha.LongLength == 4)
                        {
                            try
                            {
                                Book novaKniha = new Book(vstupKniha);
                                knihovna.Add(novaKniha);
                                Console.WriteLine("Kniha byla přidána do knihovny.");
                                novaKniha.Kniha();
                                
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

                    break;
                case "LIST":
                    var serazeneKnihy = knihovna.OrderBy(k => k.PublishedDate);
                    foreach (var k in serazeneKnihy)
                    {
                        k.Kniha();
                    }
                    break;
                case "STATS":
                    var prumerStran = knihovna.Average(k =>k.Pages);
                    Console.WriteLine($"Průměrný počet stran je: {prumerStran}");

                    var seskupene = knihovna.GroupBy(k => k.Author);
                    foreach (var skupina in seskupene)
                    {
                        foreach (var kniha in skupina)
                        {
                            Console.WriteLine($"  - {kniha.Title} ({kniha.PublishedDate.Year}, {kniha.Author})");
                        }
                    } 

                    var unikatniSlova = knihovna
                        .SelectMany(k => k.Title
                            .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                        .Select(slovo => slovo.Trim(new[] { '.', ',', '!', '?', ':', ';', '-', '„', '“', '"' }))
                        .Where(slovo => !string.IsNullOrWhiteSpace(slovo))
                        .Select(slovo => slovo.ToLower())
                        .Distinct();
                        Console.WriteLine($"Počet unikátních slov v názvech knih: {unikatniSlova.Count()}");
                    break;
                case "FIND":
                    string klicoveSlovo = rozdelene[1].Trim().ToLower();
                    if (klicoveSlovo.Length > 0)
                    {
                        var find = knihovna.Where(k => k.Title.ToLower().Contains(klicoveSlovo));
                        if (find.Any ())
                        {
                            foreach (var k in find)
                            {
                                k.Kniha();
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