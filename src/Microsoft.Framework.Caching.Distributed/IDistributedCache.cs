// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;

namespace Microsoft.Framework.Caching.Distributed
{
    public interface IDistributedCache
    {
        void Connect();

        Stream Set(string key, object state, CacheEntryOptions options);

        bool TryGetValue(string key, out Stream value);

        void Refresh(string key);

        void Remove(string key);
    }
}