using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Navigation;
using Chemistry.Data;
using Chemistry.Services;
using Chemistry.ViewModels;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Chemistry
{
    public partial class App : Application
    {
        public static ChemistryDataContext ChemistryDb { get; set; }

        private static Storage _storage;
        public static Storage Storage
        {
            get
            {
                return _storage ?? (_storage = new Storage());
            }
        }

        private static Settings _settings;
        public static Settings Settings
        {
            get
            {
                return _settings ?? (_settings = new Settings());
            }
        }

        //Загрузка данных при запуске приложения
        private static void LoadData()
        {
            //Установка языка приложения
            if (Storage.Exists("LanguageSelectedIndex"))
            {
                Settings.Language.SelectedIndex = (int)Storage.Load("LanguageSelectedIndex");
            }
            else
            {
                var cultureInfo 
                    = Settings.Language.RussianUICulture.Contains(Thread.CurrentThread.CurrentUICulture.Name)
                    ? new CultureInfo("ru-RU")
                    : Settings.Language.SpanishUICulture.Contains(Thread.CurrentThread.CurrentUICulture.Name)
                    ? new CultureInfo("es-ES")
                    : new CultureInfo("en-US");

                Settings.Language.SelectedIndex = Settings.Language.Languages.IndexOf(cultureInfo);
                Storage.Save("LanguageSelectedIndex", Settings.Language.SelectedIndex);
            }
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture =
                Settings.Language.Languages[Settings.Language.SelectedIndex];

            //Создание базы данных. Если она есть, то удаляется и создается вновь
            //Это необходимо на случай, если будут произведены какие-то изменения с элементами, веществами, реакциями и тестами
            using (var db = new ChemistryDataContext(ChemistryDataContext.DbConnectionString))
            {
                if (db.DatabaseExists())
                {
                    db.DeleteDatabase();
                }
                db.CreateDatabase();
                (new Database()).Load();
            }
        }

        //Сохранение данных при сворачивании или закрытии приложения
        private static void SaveData()
        {
            if (Settings.Language.IsChanged) Storage.Save("LanguageSelectedIndex", Settings.Language.SelectingIndex);
        }

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            ThemeManager.ToDarkTheme();
            ThemeManager.OverrideOptions = ThemeManagerOverrideOptions.None; 

            // XNA initialization
            //InitializeXnaApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            LoadData();
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            SaveData();
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            SaveData();
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // AddStorageData the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // ExerciseSet the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}