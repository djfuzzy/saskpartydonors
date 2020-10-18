using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace SaskPartyDonors.Extensions
{
    public static class HttpRequestExtensions
    {
        private static readonly FormOptions _formOptions = new FormOptions();

        public static MultipartReader GetMultipartReader(this HttpRequest request)
        {
            var lengthLimit = _formOptions.MultipartBoundaryLengthLimit;

            MediaTypeHeaderValue.TryParse(request.ContentType, out var contentType);
            var boundary = HeaderUtilities.RemoveQuotes(contentType.Boundary);

            if (StringSegment.IsNullOrEmpty(boundary))
            {
                throw new InvalidDataException("Missing content-type boundary.");
            }

            if (boundary.Length > lengthLimit)
            {
                throw new InvalidDataException($"Multipart boundary length limit {lengthLimit} exceeded.");
            }

            return new MultipartReader(boundary.ToString(), request.Body);
        }
    }
}
