using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WhatsAppBotAPi.Services;
using WhatsAppBotAPi.Services.Configurations;
using WhatsAppBotAPi.Services.Exceptions;
using WhatsAppBotAPi.Services.Helpers;
using WhatsAppBotAPi.Services.Interfaces;
using WhatsAppBotAPi.Services.Messages.Requests;
using WhatsAppBotAPi.Services.Response;
using WhatsAppBotAPi.Services.SendMessageTemplate;
using WhatsAppBotAPi.Services.ViewModel;
namespace whatsappbotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMessageController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly WhatsAppConfig _waConfig;
        private readonly IWhatsAppBusinessClient _whatsAppBusinessClient;
        private readonly IWhatsAppBussinesManager _whatsAppBusinessManager;
        public SendMessageController(IConfiguration configuration, IWhatsAppBusinessClient whatsAppBusinessClient, IWhatsAppBussinesManager whatsAppBussinesManager)
        {
            _configuration = configuration;
            _whatsAppBusinessManager = whatsAppBussinesManager;
            _waConfig = new WhatsAppConfig();
            _configuration.GetSection("WhatsAppBusinessCloudApiConfiguration").Bind(_waConfig);
            _whatsAppBusinessClient = whatsAppBusinessClient;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _waConfig.AccessToken);
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage(SendTextMessageViewModel req)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    TextMessageRequest textMessageRequest = new TextMessageRequest();
                    textMessageRequest.To = req.RecipientPhoneNumber;
                    textMessageRequest.Text = new WhatsAppText();
                    textMessageRequest.Text.Body = req.Message;
                    textMessageRequest.Text.PreviewUrl = false;

                    var results = await _whatsAppBusinessClient.SendTextMessageAsync(textMessageRequest);

                    return Ok(new { Success = "Successfully sent text message" });
                }
                return BadRequest(ModelState);
            }
            catch (WhatsAppBotAPiException ex)
            {
                //_logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        [Route("SendWhatsApp_TextAsync")]
        public async Task<ActionResult<WhatsAppResponse>> SendWhatsApp_TextAsync(SendWhatsAppPayload payload)
        {
            try
            {
                if (payload.Template == null)
                {// Simple text only
                    TextMessageRequest textMsgPayload = new();
                    textMsgPayload.To = payload.SendText.ToNum;
                    textMsgPayload.Text = new WhatsAppText();
                    textMsgPayload.Text.Body = payload.SendText.Message;
                    textMsgPayload.Text.PreviewUrl = payload.SendText.PreviewUrl;

                    var result = await _whatsAppBusinessClient.SendTextMessageAsync(textMsgPayload);
                    string WAMIds = CommonHelper.GetWAMId(result);

                    return result;
                }
                else
                {// Text Template
                    TextTemplateMessageRequest textMsgPayload = new TextTemplateMessageRequest();
                    textMsgPayload.To = payload.SendText.ToNum;
                    textMsgPayload.Template = new TextMessageTemplate();
                    textMsgPayload.Template.Name = payload.Template.Name;
                    textMsgPayload.Template.Language = new TextMessageLanguage();
                    textMsgPayload.Template.Language.Code = LanguageCode.English_US;

                    if (payload.Template.Params != null)
                    {// Text Template with params
                     // For Text WhatsappTemplate message with parameters supported component type is body only				
                        textMsgPayload.Template.Components = new List<TextMessageComponent>();

                        var parameters = new List<TextMessageParameter>();

                        foreach (var txt in payload.Template.Params)
                        {
                            var param = new TextMessageParameter()
                            {
                                Type = "text",
                                Text = txt
                            };
                            parameters.Add(param);
                        }

                        textMsgPayload.Template.Components.Add(new TextMessageComponent()
                        {
                            Type = "body",
                            Parameters = parameters
                        });
                    }
                    var result = await _whatsAppBusinessClient.SendTextMessageTemplateAsync(textMsgPayload);
                    string WAMIds = CommonHelper.GetWAMId(result);
                    return result;
                }
            }
            catch (WhatsAppBotAPiException ex)
            {
                return Ok(-1);
            }
        }

        [HttpPost("SendWhatsAppMediaMessage")]
        public async Task<ActionResult<WhatsAppResponse>> SendWhatsApp_MediaAsync(SendWhatsAppPayload payload)
        {
            try
            {
                WhatsAppResponse results = null;
                switch (payload.MessageType)
                {
                    case enumMessageType.Audio:
                        if (!string.IsNullOrWhiteSpace(payload.Media.ID))
                        {  // Usaing IDs is much better, Upload the file to WhatsApp and then use the ID returned
                            AudioMessageByIdRequest audioMessage = new AudioMessageByIdRequest();
                            audioMessage.To = payload.SendText.ToNum;
                            audioMessage.Audio = new MediaAudio();
                            audioMessage.Audio.Id = payload.Media.ID;

                            results = await _whatsAppBusinessClient.SendAudioAttachmentMessageByIdAsync(audioMessage);
                        }
                        else //if (!string.IsNullOrWhiteSpace(payload.URL))
                        {
                            AudioMessageByUrlRequest audioMessage = new AudioMessageByUrlRequest();
                            audioMessage.To = payload.SendText.ToNum;
                            audioMessage.Audio = new MediaAudioUrl();
                            audioMessage.Audio.Link = payload.Media.URL;

                            results = await _whatsAppBusinessClient.SendAudioAttachmentMessageByUrlAsync(audioMessage);
                        }
                        break;

                    case enumMessageType.Doc:
                        if (!string.IsNullOrWhiteSpace(payload.Media.ID))
                        {  // Usaing IDs is much better, Upload the file to WhatsApp and then use the ID returned
                            DocumentMessageByIdRequest documentMessage = new DocumentMessageByIdRequest();
                            documentMessage.To = payload.SendText.ToNum;
                            documentMessage.Document = new MediaDocument();
                            documentMessage.Document.Id = payload.Media.ID;
                            documentMessage.Document.Caption = payload.Media.Caption;

                            results = await _whatsAppBusinessClient.SendDocumentAttachmentMessageByIdAsync(documentMessage);
                        }
                        else //if (!string.IsNullOrWhiteSpace(payload.URL))
                        {
                            DocumentMessageByUrlRequest documentMessage = new DocumentMessageByUrlRequest();
                            documentMessage.To = payload.SendText.ToNum;
                            documentMessage.Document = new MediaDocumentUrl();
                            documentMessage.Document.Link = payload.Media.URL;
                            documentMessage.Document.Caption = payload.Media.Caption;

                            results = await _whatsAppBusinessClient.SendDocumentAttachmentMessageByUrlAsync(documentMessage);
                        }
                        break;

                    case enumMessageType.Image:
                        if (!string.IsNullOrWhiteSpace(payload.Media.ID))
                        {  // Usaing IDs is much better, Upload the file to WhatsApp and then use the ID returned
                            ImageMessageByIdRequest imageMessage = new ImageMessageByIdRequest();
                            imageMessage.To = payload.SendText.ToNum;
                            imageMessage.Image = new MediaImage();
                            imageMessage.Image.Id = payload.Media.ID;
                            imageMessage.Image.Caption = payload.Media.Caption;

                            results = await _whatsAppBusinessClient.SendImageAttachmentMessageByIdAsync(imageMessage);
                        }
                        else //if (!string.IsNullOrWhiteSpace(payload.URL))
                        {
                            ImageMessageByUrlRequest imageMessage = new ImageMessageByUrlRequest();
                            imageMessage.To = payload.SendText.ToNum;
                            imageMessage.Image = new MediaImageUrl();
                            imageMessage.Image.Link = payload.Media.URL;
                            imageMessage.Image.Caption = payload.Media.Caption;

                            results = await _whatsAppBusinessClient.SendImageAttachmentMessageByUrlAsync(imageMessage);
                        }
                        break;

                    //case "STICKER":
                    //    if (!string.IsNullOrWhiteSpace(payload.Media.ID))
                    //    {  // Usaing IDs is much better, Upload the file to WhatsApp and then use the ID returned
                    //        StickerMessageByIdRequest stickerMessage = new StickerMessageByIdRequest();
                    //        stickerMessage.To = payload.SendText.ToNum;
                    //        stickerMessage.Sticker = new MediaSticker();
                    //        stickerMessage.Sticker.Id = payload.Media.ID;

                    //        results = await _whatsAppBusinessClient.SendStickerMessageByIdAsync(stickerMessage);
                    //    }
                    //    else //if (!string.IsNullOrWhiteSpace(payload.URL))
                    //    {
                    //        StickerMessageByUrlRequest stickerMessage = new StickerMessageByUrlRequest();
                    //        stickerMessage.To = payload.SendText.ToNum;
                    //        stickerMessage.Sticker = new MediaStickerUrl();
                    //        stickerMessage.Sticker.Link = payload.Media.URL;

                    //        results = await _whatsAppBusinessClient.SendStickerMessageByUrlAsync(stickerMessage);
                    //    }
                    //    break;

                    case enumMessageType.Video:
                        if (!string.IsNullOrWhiteSpace(payload.Media.ID))
                        {  // Usaing IDs is much better, Upload the file to WhatsApp and then use the ID returned
                            VideoMessageByIdRequest videoMessage = new VideoMessageByIdRequest();
                            videoMessage.To = payload.SendText.ToNum;
                            videoMessage.Video = new MediaVideo();
                            videoMessage.Video.Id = payload.Media.ID;
                            videoMessage.Video.Caption = payload.Media.Caption;

                            results = await _whatsAppBusinessClient.SendVideoAttachmentMessageByIdAsync(videoMessage);
                        }
                        else //if (!string.IsNullOrWhiteSpace(payload.URL))
                        {
                            VideoMessageByUrlRequest videoMessage = new VideoMessageByUrlRequest();
                            videoMessage.To = payload.SendText.ToNum;
                            videoMessage.Video = new MediaVideoUrl();
                            videoMessage.Video.Link = payload.Media.URL;
                            videoMessage.Video.Caption = payload.Media.Caption;

                            results = await _whatsAppBusinessClient.SendVideoAttachmentMessageByUrlAsync(videoMessage);
                        }
                        break;
                }
                return results;
            }
            catch (WhatsAppBotAPiException ex)
            {
                return Ok(-1);      // RedirectToAction(nameof(SendWhatsApp_MediaAsync)).WithDanger("Error", ex.Message);
            }
        }
        [HttpPost]
        [Route("SendWhatsAppTemplateImage")]
        public async Task<ActionResult<WhatsAppResponse>> SendWhatsApp_TemplateImage_ParameterAsync(SendWhatsAppPayload payload)
        {
            // Tested with facebook predefined template name: sample_movie_ticket_confirmation
            ImageTemplateMessageRequest imageTemplateMessage = new ImageTemplateMessageRequest();
            imageTemplateMessage.To = payload.SendText.ToNum;
            imageTemplateMessage.Template = new ImageMessageTemplate();
            imageTemplateMessage.Template.Name = payload.Template.Name;
            imageTemplateMessage.Template.Language = new ImageMessageLanguage();
            imageTemplateMessage.Template.Language.Code = LanguageCode.English_US;

            imageTemplateMessage.Template.Components = new List<ImageMessageComponent>(); // Move this line here

            imageTemplateMessage.Template.Components.Add(new ImageMessageComponent()
            {
                Type = "header",
                Parameters = new List<ImageMessageParameter>()
                {
                    new ImageMessageParameter()
                    {
                        Type = "image",
                        Image = new WhatsAppBotAPi.Services.Messages.Requests.Image()
                        {
                            Id = !string.IsNullOrEmpty(payload.Media.ID) ? payload.Media.ID : null,
                            Link = string.IsNullOrEmpty(payload.Media.ID) ? payload.Media.URL : null,
                            //Caption = !string.IsNullOrEmpty(payload.Caption) ? payload.Caption : null
                        }
                    }
                },
            });

            if (payload.Template.Params != null)
            {
                // Loop and Compile Body Params
                var bodyParams = new List<ImageMessageParameter>();

                foreach (var txt in payload.Template.Params)
                {
                    var param = new ImageMessageParameter()
                    {
                        Type = "text",
                        Text = txt
                    };
                    bodyParams.Add(param);
                }

                // Add the Body Params
                imageTemplateMessage.Template.Components.Add(new ImageMessageComponent()
                {
                    Type = "body",
                    Parameters = bodyParams
                });
            }

            var results = await _whatsAppBusinessClient.SendImageAttachmentTemplateMessageAsync(imageTemplateMessage);
            return results;
        }
        [HttpPost]
        [Route("SendWhatsAppTemplateVideo")]
        public async Task<ActionResult<WhatsAppResponse>> SendWhatsApp_TemplateVideo_ParameterAsync(SendWhatsAppPayload payload)
        {
            // Senbd a Video WhatsappTemplate with Parameters

            VideoTemplateMessageRequest videoTemplateMessage = new();
            videoTemplateMessage.To = payload.SendText.ToNum;
            videoTemplateMessage.Template = new();
            videoTemplateMessage.Template.Name = payload.Template.Name;
            videoTemplateMessage.Template.Language = new();
            videoTemplateMessage.Template.Language.Code = LanguageCode.English_US;

            videoTemplateMessage.Template.Components = new List<VideoMessageComponent>();

            videoTemplateMessage.Template.Components.Add(new VideoMessageComponent()
            {
                Type = "header",
                Parameters = new List<VideoMessageParameter>()
                {
                    new VideoMessageParameter()
                    {
                        Type = "video",
                        Video = new WhatsAppBotAPi.Services.Messages.Requests.Video()
                        {
                            Id = !string.IsNullOrEmpty(payload.Media.ID) ? payload.Media.ID : null,
                            Link = string.IsNullOrEmpty(payload.Media.ID) ? payload.Media.URL : null,
                            //Caption = !string.IsNullOrEmpty(payload.Caption) ? payload.Caption : null
                        }
                    }
                },
            });

            if (payload.Template.Params != null)
            { // There are Params, Loop and Compile Body Params
                var bodyParams = new List<VideoMessageParameter>();

                foreach (var txt in payload.Template.Params)
                {
                    var param = new VideoMessageParameter()
                    {
                        Type = "text",
                        Text = txt
                    };
                    bodyParams.Add(param);
                }

                // Add the Body Params
                videoTemplateMessage.Template.Components.Add(new VideoMessageComponent()
                {
                    Type = "body",
                    Parameters = bodyParams
                });
            }

            var results = await _whatsAppBusinessClient.SendVideoAttachmentTemplateMessageAsync(videoTemplateMessage);

            return results;
        }

        [HttpPost]
        [Route("SendPizzaTemplateImage")]
        public async Task<ActionResult<WhatsAppResponse>> SendPizzaTemplateImage(SendWhatsAppPizzaPayload payload)
        {
            return await _whatsAppBusinessManager.SendFirstTemplateMessageAsync(payload);
        }
        [HttpPost]
        [Route("SendOrderTemplateImage")]
        public async Task<ActionResult<WhatsAppResponse>> SendOrderTemplateImage(SendWhatsAppPayload2 payload)
        {
            return await _whatsAppBusinessManager.SendFirstTemplateMessageAsync(payload);
        }


    }
}
