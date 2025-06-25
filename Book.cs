public class Kniha
{
    public string Nazev { get; set; }
    public string Autor { get; set; }
    public DateTime DatumVydani { get; set; }

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
    public Kniha(string nazev, string autor, string datumVydani, int pages)
    {
        Nazev = nazev;
        Autor = autor;
        DatumVydani = DateTime.Parse(datumVydani);
        Pages = pages;
    }

    public Kniha(string[] vstup)
    {
        try
        {
            if (vstup[0].Trim().Length > 0)
            {
                Nazev = vstup[0].Trim();
            }
            else
            {
                throw new ArgumentException("Název knihy je povinný.");
            }

            if (vstup[1].Trim().Length > 0)
            {
                Autor = vstup[1].Trim();
            }
            else
            {
                throw new ArgumentException("Autor je povinný.");
            }

            if (DateTime.TryParse(vstup[2].Trim(), out DateTime datum))
            {
                DatumVydani = datum;
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
    public void VypisKnihu()
    {
        Console.WriteLine($"Název:{Nazev}, Autor:{Autor}, Rok Vydání: {DatumVydani}, Počet stran: {Pages}");
    }

    }