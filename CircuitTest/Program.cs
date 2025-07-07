using CircuitTest.Components;

using Microsoft.AspNetCore.Components.Server.Circuits;

namespace CircuitTest;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        // MyCircuitHandler를 DI 컨테이너에 Scoped(요청별)로 등록합니다.
        builder.Services.AddScoped<MyCircuitHandler>();

        // Blazor의 CircuitHandler로 MyCircuitHandler를 사용하도록 등록합니다.
        // Blazor 서버는 CircuitHandler 타입의 서비스를 자동으로 감지하여 Circuit 이벤트를 처리합니다.
        builder.Services.AddScoped<CircuitHandler>(sp => sp.GetRequiredService<MyCircuitHandler>());

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
