using System;
using System.Linq;

namespace Cowbot_Beep_Boop.Data
{
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

        public void OnNext(T value_)
            => actions_Next(value_);

        public void OnError(Exception exception)
            => actions_Error(exception);

        public void OnCompleted()
        {
            actions_Completed();
            actions_Next = null!;
            actions_Error = null!;
            actions_Completed = null!;
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