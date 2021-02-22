using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Canducci.EntityFramework.Operation
{

    public static class EntityFrameworkOperationExtension
    {
        #region GetEntities
        internal static List<T> GetEntities<T>(DbContext dbContext, Expression<Func<T, bool>> where) 
            where T : class, new()
        {
            return dbContext
                .Set<T>()
                .Where(where)
                .AsNoTrackingWithIdentityResolution()
                .ToList();
        }
        #endregion

        #region SetEntities
        internal static void SetEntities<T>(List<T> entities, DbContext dbContext, Expression<Func<T, int>> property, TypeOperation typeOperation, int value)
            where T : class, new()
        {
            entities.ForEach(c =>
            {
                PropertyEntry<T, int> propertyEntry = dbContext.Entry(c).Property(property);
                if (TypeOperation.Increment == typeOperation)
                {
                    propertyEntry.CurrentValue += value;
                }
                else
                {
                    propertyEntry.CurrentValue -= value;
                }
                propertyEntry.IsModified = true;
            });
        }

        internal static void SetEntities<T>(List<T> entities, DbContext dbContext, Expression<Func<T, long>> property, TypeOperation typeOperation, long value)
            where T : class, new()
        {
            entities.ForEach(c =>
            {
                PropertyEntry<T, long> propertyEntry = dbContext.Entry(c).Property(property);
                if (TypeOperation.Increment == typeOperation) 
                {
                    propertyEntry.CurrentValue += value;
                } 
                else
                {
                    propertyEntry.CurrentValue -= value;
                }
                propertyEntry.IsModified = true;
            });
        }

        #endregion

        #region BaseOperation
        internal static void BaseOperation<T>(this DbContext dbContext, Expression<Func<T, int>> property, Expression<Func<T, bool>> where, TypeOperation typeOperation, int value)
            where T : class, new()
        {
            List<T> entities = GetEntities(dbContext, where);
            SetEntities(entities, dbContext, property, typeOperation, value);
        }

        internal static void BaseOperation<T>(this DbContext dbContext, Expression<Func<T, long>> property, Expression<Func<T, bool>> where, TypeOperation typeOperation, long value)
            where T : class, new()
        {
            List<T> entities = GetEntities(dbContext, where);
            SetEntities(entities, dbContext, property, typeOperation, value);
        }

        #endregion

        #region Increment
        public static void Increment<T>(this DbContext dbContext, Expression<Func<T, int>> property, Expression<Func<T, bool>> where, int value = 1) 
            where T : class, new()
        {
            BaseOperation(dbContext, property, where, TypeOperation.Increment, value);
        } 

        public static void Increment<T>(this DbContext dbContext, Expression<Func<T, long>> property, Expression<Func<T, bool>> where, long value = 1) 
            where T: class, new()
        {
            BaseOperation(dbContext, property, where, TypeOperation.Increment, value);
        }
        #endregion

        #region Decrement
        public static void Decrement<T>(this DbContext dbContext, Expression<Func<T, int>> property, Expression<Func<T, bool>> where, int value = 1)
            where T : class, new()
        {
            BaseOperation(dbContext, property, where, TypeOperation.Decrement, value);
        }

        public static void Decrement<T>(this DbContext dbContext, Expression<Func<T, long>> property, Expression<Func<T, bool>> where, long value = 1)
            where T : class, new()
        {
            BaseOperation(dbContext, property, where, TypeOperation.Decrement, value);
        }
        #endregion

    }
}
