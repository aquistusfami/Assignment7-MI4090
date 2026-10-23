/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

/// <summary>
/// Lớp ngoại lệ thiết bị thêm vào bị trùng mã
/// </summary>
public class DuplicateIdException : BorrowingException 
{
  public string DuplicateId { get; }

  public DuplicateIdException(string id) 
  : base($"<!> Thiết bị mã {id} đã tồn tại trong database")
  {
    DuplicateId = id;
  }
}
