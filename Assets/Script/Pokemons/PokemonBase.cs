using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName="Pokemon", menuName = "Pokemon/Create new Pokemon")]

public class PokemonBase : ScriptableObject
{
    [SerializeField] string name;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite frontSprite;
    [SerializeField] Sprite backSprite;

    [SerializeField] PokemonType type1;
    [SerializeField] PokemonType type2;

    // Base Stats
    [SerializeField] int maxHp=0;
    [SerializeField] int attack = 0;
    [SerializeField] int defense = 0;
    [SerializeField] int spAttack = 0;
    [SerializeField] int spDefense = 0;
    [SerializeField] int speed = 0;

    [SerializeField] List<LearnableMoves> learneableMoves;

    void Start()
    {
        frontSprite = Resources.Load<Sprite>("4");
        backSprite = Resources.Load<Sprite>("4");
    }

    public string Name
    {
        get { return name; }
    }
    public string Description
    {
        get { return description; }
    }
    public Sprite FrontSprite
    {
        get { return frontSprite; }
    }
    public Sprite BackSprite
    {
        get { return backSprite; }
    }
    public PokemonType Type1
    {
        get { return type1; }
    }
    public PokemonType Type2
    {
        get { return type2; }
    }
    public int MaxHp
    {
        get { return maxHp; }
    }
    public int Attack
    {
        get { return attack; }
    }
    public int Defense
    {
        get { return defense; }
    }
    public int Speed
    {
        get { return speed; }
    }
    public int SpAttack
    {
        get { return spAttack;  }
    }
    public int SpDefense
    {
        get { return spDefense; }
    }
    public List<LearnableMoves> LearnableMoves
    {
        get { return learneableMoves; }
    }

}

public enum PokemonType
{
    None,
    Normal,
    Fire,
    Water,
    Electric,
    Grass,
    Ice,
    Fighting,
    Poison,
    Ground,
    Flying,
    Psychic,
    Bug,
    Rock,
    Ghost,
    Dragon
}
public class TypeChart
{
    static float[][] chart =
   {                           //Nor       Fir     Wat     Ele     Gra     Ice     Figt    Poi     Gro     Fly     Psy     Bug     Roc     Gho     Dra     Dar     Ste
        /*Normal*/      new float[]{1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     0.5f,   0f,     1f,     1f,     0.5f},
       /* Fire */       new float[]{1f,     0.5f,   0.5f,   1f,     2f,     2f,     1f,     1f,     1f,     1f,     1f,     2f,     0.5f,   1f,     0.5f,   1f,     2f},
       /* Water   */    new float[]{1f,     2f,     0.5f,   1f,     0.5f,   1f,     1f,     1f,     2f,     1f,     1f,     1f,     2f,     1f,     0.5f,   1f,     1f},
       /* Electric  */  new float[]{1f,     1f,     2f,     0.5f,   0.5f,   1f,     1f,     1f,     0f,     2f,     1f,     1f,     1f,     1f,     0.5f,   1f,     1f},
       /* Grass    */   new float[]{1f,     0.5f,   2f,     0.5f,   1f,     1f,     1f,     0.5f,   2f,     0.5f,   1f,     0.5f,   2f,     1f,     0.5f,   1f,     0.5f},
       /* Ice      */   new float[]{1f,     0.5f,   0.5f,   1f,     2f,     0.5f,   1f,     1f,     2f,     2f,     1f,     1f,     1f,     1f,     2f,     1f,     0.5f},
       /* Fighting  */  new float[]{2f,     1f,     1f,     1f,     1f,     2f,     1f,     0.5f,   1f,     0.5f,   0.5f,   0.5f,   2f,     0f,     1f,     2f,     2f},
       /* Poison   */   new float[]{1f,     1f,     1f,     1f,     2f,     1f,     1f,     0.5f,   0.5f,   1f,     1f,     1f,     0.5f,   0.5f,   1f,     1f,     0f},
       /* Ground   */   new float[]{1f,     2f,     1f,     2f,     0.5f,   1f,     1f,     2f,     1f,     0f,     1f,     0.5f,   2f,     1f,     1f,     1f,     2f},
      /*  Flying   */   new float[]{1f,     1f,     1f,     0.5f,   2f,     1f,     2f,     1f,     1f,     1f,     1f,     2f,     0.5f,   1f,     1f,     1f,     0.5f},
       /* Psychic */    new float[]{1f,     1f,     1f,     1f,     1f,     1f,     1f,     2f,     2f,     1f,     1f,     0.5f,  1f,      1f,     1f,     0f,     1f},
       /* Bug     */    new float[]{1f,     0.5f,   1f,     1f,     2f,     1f,     0.5f,   0.5f,   1f,     0.5f,   2f,     1f,     1f,     0.5f,   1f,     2f,     0.5f},
       /* Rock     */   new float[]{1f,     2f,     1f,     1f,     1f,     2f,     0.5f,   1f,     0.5f,   2f,     1f,     2f,     1f,     1f,     1f,     1f,     0.5f},
       /* Ghost */      new float[]{0f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     2f,     1f,     1f,     2f,     1f,     0.5f,   1f},
       /* Dragon  */    new float[]{1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     2f,     1f,     0.5f},
      /*  Dark   */     new float[]{1f,     1f,     1f,     1f,     1f,     1f,     0.5f,   1f,     1f,     1f,     2f,     1f,     1f,     2f,     1f,     0.5f,   1f},
      /* Steel   */     new float[]{1f,     0.5f,   0.5f,   0.5f,   2f,     1f,     1f,     1f,     1f,     1f,     1f,     1f,     2f,     1f,     1f,     1f,     0.5f}
        };

    public static float GetEffectiveness(PokemonType attackType, PokemonType defenseType)
    {
        if (attackType == PokemonType.None || defenseType == PokemonType.None)
        {
            return 1;
        }
        int row = (int)attackType - 1;
        int col = (int)defenseType - 1;
        return (float)TypeChart.chart[row][col];
    }
}



[System.Serializable]
public class LearnableMoves
{
    [SerializeField] public MoveBase moveBase;
    [SerializeField] public int level;
}