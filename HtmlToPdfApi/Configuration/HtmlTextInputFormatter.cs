﻿using Microsoft.AspNetCore.Mvc.Formatters;
using System.IO;
using System.Net.Mime;
using System.Threading.Tasks;

namespace HtmlToPdfApi.Configuration
{
    public class HtmlTextInputFormatter : InputFormatter
    {
        private const string ContentType = MediaTypeNames.Text.Html;

        public HtmlTextInputFormatter()
        {
            SupportedMediaTypes.Add(ContentType);
        }

        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            using (var reader = new StreamReader(request.Body))
            {
                var content = await reader.ReadToEndAsync();
                return await InputFormatterResult.SuccessAsync(content);
            }
        }

        public override bool CanRead(InputFormatterContext context)
        {
            var contentType = context.HttpContext.Request.ContentType;
            return contentType.StartsWith(ContentType);
        }
    }
}
