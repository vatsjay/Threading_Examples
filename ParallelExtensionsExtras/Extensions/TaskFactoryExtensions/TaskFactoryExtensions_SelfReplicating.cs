////--------------------------------------------------------------------------
//// 
////  Copyright (c) Microsoft Corporation.  All rights reserved. 
//// 
////  File: TaskFactoryExtensions_SelfReplicating.cs
////
////--------------------------------------------------------------------------

//namespace System.Threading.Tasks
//{
//    /// <summary>Adds extension methods on to TaskFactory for creating self replicating tasks.</summary>
//    public static partial class TaskFactoryExtensions
//    {
//        /// <summary>Creates and starts a self-replicating task.</summary>
//        /// <param name="factory">The TaskFactory that contains the default values for creating new tasks.</param>
//        /// <param name="action">The action delegate to execute asynchronously in each replica.</param>
//        /// <param name="shouldReplicate">A function that determines whether another replica should be created.</param>
//        /// <returns>The started Task.</returns>
//        public static Task StartNewSelfReplicating(
//            this TaskFactory factory,
//            Action action)
//        {
//            return StartNewSelfReplicating(factory, action, () => true, factory.CreationOptions);
//        }

//        /// <summary>Creates and starts a self-replicating task.</summary>
//        /// <param name="factory">The TaskFactory that contains the default values for creating new tasks.</param>
//        /// <param name="action">The action delegate to execute asynchronously in each replica.</param>
//        /// <param name="shouldReplicate">A function that determines whether another replica should be created.</param>
//        /// <returns>The started Task.</returns>
//        public static Task StartNewSelfReplicating(
//            this TaskFactory factory,
//            Action action, Func<Boolean> shouldReplicate)
//        {
//            return StartNewSelfReplicating(factory, action, shouldReplicate, factory.CreationOptions);
//        }

//        /// <summary>Creates and starts a self-replicating task.</summary>
//        /// <param name="factory">The TaskFactory that contains the default values for creating new tasks.</param>
//        /// <param name="action">The action delegate to execute asynchronously in each replica.</param>
//        /// <param name="shouldReplicate">A function that determines whether another replica should be created.</param>
//        /// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created Task.</param>
//        /// <returns>The started Task.</returns>
//        public static Task StartNewSelfReplicating(
//            this TaskFactory factory,
//            Action action, Func<Boolean> shouldReplicate, 
//            TaskCreationOptions creationOptions)
//        {
//            return StartNewSelfReplicating(factory, action, shouldReplicate, creationOptions, factory.Scheduler);
//        }

//        /// <summary>Creates and starts a self-replicating task.</summary>
//        /// <param name="factory">The TaskFactory that contains the default values for creating new tasks.</param>
//        /// <param name="action">The action delegate to execute asynchronously in each replica.</param>
//        /// <param name="shouldReplicate">A function that determines whether another replica should be created.</param>
//        /// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created Task.</param>
//        /// <param name="cancellationToken">The CancellationToken to assign to the Task.</param>
//        /// <param name="scheduler">The TaskScheduler that is used to schedule the Task.</param>
//        /// <returns>The started Task.</returns>
//        public static Task StartNewSelfReplicating(
//            this TaskFactory factory,
//            Action action, Func<Boolean> shouldReplicate,
//            CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
//        {
//            Task t = new SelfReplicatingTask(action, shouldReplicate, cancellationToken, creationOptions);
//            t.Start(scheduler);
//            return t;
//        }
//    }

//    /// <summary>Provides a task capable of replicating itself.</summary>
//    public sealed class SelfReplicatingTask : Task
//    {
//        /// <summary>Initializes the ReplicableTask.</summary>
//        /// <param name="action">The action delegate to execute asynchronously in each replica.</param>
//        public SelfReplicatingTask(Action action) :
//            this(action, () => true, TaskCreationOptions.None) { }

//        /// <summary>Initializes the ReplicableTask.</summary>
//        /// <param name="action">The action delegate to execute asynchronously in each replica.</param>
//        /// <param name="shouldReplicate">A function that determines whether another replica should be created.</param>
//        public SelfReplicatingTask(Action action, Func<Boolean> shouldReplicate) :
//            this(action, shouldReplicate, TaskCreationOptions.None) { }

//        /// <summary>Initializes the ReplicableTask.</summary>
//        /// <param name="action">The action delegate to execute asynchronously in each replica.</param>
//        /// <param name="shouldReplicate">A function that determines whether another replica should be created.</param>
//        /// <param name="creationOptions">A TaskCreationOptions value that controls the behavior of the created Task.</param>
//        public SelfReplicatingTask(Action action, Func<Boolean> shouldReplicate, CancellationToken cancellationToken, TaskCreationOptions creationOptions) :
//            base(new SelfReplicatingTaskBody(action, shouldReplicate, creationOptions).ReplicateAndRun, cancellationToken, creationOptions)
//        {
//            // Validate parameters
//            if (action == null) throw new ArgumentNullException("action");
//            if (shouldReplicate == null) throw new ArgumentNullException("shouldReplicate");
//            if ((creationOptions & TaskCreationOptions.LongRunning) == TaskCreationOptions.LongRunning)
//            {
//                throw new ArgumentOutOfRangeException("creationOptions");
//            }
//        }

//        private class SelfReplicatingTaskBody
//        {
//            private Action _body;
//            private TaskCreationOptions _creationOptions;
//            private Func<Boolean> _shouldReplicate;
//            private CancellationTokenSource _cancellation;

//            internal SelfReplicatingTaskBody(Action body, Func<Boolean> shouldReplicate, TaskCreationOptions creationOptions)
//            {
//                // Store the replication data
//                _body = body;
//                _shouldReplicate = shouldReplicate;
//                _creationOptions = creationOptions;
//                _cancellation = new CancellationTokenSource();
//            }

//            // This method is the actual body of the Task
//            internal void ReplicateAndRun()
//            {
//                Task replica = null;
//                try
//                {
//                    // Check to see if we should replicate.
//                    if (_shouldReplicate())
//                    {
//                        // Create a copy of the replicable task with the same parameters and start it
//                        // However, we want to ensure that the replicas are attached, so remove
//                        // the detached option if present.
//                        replica = new SelfReplicatingTask(_body, _shouldReplicate, 
//                            _creationOptions & ~TaskCreationOptions.DetachedFromParent);
//                        replica.Start();
//                    }

//                    // Now that we've replicated, run the body
//                    _body();
//                }
//                finally
//                {
//                    // When the body completes, cancel the replica.  We'll implicitly wait for it.
//                    if (replica != null) replica.Cancel();
//                }
//            }
//        }
//    }
//}
