using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Movement.Data
{
    [Serializable]
    public class JumpData
    {
        [field: SerializeField] public float RaycastDistance { get; private set; }
        [field: SerializeField] public LayerMask GroundLayerMask{ get; private set; }
    }
}