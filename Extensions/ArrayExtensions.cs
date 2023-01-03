namespace Extensions.Array;

public static class ArrayExtensions
{
    public static void CopyTo<T, U>(this T[] src, U[] dest, int sourceIndex, int destinationIndex, int length, Func<T, U> converter)
    {
        var offset = destinationIndex - sourceIndex;
        for (int i = sourceIndex; i < sourceIndex + length; i++)
            dest[i + offset] = converter(src[i]);
    }
}
