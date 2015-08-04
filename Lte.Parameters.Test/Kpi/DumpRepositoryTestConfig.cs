﻿using Lte.Parameters.Abstract;
using Lte.Parameters.Region.Abstract;
using Moq;

namespace Lte.Parameters.Test.Dump
{
    public class DumpRepositoryTestConfig
    {
        protected string mmlFileContents = @"
ADD BSCBTSINF: BTSTP=IBSC, BTSID=1440, BTSNM=""张槎邮政宿舍"", SPUFN=6, SN=4, SSN=0, ABISCAPOPTMSW=OFF, ABISOPTMCRCSW=OFF, ABISREVFRAMEPERIOD=3, HIGHPW=NOHIGHPW, ABISREDUNDANCESW=OFF, INTRAFADAPFILTER=OFF, ABISBETRFBWTHR=0, PNSHARENUM1X=0, PNSHARENUMDO=0, ABISSATTRANSSW=OFF, DATATRFCRCSW=OFF, TODSW=OFF, VIP1XOCCUPYRES=OFF, BTSGRADE=GRADEC, BTSLOCATIONTYPE=LOCATION0, BTSPHYTYPE=MACRO, BTSSUPPORTFESW=ON;
ADD BSCBTSINF: BTSTP=IBSC, BTSID=2393, BTSNM=""信息大厦BBU1"", SPUFN=5, SN=10, SSN=1, ABISCAPOPTMSW=OFF, ABISOPTMCRCSW=OFF, ABISREVFRAMEPERIOD=3, HIGHPW=NOHIGHPW, ABISREDUNDANCESW=OFF, INTRAFADAPFILTER=OFF, ABISBETRFBWTHR=0, PNSHARENUM1X=0, PNSHARENUMDO=0, ABISSATTRANSSW=OFF, DATATRFCRCSW=OFF, TODSW=OFF, VIP1XOCCUPYRES=OFF, BTSGRADE=GRADEC, BTSLOCATIONTYPE=LOCATION0, BTSPHYTYPE=MACRO, BTSSUPPORTFESW=ON;
ADD BSCBTSINF: BTSTP=IBSC, BTSID=2393, BTSNM=""信息大厦BBU1"", SPUFN=6, SN=4, SSN=0, ABISCAPOPTMSW=OFF, ABISOPTMCRCSW=OFF, ABISREVFRAMEPERIOD=3, HIGHPW=NOHIGHPW, ABISREDUNDANCESW=OFF, INTRAFADAPFILTER=OFF, ABISBETRFBWTHR=0, PNSHARENUM1X=0, PNSHARENUMDO=0, ABISSATTRANSSW=OFF, DATATRFCRCSW=OFF, TODSW=OFF, VIP1XOCCUPYRES=OFF, BTSGRADE=GRADEC, BTSLOCATIONTYPE=LOCATION0, BTSPHYTYPE=MACRO, BTSSUPPORTFESW=ON;
ADD BSCBTSINF: BTSTP=IBSC, BTSID=98, BTSNM=""东方广场翡翠城"", SPUFN=4, SN=8, SSN=2, ABISCAPOPTMSW=OFF, ABISOPTMCRCSW=OFF, ABISREVFRAMEPERIOD=3, HIGHPW=NOHIGHPW, ABISREDUNDANCESW=OFF, INTRAFADAPFILTER=OFF, ABISBETRFBWTHR=0, PNSHARENUM1X=0, PNSHARENUMDO=0, ABISSATTRANSSW=OFF, DATATRFCRCSW=OFF, TODSW=OFF, VIP1XOCCUPYRES=OFF, BTSGRADE=GRADEC, BTSLOCATIONTYPE=LOCATION0, BTSPHYTYPE=MACRO, BTSSUPPORTFESW=ON;
ADD BSCBTSINF: BTSTP=IBSC, BTSID=98, BTSNM=""东方广场翡翠城"", SPUFN=6, SN=10, SSN=1, ABISCAPOPTMSW=OFF, ABISOPTMCRCSW=OFF, ABISREVFRAMEPERIOD=3, HIGHPW=NOHIGHPW, ABISREDUNDANCESW=OFF, INTRAFADAPFILTER=OFF, ABISBETRFBWTHR=0, PNSHARENUM1X=0, PNSHARENUMDO=0, ABISSATTRANSSW=OFF, DATATRFCRCSW=OFF, TODSW=OFF, VIP1XOCCUPYRES=OFF, BTSGRADE=GRADEC, BTSLOCATIONTYPE=LOCATION0, BTSPHYTYPE=MACRO, BTSSUPPORTFESW=ON;
ADD BSCBTSINF: BTSTP=IBSC, BTSID=1666, BTSNM=""石头红旗村"", SPUFN=4, SN=10, SSN=6, ABISCAPOPTMSW=OFF, ABISOPTMCRCSW=OFF, ABISREVFRAMEPERIOD=3, HIGHPW=NOHIGHPW, ABISREDUNDANCESW=OFF, INTRAFADAPFILTER=OFF, ABISBETRFBWTHR=0, PNSHARENUM1X=0, PNSHARENUMDO=0, ABISSATTRANSSW=OFF, DATATRFCRCSW=OFF, TODSW=OFF, VIP1XOCCUPYRES=OFF, BTSGRADE=GRADEC, BTSLOCATIONTYPE=LOCATION0, BTSPHYTYPE=MACRO, BTSSUPPORTFESW=ON;
ADD BSCBTSINF: BTSTP=IBSC, BTSID=1666, BTSNM=""石头红旗村"", SPUFN=6, SN=10, SSN=3, ABISCAPOPTMSW=OFF, ABISOPTMCRCSW=OFF, ABISREVFRAMEPERIOD=3, HIGHPW=NOHIGHPW, ABISREDUNDANCESW=OFF, INTRAFADAPFILTER=OFF, ABISBETRFBWTHR=0, PNSHARENUM1X=0, PNSHARENUMDO=0, ABISSATTRANSSW=OFF, DATATRFCRCSW=OFF, TODSW=OFF, VIP1XOCCUPYRES=OFF, BTSGRADE=GRADEC, BTSLOCATIONTYPE=LOCATION0, BTSPHYTYPE=MACRO, BTSSUPPORTFESW=ON;
ADD BSCBTSINF: BTSTP=IBSC, BTSID=2245, BTSNM=""城西电信BBU3"", SPUFN=2, SN=10, SSN=2, ABISCAPOPTMSW=OFF, ABISOPTMCRCSW=OFF, ABISREVFRAMEPERIOD=3, HIGHPW=NOHIGHPW, ABISREDUNDANCESW=OFF, INTRAFADAPFILTER=OFF, ABISBETRFBWTHR=0, PNSHARENUM1X=0, PNSHARENUMDO=0, ABISSATTRANSSW=OFF, DATATRFCRCSW=OFF, TODSW=OFF, VIP1XOCCUPYRES=OFF, BTSGRADE=GRADEC, BTSLOCATIONTYPE=LOCATION0, BTSPHYTYPE=MACRO, BTSSUPPORTFESW=ON;
ADD BSCBTSINF: BTSTP=IBSC, BTSID=2245, BTSNM=""城西电信BBU3"", SPUFN=6, SN=10, SSN=3, ABISCAPOPTMSW=OFF, ABISOPTMCRCSW=OFF, ABISREVFRAMEPERIOD=3, HIGHPW=NOHIGHPW, ABISREDUNDANCESW=OFF, INTRAFADAPFILTER=OFF, ABISBETRFBWTHR=0, PNSHARENUM1X=0, PNSHARENUMDO=0, ABISSATTRANSSW=OFF, DATATRFCRCSW=OFF, TODSW=OFF, VIP1XOCCUPYRES=OFF, BTSGRADE=GRADEC, BTSLOCATIONTYPE=LOCATION0, BTSPHYTYPE=MACRO, BTSSUPPORTFESW=ON;
ADD BSCBTSINF: BTSTP=IBSC, BTSID=2381, BTSNM=""张槎宏利楼"", SPUFN=6, SN=10, SSN=0, ABISCAPOPTMSW=OFF, ABISOPTMCRCSW=OFF, ABISREVFRAMEPERIOD=3, HIGHPW=NOHIGHPW, ABISREDUNDANCESW=OFF, INTRAFADAPFILTER=OFF, ABISBETRFBWTHR=0, PNSHARENUM1X=0, PNSHARENUMDO=0, ABISSATTRANSSW=OFF, DATATRFCRCSW=OFF, TODSW=OFF, VIP1XOCCUPYRES=OFF, BTSGRADE=GRADEC, BTSLOCATIONTYPE=LOCATION0, BTSPHYTYPE=MACRO, BTSSUPPORTFESW=ON;
ADD BSCBTSINF: BTSTP=IBSC, BTSID=140, BTSNM=""石湾宾馆-G"", SPUFN=4, SN=10, SSN=4, ABISCAPOPTMSW=OFF, ABISOPTMCRCSW=OFF, ABISREVFRAMEPERIOD=3, HIGHPW=NOHIGHPW, ABISREDUNDANCESW=OFF, INTRAFADAPFILTER=OFF, ABISBETRFBWTHR=0, PNSHARENUM1X=0, PNSHARENUMDO=0, ABISSATTRANSSW=OFF, DATATRFCRCSW=OFF, TODSW=OFF, VIP1XOCCUPYRES=OFF, BTSGRADE=GRADEC, BTSLOCATIONTYPE=LOCATION0, BTSPHYTYPE=MACRO, BTSSUPPORTFESW=ON;
ADD BSCBTSINF: BTSTP=IBSC, BTSID=140, BTSNM=""石湾宾馆-G"", SPUFN=5, SN=10, SSN=4, ABISCAPOPTMSW=OFF, ABISOPTMCRCSW=OFF, ABISREVFRAMEPERIOD=3, HIGHPW=NOHIGHPW, ABISREDUNDANCESW=OFF, INTRAFADAPFILTER=OFF, ABISBETRFBWTHR=0, PNSHARENUM1X=0, PNSHARENUMDO=0, ABISSATTRANSSW=OFF, DATATRFCRCSW=OFF, TODSW=OFF, VIP1XOCCUPYRES=OFF, BTSGRADE=GRADEC, BTSLOCATIONTYPE=LOCATION0, BTSPHYTYPE=MACRO, BTSSUPPORTFESW=ON;
ADD BSCBTSINF: BTSTP=IBSC, BTSID=1442, BTSNM=""雅居乐花园C区-G"", SPUFN=6, SN=4, SSN=2, ABISCAPOPTMSW=OFF, ABISOPTMCRCSW=OFF, ABISREVFRAMEPERIOD=3, HIGHPW=NOHIGHPW, ABISREDUNDANCESW=OFF, INTRAFADAPFILTER=OFF, ABISBETRFBWTHR=0, PNSHARENUM1X=0, PNSHARENUMDO=0, ABISSATTRANSSW=OFF, DATATRFCRCSW=OFF, TODSW=OFF, VIP1XOCCUPYRES=OFF, BTSGRADE=GRADEC, BTSLOCATIONTYPE=LOCATION0, BTSPHYTYPE=MACRO, BTSSUPPORTFESW=ON;
ADD BSCBTSINF: BTSTP=IBSC, BTSID=122, BTSNM=""文华工业区-G"", SPUFN=5, SN=10, SSN=4, ABISCAPOPTMSW=OFF, ABISOPTMCRCSW=OFF, ABISREVFRAMEPERIOD=3, HIGHPW=NOHIGHPW, ABISREDUNDANCESW=OFF, INTRAFADAPFILTER=OFF, ABISBETRFBWTHR=0, PNSHARENUM1X=0, PNSHARENUMDO=0, ABISSATTRANSSW=OFF, DATATRFCRCSW=OFF, TODSW=OFF, VIP1XOCCUPYRES=OFF, BTSGRADE=GRADEC, BTSLOCATIONTYPE=LOCATION0, BTSPHYTYPE=MACRO, BTSSUPPORTFESW=ON;

/*------------增加基站操作维护链路------------*/
ADD BTSOMLNK: BTSID=2, IFBFN=3, IFBSN=22, LM=MLPPP, BCIMIP=""192.82.133.1"", MLPPPGRP=5;
ADD BTSOMLNK: BTSID=3, IFBFN=3, IFBSN=23, LM=MLPPP, BCIMIP=""192.83.133.1"", MLPPPGRP=5;

/*------------增加小区------------*/
ADD CELL: BTSID=2393, SPUFN=3, SPUSN=4, SPUSSN=3, CN=2, SCTIDLST=""0"", PNLST=""128"", SID=13832, NID=65535, PZID=1, TYP=CDMA1X, LAC=""0x2141"", LCN=2, LSCTID=""0"", ASSALW1X=YES, IFBORDCELL=NO, REVRSSICARRASSNSW=OFF, AUTODWNFWDEQLCHANTHD=20, AUTODWNCOUNTTHD=600, UNBLKFWDEQLCHANTHD=40, LOCATE=URBAN, MICROCELL=NO, HARDASSIGNTYPE=BOTH_VOICE_DATA, ANASSIST1XDOSW=OFF;
ADD CELL: BTSID=2393, SPUFN=3, SPUSN=4, SPUSSN=3, CN=2, SCTIDLST=""1"", PNLST=""296"", SID=13832, NID=65535, PZID=1, TYP=CDMA1X, LAC=""0x2141"", LCN=2, LSCTID=""1"", ASSALW1X=YES, IFBORDCELL=NO, REVRSSICARRASSNSW=OFF, AUTODWNFWDEQLCHANTHD=20, AUTODWNCOUNTTHD=600, UNBLKFWDEQLCHANTHD=40, LOCATE=URBAN, MICROCELL=NO, HARDASSIGNTYPE=BOTH_VOICE_DATA, ANASSIST1XDOSW=OFF;
ADD CELL: BTSID=2393, SPUFN=3, SPUSN=4, SPUSSN=3, CN=2, SCTIDLST=""2"", PNLST=""464"", SID=13832, NID=65535, PZID=1, TYP=CDMA1X, LAC=""0x2141"", LCN=2, LSCTID=""2"", ASSALW1X=YES, IFBORDCELL=NO, REVRSSICARRASSNSW=OFF, AUTODWNFWDEQLCHANTHD=20, AUTODWNCOUNTTHD=600, UNBLKFWDEQLCHANTHD=40, LOCATE=URBAN, MICROCELL=NO, HARDASSIGNTYPE=BOTH_VOICE_DATA, ANASSIST1XDOSW=OFF;
ADD CELL: BTSID=2393, SPUFN=5, SPUSN=10, SPUSSN=0, CN=2002, SCTIDLST=""0"", PNLST=""128"", SID=13832, NID=65535, PZID=1, TYP=EVDO, LCN=2002, LSCTID=""0"", ASSALWDO=NO, DOAREVRSSICARRASSNSW=OFF, DOAPRVPRIASSSW=OFF, DOMULTIBANDASSIGNSW=OFF, DOUSERCOUNTTHD=20, DOAUTODWNCOUNTTHD=600, DOUNBLKUSERCOUNTTHD=40, LOCATE=URBAN, MICROCELL=NO, STAYMODE=MODE0, BANDCLASSASSIGNSW=OFF, DOBLOADEQUIARISW=OFF;
ADD CELL: BTSID=2393, SPUFN=5, SPUSN=10, SPUSSN=0, CN=2002, SCTIDLST=""1"", PNLST=""296"", SID=13832, NID=65535, PZID=1, TYP=EVDO, LCN=2002, LSCTID=""1"", ASSALWDO=YES, DOAREVRSSICARRASSNSW=OFF, DOAPRVPRIASSSW=OFF, DOMULTIBANDASSIGNSW=OFF, DOUSERCOUNTTHD=20, DOAUTODWNCOUNTTHD=600, DOUNBLKUSERCOUNTTHD=40, LOCATE=URBAN, MICROCELL=NO, STAYMODE=MODE0, BANDCLASSASSIGNSW=OFF, DOBLOADEQUIARISW=OFF;
ADD CELL: BTSID=2393, SPUFN=5, SPUSN=10, SPUSSN=0, CN=2002, SCTIDLST=""2"", PNLST=""464"", SID=13832, NID=65535, PZID=1, TYP=EVDO, LCN=2002, LSCTID=""2"", ASSALWDO=NO, DOAREVRSSICARRASSNSW=OFF, DOAPRVPRIASSSW=OFF, DOMULTIBANDASSIGNSW=OFF, DOUSERCOUNTTHD=20, DOAUTODWNCOUNTTHD=600, DOUNBLKUSERCOUNTTHD=40, LOCATE=URBAN, MICROCELL=NO, STAYMODE=MODE0, BANDCLASSASSIGNSW=OFF, DOBLOADEQUIARISW=OFF;
ADD CELL: BTSID=3, SPUFN=3, SPUSN=4, SPUSSN=3, CN=3, SCTIDLST=""0"", PNLST=""80"", SID=13832, NID=65535, PZID=1, TYP=CDMA1X, LAC=""0x2141"", LCN=3, LSCTID=""0"", ASSALW1X=YES, IFBORDCELL=NO, REVRSSICARRASSNSW=OFF, AUTODWNFWDEQLCHANTHD=20, AUTODWNCOUNTTHD=600, UNBLKFWDEQLCHANTHD=40, LOCATE=URBAN, MICROCELL=NO, HARDASSIGNTYPE=BOTH_VOICE_DATA, ANASSIST1XDOSW=OFF;
ADD CELL: BTSID=3, SPUFN=3, SPUSN=4, SPUSSN=3, CN=3, SCTIDLST=""1"", PNLST=""248"", SID=13832, NID=65535, PZID=1, TYP=CDMA1X, LAC=""0x2141"", LCN=3, LSCTID=""1"", ASSALW1X=YES, IFBORDCELL=NO, REVRSSICARRASSNSW=OFF, AUTODWNFWDEQLCHANTHD=20, AUTODWNCOUNTTHD=600, UNBLKFWDEQLCHANTHD=40, LOCATE=URBAN, MICROCELL=NO, HARDASSIGNTYPE=BOTH_VOICE_DATA, ANASSIST1XDOSW=OFF;
ADD CELL: BTSID=3, SPUFN=3, SPUSN=4, SPUSSN=3, CN=3, SCTIDLST=""2"", PNLST=""416"", SID=13832, NID=65535, PZID=1, TYP=CDMA1X, LAC=""0x2141"", LCN=3, LSCTID=""2"", ASSALW1X=YES, IFBORDCELL=NO, REVRSSICARRASSNSW=OFF, AUTODWNFWDEQLCHANTHD=20, AUTODWNCOUNTTHD=600, UNBLKFWDEQLCHANTHD=40, LOCATE=URBAN, MICROCELL=NO, HARDASSIGNTYPE=BOTH_VOICE_DATA, ANASSIST1XDOSW=OFF;
";
        protected Mock<ITownRepository> townRepository = new Mock<ITownRepository>();
        protected IENodebRepository eNodebRepository;
        protected ICellRepository cellRepository;
        protected IBtsRepository btsRepository;
        protected ICdmaCellRepository cdmaCellRepository;
    }
}
