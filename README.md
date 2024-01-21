# DỰ ÁN API_HotelManagement (Quản lý nhà nghỉ, khách sạn)

## MÔ TẢ DỰ ÁN
> API_HotelManagement....... {Chưa có mô tả}

## HƯỚNG DẪN TẢI VÀ CÀI ĐẶT

### TẢI DỰ ÁN

- Tải dự án từ link git bằng "git clone" hoặc file zip

> git clone https://github.com/DHY-SOLUTIONS/hotel-api.git

### CẤU HÌNH KẾT NỐI DATABASE

- csdl : PostgreSQL
- cấu hình lại file appsettings.json 
- chỉnh lại 
  "ConnectionStrings": {
     "WebHotelApiDbManagement_PostreSql": "Host=localhost; Port=5432; Database=DBHotelManagement; Username=postgres; Password=HoangTV@123"
  },
- Cấu hình đường dẫn ConnectionStrings phù hợp với postgresql của bạn. 

- Mở Package Manager consoler > nhập: update-database

### RUN APP
--> sau đó khởi chạy được

phím nhanh
- Khởi chạy Debug -> F5
- Khởi chạy Release -> Ctrl + F5
### CÁC ƯU ĐIỂM CỦA DỰ ÁN
