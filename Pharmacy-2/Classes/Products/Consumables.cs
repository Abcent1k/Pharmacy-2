﻿using Pharmacy_2.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Pharmacy_2.Classes.Products
{
    enum ConsumableType
    {
        Patch,
        Syringe,
        Needle,
        Gauze
    }

    internal class Consumables : Product, IExpiration
    {
		[Required]
		public DateTime ExpirationDate { get; }
		[Required]
		public ConsumableType ConsumableType { get; }

        public Consumables(uint uPC,
                     string name,
                     decimal price,
                     uint eDRPOU,
                     DateTime expirationDate,
                     ConsumableType consumableType) : base(uPC, name, price, eDRPOU)
        {
			ExpirationDate = expirationDate;
			ConsumableType = consumableType;
        }
		public Consumables(uint uPC,
			 string name,
			 decimal price,
			 uint eDRPOU,
			 ConsumableType consumableType) : base(uPC, name, price, eDRPOU)
		{
			ConsumableType = consumableType;
		}

		public override void Show()
        {
            Console.WriteLine(ToString());
        }
        public override string ToString()
        {
            return $"{ConsumableType}\nName: {Name}\nPrice: {Price}";
        }
    }
}
