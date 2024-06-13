using System.Net.Http.Headers;

namespace Yotsuba;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Use(async (context, next) =>
        {
            var icic = StringComparison.InvariantCultureIgnoreCase;

            if (context.IsEnPage())
            {
                await next(context);
            }
            else if(context.IsJaPage())
            {
                await next(context);
            }
            else
            {
                var acceptLanguage = context.Request.Headers.AcceptLanguage.FirstOrDefault() ?? "";

                var languages = acceptLanguage.Split(',')
                    .Select(StringWithQualityHeaderValue.Parse)
                    .OrderByDescending(s => s.Quality.GetValueOrDefault(1));

                foreach (var language in languages)
                {
                    if (language.Value.StartsWith("en", icic))
                    {
                        context.Response.Redirect("/en");
                        return;
                    }
                    else if (language.Value.StartsWith("ja", icic))
                    {
                        context.Response.Redirect("/ja");
                        return;
                    }
                }

                context.Response.Redirect("/en");
            }
        });

        app.Run();
    }
}