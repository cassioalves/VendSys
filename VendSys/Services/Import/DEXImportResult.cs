namespace VendSys.Services.Import
{
    public class DEXImportResult
    {
        public bool IsSuccess { get; set; }
        public string? Error { get; set; }
        public object? Payload { get; set; }

        public static DEXImportResult Success(object payload) => new() { IsSuccess = true, Payload = payload };

        public static DEXImportResult Fail(string error) => new() { IsSuccess = false, Error = error };
    }
}
