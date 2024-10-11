using SourceBindingTest.Effects;

namespace SourceBindingTest
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if WINDOWS
            builder.ConfigureEffects(effects =>
            {
                effects.Add<PressedRoutingEffect, PressedPlatformEffect>();
            });
#endif

#if ANDROID
            builder.ConfigureEffects(effects =>
            {
                effects.Add<PressedRoutingEffect, PressedPlatformEffect>();
            });
#endif

#if IOS
            builder.ConfigureMauiHandlers(handlers =>
            {
                //Outcomment this line, to have working, attached properties in effect again!
                handlers.AddHandler<Microsoft.Maui.Controls.CollectionView, Microsoft.Maui.Controls.Handlers.Items2.CollectionViewHandler2>();
            });

            builder.ConfigureEffects(effects =>
            {
                effects.Add<PressedRoutingEffect, PressedPlatformEffect>();
            });
#endif

            return builder.Build();
        }
    }
}
