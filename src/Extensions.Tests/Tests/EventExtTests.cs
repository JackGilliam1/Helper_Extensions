using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    //[TestClass]
    //public class EventExtTests
    //{
    //    private static bool waiting;

    //    [TestMethod]
    //    public void Events_Are_Triggered()
    //    {
    //        //Arrange
    //        var noParamsObj = new TestObject<object>();
    //        var noParamsEventObj = new EventObject<object>(ref noParamsObj);
    //        string noParamsRefName = "Object1";
    //        EventHandler noParamsHandler = OnFinish;

    //        //Act
    //        Extensions.Events.EventExts.Listen(noParamsEventObj, noParamsRefName, "Add", delegate(object source, EventArgs e)
    //        {
    //            noParamsEventObj.CallAdd(source, e);
    //            noParamsHandler.Invoke(noParamsObj, new EventArgs());
    //        });
    //        waiting = Extensions.Events.EventExts.Dispatch("Add", null);

    //        ////Assert
    //        while (waiting)
    //        { }
    //    }

    //    /// <summary>
    //    /// Called when events finish
    //    /// </summary>
    //    /// <param name="source">The caller</param>
    //    /// <param name="e">Event information</param>
    //    public void OnFinish(object source, EventArgs e)
    //    {
    //        EventExtTests.waiting = false;
    //    }
    //}
}