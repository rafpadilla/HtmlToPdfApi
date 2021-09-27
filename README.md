# Html to Pdf Api
With this service you can generate a pdf file from a html text input, you can consume it through a API.

It is intended to use as a docker container and delegate the creation of pdf to this service.

This service is a wrapper of [Wkhtmltopdf.NetCore](https://github.com/fpanaccia/Wkhtmltopdf.NetCore) library, who implements the wkhtmltopdf library 
working in Windows, Linux, macOS and docker, exposing an API endpoint as service for generate Pdf.

The services accepts the media type `text/html` as body, and has two optional parameters:
- *landscape*: (boolean) with default false,
- *fileName*: if using with swagger and download the file directly with the specified file name.

## Docker image
You can pull the image using `docker pull rafpadilla/htmltopdfapi:latest` ([docker pull rafpadilla/htmltopdfapi:latest](docker pull rafpadilla/htmltopdfapi:latest))

## Endpoint for pdf creation
The endpoint accepts a PUT request with a `text/html` content type, passing a html body you will get the pdf generated.

By default the endpoint is: `http://localhost:5005/api/v1/pdf/getpdf`

## Sample of consumer for this service

```c#
public async Task<byte[]> CreatePdfFromHtml(string htmlTemplate)
{
    var content = new StringContent(htmlTemplate, Encoding.UTF8, MediaTypeNames.Text.Html);
    using (var httpClient = new HttpClient())
    {
        var response = await httpClient.PutAsync("http://localhost:5005/api/v1/pdf/getpdf", content);
        if (response.StatusCode == HttpStatusCode.OK)
        {
            return await response.Content.ReadAsByteArrayAsync();
        }
    }
    return null;
}
```

## More options
You can modify the code and add any other setting during pdf processing.

## Docker Compose
You can run your docker-compose like this sample:

```yaml
version: '3.8'

services:
  htmltopdfapi:
    image: rafpadilla/htmltopdfapi:latest
    container_name: htmltopdfapi
    ports:
      - 5005:80
    restart: unless-stopped
```