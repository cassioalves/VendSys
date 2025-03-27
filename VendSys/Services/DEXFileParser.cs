using VendSys.Enum;
using VendSys.Models;
using VendSys.Services.Interface;

namespace VendSys.Services
{
    public class DEXFileParser : IDEXFileParser
    {
        public async Task<List<DEXLaneMeter>> ParseDEXLaneMeterAsync(int dexMeterId, IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());

            // Temporary variables to hold parsed values between lines
            string productIdentifier = string.Empty;
            string price = string.Empty;
            string numberOfVends = string.Empty;
            string valueOfPaidSales = string.Empty;
            var result = new List<DEXLaneMeter>();

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();

                if (string.IsNullOrWhiteSpace(line)) continue;

                // Extract specific values from lines based on the expected identifiers
                productIdentifier = TryExtractField(productIdentifier, line, DEXFileEnum.PRODUCT_IDENTIFIER.Value, DEXFileEnum.PRODUCT_IDENTIFIER.Index);
                price = TryExtractField(price, line, DEXFileEnum.PRICE.Value, DEXFileEnum.PRICE.Index);
                numberOfVends = TryExtractField(numberOfVends, line, DEXFileEnum.NUMBER_OF_VENDS.Value, DEXFileEnum.NUMBER_OF_VENDS.Index);
                valueOfPaidSales = TryExtractField(valueOfPaidSales, line, DEXFileEnum.VALUE_OF_PAID_SALES.Value, DEXFileEnum.VALUE_OF_PAID_SALES.Index);

                // Proceed only when all required fields are available
                if (IsCompleteLaneEntry(productIdentifier, price, numberOfVends, valueOfPaidSales))
                {
                    // Ensure that all fields can be parsed correctly
                    if (TryParseLaneValues(price, numberOfVends, valueOfPaidSales) is false) continue;

                    result.Add(new DEXLaneMeter()
                    {
                        DEXMeterId = dexMeterId,
                        NumberOfVends = int.Parse(numberOfVends),
                        ProductIdentifier = productIdentifier,
                        ValueOfPaidSalves = decimal.Parse(valueOfPaidSales) / 100,
                        Price = decimal.Parse(price) / 100,
                    });

                    // Reset temp values after each complete and valid entry
                    productIdentifier = string.Empty;
                    price = string.Empty;
                    numberOfVends = string.Empty;
                    valueOfPaidSales = string.Empty;

                    continue;
                }
            }

            return result;
        }

        public async Task<DEXMeter> ParseDEXMeterAsync(string machineName, IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());
            string serialNumber = string.Empty;
            string valueOfPaidVends = string.Empty;

            while (!reader.EndOfStream)
            {
                var line = await reader.ReadLineAsync();

                if (string.IsNullOrWhiteSpace(line)) continue;

                // Extract meter-level information (once found, parsing can stop)
                serialNumber = TryExtractField(serialNumber, line, DEXFileEnum.SERIAL_NUMBER.Value, DEXFileEnum.SERIAL_NUMBER.Index);
                valueOfPaidVends = TryExtractField(valueOfPaidVends, line, DEXFileEnum.VALUE_OF_PAID_VENDS.Value, DEXFileEnum.VALUE_OF_PAID_VENDS.Index);

                if (string.IsNullOrEmpty(serialNumber) is false && string.IsNullOrEmpty(valueOfPaidVends) is false) break;
            }

            var result = new DEXMeter()
            {
                Machine = machineName,
                MachineSerialNumber = serialNumber,
                ValueOfPaidVends = decimal.Parse(valueOfPaidVends) / 100,
                DEXDateTime = DateTime.Now,
            };

            return result;
        }

        /// <summary>
        /// Extracts a field value from a line if it matches the expected identifier and the value has not already been set.
        /// </summary>
        private string TryExtractField(string currentValue, string line, string lineId, int lineIndex)
        {
            if (string.IsNullOrEmpty(currentValue) is false) return currentValue;

            if (line.StartsWith(lineId, StringComparison.OrdinalIgnoreCase) is false) return "";

            var parts = line.Split('*', StringSplitOptions.TrimEntries);

            // Defensive: ensure index is valid
            return parts.Length > lineIndex ? parts[lineIndex] : "";
        }

        /// <summary>
        /// Validates whether the field strings can be parsed to their expected numeric types.
        /// </summary>
        private bool TryParseLaneValues(string priceStr, string vendsStr, string valueStr)
        {
            return decimal.TryParse(priceStr, out var parsedPrice)
                && int.TryParse(vendsStr, out var parsedVends)
                && decimal.TryParse(valueStr, out var parsedValue);
        }

        /// <summary>
        /// Checks whether all required fields for a DEXLaneMeter entry are populated.
        /// </summary>
        private bool IsCompleteLaneEntry(string productId, string price, string vends, string value)
        {
            return !string.IsNullOrEmpty(productId)
                && !string.IsNullOrEmpty(price)
                && !string.IsNullOrEmpty(vends)
                && !string.IsNullOrEmpty(value);
        }
    }
}
