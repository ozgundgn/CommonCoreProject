using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    public class Modelnterception:ModelIntercepitonBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation) { }
        protected virtual void OnAfter(IInvocation invocation) { }
        protected virtual void OnException(IInvocation invocation) { }
        protected virtual void OnSuccess(IInvocation invocation) { }

        public override void Intercept(IInvocation invocation) //invocation indicate each methods 
        {
            bool isSuccess = true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception e)
            {
                isSuccess = false;
                OnException(invocation);
                throw;
            }
            finally
            {
                if(isSuccess)
                    OnSuccess(invocation);
            }
            OnAfter(invocation);
        }
    }
}
