using System;
using Items.Enum;
using UnityEngine;

namespace Items.Data
{ 
    [Serializable]
    public class WeaponDescriptor: ItemDescriptor
    {
        [field: SerializeField] public WeaponType WeaponType { get; private set; }
        
        public WeaponDescriptor(ItemId itemId, ItemType type, Sprite itemSprite, ItemRarity itemRarity, float price,WeaponType weaponType) : base(itemId, type, itemSprite, itemRarity, price)
        {
            WeaponType = weaponType;
        }
    }
}