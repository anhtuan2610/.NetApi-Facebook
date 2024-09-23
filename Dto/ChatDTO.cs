namespace Facebook_be.Dto
{
    public class ChatDTO
    {
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public string? Content { get; set; }
        public bool? IsRead { get; set; }
    }
}
