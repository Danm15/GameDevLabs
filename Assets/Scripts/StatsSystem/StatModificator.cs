using System;
using Mono.CompilerServices.SymbolWriter;
using StatsSystem.Enum;
using UnityEngine;

namespace StatsSystem
{
    [Serializable]
    public class StatModificator
    {
        [field: SerializeField] public Stat Stat { get; private set; }
        [field: SerializeField] public StatModificatorType Type { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        
        public float StartTime { get; }
        
        public StatModificator(Stat stat, StatModificatorType statModificatorType, float duration, float startTime)
        {
            Stat = stat;
            Type = statModificatorType;
            Duration = duration;
            StartTime = startTime;
        }
        
        public StatModificator(Stat stat, StatModificatorType statModificatorType, float duration)
        {
            Stat = stat;
            Type = statModificatorType;
            Duration = duration;
        }
        
        public StatModificator GetReverseModificator()
        {
            var reverseStat = new Stat(Stat.Type, Type == StatModificatorType.Additive ? -Stat : 1 / Stat);
            return new StatModificator(reverseStat, Type, Duration);
        }
    }
}