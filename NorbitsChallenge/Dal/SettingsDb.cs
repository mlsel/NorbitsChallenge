using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Bll;

namespace NorbitsChallenge.Dal
{
    public class SettingsDb
    {
        private readonly IConfiguration _config;

        public SettingsDb(IConfiguration config)
        {
            _config = config;
        }

        public string GetCompanyName(int companyId)
        {
            string companyName = "Ukjent selskap";

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand {Connection = connection, CommandType = CommandType.Text})
                {
                    command.CommandText = "select * from settings where setting = 'companyname'";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["companyId"].ToString() == companyId.ToString())
                            {
                                companyName = reader["settingValue"].ToString();
                            }
                        }
                    }
                }
            }

            return companyName;
        }

        public List<Setting> GetSettings(int companyId)
        {
            var settings = new List<Setting>();

            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = CommandType.Text })
                {
                    command.CommandText = $"select * from settings where companyId = {companyId}";

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var setting = new Setting();

                            setting.Key = reader["setting"].ToString();
                            setting.Value = reader["settingValue"].ToString();
                            setting.CompanyId = companyId;

                            settings.Add(setting);
                        }
                    }
                }
            }

            if (!settings.Any(s => s.Key == "companyname"))
            {
                UpdateSetting(new Setting
                {
                    Key = "companyname",
                    Value = "Ukjent selskap (default)",
                    CompanyId = companyId
                }, companyId);

                // Refresh instillinger
                return GetSettings(companyId);
            }

            return settings;
        }

        public void UpdateSetting(Setting setting, int companyId)
        {
            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqlCommand
                {
                    Connection = connection,
                    CommandType = CommandType.Text,
                    CommandText = "UPDATE Settings SET settingValue = @Value " +
                                  "WHERE setting = @Key AND companyId = @CompanyId"
                })
                {
                    command.Parameters.AddWithValue("@Value", setting.Value);
                    command.Parameters.AddWithValue("@Key", setting.Key);
                    command.Parameters.AddWithValue("@CompanyId", companyId);

                    // UPDATE
                    int rowsAffected = command.ExecuteNonQuery();

                    // INSERT
                    if (rowsAffected == 0)
                    {
                        using (var insertCommand = new SqlCommand
                        {
                            Connection = connection,
                            CommandType = CommandType.Text,
                            CommandText = "INSERT INTO Settings (setting, settingValue, companyId) " +
                                          "VALUES (@Key, @Value, @CompanyId)"
                        })
                        {
                            insertCommand.Parameters.AddWithValue("@Key", setting.Key);
                            insertCommand.Parameters.AddWithValue("@Value", setting.Value);
                            insertCommand.Parameters.AddWithValue("@CompanyId", companyId);

                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

    }
}