using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    
    public class Progression : ScriptableObject
    {
        //Added NonReordable as inspector is bugging out
        [NonReorderable]
        [SerializeField] ProgressionCharacterClass[] characterClasses = null;
        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;
        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookup();

            float[] levels = lookupTable[characterClass][stat];

            if(levels.Length < level)
            {
                return 0;
            } 
            return levels[level - 1];
        }

        private void BuildLookup()
        {
            if(lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach(ProgressionCharacterClass progressionClass in characterClasses)
            {
                var statLookupTable = new Dictionary<Stat, float[]>();

                foreach(ProgressionStat progressionStat in progressionClass.stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;
                }

                lookupTable[progressionClass.characterClass] = statLookupTable;
            }
        }

        [System.Serializable]
        
        class ProgressionCharacterClass
        {
            [NonReorderable]
            public CharacterClass characterClass;
            //public float[] health;
            [NonReorderable]
            public ProgressionStat[] stats;

        }
        [System.Serializable]
        class ProgressionStat
        {
            [NonReorderable]
            public Stat stat;
            [NonReorderable]
            public float[] levels;
        }

        //Added this as I can't access inspector values properly without overlapping
        //Copy from QandA 135
        [ContextMenu("Populate All Categories")]
        void BuildDefaultValues()
        {
            var classes = (CharacterClass[])System.Enum.GetValues(typeof(CharacterClass));
            var stats = (Stat[]) System.Enum.GetValues(typeof(Stat));
            characterClasses = new ProgressionCharacterClass[classes.Length];
            for (int i = 0; i < classes.Length; i++)
            {
                ProgressionCharacterClass c = new ProgressionCharacterClass();
                c.characterClass = classes[i];
                c.stats = new ProgressionStat[stats.Length];
                for (int j = 0; j < stats.Length; j++)
                {
                    ProgressionStat s = new ProgressionStat();
                    s.stat = stats[j];
                    c.stats[j] = s;
                }
                characterClasses[i] = c;
            }
        }
    }
}