// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Microsoft.Framework.Caching.Memory
{
    public static class CacheExtensions
    {
        public static object Get(this IMemoryCache cache, string key)
        {
            object value = null;
            cache.TryGetValue(key, out value);
            return value;
        }

        public static TItem Get<TItem>(this IMemoryCache cache, string key)
        {
            TItem value;
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

        public static object Set(this IMemoryCache cache, string key, object value)
        {
            return cache.Set(key, value, new CacheEntryOptions());
        }

        public static object Set(this IMemoryCache cache, string key, object value, CacheEntryOptions options)
        {
            return cache.Set(key, value, EntryLinkHelpers.ContextLink, options);
        }

        public static TItem Set<TItem>(this IMemoryCache cache, string key, TItem value)
        {
            return (TItem)cache.Set(key, (object)value, new CacheEntryOptions());
        }

        public static TItem Set<TItem>(this IMemoryCache cache, string key, TItem value, CacheEntryOptions options)
        {
            return (TItem)cache.Set(key, (object)value, options);
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