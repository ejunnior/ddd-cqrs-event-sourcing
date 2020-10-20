namespace Finance.Domain.Core
{
    using System;

    public abstract class BaseEntity
    {
    }

    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract class Entity<TId> : BaseEntity, IEquatable<Entity<TId>>
    {
        #region Members

        private int? _requestedHashCode;
        public TId Id { get; protected set; }

        #endregion Members

        #region Properties

        /// <summary>
        /// Get or set the persisten object identifier
        /// </summary>
        //public virtual TId Id
        //{
        //    get
        //    {
        //        return _Id;
        //    }
        //    set
        //    {
        //        _Id = value;

        //        OnIdChanged();
        //    }
        //}

        #endregion Properties

        #region Constructor

        protected Entity()
        {
        }

        protected Entity(TId id)
        {
            if (object.Equals(id, default(TId)))
                throw new ArgumentException("The Id can not be the type's default value.", "id");

            this.Id = id;
        }

        #endregion Constructor

        #region Abstract Methods

        /// <summary>
        /// When POID is changed
        /// </summary>
        protected virtual void OnIdChanged()
        {
        }

        #endregion Abstract Methods

        #region Public Methods

        /// <summary>
        /// Check if this entity is transient, ie, without identity at this moment
        /// </summary>
        /// <returns>True if entity is transient, else false</returns>
        //TODO: need to be refacotry
        public bool IsTransient()
        {
            return object.Equals(this.Id, default(TId));
        }

        #endregion Public Methods

        #region Override Methods

        public override bool Equals(object other)
        {
            var entity = other as Entity<TId>;

            if (entity != null)
                return this.Equals(entity);

            return base.Equals(other);
        }

        //public override bool Equals(object obj)
        //{
        //    if (obj == null || !(obj is Entity))
        //        return false;

        //    if (Object.ReferenceEquals(this, obj))
        //        return true;

        //    Entity item = (Entity)obj;

        //    if (item.IsTransient() || this.IsTransient())
        //        return false;
        //    else
        //        return item.Id == this.Id;
        //}

        //TODO: need to be refacotry
        //public override int GetHashCode()
        //{
        //    if (!IsTransient())
        //    {
        //        if (!_RequestedHashCode.HasValue)
        //            _RequestedHashCode = this.Id.GetHashCode() ^ 31;

        //        return _RequestedHashCode.Value;
        //    }
        //    else
        //        return base.GetHashCode();

        //}

        //TODO: need to be refacotry
        //public static bool operator ==(Entity left, Entity right)
        //{
        //    if (Object.Equals(left, null))
        //        return (Object.Equals(right, null)) ? true : false;
        //    else
        //        return left.Equals(right);
        //}

        //public static bool operator !=(Entity left, Entity right)
        //{
        //    return !(left == right);
        //}

        #endregion Override Methods

        public bool Equals(Entity<TId> other)
        {
            if (other == null)
                return false;

            return this.Id.Equals(other.Id);
        }
    }
}