// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.Framework.Caching.Memory
{
    public static class CacheExtensions
    {
        public static object Set(this IMemoryCache cache, string key, object obj)
        {
            return cache.Set(key, EntryLinkHelpers.ContextLink, state: obj, create: context => context.State);
        }

        public static TItem Set<TItem>(this IMemoryCache cache, string key, TItem obj)
        {
            return (TItem)cache.Set(key, (object)obj);
        }

        public static object Set(this IMemoryCache cache, string key, Func<ICacheSetContext, object> create)
        {
            return cache.Set(key, EntryLinkHelpers.ContextLink, create);
        }

        public static object Set(this IMemoryCache cache, string key, object state, Func<ICacheSetContext, object> create)
        {
            return cache.Set(key, EntryLinkHelpers.ContextLink, state, create);
        }

        public static TItem Set<TItem>(this IMemoryCache cache, string key, Func<ICacheSetContext, TItem> create)
        {
            return (TItem)cache.Set(key, create, context =>
            {
                var myCreate = (Func<ICacheSetContext, TItem>)context.State;
                return (object)myCreate(context);
            });
        }

        public static TItem Set<TItem>(this IMemoryCache cache, string key, object state, Func<ICacheSetContext, TItem> create)
        {
            return (TItem)cache.Set(key, state, context =>
            {
                return (object)create(context);
            });
        }

        public static object Get(this IMemoryCache cache, string key)
        {
            object value = null;
            cache.TryGetValue(key, out value);
            return value;
        }

        public static TItem Get<TItem>(this IMemoryCache cache, string key)
        {
            TItem value = default(TItem);
            cache.TryGetValue<TItem>(key, out value);
            return value;
        }

        public static bool TryGetValue<TItem>(this IMemoryCache cache, string key, out TItem value)
        {
            object obj = null;
            if (cache.TryGetValue(key, EntryLinkHelpers.ContextLink, out obj))
            {
                value = (TItem)obj;
                return true;
            }
            value = default(TItem);
            return false;
        }

        public static object GetOrSet(this IMemoryCache cache, string key, object value)
        {
            object obj;
            if (cache.TryGetValue(key, out obj))
            {
                return obj;
            }
            return cache.Set(key, value);
        }

        public static object GetOrSet(this IMemoryCache cache, string key, Func<ICacheSetContext, object> create)
        {
            object obj;
            if (cache.TryGetValue(key, out obj))
            {
                return obj;
            }
            return cache.Set(key, state: null, create: create);
        }

        public static object GetOrSet(this IMemoryCache cache, string key, object state, Func<ICacheSetContext, object> create)
        {
            object obj;
            if (cache.TryGetValue(key, out obj))
            {
                return obj;
            }
            return cache.Set(key, state, create);
        }

        public static TItem GetOrSet<TItem>(this IMemoryCache cache, string key, Func<ICacheSetContext, TItem> create)
        {
            TItem obj;
            if (cache.TryGetValue(key, out obj))
            {
                return obj;
            }
            return cache.Set(key, create);
        }

        public static TItem GetOrSet<TItem>(this IMemoryCache cache, string key, object state, Func<ICacheSetContext, TItem> create)
        {
            TItem obj;
            if (cache.TryGetValue(key, out obj))
            {
                return obj;
            }
            return cache.Set(key, state, create);
        }

        /// <summary>
        /// Adds inherited trigger and absolute expiration information.
        /// </summary>
        /// <param name="link"></param>
        public static void AddEntryLink(this ICacheSetContext context, IEntryLink link)
        {
            foreach (var trigger in link.Triggers)
            {
                context.AddExpirationTrigger(trigger);
            }

            if (link.AbsoluteExpiration.HasValue)
            {
                context.SetAbsoluteExpiration(link.AbsoluteExpiration.Value);
            }
        }
    }
}