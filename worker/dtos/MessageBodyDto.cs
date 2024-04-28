using Newtonsoft.Json;

public class MessageBodyDto
{
    public string Title { get; set; }
    public string Url { get; set; }

    [JsonProperty("image_url")]
    public string ImageUrl { get; set; }
    public string Summary { get; set; }
}