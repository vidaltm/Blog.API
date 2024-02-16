using System.ComponentModel.DataAnnotations;

namespace Blog.Api.ViewModels
{
    public class CreatePostViewModel
    {
        [Required]
        public string NomeEditor { get; set; }
        [Required]
        public string Texto { get; set; }
    }
}
