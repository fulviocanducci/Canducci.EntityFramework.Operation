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
        internal static List<T> GetEntities<T>(DbContext dbContext, Expression<Func<T, bool>> where) 
            where T : class, new()
        {
            return dbContext
                .Set<T>()
                .Where(where)
                .AsNoTrackingWithIdentityResolution()
                .ToList();
        }

        internal static void SetEntities<T>(List<T> entities, DbContext dbContext, Expression<Func<T, int>> property, TypeOperation typeOperation)
            where T : class, new()
        {
            entities.ForEach(c =>
            {
                PropertyEntry<T, int> propertyEntry = dbContext.Entry(c).Property(property);
                if (TypeOperation.Increment == typeOperation)
                {
                    propertyEntry.CurrentValue += 1;
                }
                else
                {
                    propertyEntry.CurrentValue -= 1;
                }
                propertyEntry.IsModified = true;
            });
        }

        internal static void SetEntities<T>(List<T> entities, DbContext dbContext, Expression<Func<T, long>> property, TypeOperation typeOperation)
            where T : class, new()
        {
            entities.ForEach(c =>
            {
                PropertyEntry<T, long> propertyEntry = dbContext.Entry(c).Property(property);
                if (TypeOperation.Increment == typeOperation) 
                {
                    propertyEntry.CurrentValue += 1;
                } 
                else
                {
                    propertyEntry.CurrentValue -= 1;
                }
                propertyEntry.IsModified = true;
            });
        }

        internal static bool BaseOperation<T>(this DbContext dbContext, Expression<Func<T, int>> property, Expression<Func<T, bool>> where, TypeOperation typeOperation)
            where T : class, new()
        {
            List<T> entities = GetEntities(dbContext, where);
            SetEntities(entities, dbContext, property, typeOperation);
            return dbContext.SaveChanges() > 0;
        }

        internal static bool BaseOperation<T>(this DbContext dbContext, Expression<Func<T, long>> property, Expression<Func<T, bool>> where, TypeOperation typeOperation)
            where T : class, new()
        {
            List<T> entities = GetEntities(dbContext, where);
            SetEntities(entities, dbContext, property, typeOperation);
            return dbContext.SaveChanges() > 0;
        }

        public static bool Increment<T>(this DbContext dbContext, Expression<Func<T, int>> property, Expression<Func<T, bool>> where) 
            where T : class, new()
        {
            return BaseOperation(dbContext, property, where, TypeOperation.Increment);
        } 

        public static bool Increment<T>(this DbContext dbContext, Expression<Func<T, long>> property, Expression<Func<T, bool>> where) 
            where T: class, new()
        {
            return BaseOperation(dbContext, property, where, TypeOperation.Increment);
        }


        public static bool Decrement<T>(this DbContext dbContext, Expression<Func<T, int>> property, Expression<Func<T, bool>> where)
            where T : class, new()
        {
            return BaseOperation(dbContext, property, where, TypeOperation.Decrement);
        }

        public static bool Decrement<T>(this DbContext dbContext, Expression<Func<T, long>> property, Expression<Func<T, bool>> where)
            where T : class, new()
        {
            return BaseOperation(dbContext, property, where, TypeOperation.Decrement);
        }


    }
}
