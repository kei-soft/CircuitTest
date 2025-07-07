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

        // MyCircuitHandler�� DI �����̳ʿ� Scoped(��û��)�� ����մϴ�.
        builder.Services.AddScoped<MyCircuitHandler>();

        // Blazor�� CircuitHandler�� MyCircuitHandler�� ����ϵ��� ����մϴ�.
        // Blazor ������ CircuitHandler Ÿ���� ���񽺸� �ڵ����� �����Ͽ� Circuit �̺�Ʈ�� ó���մϴ�.
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
