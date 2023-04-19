using System.Collections.Generic;
using Items.Data;
using UnityEngine;

namespace Items.Storage
{
    [CreateAssetMenu(fileName = "ItemsStorage", menuName = "ItemsStorage")]
    public class ItemRarityDescriptorsStorage:ScriptableObject
    {
        [field: SerializeField] public List<RarityDescriptor> RarityDescriptors { get; private set; }
    }
}