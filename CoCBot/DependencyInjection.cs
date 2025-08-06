using CoCBot.Configurations;
using CoCBot.Controllers;
using CoCBot.Helpers;
using CoCBot.Interfaces;
using CoCBot.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CoCBot
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBotServices(this IServiceCollection services)
        {
            // Services
            services.AddSingleton<IVisionService, VisionService>();
            services.AddSingleton<IEmulatorService, EmulatorService>();
            services.AddSingleton<IEmulatorSelectorService, EmulatorSelectorService>();
            services.AddSingleton<IClanInviteService, ClanInviteService>();
            services.AddSingleton<IScreenshotStorageService, ScreenshotStorageService>();

            // Helpers
            services.AddSingleton<IClickRandomizer, ClickRandomizer>();

            // Bot Configurations
            services.AddSingleton(new BotPathOptions());

            // Bot Controrller
            services.AddSingleton<IBotController, BotController>();

            return services;
        }
    }
}