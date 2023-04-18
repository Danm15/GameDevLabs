using System;
using Items.Core;
using Items.Data;
using Items.Enum;
using StatsSystem;

namespace Items
{
    public class ItemsFactory
    {
        private readonly StatsController _statsController;
        public ItemsFactory(StatsController statsController) => _statsController = statsController;

        public Item CreateItem(ItemDescriptor itemDescriptor)
        {
            switch (itemDescriptor.Type)
            {
                case ItemType.Potion:
                    return new Potion(itemDescriptor, _statsController);
                case ItemType.Armor:
                case ItemType.Belt:
                case ItemType.Boots:
                case ItemType.Gloves:
                case ItemType.Helmet:
                case ItemType.Shield:
                case ItemType.Weapon:
                    return new Equipment(itemDescriptor, _statsController,GetEquipmentType(itemDescriptor));
                default:
                    throw new NullReferenceException($"Itrm type{itemDescriptor.Type} is not implemented yet");
            }
        }

        private EquipmentType GetEquipmentType(ItemDescriptor itemDescriptor)
        {
            switch (itemDescriptor.Type)
            {
                case ItemType.Armor:
                    return EquipmentType.Armor;
                case ItemType.Belt:
                    return EquipmentType.Belt;
                case ItemType.Boots:
                    return EquipmentType.Boots;
                case ItemType.Gloves:
                    return EquipmentType.Gloves;
                case ItemType.Helmet:
                    return EquipmentType.Helmet;
                case ItemType.Shield:
                    return EquipmentType.LeftHand;
                case ItemType.Weapon:
                    var weaponDescriptor = itemDescriptor as WeaponDescriptor;
                    switch (weaponDescriptor.WeaponType)
                    {
                        case WeaponType.Bow:
                        case WeaponType.Dual:
                        case WeaponType.TwoHands:
                            return EquipmentType.BothHands;
                        case WeaponType.Caster:
                        case WeaponType.OneHand:
                            return EquipmentType.RightHand;
                    }

                    throw new NullReferenceException("Weapon has wrong type");
                    
                case ItemType.Jewelry:     
                case ItemType.Misc:
                case ItemType.None:
                case ItemType.Potion: 
                default:
                    return EquipmentType.None;
            }
        }
    }
}