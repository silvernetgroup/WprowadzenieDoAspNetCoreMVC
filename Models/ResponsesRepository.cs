using System.Collections.Generic;

namespace PierwszaAplikacjaMVC.Models
{
    public class ResponsesRepository : IResponsesRepository
    {
        // to jest złe podejście, będziemy używać mechanizmu "wstrzykiwania zależności" jak zrobiliśmy to w HomeController:
        private readonly ApplicationDbContext _context = new ApplicationDbContext(); 

        // inicjalizacja właściwości, nasza właściwość będzie umożliwiała odczyt zaproszen z bazy danych:
        public IEnumerable<GuestResponse> Responses
        {
            get
            {
                return _context.Invites;
            }
        }
        // alternatywny sposób:
        // public IEnumerable<GuestResponse> Responses => _context.Invites;

        // implementujemy metodę interfejsu, dodajemy zaproszenie do bazy danych i zapisujemy zmiany w bazie:
        public void AddNewResponse(GuestResponse response)
        {
            _context.Add(response);
            _context.SaveChanges();
        }
    }
}