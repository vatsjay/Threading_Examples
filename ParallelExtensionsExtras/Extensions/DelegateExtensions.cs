//--------------------------------------------------------------------------
// 
//  Copyright (c) Microsoft Corporation.  All rights reserved. 
// 
//  File: DelegateExtensions.cs
//
//--------------------------------------------------------------------------

using System.Threading.Tasks;

namespace System
{
    /// <summary>Parallel extensions for the Delegate class.</summary>
    public static class DelegateExtensions
    {
        /// <summary>Dynamically invokes (late-bound) in parallel the methods represented by the delegate.</summary>
        /// <param name="multicastDelegate">The delegate to be invoked.</param>
        /// <param name="args">An array of objects that are the arguments to pass to the delegates.</param>
        /// <returns>The return value of one of the delegate invocations.</returns>
        public static object ParallelDynamicInvoke(this Delegate multicastDelegate, params object[] args)
        {
            if (multicastDelegate == null) throw new ArgumentNullException("multicastDelegate");
            if (args == null) throw new ArgumentNullException("args");
            object result = null;
            Parallel.ForEach(multicastDelegate.GetInvocationList(), d => result = d.DynamicInvoke(args));
            return result;
        }
    }
}