using Environment = Braintree.Environment;

namespace Drutol.FigureRepository.Api.Models.Configuration
{
    public class BraintreeConfiguration
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
    }
}
