using System.Collections.Generic;
using Items.Data;
using UnityEngine;

namespace Items.Storage
{
    [CreateAssetMenu(fileName = "ItemsRarityStorage", menuName = "ItemsStorage/ItemsRarityStorage")]
    public class ItemRarityDescriptorsStorage:ScriptableObject
    {
        [field: SerializeField] public List<RarityDescriptor> RarityDescriptors { get; private set; }
    }
}