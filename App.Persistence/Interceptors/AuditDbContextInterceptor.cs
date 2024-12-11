﻿using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Domain.Entities.Common;

namespace App.Persistence.Interceptors;
public class AuditDbContextInterceptor : SaveChangesInterceptor
{
    //Kaydedilmeden önce ve sonra herşeyi burda yapabiliriz.

    private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> Behaviors = new()
    {
        {EntityState.Added,AddBehavior },
         {EntityState.Modified,ModifiedBehavior }
    };



    private static void AddBehavior(DbContext context, IAuditEntity auditEntity)
    {
        auditEntity.Created = DateTime.Now;
        context.Entry(auditEntity).Property(x => x.Updated).IsModified = false;
    }

    private static void ModifiedBehavior(DbContext context, IAuditEntity auditEntity)
    {
        context.Entry(auditEntity).Property(x => x.Created).IsModified = false;
        auditEntity.Updated = DateTime.Now;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
        {
            if (entityEntry.Entity is not IAuditEntity auditEntity) continue;

            if (entityEntry.State is not (EntityState.Added or EntityState.Modified)) continue;

            Behaviors[entityEntry.State](eventData.Context, auditEntity);

            //switch (entityEntry.State)
            //{
            //    case EntityState.Added:

            //        AddBehavior(eventData.Context, auditEntity);


            //        break;
            //    case EntityState.Modified:
            //        ModifiedBehavior(eventData.Context, auditEntity);
            //        break;

            //    case EntityState.Deleted:
            //        break;

            //}

        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }


}