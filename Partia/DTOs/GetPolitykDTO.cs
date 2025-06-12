namespace Partia.DTOs;

public class GetPolitykDTO
{
    public int Id { get; set; }
    public String Imie { get; set; }
    public String Nazwisko {get; set;}
    public String? Powiedzenie { get; set; }
    public List<GetPrzynaleznoscDTO> Przynaleznosc { get; set; } = new();
}