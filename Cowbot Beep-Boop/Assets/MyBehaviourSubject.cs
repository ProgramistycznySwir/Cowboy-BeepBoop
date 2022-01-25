using System;
using System.Linq;

namespace Cowbot_Beep_Boop.Data
{
    // This class does not implement IObservable interface cause T wanted to make it more flexible letting users to
    //   subscribe with lambdas and not having to create whole classes for observing only one variable.
    //   The most prominent example is in display classes which potentially can display data from many observables,
    //   but IObservable lets creation of only one OnNext() method. Hence it would have to be tuple and update whole
    //   tuple, or there would have to be many intermediate classes, one for each variable we would like to observe.
    // This class implements methods of ISubject, IObservable and IObserver, but in it's unique way.
    public class MyBehaviourSubject<T>
    {
        private Action<T> actions_Next = (param) => {};
        private Action<Exception> actions_Error = (param) => {};
        private Action actions_Completed = () => {};
        private T _value;
        public T Value {
            get => _value;
            set => OnNext(_value = value);
        }
        
        public MyBehaviourSubject()
            => this._value = default!;
        public MyBehaviourSubject(T initialValue)
            => this._value = initialValue;
        
        /// <summary>
        /// Makes sure that actions are subscribed only once to this observable.
        /// </summary>
        /// <param name="next">Invoked on data change.</param>
        /// <param name="error">Invoked when there is error with data.</param>
        /// <param name="complete">Invoked on end of changes stream.</param>
        /// <returns>Object used to unsubscribe from this particular observable.</returns>
        public IDisposable SubscribeOnce(Action<T> next = null!, Action<Exception> error = null!, Action complete = null!)
        {
            if(next is not null && (actions_Next is null || actions_Next.GetInvocationList().Contains(next) is false))
                actions_Next += next;
            if(error is not null && (actions_Error is null || actions_Error.GetInvocationList().Contains(error) is false))
                actions_Error += error;
            if(complete is not null && (actions_Completed is null || actions_Completed.GetInvocationList().Contains(complete) is false))
                actions_Completed += complete;
            return new Unsubscriber(this, next!, error!, complete!);
        }
        public void UnSubscribeAll(Action<T> next, Action<Exception> error, Action complete)
        {
            actions_Next = (Action<T>)Action<T>.RemoveAll(actions_Next, next)!;
            actions_Error = (Action<Exception>)Action<Exception>.RemoveAll(actions_Error, error)!;
            actions_Completed = (Action)Action.RemoveAll(actions_Completed, complete)!;
        }

        /// <summary>
        /// Only emits value without changing MyBehaviourSubject.Value.
        ///  Use with care!
        /// </summary>
        public void OnNext(T value_)
            => actions_Next(value_);
        public void OnNext()
            => actions_Next(_value);

        public void OnError(Exception exception)
            => actions_Error(exception);

        public void OnCompleted()
        {
            actions_Completed();
            actions_Next = null!;
            actions_Next = (param) => {};
            actions_Error = null!;
            actions_Error = (param) => {};
            actions_Completed = null!;
            actions_Completed = () => {};
        }

        private class Unsubscriber : IDisposable
        {
            private MyBehaviourSubject<T> _observed;
            private Action<T> _next;
            private Action<Exception> _error;
            private Action _complete;

            public Unsubscriber(MyBehaviourSubject<T> observed, Action<T> next, Action<Exception> error, Action complete)
                => (_observed, _next, _error, _complete) = (observed, next, error, complete);

            public void Dispose()
                => _observed.UnSubscribeAll(_next, _error, _complete);
        }
    }
}