using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using PierwszaAplikacjaMVC.Models;

namespace PierwszaAplikacjaMVC.Controllers
{
    public class HomeController: Controller
    {
        // chcemy móc korzystać z metod i właściwości naszego repozytorium
        // możemy też tutaj podać jako typ ResponseRepository,
        // ale tutaj demonstrujemy "wstrzykiwanie zależności", czyli
        // pod zarejestrowany interfejs IResponseRepository chcemy, aby
        // aplikacja sama podawała konkretną implementację:
        private readonly IResponsesRepository _repository;

        // aplikacja wie z pliku Startup.cs że w miejsce IResponseRepository
        // ma podać ResponseRepository:
        public HomeController(IResponsesRepository repo)
        {
            _repository = repo;
        }

        // metoda akcji definiująca podstawową ścieżkę domyślną po uruchomieniu aplikacji:
        public ViewResult Index()
        {
           int hour = DateTime.Now.Hour;
            // dynamiczny pojemnik na dane z kontrolera do widoku, który tworzy się w momencie przypisywania:
            ViewData["Powitanie"] = hour < 16 ? "Dzien dobry! " : "Dobry wieczór!";
            // alternatywny sposob:
           // ViewBag.Powitanie = hour < 16 ? "Dzien dobry! " : "Dobry wieczór!";

            // zwracamy widok o nazwie Welcome z katalogu Views/Home, "Home" brane jest z nazwy HomeController:
           return View("Welcome");
        }

        // odwołując się do tej akcji z widoku zostanie nam zwrócony widok o nazwie ResponseForm,
        // bo nie podaliśmy parametru z nazwą widoku:
        [HttpGet]
        public ViewResult ResponseForm()
        {
            return View();
        }

        // metoda akcji po nacisnieciu przycisku submit w widoku ResponseForm, która otrzyma dane z wypełnionego
        // przez nas formularza:
        [HttpPost]
        public ViewResult ResponseForm(GuestResponse guestResponse)
        {
            // sprawdzamy czy otrzymane dane - guestResponse to poprawny model:
            if(ModelState.IsValid) 
            {
                // jeśli tak, dodajemy przez nasze repozytorium obiekt z danymi do bazy danych:
                _repository.AddNewResponse(guestResponse);
                // i zwracamy w przeglądarce widok o nazwie "Thanks" do którego przesyłamy jako argument obiekt z danymi:
                return View("Thanks", guestResponse);
            }
            else
            {
                // jeśli dane/model nie jest poprawny (GuestResponse) zwracamy widok ResponseForm:
                return View();
            }
        }

        // metoda akcji zwracająca widok ListResponses i przekazująca do niego przefiltrowane odpowiedzi:
        [HttpGet]
        public ViewResult ListResponses()
        {
            // przez repozytorium odwołujemy się do bazy danych do: Responses,
            // Responses jest to kolekcja obiektów GuestResponse (IEnumerable<GuestResponse>),
            // aby użyć Where do filtrowania to musimy dodać odpowiedni using z LINQ.
            // jako argument przekazujemy do Where delegat (kod), który na wejściu przyjmuje GuestResponse,
            // a zwraca true/false. Filter wybierze tylko te elementy p które spełniają podany warunek:
            return View(_repository.Responses.Where(p => p.WillAttend == true));
        }
    }
}