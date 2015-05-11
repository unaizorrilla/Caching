// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.Framework.Caching.Memory
{
    public static class CacheExtensions
    {
        public static object Set(this IMemoryCache cache, string key, object value)
        {
            return cache.Set(key, value, new CacheEntryOptions());
        }

        public static object Set(
            this IMemoryCache cache,
            string key,
            object value,
            CacheEntryOptions cacheEntryOptions)
        {
            return cache.Set(key, value, EntryLinkHelpers.ContextLink, cacheEntryOptions);
        }

        public static TItem Set<TItem>(this IMemoryCache cache, string key, TItem value)
        {
            return cache.Set(key, value, new CacheEntryOptions());
        }

        public static TItem Set<TItem>(
            this IMemoryCache cache,
            string key,
            TItem value,
            CacheEntryOptions cacheEntryOptions)
        {
            return (TItem)cache.Set(key, value, EntryLinkHelpers.ContextLink, cacheEntryOptions);
        }

        public static TItem Set<TItem>(
            this IMemoryCache cache,
            string key,
            TItem value,
            IEntryLink entryLink,
            CacheEntryOptions cacheEntryOptions)
        {
            return (TItem)cache.Set(key, value, entryLink, cacheEntryOptions);
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

        /// <summary>
        /// Adds inherited trigger and absolute expiration information.
        /// </summary>
        /// <param name="link"></param>
        public static void AddEntryLink(this CacheEntryOptions cacheEntryOptions, IEntryLink link)
        {
            foreach (var trigger in link.Triggers)
            {
                cacheEntryOptions.Triggers.Add(trigger);
            }

            cacheEntryOptions.AbsoluteExpiration = link.AbsoluteExpiration;
        }
    }
}