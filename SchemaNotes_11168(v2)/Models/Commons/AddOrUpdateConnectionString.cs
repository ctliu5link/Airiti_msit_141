using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace SchemaNotes_11168_v2_.Models.Commons
{
    public class AddOrUpdateConnectionString
    {
        /// <summary>
        /// Updates the setting.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void UpdateSetting(string key, string value)
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            if (config.AppSettings.Settings[key] == null)
            {
                config.AppSettings.Settings.Add(key, value);
            }
            else
            {
                config.AppSettings.Settings[key].Value = value;
            }
            config.Save();
            ConfigurationManager.RefreshSection("appSettings");
        }
        /// <summary>
        /// Updates the connection string.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void UpdateConnectionString(string key, string value)
        {
            Configuration config = WebConfigurationManager.OpenWebConfiguration("~");
            if (config.ConnectionStrings.ConnectionStrings[key] == null)
            {
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings(key, value));
            }
            else
            {
                config.ConnectionStrings.ConnectionStrings[key].ConnectionString = value;
            }
            config.Save();
            ConfigurationManager.RefreshSection("connectionStrings");
        }
    }
}