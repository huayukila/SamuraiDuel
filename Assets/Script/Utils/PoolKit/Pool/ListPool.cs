using System.Collections.Generic;


public static class ListPool<T>
{
    static Stack<List<T>> mListStack = new Stack<List<T>>(8);
    
    /// <summary>
    /// ���X�gpool�l��
    /// </summary>
    /// <returns></returns>
    public static List<T> Get()
    {
        if (mListStack.Count == 0)
        {
            return new List<T>(8);
        }

        return mListStack.Pop();
    }

    /// <summary>
    /// ���z���ꂽ���X�gpool�����ɖ߂��āAstack�֋A��
    /// </summary>
    /// <param name="toRelease"></param>
    /// <exception cref="InvalidOperationException"></exception>
    public static void Release(List<T> toRelease)
    {
        if (mListStack.Contains(toRelease))
        {
            throw new System.InvalidOperationException("The List is released even though it is in the pool");
        }

        toRelease.Clear();
        mListStack.Push(toRelease);
    }
}

public static class ListPoolExtensions
{
    public static void Release2Pool<T>(this List<T> self)
    {
        ListPool<T>.Release(self);
    }
}