using System.Collections.Generic;

namespace WhatsAppBotAPi.Services.ViewModel
{
	public class QRCodeMessageViewModel
	{
		public string Message { get; set; }
		public List<string> ImageFormat { get; set; }
		public string SelectedImageFormat { get; set; }
	}
}
