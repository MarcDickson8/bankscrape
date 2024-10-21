using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace bankscrape
{
    public partial class App : Application
    {
        public static string[]? Args = null;

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                Args = desktop.Args;

                if (Args == null || Args.Length < 2)
                {
                    Console.WriteLine("Provide username and password as arguments.");
                    return; // Exit if there are not enough arguments
                }

                desktop.MainWindow = new MainWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }

        // New method to return args as a List<string>
        public static List<string> GetArguments()
        {
            // Ensure Args is not null
            return Args?.ToList() ?? new List<string>();
        }
    }
}