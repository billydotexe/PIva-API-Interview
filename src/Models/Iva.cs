namespace PIva.Api.Models
{
    public class Iva
    {
        public string CompanyName { get; set; } = default!;
        public string Address { get; set; } = default!;
        public string City { get; set; } = default!; 
        public string Zip { get; set; } = default!;
        public string CountryName { get; set; } = default!;
        public string CountryCode { get; set; } = default!;
    }
}
