

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Learn_AspNetMVC.ExtendMethods
{
    public static class AppExtends
    {
        public static void AddStatusCodePage(this IApplicationBuilder app)
        {
            app.UseStatusCodePages(appError =>
            {
                appError.Run(async context =>
                {
                    var respone = context.Response;
                    var code = respone.StatusCode;
                    var content = $@"<html>
                        <head></head>
                        <body>
                        <p>Co loi xay ra: {code} - {(HttpStatusCode)code}</p>
                        </body>
                        </html>";
                    await respone.WriteAsync(content);
                });
            });
        }
    }
}
