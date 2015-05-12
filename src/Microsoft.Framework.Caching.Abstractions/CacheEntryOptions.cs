// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Microsoft.Framework.Caching.Memory;

namespace Microsoft.Framework.Caching
{
    public class CacheEntryOptions
    {
        public CacheEntryOptions()
        {
            Triggers = new List<IExpirationTrigger>(1);
            PostEvictionCallbacks = new List<PostEvictionCallbackRegistration>(1);
        }

        /// <summary>
        /// Gets or sets an absolute expiration date for this entry.
        /// </summary>
        public DateTimeOffset? AbsoluteExpiration { get; set; }

        /// <summary>
        /// Gets or sets how long this entry can be inactive (e.g. not accessed) before it will be removed.
        /// This will not extend the entry lifetime beyond the absolute expiration (if set).
        /// </summary>
        public TimeSpan? SlidingExpiration { get; set; }

        /// <summary>
        /// Gets or sets the events which are fired when the cache entry expires.
        /// </summary>
        public IList<IExpirationTrigger> Triggers { get; set; }

        /// <summary>
        /// Gets or sets the callbacks will be fired after the cache entry is evicted from the cache.
        /// </summary>
        public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks { get; set; }

        /// <summary>
        /// Gets or sets the priority for keeping this entry in the cache during a memory pressure triggered cleanup.
        /// </summary>
        /// <param name="priority"></param>
        public CacheItemPriority Priority { get; set; }
    }
}