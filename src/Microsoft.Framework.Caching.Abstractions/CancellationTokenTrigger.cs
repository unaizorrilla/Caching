// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading;

namespace Microsoft.Framework.Caching
{
    public class CancellationTokenTrigger : IExpirationTrigger
    {
        public CancellationTokenTrigger(CancellationToken cancellationToken)
        {
            Token = cancellationToken;
        }

        private CancellationToken Token { get; set; }

        public bool ActiveExpirationCallbacks
        {
            get { return true; }
        }

        public bool IsExpired
        {
            get { return Token.IsCancellationRequested; }
        }

        public IDisposable RegisterExpirationCallback(Action<object> callback, object state)
        {
            return Token.Register(callback, state);
        }
    }
}