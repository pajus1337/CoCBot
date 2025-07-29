using Microsoft.Extensions.DependencyInjection;

using CoCBot.Services;
using CoCBot.Interfaces;
using CoCBot.Controllers;
using CoCBot.Forms;

namespace CoCBot
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBotServices(this IServiceCollection services)
        {
            services.AddSingleton<IVisionService, VisionService>();
            services.AddSingleton<IEmulatorService, EmulatorService>();
            services.AddSingleton<BotController>();
            services.AddSingleton<MainForm>();
            return services;
        }
    }
}