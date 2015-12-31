using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[TestFixture]
public class UnitTest1
{
    [Test]
    [Category("Sequential")]
    public void AddTest()
    {
        //MathsHelper helper = new MathsHelper();
        int result = 20 + 10;
        System.Console.WriteLine("done1");
        Assert.AreEqual(30, result);
    }

    [Test]
    public void AddTestnew()
    {
        //MathsHelper helper = new MathsHelper();
        int result = 20 + 10;
        System.Console.WriteLine("done1");
        Assert.AreEqual(30, result);
    }


    [Test]
    public void SubtractTest()
    {
        //MathsHelper helper = new MathsHelper();
        int result = 20 - 10;
        System.Console.WriteLine("done2");
        Assert.AreEqual(10, result);
    }

    [Test]
    public void SubtractTestnew()
    {
        //MathsHelper helper = new MathsHelper();
        int result = 20 - 10;
        System.Console.WriteLine("done2");
        Assert.AreEqual(10, result);
    }


}