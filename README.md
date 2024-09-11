# PerfectTrip-Server-Csharp
專題轉寫為.net core版本  自我練習

# appsettings.json
因為有帳號密碼等設定, 所以沒有上傳到github
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=資料庫連線位置;Database=PerfectTripDb;User Id=資料庫帳號;Password=資料庫密碼;"
  },
  "Redis": {
    "Hostname": "redis連線位置",
    "Port": port,
    "Password": "密碼",
    "Database": 0
  },
  "JwtSettings": {
    "Issuer": "PerfectTrip-Server",
    "Audience": "PerfectTrip-Client",
    "Secret": "JWT 密鑰",
    "ExpiresInMinutes": 30, // 30分鐘
    "RememberMeExpiresInMinutes": 43200 // 30天
  }
}
```
