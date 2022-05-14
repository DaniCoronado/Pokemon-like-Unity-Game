using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pokemon 
{
    [SerializeField] PokemonBase _base;
    [SerializeField] int level;

    public PokemonBase Base { get; set; }
    public int Level { get; set; }

    public int HP { get; set; }

    public List<Move> Moves { get; set; }

    public Pokemon(PokemonBase pBase, int plevel)
    {
        Base = pBase;
        Level = plevel;
        HP = MaxHp;

        //Generate Moves
        Moves = new List<Move>();
        foreach(var move in Base.LearnableMoves)
        {
            if (Level > move.level) 
            { 
                Moves.Add(new Move(move.moveBase)); 
            }
            if (Moves.Count >= 4)
            {
                break;
            }
        }
    }

    public int Attack
    {
        get { return Mathf.FloorToInt((Base.Attack * Level)/100f)+5; }
    }
    public int SpAttack
    {
        get { return Mathf.FloorToInt((Base.SpAttack * Level) / 100f) + 5; }
    }
    public int Defense
    {
        get { return Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5; }
    }
    public int SpDefense
    {
        get { return Mathf.FloorToInt((Base.SpDefense * Level) / 100f) + 5; }
    }
    public int Speed
    {
        get { return Mathf.FloorToInt((Base.Speed * Level) / 100f) + 5; }
    }
    public int MaxHp
    {
        get { return Mathf.FloorToInt((Base.MaxHp * Level) / 100f) + 10; }
    }

    public DamageDetails TakeDamage(Move move, Pokemon attacker)
    {
        
        float critical = 1f;
        if (Random.value * 100f <= 6.25f)
        {
            critical = 2f;
        }

        float type = TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type1) * TypeChart.GetEffectiveness(move.Base.Type, this.Base.Type2);

        var damageDetails = new DamageDetails()
        {
            Effectiveness = type,
            Critical = critical,
            Fainted = false
        };

        float attack = (move.Base.IsSpecial) ? attacker.SpAttack : attacker.Attack;
        float defense = (move.Base.IsSpecial) ? SpDefense : Defense;

        float modifiers = Random.Range(0.85f, 1f)*type*critical;
        float a=(2*attacker.Level+10)/250f;
        float d = a * move.Base.Power * ((float)attack / defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        HP-=damage;
        if (HP <= 0)
        {
            HP = 0;
            damageDetails.Fainted=true;
        }
        return damageDetails;
    }
    public Move GetRandomMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }

    public Move SelectMove(Pokemon enemy){
        
        return Moves[GetMove(enemy,1)];
    }

    public int GetMove(Pokemon enemy,int turno)
    {
        int count = 10000000;
        int sol = 0;
        int n;
        for (int i=0 ; i < Moves.Count ; i++){
            Pokemon aux = enemy;
            DamageDetails damage = aux.TakeDamage(Moves[i],this);
            if (damage.Fainted == true){
                n = turno;
            }else{
                n = aux.GetMove(this,turno + 1) * -1;
            }
            if (n > 0 && n < count){
                count = n;
                sol = i;
            }
        }

        if (turno == 1){
            return sol;
        }else{
            return count;
        }
    }
}

public class DamageDetails
{
    public bool Fainted {get; set;}
    public float Critical { get; set;}
    public float Effectiveness { get; set;}
}

