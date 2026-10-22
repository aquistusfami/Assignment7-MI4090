# Mã nguồn bài tập tuần 7 - MI4090
### UML 

### Lớp trừu tượng và các lớp dẫn xuất
- `TeachingDevice` là lớp trừu tượng cùng với các lớp dẫn xuất là `ACRemote`, `Micro` và `ProjectorRemote`

### Interface dùng cho kho lưu trữ
- `IRepository` cài đặt làm interface kho lưu trữ.

### Đa hình
- Tính đa hình thể hiện rõ nhất ở 2 abstract method từ `TeachingDevice` là `IsReadyForUse()` và `GetDefaultPriorityScore()` mỗi method được xác định dựa trên object vào thời điểm thực thi.

- Và method `Select` của interface `ISelectionStrategy` được thực thi bởi 2 lớp mỗi lớp có cách thức xây dựng cài đặt riêng.

### Lớp , interface generic
- Có lớp `InMemoryRepository` generic với tham số kiểu constrain vào `IIdentifiable` nhằm xác định được mã của thiết bị chung. cùng với đó là `HighestPriorityStrategy` và `FirstAvailableStrategy`. và thêm lớp `AllocationService` với tham số kiểu `TeachingDevice` constrain nhằm phục vụ các lớp dẫn xuất không phải ép kiểu về đúng dạng.

- Interface generic `IRepository` và `ISelectionStrategy` maybe tương tự. 

### Quyết định thiết kế
- Lựa chọn List cho `items` của `InMemoryRepository` vì dựa trên số lượng thiết bị hay là loại thiết bị hiện tại chỉ có 3 loại và khi lập trình cũng chỉ test với vài thiết bị lẻ tẻ nên sẽ phải đánh đổi bằng việc khả năng mở rộng hay tốc độ khi thiết kế cho hệ thống lớn cần tốc độ khả năng lưu trữ tốt thì nên dùng Dictionary.
