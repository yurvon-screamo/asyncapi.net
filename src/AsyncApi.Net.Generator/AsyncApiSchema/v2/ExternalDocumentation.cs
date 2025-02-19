using System;

using Newtonsoft.Json;

namespace AsyncApi.Net.Generator.AsyncApiSchema.v2;

public class ExternalDocumentation
{
    public ExternalDocumentation(string url)
    {
        Url = url ?? throw new ArgumentNullException(nameof(url));
    }

    /// <summary>
    /// A short description of the target documentation.
    /// CommonMark syntax can be used for rich text representation.
    /// </summary>
    [JsonProperty("description")]
    public string? Description { get; set; }

    /// <summary>
    /// The URL for the target documentation.
    /// Value MUST be in the format of a URL.
    /// </summary>
    [JsonProperty("url")]
    public string? Url { get; }
}