using System.Linq;
using System.Collections.Generic;

public struct DrinkMix
{
    public static DrinkMix Empty()
    {
        return new DrinkMix(new Liquid[] {});
    }

    public List<Liquid> Liquids;

    public int LiquidCount => Liquids.Count;

    public DrinkMix(params Liquid[] liquids)
    {
        Liquids = new List<Liquid>(liquids);
        Liquids.OrderBy(x => x.ID);
    }

    public void AddLiquid(Liquid liquid)
    {
        Liquids.Add(liquid);
        Liquids.OrderBy(x => x.ID);
    }

    public bool CompareDrink(DrinkMix other)
    {
        return Liquids.Equals(other.Liquids);
    }

    public void Clear()
    {
        Liquids.Clear();
    }
}
