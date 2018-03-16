using System;
using System.Threading;
using System.Threading.Tasks;

namespace core
{
    public static class TaskUtils
    {
        public static T ExecuteSync<T>(this Task<T> task)
        {
            task.Wait();
            return task.Result;
        }

        public static T ExecuteSync<T>(this Task<T> task, CancellationToken cancellationToken)
        {
            task.Wait(cancellationToken);
            return task.Result;
        }

        public static T ExecuteSync<T>(this Task<T> task, TimeSpan timeout)
        {
            task.Wait(timeout);
            return task.Result;
        }

        public static T ExecuteSync<T>(this Task<T> task, int millisecondsTimeout)
        {
            task.Wait(millisecondsTimeout);
            return task.Result;
        }

        public static T ExecuteSync<T>(this Task<T> task, int millisecondsTimeout, CancellationToken cancellationToken)
        {
            task.Wait(millisecondsTimeout, cancellationToken);
            return task.Result;
        }

        public static Task ContinueWith(this Task task, Action<Task> continuationAction, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
            => task.ContinueWith(continuationAction, CancellationToken.None, continuationOptions, scheduler);

        public static Task<TResult> ContinueWith<TResult>(this Task<TResult> task, Func<Task, TResult> continuationFunction, TaskContinuationOptions continuationOptions, TaskScheduler scheduler)
            => task.ContinueWith(continuationFunction, CancellationToken.None, continuationOptions, scheduler);
    }
}
