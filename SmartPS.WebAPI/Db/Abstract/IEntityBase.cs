using System;

namespace SmartPS.WebAPI.Db.Abstract
{
    public interface IEntityBase<T> where T: IEquatable<T>
    {
        T Id { get; set; }
    }
}
