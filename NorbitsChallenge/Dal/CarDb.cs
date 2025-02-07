using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using NorbitsChallenge.Models;

namespace NorbitsChallenge.Dal
{
    public class CarDb
    {
        private readonly IConfiguration _config;

        public CarDb(IConfiguration config)
        {
            _config = config;
        }

        public Car GetCarDetails(int companyId, string licensePlate) //Bytta GetTireCount til GetCarDetails
        {
            Car car = null;
            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Car WHERE CompanyId = @CompanyId AND LicensePlate = @LicensePlate", connection))
                {
                    command.Parameters.AddWithValue("@CompanyId", companyId);
                    command.Parameters.AddWithValue("@LicensePlate", licensePlate);

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            car = new Car
                            {
                                LicensePlate = reader["LicensePlate"].ToString(),
                                Description = reader["Description"]?.ToString(),
                                Model = reader["Model"]?.ToString(),
                                Brand = reader["Brand"]?.ToString(),
                                TireCount = reader.IsDBNull(reader.GetOrdinal("TireCount")) ? 0 : reader.GetInt32(reader.GetOrdinal("TireCount")),
                                CompanyId = companyId
                            };
                        }
                    }
                }
            }

            return car;
        }

        public List<Car> GetAllCars(int companyId)
        {
            var cars = new List<Car>();
            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT * FROM Car WHERE CompanyId = @CompanyId", connection))
                {
                    command.Parameters.AddWithValue("@CompanyId", companyId);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var car = new Car
                            {
                                LicensePlate = reader["LicensePlate"].ToString(),
                                Description = reader["Description"]?.ToString(),
                                Model = reader["Model"]?.ToString(),
                                Brand = reader["Brand"]?.ToString(),
                                TireCount = reader.IsDBNull(reader.GetOrdinal("TireCount")) ? 0 : reader.GetInt32(reader.GetOrdinal("TireCount")),
                            };

                            cars.Add(car);
                        }
                    }
                }
            }

            return cars;
        }

        public void AddCar(Car car)
        {
            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand { Connection = connection, CommandType = System.Data.CommandType.Text })
                {
                    command.CommandText = "INSERT INTO Car (LicensePlate, Description, Model, Brand, TireCount, CompanyId) " +
                                          "VALUES (@LicensePlate, @Description, @Model, @Brand, @TireCount, @CompanyId)";

                    command.Parameters.AddWithValue("@LicensePlate", car.LicensePlate);
                    command.Parameters.AddWithValue("@Description", (object)car.Description ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Model", (object)car.Model ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Brand", (object)car.Brand ?? DBNull.Value);
                    command.Parameters.AddWithValue("@TireCount", car.TireCount);
                    command.Parameters.AddWithValue("@CompanyId", (object)car.CompanyId ?? DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateCar(Car car)
        {
            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(
                    "UPDATE Car SET Description = @Description, Model = @Model, Brand = @Brand, TireCount = @TireCount WHERE LicensePlate = @LicensePlate AND CompanyId = @CompanyId",
                    connection))
                {
                    command.Parameters.AddWithValue("@Description", (object)car.Description ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Model", (object)car.Model ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Brand", (object)car.Brand ?? DBNull.Value);
                    command.Parameters.AddWithValue("@TireCount", car.TireCount);
                    command.Parameters.AddWithValue("@LicensePlate", car.LicensePlate);
                    command.Parameters.AddWithValue("@CompanyId", car.CompanyId);

                    command.ExecuteNonQuery();
                }
            }
        }

        public bool DeleteCar(int companyId, string licensePlate)
        {
            var connectionString = _config.GetSection("ConnectionString").Value;

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("DELETE FROM Car WHERE LicensePlate = @LicensePlate AND CompanyId = @CompanyId", connection))
                {
                    command.Parameters.AddWithValue("@LicensePlate", licensePlate);
                    command.Parameters.AddWithValue("@CompanyId", companyId);

                    int rowsAffected = command.ExecuteNonQuery();
                    return rowsAffected > 0;
                }
            }
        }
    }
}
