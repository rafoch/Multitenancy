using System;
using System.ComponentModel.DataAnnotations;

namespace Multitenancy.Common.EntityBase
{
    public abstract class BaseEntity<T> where T: IEquatable<T>
    {
        [Key] public T Id { get; set; }
    }
}