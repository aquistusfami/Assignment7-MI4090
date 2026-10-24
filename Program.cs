namespace Assignment7_MI4090;

class Program
{
    static void Main(string[] args)
    {
        int pass = 0; 
        int fail = 0; 
        
        IRepository<TeachingDevice> Data()
        {
            var repo = new InMemoryRepository<TeachingDevice>();
            repo.Add(new ProjectorRemote("pr001", "Máy chiếu casio", "Phòng chờ GV D8", 50));
            repo.Add(new ProjectorRemote("pr002", "Máy chiếu daikin", "Phòng chờ GV D8", 90));
            repo.Add(new ProjectorRemote("pr003", "Máy chiếu panasonic", "Phòng chờ GV D8", 10));
            repo.Add(new ACRemote("AC001", "Đk điều hòa Panasonic", "Phòng chờ GV D8", new[] { "D8-101", "D8-102" }));
            repo.Add(new ACRemote("AC002", "Đk điều hòa Daikin", "Phòng chờ GV D8", new[] { "D8-201" }));
            repo.Add(new Micro("WM001", "Micro 1", "Phòng chờ GV D8", 90, 95));
            repo.Add(new Micro("WM002", "Micro 2", "Phòng chờ GV D8", 20, 95));
            repo.Add(new Micro("WM003", "Micro 3", "Phòng chờ GV D8", 60, 30));
            return repo;
        }
        
        AllocationService<TeachingDevice> Svc(IRepository<TeachingDevice> repo, ISelectionStrategy<TeachingDevice> strategy)
            => new AllocationService<TeachingDevice>(repo, strategy);

        ISelectionStrategy<TeachingDevice> Highest()
            => new HighestPriorityStrategy<TeachingDevice>(d => d.GetDefaultPriorityScore());
        // 1. Thêm thiết bị
        try
        {
            var repo = Data();
            if(repo.GetAll().Count != 8) throw new Exception($"<!> Phải có 8, thực tế {repo.GetAll().Count}");
            pass++; Console.WriteLine("OK       1. Thêm thiết bị vào repository");
        }
        catch (Exception ex)
        {
            fail++;
            Console.WriteLine($"FAIL       1. Thêm thiết bị vào repository: {ex.Message}");
        }

        // 2. Thêm 2 tbi trùng mã 
        try
        {
            var repo = Data();
            repo.Add(new ProjectorRemote("pr001", "Máy chiếu casio", "Phòng chờ GV D8", 50));
            fail++; Console.WriteLine("FAIL       2. Thêm 2 thiết bị trùng mã");
        }
        catch (DuplicateIdException ex)
        {
            pass++;
            Console.WriteLine($"OK       2. Thêm 2 thiết bị trùng mã: {ex.Message}");
        }

        // 3. Tìm thiết bị theo mã 
        try
        {
            var repo = Data();
            var found = repo.FindById("WM001");
            if (found == null) throw new Exception("WM001 phải tìm thấy");
            if (found.Name != "Micro 1") throw new Exception($"sai tên: {found.Name}");
            if (repo.FindById("abc") != null) throw new Exception("abc phải trả null");
            pass++; Console.WriteLine("OK       3. Tìm theo mã");
        }
        catch (Exception ex)
        {
            fail++;
            Console.WriteLine($"FAIL       3. Tìm theo mã: {ex.Message}");
        }

        // 4. Lọc các thiết bị sẵn sàng
        try
        {
            var repo = Data();
            var ready = repo.FindAll(d => d.IsReadyForUse());
            if (ready.Count != 5) throw new Exception($"phải có 5, thực tế {ready.Count}");
            foreach (var id in new[] { "pr001", "pr002", "AC001", "AC002", "WM001" })
                if (!ready.Any(d => d.Id == id)) throw new Exception($"thiếu {id}");
            foreach (var id in new[] { "pr003", "WM002", "WM003" })
                if (ready.Any(d => d.Id == id)) throw new Exception($"không được có {id}");
            pass++; Console.WriteLine("OK       4. Lọc sẵn sàng");
        }
        catch (Exception ex)
        {
            fail++;
            Console.WriteLine($"FAIL       4. Lọc sẵn sàng: {ex.Message}");
        }

        // 5. Chọn máy chiếu điểm cao nhất 
        try
        {
            var chosen = Svc(Data(), Highest()).Allocate(d => d is ProjectorRemote);
            if (chosen.Id != "pr002") throw new Exception($"phải là pr002, thực tế {chosen.Id}");
            pass++; Console.WriteLine("OK       5. Chọn máy chiếu điểm cao nhất");
        }
        catch (Exception ex)
        {
            fail++;
            Console.WriteLine($"FAIL       5. Chọn máy chiếu điểm cao nhất: {ex.Message}");
        }

        // 6. Chọn điều khiển điều hòa tương ứng phòng
        try
        {
            var d8102= Svc(Data(), Highest()).Allocate(d => d is ACRemote ac && ac.IsCompatibleWith("D8-102"));
            if (d8102.Id != "AC001") throw new Exception($"Phòng D8-102 phải tương ứng điều khiển AC001, thực tế {d8102.Id}");

            var d8201= Svc(Data(), Highest()).Allocate(d => d is ACRemote ac && ac.IsCompatibleWith("D8-201"));
            if (d8201.Id != "AC002") throw new Exception($"Phòng D8-201 phải tương ứng điều khiển AC002, thực tế {d8201.Id}");
            pass++; Console.WriteLine("OK       6. Điều khiển điều hòa tương thích với phòng");
        }
        catch (Exception ex)
        {
            fail++;
            Console.WriteLine($"FAIL       6. Điều khiển điều hòa tương thích với phòng: {ex.Message}");
        }

        // 7. Chọn micro đầu tiên sẵn sàng
        try
        {
            var chosen = Svc(Data(), new FirstAvailableStrategy<TeachingDevice>()).Allocate(d => d is Micro);
            if (chosen.Id != "WM001") throw new Exception($"phải là WM001, thực tế {chosen.Id}");
            pass++; Console.WriteLine("OK       7. Chọn micro đầu tiên sẵn sàng");
        }
        catch (Exception ex)
        {
            fail++;
            Console.WriteLine($"FAIL       7. Chọn micro đầu tiên sẵn sàng: {ex.Message}");
        }

        // 8. Đổi chiến lược khi chạy
        try
        {
            var svc = Svc(Data(), new FirstAvailableStrategy<TeachingDevice>());

            var first = svc.Allocate(d => d is ProjectorRemote);
            if (first.Id != "pr001") throw new Exception($"lần 1 phải là pr001, thực tế {first.Id}");

            svc.ChangeStrategy(Highest());
            var best = svc.Allocate(d => d is ProjectorRemote);
            if (best.Id != "pr002") throw new Exception($"lần 2 phải là pr002, thực tế {best.Id}");

            pass++; Console.WriteLine("OK       8. Đổi chiến lược khi chạy");
        }
        catch (Exception ex)
        {
            fail++;
            Console.WriteLine($"FAIL       8. Đổi chiến lược khi chạy: {ex.Message}");
        }

        // 9. Thử cấp phát khi không có ứng viên phù hợp
        try
        {
            Svc(Data(), Highest()).Allocate(d => d is ACRemote ac && ac.IsCompatibleWith("D9-102"));
            throw new Exception("ngoại lệ NoSuitableItemException cần được ném");
        }
        catch (NoSuitableItemException)
        {
            pass++; Console.WriteLine("OK       9. Không có ứng viên");
        }
        catch (Exception ex)
        {
            fail++; Console.WriteLine($"FAIL       9. Không có ứng viên: {ex.Message}");
        }

        // 10. Thiết bị được chọn chuyển sang Borrowed 
        try
        {
            var svc = Svc(Data(), Highest());
            var chosen = svc.Allocate(d => d is ProjectorRemote);

            if (chosen.Status != DeviceStatus.Borrowed) throw new Exception($"phải là Borrowed, thực tế {chosen.Status}");
            if (chosen.IsReadyForUse()) throw new Exception("đã cấp phát mà vẫn báo sẵn sàng");
            var test = svc.Allocate(d => d is ProjectorRemote);
            if (test.Id == chosen.Id) throw new Exception("cấp phát lần 2 vẫn trả lại thiết bị cũ");

            pass++; Console.WriteLine("OK       10. Chuyển sang Borrowed");
        }
        catch (Exception ex)
        {
            fail++; Console.WriteLine($"FAIL       10. Chuyển sang Borrowed: {ex.Message}");
        }
        
        // 11. Repo tổng quát cho 2 loại 
        try
        {
            var pjrepo = new InMemoryRepository<ProjectorRemote>();
            pjrepo.Add(new ProjectorRemote("pr001", "Máy chiếu casio", "Phòng chờ GV D8", 50));

            var micrepo = new InMemoryRepository<Micro>();
            micrepo.Add(new Micro("WM001", "Micro 1", "Phòng chờ GV D8", 90, 95));

            if (pjrepo.GetAll().Count != 1) throw new Exception("kho máy chiếu phải có 1 thiết bị");
            if (micrepo.GetAll().Count != 1) throw new Exception("kho micro phải có 1 thiết bị");

            pass++; Console.WriteLine("OK       11. Repo tổng quát cho 2 loại");
        }
        catch (Exception ex)
        {
            fail++; Console.WriteLine($"FAIL       11. Repo tổng quát cho 2 loại: {ex.Message}");
        }
        Console.WriteLine();
        Console.WriteLine($"Pass: {pass}        Fail: {fail}");
    }
}
