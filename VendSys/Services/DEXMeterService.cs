using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using VendSys.Data;
using VendSys.Models;
using VendSys.Services.Interface;

namespace VendSys.Services
{
    public class DEXMeterService : IDEXMeterService
    {
        private readonly string _connectionString;

        public DEXMeterService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public async Task<DEXMeter> SaveAsync(DEXMeter dexMeter)
        {
            using var connection = new SqlConnection(_connectionString);

            // Prepare stored procedure parameters
            var parameters = new DynamicParameters();
            parameters.Add("@Machine", dexMeter.Machine);
            parameters.Add("@DEXDateTime", dexMeter.DEXDateTime);
            parameters.Add("@MachineSerialNumber", dexMeter.MachineSerialNumber);
            parameters.Add("@ValueOfPaidVends", dexMeter.ValueOfPaidVends);

            // Execute stored procedure and retrieve the generated Id (SCOPE_IDENTITY)
            var newId = await connection.ExecuteScalarAsync<int>(
                "InsertDEXMeter",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            dexMeter.Id = newId;

            return dexMeter;
        }

        public async Task<DEXLaneMeter> SaveLaneAsync(DEXLaneMeter lane)
        {
            using var connection = new SqlConnection(_connectionString);

            // Prepare stored procedure parameters
            var parameters = new DynamicParameters();
            parameters.Add("@DEXMeterId", lane.DEXMeterId);
            parameters.Add("@ProductIdentifier", lane.ProductIdentifier);
            parameters.Add("@Price", lane.Price);
            parameters.Add("@NumberOfVends", lane.NumberOfVends);
            parameters.Add("@ValueOfPaidSalves", lane.ValueOfPaidSalves);

            // Execute stored procedure and retrieve the generated Id
            var newId = await connection.ExecuteScalarAsync<int>(
                "InsertDEXLaneMeter",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            lane.Id = newId;
            return lane;
        }
    }
}
