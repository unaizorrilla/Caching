// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.Framework.Caching.Memory;

namespace Microsoft.Framework.Caching
{
    public static class CacheExtensions
    {
        /// <summary>
        /// Sets the priority for keeping this entry in the cache during a memory pressure triggered cleanup.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="priority"></param>
        public static CacheEntryOptions SetPriority(this CacheEntryOptions options, CacheItemPriority priority)
        {
            options.Priority = priority;
            return options;
        }

        /// <summary>
        /// Expire this entry if the given event occurs.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="trigger"></param>
        public static CacheEntryOptions AddExpirationTrigger(
            this CacheEntryOptions options,
            IExpirationTrigger trigger)
        {
            if (options.Triggers == null)
            {
                options.Triggers = new List<IExpirationTrigger>(1);
            }
            options.Triggers.Add(trigger);
            return options;
        }

        ///// <summary>
        ///// Sets an absolute expiration time, relative to now.
        ///// </summary>
        ///// <param name="options"></param>
        ///// <param name="relative"></param>
        //public static CacheEntryOptions SetAbsoluteExpiration(this CacheEntryOptions options, TimeSpan relative)
        //{
        //    if (relative <= TimeSpan.Zero)
        //    {
        //        throw new ArgumentOutOfRangeException(
        //            nameof(relative),
        //            relative,
        //            "The relative expiration value must be positive.");
        //    }
        //    options.SetAbsoluteExpiration(DateTimeOffset.UtcNow + relative);
        //    return options;
        //}

        /// <summary>
        /// Sets an absolute expiration date for this entry.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="absolute"></param>
        public static CacheEntryOptions SetAbsoluteExpiration(this CacheEntryOptions options, DateTimeOffset absolute)
        {
            if (!options.AbsoluteExpiration.HasValue)
            {
                options.AbsoluteExpiration = absolute;
            }
            else if (absolute < options.AbsoluteExpiration.Value)
            {
                options.AbsoluteExpiration = absolute;
            }
            return options;
        }

        /// <summary>
        /// Sets how long this entry can be inactive (e.g. not accessed) before it will be removed.
        /// This will not extend the entry lifetime beyond the absolute expiration (if set).
        /// </summary>
        /// <param name="options"></param>
        /// <param name="offset"></param>
        public static CacheEntryOptions SetSlidingExpiration(this CacheEntryOptions options, TimeSpan offset)
        {
            options.SlidingExpiration = offset;
            return options;
        }

        /// <summary>
        /// The given callback will be fired after this entry is evicted from the cache.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        public static CacheEntryOptions RegisterPostEvictionCallback(
            this CacheEntryOptions options,
            PostEvictionDelegate callback,
            object state)
        {
            if (options.PostEvictionCallbacks == null)
            {
                options.PostEvictionCallbacks = new List<PostEvictionCallbackRegistration>(1);
            }
            options.PostEvictionCallbacks.Add(new PostEvictionCallbackRegistration()
            {
                EvictionCallback = callback,
                State = state
            });
            return options;
        }
    }
}