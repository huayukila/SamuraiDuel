
using System;

public class SimpleObjectPool<T> : Pool<T>
{
    readonly Action<T> mResetMethod;

    /// <summary>
    /// pool�X�g���b�N�֐�
    /// </summary>
    /// <param name="factoryMethod">object ���g�݊֐�</param>
    /// <param name="resetMethod">����������s��reset�֐�</param>
    /// <param name="initCount">pool���������A�܂߂Ă�object��</param>
    public SimpleObjectPool(Func<T> factoryMethod,Action<T> resetMethod=null,int initCount = 0)
    {
        mFactory=new CustomObjectFactory<T>(factoryMethod);
        mResetMethod=resetMethod;

        for(int i = 0; i < initCount; i++)
        {
            mCacheStack.Push(mFactory.Create());
        }
    }

    /// <summary>
    /// �I�u�W�F�N�g�̉���֐�(pool������o����object�����A�g���Ă�������)
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Recycle(T obj)
    {
        mResetMethod?.Invoke(obj);
        mCacheStack.Push(obj);

        return true;
    }
}
