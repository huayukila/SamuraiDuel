public interface IPool<T>
{
    /// <summary>
    /// ���z�I�u�W�F�N�g
    /// </summary>
    /// <returns></returns>
    T Allocate();
    /// <summary>
    /// ����I�u�W�F�N�g
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    bool Recycle(T obj);
}
