using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Darkness Balde", menuName = "Skill/Darkness Blade")]
public class DarknessBlade : BaseSkill
{
    public override void UsingSkill(FightingUnit currentUnit, List<FightingUnit> ChosenUnit)
    {
        currentUnit.InitializeEffect(EffectPos.InMiddle,EffectManager.EffectName.TheDarknessBlade, 0.85f);
        Buff IncreaseDamaged = new Buff();

        IncreaseDamaged.type = Buff.Type.IncreaseDamage;
        IncreaseDamaged.duration = 1;

        Texture2D texture = Resources.Load<Texture2D>("Buff/Attack Boost");
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        IncreaseDamaged.Icon = sprite;

        IncreaseDamaged.ValueChange = currentUnit.basicStatsCharacter.AttackDamage * 0.3f;

        IncreaseDamaged.Activation = () =>
        {
            currentUnit.character.AttackDamage += IncreaseDamaged.ValueChange;
        };

        IncreaseDamaged.Deactivation = () =>
        {
            currentUnit.character.AttackDamage -= IncreaseDamaged.ValueChange;
        };

        IncreaseDamaged.Onactivation = () =>
        {
        };

        currentUnit.AddBuff(IncreaseDamaged);
    }
}