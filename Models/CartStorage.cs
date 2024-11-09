using System.Collections.Generic;
using Ecycle.Models;

namespace Ecycle.Pages
{
    public static class CartStorage
    {
        public static List<CartItemModel> Items { get; } = new List<CartItemModel>();
    }
}
