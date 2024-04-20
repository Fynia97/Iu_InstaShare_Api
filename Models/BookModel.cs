using System.ComponentModel.DataAnnotations.Schema;

namespace Iu_InstaShare_Api.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    }
}
