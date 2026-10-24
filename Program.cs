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
            foreach (var id in new[] { "or003", "WM002", "WM003" })
                if (ready.Any(d => d.Id == id)) throw new Exception($"không được có {id}");
            pass++; Console.WriteLine("OK       4. Loc san sang");
        }
        catch (Exception ex)
        {
            fail++;
            Console.WriteLine($"FAIL       4. Lọc sẵn sàng: {ex.Message}");
        }
    }
}
