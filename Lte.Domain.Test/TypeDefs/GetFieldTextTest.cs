using System;
using Lte.Domain.Regular;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lte.Domain.TypeDefs;
using System.Reflection;
using System.Collections.Generic;
using System.Globalization;

namespace Lte.Domain.Test.TypeDefs
{
    [TestClass]
    public class GetFieldTextTest
    {
        private const string line = "Sequence=0,Time=2014-02-08 13:15:12:900,MsgType=S1口,MsgName=UE CONTEXT RELEASE REQUEST,Direction=eNodeB发出,eNodeBId=501841,Cell=0,GID=16506,FN=0,SFN=0,Version=130606,MsgId=57554,DataId=0,MsgCode=0012401900000300000005c007989f8d0008000340407a000240020280,IMSI=--,";

        [TestMethod]
        public void TestGetFieldText_BasicOperations()
        {
            char interSplitter 
                = (Attribute.GetCustomAttribute((typeof(ZteSignal)), typeof(RowAttribute)) 
                as RowAttribute).InterColumnSplitter;
            char intraSplitter 
                = (Attribute.GetCustomAttribute((typeof(ZteSignal)), typeof(RowAttribute)) 
                as RowAttribute).IntraColumnSplitter;
            Assert.AreEqual(interSplitter, ',');
            Assert.AreEqual(intraSplitter, '=');
        }

        [TestMethod]
        public void Test_GetFieldTextList()
        {
            Dictionary<string, string> result = line.GetFieldTextList<ZteSignal>();
            Assert.IsNotNull(result);
            Assert.AreEqual(result["Time"], "2014-02-08 13:15:12:900");
            Assert.AreEqual(result["MsgCode"], "0012401900000300000005c007989f8d0008000340407a000240020280");
        }

        [TestMethod]
        public void Test_SetValueByText_Sequence()
        {
            PropertyInfo property = (typeof(ZteSignal)).GetProperty("Sequence");
            ZteSignal signal = new ZteSignal { Sequence = 1 };
            signal.SetValueByText(property, "2");
            Assert.AreEqual(signal.Sequence, 2);
        }

        [TestMethod]
        public void Test_DateTimeParse()
        {
            string timeText = "2014-02-08 13:15:12:900";
            DateTime time = DateTime.ParseExact(timeText, "yyyy-MM-dd HH:mm:ss:fff",
                CultureInfo.CurrentCulture, DateTimeStyles.None);
            Assert.AreEqual(time.Hour, 13);
        }

        [TestMethod]
        public void Test_SetValueByText_Time()
        {
            PropertyInfo property = (typeof(ZteSignal)).GetProperty("Time");
            ZteSignal signal = new ZteSignal { Time = DateTime.Now };
            signal.SetValueByText(property, "2014-02-08 13:15:12:900");
            Assert.AreEqual(signal.Time.Hour, 13);
        }

        [TestMethod]
        public void Test_GenerateOneRowFromText()
        {
            ZteSignal signal = line.GenerateOneRowFromText<ZteSignal>();
            Assert.IsNotNull(signal);
            Assert.AreEqual(signal.MsgCode, "0012401900000300000005c007989f8d0008000340407a000240020280");
            Assert.AreEqual(signal.Gid, 16506);
        }

        [TestMethod]
        public void Test_StringFormat()
        {
            Assert.AreEqual(string.Format("{0:00.00}", 27.333), "27.33");
        }
    }
}
