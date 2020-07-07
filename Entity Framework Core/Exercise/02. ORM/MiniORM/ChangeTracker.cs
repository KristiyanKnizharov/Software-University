namespace MiniORM
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    // TODO: Create your ChangeTracker class here.
    internal class ChangeTracker<T>
            where T: class, new()
    {
        private readonly List<T> allEntities;
        private readonly List<T> added;
        private readonly List<T> removed;

        public ChangeTracker(IEnumerable<T> entities)
        {
            this.added = new List<T>();
            this.removed = new List<T>();

            this.allEntities = CloneEntities(entities);
        }

        public IReadOnlyCollection<T> AllEntityes => this.allEntities.AsReadOnly();

        public IReadOnlyCollection<T> Added => this.added.AsReadOnly();

        public IReadOnlyCollection<T> Removed => this.removed.AsReadOnly();

        public void Add(T item) => this.added.Add(item);

        public void Remove(T item) => this.removed.Add(item);

        public IEnumerable<T> GetModifiedEntities(DbSet<T> dbSet)
        {
            var modifiedEntities = new List<T>();

            PropertyInfo[] primaryKeys = typeof(T).GetProperties()
                .Where(pi => pi.HasAttribute<KeyAttribute>())
                .ToArray();

            // foreach of local entiy from ChangeTracker
            foreach (T proxyEntity in this.AllEntityes)
            {
                object[] primaryKeyValues =
                    GetPrimaryKeyValues(primaryKeys, proxyEntity)
                    .ToArray();

                // Entity with same value of keys like proxyEnity
                T entity = dbSet
                    .Entities
                    .Single(e =>
                        GetPrimaryKeyValues(primaryKeys, e)
                        .SequenceEqual(primaryKeyValues));

                bool isModified = IsModified(entity, proxyEntity);

                if (isModified)
                {
                    modifiedEntities.Add(entity);
                }
            }

            return modifiedEntities;
        }

        // entity - Db, proxyEntity - Local(ChangeTracker)
        private static bool IsModified(T entity, T proxyEntity)
        {
            PropertyInfo[] monitoredProperties = typeof(T)
                 .GetProperties()
                 .Where(pi => DbContext.AllowedSqlTypes.Contains(pi.PropertyType))
                 .ToArray();

            PropertyInfo[] modifiedProperties = monitoredProperties
                .Where(pi => !Equals(pi.GetValue(entity), pi.GetValue(proxyEntity)))
                .ToArray();

            return modifiedProperties.Any();
        }

        private IEnumerable<object> GetPrimaryKeyValues(IEnumerable<PropertyInfo> primaryKeys, T proxyEnitity)
        {
            return primaryKeys.Select(pk => pk.GetValue(proxyEnitity));
        }

        private List<T> CloneEntities(IEnumerable<T> entities)
        {
            var clonedEntities = new List<T>();

            PropertyInfo[] propertiesToClone = typeof(T)
                .GetProperties()
                .Where(pi =>
                    DbContext.AllowedSqlTypes.Contains(pi.PropertyType))
                .ToArray();

            foreach (T entity in entities)
            {
                T clone = Activator.CreateInstance<T>();

                foreach (PropertyInfo property in propertiesToClone)
                {
                    object value = property.GetValue(entity);
                    property.SetValue(clone, value);
                }

                clonedEntities.Add(clone);
            }

            return clonedEntities;
        }
    }

}