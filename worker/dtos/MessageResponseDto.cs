public class MessageResponseDto
{
    public string Id { get; set; }
    public DateTimeOffset EnqueuedAt { get; set; }
    public string State { get; set; }
    public string BodySize { get; set; }
    public MessageBodyDto Message { get; set; }
}