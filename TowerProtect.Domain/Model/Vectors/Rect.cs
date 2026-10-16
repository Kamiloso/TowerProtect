namespace TowerProtect.Domain.Model.Vectors;

public readonly record struct Rect(Vec2 Center, Vec2 Size)
{
    public Vec2 CornerMain => Center - Size / 2.0;
    public Vec2 CornerOpposite => Center + Size / 2.0;

    public Rect(Vec2 center, double side) : this(center, new Vec2(side, side)) { }

    public static Vec2 MapPoint(Vec2 point, Rect sourceRect, Rect targetRect)
    {
        Vec2 sourceDelta = point - sourceRect.CornerMain;
        Vec2 targetDelta = new(
            x: sourceDelta.x * targetRect.Size.x / sourceRect.Size.x,
            y: sourceDelta.y * targetRect.Size.y / sourceRect.Size.y
        );

        return targetRect.CornerMain + targetDelta;
    }

    public static Vec2 MapVector(Vec2 vector, Rect sourceRect, Rect targetRect)
    {
        return new Vec2(
            x: vector.x * targetRect.Size.x / sourceRect.Size.x,
            y: vector.y * targetRect.Size.y / sourceRect.Size.y
        );
    }

    public static Rect MapRect(Rect rect, Rect sourceRect, Rect targetRect)
    {
        Vec2 targetCenter = MapPoint(rect.Center, sourceRect, targetRect);
        Vec2 targetSize = MapVector(rect.Size, sourceRect, targetRect);

        return new Rect(targetCenter, targetSize);
    }
}
