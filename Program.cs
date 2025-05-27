
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

        public Book(string vstup)
        {
            string[] casti = vstup.Split(';');
            Title = casti[0];
            Author = casti[1];
            PublishedDate = DateTime.Parse(casti[2]);
            Pages = int.Parse(casti[3]);
            
        }
        public void Kniha()
        {
            Console.WriteLine($"Kniha:{Title}, Autor:{Author}, Rok Vydání: {PublishedDate}, Počet stran: {Pages}");
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
                    Book novaKniha = new Book (rozdelene[1]);
                    Console.WriteLine("Kniha byla zadána do knihovny.");
                    knihovna.Add(novaKniha);
                    novaKniha.Kniha();
                    break;
                case "LIST":
                    break;
                case "STATS":
                    break;
                case "FIND":
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