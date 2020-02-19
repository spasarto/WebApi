using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Microsoft.AspNet.OData.Formatter
{
    /// <summary>
    /// Resolves links to external resources.
    /// </summary>
    public class ExternalResourceEntityReferenceLinkResolver
    {
        /// <summary>
        /// Resolves links to external resources.
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="resourceType"></param>
        /// <returns></returns>
        public virtual object Resolve(Uri uri, Type resourceType)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = httpClient.GetAsync(uri).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    var value = JsonConvert.DeserializeObject(json, resourceType);

                    return value;
                }
            }

            return null;
        }
    }
}
