using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsAppBotAPi.Services.Interfaces;
using WhatsAppBotAPi.Services.Messages.Requests;
using WhatsAppBotAPi.Services.Response;
using WhatsAppBotAPi.Services.SendMessageTemplate;

namespace WhatsAppBotAPi.Services.WhatsAppBusinessManager
{
    public class WhatsAppBusinessManager : IWhatsAppBussinesManager
    {
        private readonly IWhatsAppBusinessClient _whatsAppBusinessClient;
        public WhatsAppBusinessManager(IWhatsAppBusinessClient whatsAppBusinessClient)
        {
            _whatsAppBusinessClient = whatsAppBusinessClient;
        }
        public async Task<WhatsAppResponse> SendFirstTemplateMessageAsync(SendWhatsAppPizzaPayload payload)
        {
            var imageTemplateMessage = new ImageTemplateMessageRequest
            {
                To = payload.ToNum,
               
                Template = new ImageMessageTemplate
                {
                    Name = payload.TemplateName,
                    Language = new ImageMessageLanguage
                    {
                        Code = LanguageCode.English
                    },
                    Components = new List<ImageMessageComponent>()
                }
            };

            // 1. Header - Image
            imageTemplateMessage.Template.Components.Add(new ImageMessageComponent
            {
                Type = "header",
                Parameters = new List<ImageMessageParameter>
        {
            new ImageMessageParameter
            {
                Type = "image",
                Image = new WhatsAppBotAPi.Services.Messages.Requests.Image
                {
                    Id = null,
                    Link = payload.ItemImage
                }
            }
        }
            });

            // 2. Body - 3 parameters (text, currency, datetime)
            var bodyParams = new List<ImageMessageParameter>
    {
        // Param 1: item_name (Text)
        new ImageMessageParameter
        {
            Type = "text",
            Text = payload.ItemName
        },

        // Param 2: item_price (Currency)
        new ImageMessageParameter
        {
            Type = "currency",
            Currency = new ImageTemplateCurrency
            {
                FallbackValue = payload.ItemPrice.ToString(),
                Amount1000 = payload.ItemPrice * 1000,
                Code = "PKR"
            }
        },

        // Param 3: item_date (DateTime)
        new ImageMessageParameter
        {
            Type = "date_time",
            DateTime = new ImageTemplateDateTime
            {
                FallbackValue = payload.ItemDate.ToString("dd MMM yyyy"),
                Calendar = "GREGORIAN",
                Year = payload.ItemDate.Year,
                Month = payload.ItemDate.Month,
                DayOfMonth = payload.ItemDate.Day,
                Hour = payload.ItemDate.Hour,
                Minute = payload.ItemDate.Minute
            }
        }
    };

            imageTemplateMessage.Template.Components.Add(new ImageMessageComponent
            {
                Type = "body",
                Parameters = bodyParams
            });

            // 3. Optional Buttons (Uncomment if buttons are part of your template)
            // imageTemplateMessage.Template.Components.Add(new ImageMessageComponent
            // {
            //     Type = "button",
            //     SubType = "quick_reply",
            //     Index = 0,
            //     Parameters = new List<ImageMessageParameter>
            //     {
            //         new ImageMessageParameter
            //         {
            //             Type = "payload",
            //             Payload = "ORDER_NOW"
            //         }
            //     }
            // });

            var results = await _whatsAppBusinessClient.SendImageAttachmentTemplateMessageAsync(imageTemplateMessage);
            return results;
        }
    }
}
