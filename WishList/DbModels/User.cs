using System;
using System.Collections.Generic;

namespace WishList.DbModels;

public partial class User
{
    public int Id { get; set; }

    public string UserName { get; set; } = null!;

    public string UserPassword { get; set; } = null!;

    public DateOnly Birthday { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<WishList> WishLists { get; set; } = new List<WishList>();
}
