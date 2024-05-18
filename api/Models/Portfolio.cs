using System.ComponentModel.DataAnnotations.Schema;
namespace api.Models
{
    [Table("Portfolio")]
    public class Portfolio
    {
        public int Stockid { get; set; }
        public string AppUserId { get; set; } = "";
        public AppUser AppUser { get; set; } = null!;
        public Stock Stock { get; set; } = null!;
    }
}