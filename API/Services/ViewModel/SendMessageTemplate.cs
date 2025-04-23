

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WhatsAppBotAPi.Services.SendMessageTemplate
{
    public class Language
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }
    }

    public class Template
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("language")]
        public Language Language { get; set; }
    }

    public class SendMessageTemplate
    {
        public SendMessageTemplate()
        {
            MessagingProduct = "whatsapp";
            Type = "template";

            Template = new Template
            {
                Language = new Language { Code = "en_US" }
            };
        }

        [JsonPropertyName("messaging_product")]
        public string MessagingProduct { get; set; }

        [JsonPropertyName("to")]
        public string To { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("template")]
        public Template Template { get; set; }
    }
    public enum enumMessageType
    {
        Audio,
        Doc,
        Image,
        Text,
        Video
    }

    public record MessageType(enumMessageType Type, bool Template, bool Att, bool Cap);

    public record SendTextPayload
    {
        [Required(ErrorMessage= "ToNum is required!")]  
        public  string ToNum { get; set; }
        public string Message { get; set; } = "Hello";
        public bool PreviewUrl { get; set; } = false;
    }

    public record WhatsAppMedia
    {
        public string Type { get; set; }
        public string URL { get; set; }
        public string ID { get; set; }
        public string Caption { get; set; }
    }

    public record WhatsappTemplate
    {
        public string Name { get; set; }
        public List<string> Params { get; set; }
    }

    /// <summary>
    /// This Payload cater for a message
    /// 1. Text Only
    /// 2. Media
    /// 3. Templates
    /// </summary>
	public record SendWhatsAppPayload
    {
        public SendTextPayload SendText { get; set; }
        public enumMessageType MessageType { get; set; }
        public WhatsAppMedia Media { get; set; }
        public WhatsappTemplate Template { get; set; }
    }
	public class SendWhatsAppPizzaPayload
    {
        [Required(ErrorMessage = "ToNum is required!")]
        public string ToNum { get; set; }
        public string TemplateName { get; set; }
        public string ItemImage { get; set; }
        public string ItemName { get; set; }
        public long ItemPrice { get; set; }
        public DateTime ItemDate { get; set; }
    }
	public class SendWhatsAppPayload2: SendWhatsAppPizzaPayload
    {
        public string  Name { get; set; }
    }

}
