using api_reservas.Core.Models.BaseModels;

namespace api_reservas.Core.Models
{
    public class Listing : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }
        public string Coordinates { get; set; }
        public DateTime Date { get; set; }
        public List<string> ImagesUrls { get; set } 
    }
}
