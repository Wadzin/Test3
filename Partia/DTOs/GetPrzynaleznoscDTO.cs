namespace Partia.DTOs;

public class GetPrzynaleznoscDTO
{
    public String Nazwa { get; set; }
    public String Skrot { get; set; }
    public DateTime DataZalozenia { get; set; }
    public DateTime Od { get; set; }
    public DateTime? Do { get; set; }
}