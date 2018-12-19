using System.Collections.Generic;

namespace PierwszaAplikacjaMVC.Models
{
    // interfejs (nie klasa). dla rozróżnienia interfejsu od klasy na początku nazwy interfejsu dodajemy duże 'i' 
    public interface IResponsesRepository
    {
        // właściwość tylko do odczytu (jest pozbawiona akcesora "set" umożliwiającego modyfikacje)
        // IEnumerable<T> reprezentuje kolekcje T gdzie za T możemy podstawić jakiś typ, my podstawiamy GuestResponse:
        IEnumerable<GuestResponse> Responses { get; }

        // metoda czekająca do zaimplementowania przez klasę, która będzie dziedziczyła po tym interfejsie
        void AddNewResponse(GuestResponse response);
    }
}