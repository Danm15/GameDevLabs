using Items.Data;
using UnityEngine;

namespace Items.Scriptable
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "ItemsSystem/Weapon")]
    public class WeaponScriptable : BaseItemScriptable
    {
        public override ItemDescriptor ItemDescriptor => _weaponDescriptor;
        [SerializeField] private WeaponDescriptor _weaponDescriptor;
    }
}