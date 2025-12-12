using System;
using System.Collections.Generic;
using System.Text;

namespace UrlShortenerApp.Service.Responses
{
    public record FailedResponse(string Error) : ResponseBase(false, Error);
}
