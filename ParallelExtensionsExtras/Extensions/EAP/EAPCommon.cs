//--------------------------------------------------------------------------
// 
//  Copyright (c) Microsoft Corporation.  All rights reserved. 
// 
//  File: EAPCommon.cs
//
//--------------------------------------------------------------------------

using System.ComponentModel;

namespace System.Threading.Tasks
{
    internal class EAPCommon
    {
        internal static TaskCompletionSource<T> CreateTaskCompletionSource<T>(object state)
        {
            // Helper to create a new TCS with the right state and creation options
            return new TaskCompletionSource<T>(state);
        }

        internal static void TransferCompletionToTask<T>(
            TaskCompletionSource<T> tcs, AsyncCompletedEventArgs e, Func<T> getResult)
        {
            // Transfers the results from the AsyncCompletedEventArgs and getResult() to the
            // TaskCompletionSource, but only AsyncCompletedEventArg's UserState matches the TCS.
            // This latter step is important if the same WebClient is used for multiple, asynchronous
            // operations concurrently.
            if (e.UserState == tcs)
            {
                if (e.Cancelled) tcs.SetCanceled();
                else if (e.Error != null) tcs.SetException(e.Error);
                else tcs.SetResult(getResult());
            }
        }
    }
}