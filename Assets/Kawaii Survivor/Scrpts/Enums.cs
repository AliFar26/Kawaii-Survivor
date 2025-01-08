public enum GameState
{
        MENU,
        WEAPONSELECTION,
        GAME,
        GAMEOVER,
        STAGECOMPLETE,
        WAVETRANSITION,
        SHOP

}

public enum Stat
{
    Attack,
    AttackSpeed,
    CriticalChance,
    CriticalPercent,
    MoveSpeed,
    MaxHealth,
    Range,
    HealthRecoverySpeed,
    Armor,
    Luck,
    Dodge,
    LifeSteal
}



public static class Enums
{
    public static string FormatStatName(Stat stat)
    {
        string unformated = stat.ToString();
        string formated = "";

        for (int i = 0; i < unformated.Length; i++)
        {
            if (char.IsUpper(unformated[i]))
                formated += " ";

            formated += unformated[i];
        }

        return formated;
    }
}