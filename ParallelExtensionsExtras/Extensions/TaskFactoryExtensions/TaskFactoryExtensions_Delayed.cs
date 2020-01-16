//--------------------------------------------------------------------------
// 
//  Copyright (c) Microsoft Corporation.  All rights reserved. 
// 
//  File: TaskFactoryExtensions_Delayed.cs
//
//--------------------------------------------------------------------------

namespace System.Threading.Tasks
{
    public static partial class TaskFactoryExtensions
    {
        #region TaskFactory with Action
        /// <summary>Creates and schedules a task for execution after the specified time delay.</summary>
        /// <param name="factory">The factory to use to create the task.</param>
        /// <param name="millisecondsDelay">The delay after which the task will be scheduled.</param>
        /// <param name="action">The delegate executed by the task.</param>
        /// <returns>The created Task.</returns>
        public static Task StartNewDelayed(
            this TaskFactory factory,
            int millisecondsDelay, Action action)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            return StartNewDelayed(factory, millisecondsDelay, action, factory.CancellationToken, factory.CreationOptions, factory.GetTargetScheduler());
        }

        /// <summary>Creates and schedules a task for execution after the specified time delay.</summary>
        /// <param name="factory">The factory to use to create the task.</param>
        /// <param name="millisecondsDelay">The delay after which the task will be scheduled.</param>
        /// <param name="action">The delegate executed by the task.</param>
        /// <param name="creationOptions">Options that control the task's behavior.</param>
        /// <returns>The created Task.</returns>
        public static Task StartNewDelayed(
            this TaskFactory factory,
            int millisecondsDelay, Action action,
            TaskCreationOptions creationOptions)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            return StartNewDelayed(factory, millisecondsDelay, action, factory.CancellationToken, creationOptions, factory.GetTargetScheduler());
        }

        /// <summary>Creates and schedules a task for execution after the specified time delay.</summary>
        /// <param name="factory">The factory to use to create the task.</param>
        /// <param name="millisecondsDelay">The delay after which the task will be scheduled.</param>
        /// <param name="action">The delegate executed by the task.</param>
        /// <param name="cancellationToken">The cancellation token to assign to the created Task.</param>
        /// <returns>The created Task.</returns>
        public static Task StartNewDelayed(
            this TaskFactory factory,
            int millisecondsDelay, Action action,
            CancellationToken cancellationToken)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            return StartNewDelayed(factory, millisecondsDelay, action, cancellationToken, factory.CreationOptions, factory.GetTargetScheduler());
        }

        /// <summary>Creates and schedules a task for execution after the specified time delay.</summary>
        /// <param name="factory">The factory to use to create the task.</param>
        /// <param name="millisecondsDelay">The delay after which the task will be scheduled.</param>
        /// <param name="action">The delegate executed by the task.</param>
        /// <param name="cancellationToken">The cancellation token to assign to the created Task.</param>
        /// <param name="creationOptions">Options that control the task's behavior.</param>
        /// <param name="scheduler">The scheduler to which the Task will be scheduled.</param>
        /// <returns>The created Task.</returns>
        public static Task StartNewDelayed(
            this TaskFactory factory,
            int millisecondsDelay, Action action, 
            CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            if (millisecondsDelay < 0) throw new ArgumentOutOfRangeException("millisecondsDelay");
            if (action == null) throw new ArgumentNullException("action");
            if (scheduler == null) throw new ArgumentNullException("scheduler");

            // Create a trigger used to start the task
            var tcs = new TaskCompletionSource<object>();

            // Start a timer that will trigger it
            var timer = new Timer(obj => ((TaskCompletionSource<object>)obj).SetResult(null), 
                tcs, millisecondsDelay, Timeout.Infinite);

            // Create and return a task that will be scheduled when the trigger fires.
            return tcs.Task.ContinueWith(_ =>
            {
                // Clean up, and run the action
                timer.Dispose();
                action();
            }, cancellationToken, ContinuationOptionsFromCreationOptions(creationOptions), scheduler);
        }

        /// <summary>Creates and schedules a task for execution after the specified time delay.</summary>
        /// <param name="factory">The factory to use to create the task.</param>
        /// <param name="millisecondsDelay">The delay after which the task will be scheduled.</param>
        /// <param name="action">The delegate executed by the task.</param>
        /// <param name="state">An object provided to the delegate.</param>
        /// <returns>The created Task.</returns>
        public static Task StartNewDelayed(
            this TaskFactory factory,
            int millisecondsDelay, Action<object> action, object state)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            return StartNewDelayed(factory, millisecondsDelay, action, state, factory.CancellationToken, factory.CreationOptions, factory.GetTargetScheduler());
        }

        /// <summary>Creates and schedules a task for execution after the specified time delay.</summary>
        /// <param name="factory">The factory to use to create the task.</param>
        /// <param name="millisecondsDelay">The delay after which the task will be scheduled.</param>
        /// <param name="action">The delegate executed by the task.</param>
        /// <param name="state">An object provided to the delegate.</param>
        /// <param name="creationOptions">Options that control the task's behavior.</param>
        /// <returns>The created Task.</returns>
        public static Task StartNewDelayed(
            this TaskFactory factory,
            int millisecondsDelay, Action<object> action, object state,
            TaskCreationOptions creationOptions)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            return StartNewDelayed(factory, millisecondsDelay, action, state, factory.CancellationToken, creationOptions, factory.GetTargetScheduler());
        }

        /// <summary>Creates and schedules a task for execution after the specified time delay.</summary>
        /// <param name="factory">The factory to use to create the task.</param>
        /// <param name="millisecondsDelay">The delay after which the task will be scheduled.</param>
        /// <param name="action">The delegate executed by the task.</param>
        /// <param name="state">An object provided to the delegate.</param>
        /// <param name="cancellationToken">The cancellation token to assign to the created Task.</param>
        /// <returns>The created Task.</returns>
        public static Task StartNewDelayed(
            this TaskFactory factory,
            int millisecondsDelay, Action<object> action, object state,
            CancellationToken cancellationToken)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            return StartNewDelayed(factory, millisecondsDelay, action, state, cancellationToken, factory.CreationOptions, factory.GetTargetScheduler());
        }

        /// <summary>Creates and schedules a task for execution after the specified time delay.</summary>
        /// <param name="factory">The factory to use to create the task.</param>
        /// <param name="millisecondsDelay">The delay after which the task will be scheduled.</param>
        /// <param name="action">The delegate executed by the task.</param>
        /// <param name="state">An object provided to the delegate.</param>
        /// <param name="cancellationToken">The cancellation token to assign to the created Task.</param>
        /// <param name="creationOptions">Options that control the task's behavior.</param>
        /// <param name="scheduler">The scheduler to which the Task will be scheduled.</param>
        /// <returns>The created Task.</returns>
        public static Task StartNewDelayed(
            this TaskFactory factory,
            int millisecondsDelay, Action<object> action, object state,
            CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            if (millisecondsDelay < 0) throw new ArgumentOutOfRangeException("millisecondsDelay");
            if (action == null) throw new ArgumentNullException("action");
            if (scheduler == null) throw new ArgumentNullException("scheduler");

            // Create the task that will be returned
            var result = new TaskCompletionSource<object>(state);
            Timer timer = null;

            // Create the task that will run the user's action
            var actionTask = new Task(action, state, creationOptions);

            // When the action task completes, transfer the results to the returned task
            actionTask.ContinueWith(t =>
            {
                result.SetFromTask(t);
                timer.Dispose(); // clean up
            }, cancellationToken, ContinuationOptionsFromCreationOptions(creationOptions) | 
                TaskContinuationOptions.ExecuteSynchronously, scheduler);

            // Start the timer for the trigger
            timer = new Timer(obj => ((Task)obj).Start(scheduler), 
                actionTask, millisecondsDelay, Timeout.Infinite);

            return result.Task;
        }
        #endregion

        #region TaskFactory<TResult> with Func
        /// <summary>Creates and schedules a task for execution after the specified time delay.</summary>
        /// <param name="factory">The factory to use to create the task.</param>
        /// <param name="millisecondsDelay">The delay after which the task will be scheduled.</param>
        /// <param name="function">The delegate executed by the task.</param>
        /// <returns>The created Task.</returns>
        public static Task<TResult> StartNewDelayed<TResult>(
            this TaskFactory<TResult> factory,
            int millisecondsDelay, Func<TResult> function)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            return StartNewDelayed(factory, millisecondsDelay, function, factory.CancellationToken, factory.CreationOptions, factory.GetTargetScheduler());
        }

        /// <summary>Creates and schedules a task for execution after the specified time delay.</summary>
        /// <param name="factory">The factory to use to create the task.</param>
        /// <param name="millisecondsDelay">The delay after which the task will be scheduled.</param>
        /// <param name="function">The delegate executed by the task.</param>
        /// <param name="creationOptions">Options that control the task's behavior.</param>
        /// <returns>The created Task.</returns>
        public static Task<TResult> StartNewDelayed<TResult>(
            this TaskFactory<TResult> factory,
            int millisecondsDelay, Func<TResult> function,
            TaskCreationOptions creationOptions)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            return StartNewDelayed(factory, millisecondsDelay, function, factory.CancellationToken, creationOptions, factory.GetTargetScheduler());
        }

        /// <summary>Creates and schedules a task for execution after the specified time delay.</summary>
        /// <param name="factory">The factory to use to create the task.</param>
        /// <param name="millisecondsDelay">The delay after which the task will be scheduled.</param>
        /// <param name="function">The delegate executed by the task.</param>
        /// <param name="cancellationToken">The CancellationToken to assign to the Task.</param>
        /// <returns>The created Task.</returns>
        public static Task<TResult> StartNewDelayed<TResult>(
            this TaskFactory<TResult> factory,
            int millisecondsDelay, Func<TResult> function,
            CancellationToken cancellationToken)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            return StartNewDelayed(factory, millisecondsDelay, function, cancellationToken, factory.CreationOptions, factory.GetTargetScheduler());
        }

        /// <summary>Creates and schedules a task for execution after the specified time delay.</summary>
        /// <param name="factory">The factory to use to create the task.</param>
        /// <param name="millisecondsDelay">The delay after which the task will be scheduled.</param>
        /// <param name="function">The delegate executed by the task.</param>
        /// <param name="cancellationToken">The CancellationToken to assign to the Task.</param>
        /// <param name="creationOptions">Options that control the task's behavior.</param>
        /// <param name="scheduler">The scheduler to which the Task will be scheduled.</param>
        /// <returns>The created Task.</returns>
        public static Task<TResult> StartNewDelayed<TResult>(
            this TaskFactory<TResult> factory,
            int millisecondsDelay, Func<TResult> function,
            CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            if (millisecondsDelay < 0) throw new ArgumentOutOfRangeException("millisecondsDelay");
            if (function == null) throw new ArgumentNullException("function");
            if (scheduler == null) throw new ArgumentNullException("scheduler");

            // Create the trigger and the timer to start it
            var tcs = new TaskCompletionSource<object>();
            var timer = new Timer(obj => ((TaskCompletionSource<object>)obj).SetResult(null),
                tcs, millisecondsDelay, Timeout.Infinite);

            // Return a task that executes the function when the trigger fires
            return tcs.Task.ContinueWith(_ =>
            {
                timer.Dispose();
                return function();
            }, cancellationToken, ContinuationOptionsFromCreationOptions(creationOptions), scheduler);
        }

        /// <summary>Creates and schedules a task for execution after the specified time delay.</summary>
        /// <param name="factory">The factory to use to create the task.</param>
        /// <param name="millisecondsDelay">The delay after which the task will be scheduled.</param>
        /// <param name="function">The delegate executed by the task.</param>
        /// <param name="state">An object provided to the delegate.</param>
        /// <returns>The created Task.</returns>
        public static Task<TResult> StartNewDelayed<TResult>(
            this TaskFactory<TResult> factory,
            int millisecondsDelay, Func<object, TResult> function, object state)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            return StartNewDelayed(factory, millisecondsDelay, function, state, factory.CancellationToken, factory.CreationOptions, factory.GetTargetScheduler());
        }

        /// <summary>Creates and schedules a task for execution after the specified time delay.</summary>
        /// <param name="factory">The factory to use to create the task.</param>
        /// <param name="millisecondsDelay">The delay after which the task will be scheduled.</param>
        /// <param name="function">The delegate executed by the task.</param>
        /// <param name="state">An object provided to the delegate.</param>
        /// <param name="cancellationToken">The CancellationToken to assign to the Task.</param>
        /// <returns>The created Task.</returns>
        public static Task<TResult> StartNewDelayed<TResult>(
            this TaskFactory<TResult> factory,
            int millisecondsDelay, Func<object, TResult> function, object state,
            CancellationToken cancellationToken)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            return StartNewDelayed(factory, millisecondsDelay, function, state, cancellationToken, factory.CreationOptions, factory.GetTargetScheduler());
        }

        /// <summary>Creates and schedules a task for execution after the specified time delay.</summary>
        /// <param name="factory">The factory to use to create the task.</param>
        /// <param name="millisecondsDelay">The delay after which the task will be scheduled.</param>
        /// <param name="function">The delegate executed by the task.</param>
        /// <param name="state">An object provided to the delegate.</param>
        /// <param name="creationOptions">Options that control the task's behavior.</param>
        /// <returns>The created Task.</returns>
        public static Task<TResult> StartNewDelayed<TResult>(
            this TaskFactory<TResult> factory,
            int millisecondsDelay, Func<object, TResult> function, object state,
            TaskCreationOptions creationOptions)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            return StartNewDelayed(factory, millisecondsDelay, function, state, factory.CancellationToken, creationOptions, factory.GetTargetScheduler());
        }

        /// <summary>Creates and schedules a task for execution after the specified time delay.</summary>
        /// <param name="factory">The factory to use to create the task.</param>
        /// <param name="millisecondsDelay">The delay after which the task will be scheduled.</param>
        /// <param name="function">The delegate executed by the task.</param>
        /// <param name="state">An object provided to the delegate.</param>
        /// <param name="cancellationToken">The CancellationToken to assign to the Task.</param>
        /// <param name="creationOptions">Options that control the task's behavior.</param>
        /// <param name="scheduler">The scheduler to which the Task will be scheduled.</param>
        /// <returns>The created Task.</returns>
        public static Task<TResult> StartNewDelayed<TResult>(
            this TaskFactory<TResult> factory,
            int millisecondsDelay, Func<object, TResult> function, object state,
            CancellationToken cancellationToken, TaskCreationOptions creationOptions, TaskScheduler scheduler)
        {
            if (factory == null) throw new ArgumentNullException("factory");
            if (millisecondsDelay < 0) throw new ArgumentOutOfRangeException("millisecondsDelay");
            if (function == null) throw new ArgumentNullException("action");
            if (scheduler == null) throw new ArgumentNullException("scheduler");

            // Create the task that will be returned
            var result = new TaskCompletionSource<TResult>(state);
            Timer timer = null;

            // Create the task that will run the user's function
            var functionTask = new Task<TResult>(function, state, creationOptions);

            // When the function task completes, transfer the results to the returned task
            functionTask.ContinueWith(t =>
            {
                result.SetFromTask(t);
                timer.Dispose();
            }, cancellationToken, ContinuationOptionsFromCreationOptions(creationOptions) | TaskContinuationOptions.ExecuteSynchronously, scheduler);

            // Start the timer for the trigger
            timer = new Timer(obj => ((Task)obj).Start(scheduler),
                functionTask, millisecondsDelay, Timeout.Infinite);

            return result.Task;
        }
        #endregion
    }
}