using System.Numerics;

namespace TowerProtect.Domain.Inventions.Extensions;

public static class MathExtensions
{
    public static T ProperModulo<T>(this T dividend, T divisor) where T : INumber<T>
    {
        T remainder = dividend % divisor;
        return remainder < T.Zero ? remainder + divisor : remainder;
    }
}
