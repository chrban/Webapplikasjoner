using Kaffeplaneten.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kaffeplaneten
{
    public class ShoppingCart
    {
        public List<CartItem> ItemsInShoppingCart { get; set; }

        public bool createShoppingCart()
        {
            if (ItemsInShoppingCart == null)
            {
                ItemsInShoppingCart = new List<CartItem>();
                return true;
            }
            return false;
        }

        public bool addToCart(Products newProd, int quantity)
        {
            try
            {
                foreach (var item in ItemsInShoppingCart)
                {
                    if (item.product.productID == newProd.productID)
                    {
                        item.Quanitity += quantity;
                        return true;
                    }
                }
                var newCartItem = new CartItem();
                newCartItem.product = newProd;
                newCartItem.Quanitity = quantity;
                ItemsInShoppingCart.Add(newCartItem);
            }
            catch(Exception error) {
                Console.WriteLine("FAILED TO ADD ITEM: " + newProd.productID + " TO CART!");
            };
            return false;
        }

        public void removeFromCart(int prodId)
        {
            try
            {
                foreach (var item in ItemsInShoppingCart)
                {
                    if (item.product.productID == prodId)
                    {
                        ItemsInShoppingCart.Remove(item);
                        return;
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine("FAILED TO REMOVE ITEM: " + prodId + " TO CART!");
            };
        }

        public double calculateTotal()
        {
            double totalPrice = 0;
            foreach (var item in ItemsInShoppingCart)
            {
                totalPrice += (item.product.price * item.Quanitity);
            }
            return totalPrice;
        }

        public int amountOfItems()
        {
            return ItemsInShoppingCart.Count;
        }
    }

    public class CartItem{
        public Products product { get; set; }
        public int Quanitity { get; set; }
    }
}