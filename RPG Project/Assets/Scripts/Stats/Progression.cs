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
        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            foreach (ProgressionCharacterClass progressionClass in characterClasses)
            {
                if(progressionClass.characterClass != characterClass) continue;

                foreach(ProgressionStat progressionStat in progressionClass.stats)
                {
                    if(progressionStat.stat != stat) continue;

                    if(progressionStat.levels.Length < level) continue;

                    return progressionStat.levels[level - 1];
                }
            }
            return 30;
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