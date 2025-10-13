using System;
using System.Collections.Generic;

namespace WishList.DbModels;

public partial class WishList
{
    public int WishListId { get; set; }

    public int UserId { get; set; }

    public string Gift { get; set; } = null!;

    public string? Url { get; set; }

    public int Price { get; set; }

    public sbyte? Booked { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual User User { get; set; } = null!;
}
