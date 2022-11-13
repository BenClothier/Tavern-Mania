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
        Liquids = Liquids.OrderBy(x => x.ID).ToList();
    }

    public DrinkMix(List<Liquid> liquids)
    {
        Liquids = liquids;
        Liquids = Liquids.OrderBy(x => x.ID).ToList();
    }

    public void AddLiquid(Liquid liquid)
    {
        Liquids.Add(liquid);
        Liquids = Liquids.OrderBy(x => x.ID).ToList();
    }

    public bool Equals(DrinkMix other)
    {
        return Liquids.SequenceEqual(other.Liquids);
    }

    public void Clear()
    {
        Liquids.Clear();
    }
}
