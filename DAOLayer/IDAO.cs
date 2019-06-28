using System.Collections.Generic;

namespace DAOLayer
{
    public interface IDAO<T>
    {
        void Create(IConnection c, T t);

        List<T> Read(IConnection c, int start, int count);

        List<T> ReadAll(IConnection c);

        void Update(IConnection c, T t);
    }
}
