using api_reservas.Core.Models.BaseModels;

namespace api_reservas.Core.Models
{
    public class Listing : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime Date { get; set; }
    }
}
