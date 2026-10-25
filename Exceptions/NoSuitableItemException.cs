/****************/
// 202418988
// Nguyễn Văn Thế
/****************/
using System;

namespace Assignment7_MI4090;

/// <summary>
/// Lớp ngoại lệ không có thiết bị phù hợp trong chiến lược
/// </summary>
public class NoSuitableItemException : BorrowingException
{
  public NoSuitableItemException() : base("<!> Không có thiết bị phù hợp")
  {
    
  }
}
