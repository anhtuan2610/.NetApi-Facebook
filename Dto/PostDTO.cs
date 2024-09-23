namespace Facebook_be.Dto
{
    public class PostDTO
    {
        public int? UserId { get; set; }
        public string? Content { get; set; }
        public string? PostImg { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
