# Backend dự án cuối kỳ Công nghệ .NET

## Kết nối database
Ở các môi trường làm việc khác nhau, vui lòng setup lại đường dẫn database tương ứng ở file `Program.cs`:

- Ở môi trường dev, uncomment đoạn mã:
```csharp
builder.Configuration.GetConnectionString("DefaultConnection")
```

- Ở môi trường test, uncomment đoạn mã:
```csharp
builder.Configuration.GetConnectionString("OnlineConnection")
```
**Để đảm bảo việc đồng bộ với migration hiện tại, vui lòng chạy đoạn mã sau ở Package Manager Console:**
```console
update-database
```