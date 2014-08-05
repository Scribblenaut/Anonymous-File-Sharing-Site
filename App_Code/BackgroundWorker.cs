using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Remoting.Messaging;
using System.Web.UI.WebControls;
using System.Web.UI;


/// <summary>
/// Summary description for AsynchronousProcessingManager
/// </summary>
///
public class AsyncResultObject
{
    object result;
    Exception ex;
    public AsyncResultObject()
    {
        this.ex = null;
    }
    public object Result
    {
        get
        { return this.result; }
        set
        { this.result = value; }
    }
    public Exception Ex
    {
        get
        { return this.ex; }
        set
        { this.ex = value; }
    }
}

public class AsynchronousProcessingManager
{
    //fields
    AsynchronousTaskDelegate methodDelegate;
    public event AsyncProcessingEndedEventHandler OnAsyncProcessingEnded;
    //Constructor
    public AsynchronousProcessingManager(AsynchronousTaskDelegate atd)
    {
        this.methodDelegate = atd;
    }

    //IAsyncResult:represent the status of asynchronous operation
    //AsyncCallback:Reference a method to be called when a corresponding asynchronous operation compeletes
    public IAsyncResult BeginAsyncTask(object sender, EventArgs e, AsyncCallback ac, object state)
    {
        //The CLR provides each delegate type with BeginInvoke and EndInvoke methods, to enable asynchronous invocation of the delegate.
        IAsyncResult result = this.methodDelegate.BeginInvoke(ac, state);
        return result;
    }
    public void EndAsyncTask(IAsyncResult result)
    {
        //AsyncResult: Encapsulates the results of an asynchronous operation on a delegate
        //AsyncDelegate :get the delegate object on which the asynchronous call was invoked
        AsynchronousTaskDelegate cd = (AsynchronousTaskDelegate)((AsyncResult)result).AsyncDelegate;
        AsyncResultObject aro = cd.EndInvoke(result);
        if (OnAsyncProcessingEnded != null)
        {
            AsyncEndedEventArgs e = new AsyncEndedEventArgs(aro);
            OnAsyncProcessingEnded(this, e);
        }
        this.methodDelegate = null;
        cd = null;
    }
    public void AsyncCallBack(IAsyncResult result)
    {
        AsynchronousProcessingManager apm = (AsynchronousProcessingManager)result.AsyncState;
    }
    public static void ProcessAsyncTask(AsynchronousTaskDelegate asyncMethodDelegate, AsyncProcessingEndedEventHandler asyncEndedMethod, EndEventHandler timeOutMethod, Page currentPage)
    {
        AsynchronousProcessingManager apm = new AsynchronousProcessingManager(asyncMethodDelegate);
        apm.OnAsyncProcessingEnded += asyncEndedMethod;

        //PageAsyncTask: contains information about an asynchronous task registered to a page
        //BedinEventHandler: The handler to call when beginning an asynchronous task
        //EndEventHandler: The handler to call when the task is completed successfully within the time-out period
        //EndEventHandler: The handler to call when the task is not completed successfully within the time-out period
        PageAsyncTask task = new PageAsyncTask(new BeginEventHandler(apm.BeginAsyncTask), new EndEventHandler(apm.EndAsyncTask), new EndEventHandler(timeOutMethod), null);
        currentPage.RegisterAsyncTask(task);
        //currentPage.Unload+=new EventHandler(CurrentAsyncPage_Unload);
    }
    private void CurrentAsyncPage_Unload(object sender, EventArgs e)
    {
    }
}
public delegate AsyncResultObject AsynchronousTaskDelegate();
public delegate void AsyncProcessingEndedEventHandler(object sender, AsyncEndedEventArgs e);
public class AsyncEndedEventArgs
{
    AsyncResultObject resultObject;
    public AsyncEndedEventArgs(AsyncResultObject rObject)
    {
        this.resultObject = rObject;
    }
    public AsyncResultObject ResultObject
    {
        get { return this.resultObject; }
        set { this.resultObject = value; }
    }
}