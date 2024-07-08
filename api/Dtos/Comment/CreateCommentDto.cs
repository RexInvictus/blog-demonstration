using System.ComponentModel.DataAnnotations;

namespace api.Dtos.Comment
{
    public class CreateCommentDto
    {
        [Required]
        public int BlogPostId { get; set; }
        [MaxLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Content cannot exceed 500 characters.")]
        [Required]
        public string Content { get; set; } = string.Empty;
        [Required]
        public string CaptchaToken { get; set; } = string.Empty;
    }
}
