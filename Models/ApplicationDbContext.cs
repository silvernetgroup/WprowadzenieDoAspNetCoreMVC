using Microsoft.EntityFrameworkCore;

namespace PierwszaAplikacjaMVC.Models
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext()
        {
            
        }

        // nadpisujemy odziedziczoną metodą, aby określić jakiej bazy danych użyjemy.
        // będzie to w tym przypadku baza w pamięci (nie jest ona przeznaczona do wersji produkcyjnych)
        // i nazywamy ją PartyResponses
        protected override void OnConfiguring(DbContextOptionsBuilder builderOptions)
        {
            base.OnConfiguring(builderOptions);
            builderOptions.UseInMemoryDatabase("PartyResponses");
        }
        
        // jak już zdefiniujemy bazę danych to przedstawiamy źródło odczytu i zapisu danych
        // jest to właściwość reprezentująca w bazie danych nasze zaproszenia - Invites odzwzorowujące nasz model GuestResponse
        public DbSet<GuestResponse> Invites { get; set; }
    }
}