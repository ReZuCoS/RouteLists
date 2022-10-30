using System;
using System.Configuration;

namespace RouteLists.ViewModel
{
    public static class AppSettings
    {
        private static readonly Configuration Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        public static string ServiceName =>
            Config.AppSettings.Settings[nameof(ServiceName)].Value;

        public static bool DisableSQLServerOnExit
        {
            get
            {
                return Convert.ToBoolean(Config.AppSettings.Settings[nameof(DisableSQLServerOnExit)].Value);
            }
            set
            {
                Config.AppSettings.Settings[nameof(DisableSQLServerOnExit)].Value = value.ToString();
                OnSettingUpdate();
            }
        }

        public static string SyncYear
        {
            get
            {
                return Config.AppSettings.Settings[nameof(SyncYear)].Value;
            }
            set
            {
                Config.AppSettings.Settings[nameof(SyncYear)].Value = value;
                OnSettingUpdate();
            }
        }

        public static string UserSurname
        {
            get
            {
                return Config.AppSettings.Settings[nameof(UserSurname)].Value;
            }
            set
            {
                Config.AppSettings.Settings[nameof(UserSurname)].Value = value;
                OnSettingUpdate();
            }
        }

        public static string UserName
        {
            get
            {
                return Config.AppSettings.Settings[nameof(UserName)].Value;
            }
            set
            {
                Config.AppSettings.Settings[nameof(UserName)].Value = value;
                OnSettingUpdate();
            }
        }

        public static string UserPatronymic
        {
            get
            {
                return Config.AppSettings.Settings[nameof(UserPatronymic)].Value;
            }
            set
            {
                Config.AppSettings.Settings[nameof(UserPatronymic)].Value = value;
                OnSettingUpdate();
            }
        }

        public static string UserCompanyName
        {
            get
            {
                return Config.AppSettings.Settings[nameof(UserCompanyName)].Value;
            }
            set
            {
                Config.AppSettings.Settings[nameof(UserCompanyName)].Value = value;
                OnSettingUpdate();
            }
        }

        public static string UserPosition
        {
            get
            {
                return Config.AppSettings.Settings[nameof(UserPosition)].Value;
            }
            set
            {
                Config.AppSettings.Settings[nameof(UserPosition)].Value = value;
                OnSettingUpdate();
            }
        }

        private static void OnSettingUpdate()
        {
            Config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }
    }
}
