using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLab2.DAL.Entities;

namespace WebLab2.Models
{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; }
        public Cart()
        {
            Items = new Dictionary<int, CartItem>();
        }
        public int Count
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity);
            }
        }

        public int Price
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity * item.Value.Transport.Price);
            }
        }

        public virtual void AddToCart(Transport transport)
        {
            if (Items.ContainsKey(transport.TransportId))
                Items[transport.TransportId].Quantity++;
            else
                Items.Add(transport.TransportId, new CartItem
                {
                    Transport = transport,
                    Quantity = 1
                });
        }

        public virtual void RemoveFromCart(int id)
        {
            Items.Remove(id);
        }

        public virtual void ClearAll()
        {
            Items.Clear();
        }
    }
}
