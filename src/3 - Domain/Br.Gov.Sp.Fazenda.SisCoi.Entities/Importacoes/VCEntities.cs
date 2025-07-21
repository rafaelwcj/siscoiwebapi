using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes
{    
    public class VCDiskEntity : BaseImportacaoEntity
    {
        /*
         VM;
         Disk;
         Capacity MB;
         Raw;
         Disk Mode;
         Thin;
         Eagerly Scrub;
         Split;
         Write Through;
         Level;
         Shares;
         Controller;
         Unit #;
         Path;
         Raw LUN ID;
         Raw Comp. Mode;
         Annotation;
         Responsavel;
         NB_LAST_BACKUP;
         Datacenter;
         Cluster;
         Host;
         OS according to the configuration file
             */
        public string VM { get; set; }
        //public string PowerState { get; set; }
        //public bool? Template { get; set; }
        public string Disk { get; set; }
        public decimal? CapacityMB { get; set; }
        public string Raw { get; set; }
        public string DiskMode { get; set; }
        public string Thin { get; set; }
        public string EagerlyScrub { get; set; }
        public string Split { get; set; }
        public string WriteThrough { get; set; }
        public string Level { get; set; }
        public int? Shares { get; set; }
        //public string Reservation { get; set; }
        //public int? Limit { get; set; }
        public string Controller { get; set; }
        public string UnitNR { get; set; }
        public string Path { get; set; }
        public string RawLUNID { get; set; }
        public string RawCompMode { get; set; }
        public string Annotation { get; set; }
        public string Responsavel { get; set; }
        public string NB_LAST_BACKUP { get; set; }
        //public string Responsavel { get; set; }
        //public string SRM { get; set; }
        public string Datacenter { get; set; }
        public string Cluster { get; set; }
        public string Host { get; set; }
        //public string Folder { get; set; }
        public string OsCfg { get; set; }
        //public string OsVMware { get; set; }
        //public string VmId { get; set; }
        //public string VmUuid { get; set;  }
        //public string ViSdkSrv { get; set; }
        //public string ViSdkUuid { get; set; }
    }

    public class VCInfoEntity : BaseImportacaoEntity
    {
        /*
            VM;
            DNS Name;
            Powerstate;
            Connection state;
            Guest state;
            Heartbeat;
            Consolidation Needed;
            PowerOn;
            Suspend time;
            CPUs;
            Memory;
            NICs;
            Disks;
            Network #1;
            Network #2;
            Network #3;
            Network #4;
            Resource pool;
            Folder;
            vApp;
            DAS protection;
            FT State;
            FT Latency;
            FT Bandwidth;
            FT Sec. Latency;
            Boot Required;
            Provisioned MB;
            In Use MB;
            Unshared MB;
            HA Restart Priority;
            HA Isolation Response;
            Cluster rule(s);
            Cluster rule name(s);
            Path;
            Annotation;
            Responsavel;
            NB_LAST_BACKUP;
            Datacenter;
            Cluster;
            Host;
            OS according to the configuration file;
            VI SDK API Version;
            VM UUID;
            VI SDK UUID
         */
        public string VM { get; set; }
        public string DNsName { get; set; }
        public string Powerstate { get; set; }
        public string Connectionstate { get; set; }
        public string Gueststate { get; set; }
        public string Heartbeat { get; set; }
        public string ConsolidationNeeded { get; set; }
        public string PowerOn { get; set; }
        public string suspendtime { get; set; }        
        public string CPUs { get; set; }
        public string Memory { get; set; }
        public string NICs { get; set; }
        public string Disks { get; set; }
        public string NetworkNr1 { get; set; }
        public string NetworkNr2 { get; set; }
        public string NetworkNr3 { get; set; }
        public string NetworkNr4 { get; set; }        
        public string Resourcepool { get; set; }
        public string Folder { get; set; }
        public string vApp { get; set; }
        public string DAsprotection { get; set; }
        public string FTstate { get; set; }
        public string FTLatency { get; set; }
        public string FTBandwidth { get; set; }
        public string FTsecLatency { get; set; }
        public string BootRequired { get; set; }
        public string ProvisionedMB { get; set; }
        public string InUseMB { get; set; }
        public string UnsharedMB { get; set; }
        public string HARestartPriority { get; set; }
        public string HAIsolationResponse { get; set; }        
        public string Clusterrules { get; set; }
        public string Clusterrulenames { get; set; }        
        public string Path { get; set; }
        public string Annotation { get; set; }
        public string Responsavel { get; set; }
        public string NB_LAST_BACKUP { get; set; }        
        public string Datacenter { get; set; }
        public string Cluster { get; set; }
        public string Host { get; set; }
        public string OsCfg { get; set; }
        public string ViSdkApiVersion { get; set; }
        public string VmUuid { get; set; }
        public string ViSdkUuid { get; set; }
    }

    public class VCNetworkEntity : BaseImportacaoEntity
    {
        public string VM { get; set; }
        public string Powerstate { get; set; }
        public string Adapter { get; set; }
        public string Network { get; set; }
        public string Switch { get; set; }
        public string Connected { get; set; }
        public string startsConnected { get; set; }
        public string MacAddress { get; set; }
        public string Type { get; set; }
        public string IPAddress { get; set; }
        public string Folder { get; set; }
        public string Annotation { get; set; }
        public string Responsavel { get; set; }
        public string NB_LAST_BACKUP { get; set; }
        public string Datacenter { get; set; }
        public string Cluster { get; set; }
        public string Host { get; set; }
        public string Os { get; set; }
    }

    public class VCPartitionEntity : BaseImportacaoEntity
    {
        public string VM { get; set; }
        public string Disk { get; set; }
        public string CapacityMB { get; set; }
        public string FreeMB { get; set; }
        public string FreePct { get; set; }
        public string Annotation { get; set; }
        public string Responsavel { get; set; }
        public string NB_LAST_BACKUP { get; set; }
        public string Datacenter { get; set; }  
        public string Cluster { get; set; }
        public string Host { get; set; }
        public string Os { get; set; }
    }

    public class VCClusterEntity : BaseImportacaoEntity
    {
        public string Name { get; set; }
        public string ConfigStatus { get; set; }
        public string OverallStatus { get; set; }
        public int? NumHosts { get; set; }
        public int? NumEffectiveHosts { get; set; }
        public long? TotalCpu { get; set; }
        public int? NumCpuCores { get; set; }
        public int? NumCpuThreads { get; set; }
        public long? EffectiveCpu { get; set; }
        public long? TotalMemory { get; set; }
        public long? EffectiveMemory { get; set; }
        public int? NumVMotions { get; set; }
        public bool? HAenabled { get; set; }
        public int? FailoverLevel { get; set; }
        public bool? AdmissionControlEnabled { get; set; }
        public string Hostmonitoring { get; set; }
        public string HBDatastoreCandidatePolicy { get; set;  } 
        public string IsolationResponse { get; set; } 
        public string RestartPriority { get; set; } 
        public bool? ClusterSettings { get; set; } 
        public int? MaxFailures { get; set; } 
        public int? MaxFailureWindow { get; set; }
        public int? FailureInterval { get; set; } 
        public int? MinUpTime { get; set; }
        public string VMMonitoring { get; set; } 
        public bool? DRSEnabled { get; set; } 
        public string DRSDefaultVMbehavior { get; set; } 
        public int? DRSVMotionRate { get; set; }
        public bool? DPMEnabled { get; set; } 
        public string DPMDefaultbehavior { get; set; }
        public int? DPMHostPowerActionRate { get; set; }
        public string VISDKServer { get; set; }
        public string VISDKUUID { get; set; }
        public string Site { get; set; }
    }

    public class VCDataStoreEntity : BaseImportacaoEntity
    {
        public string Name { get; set; }
        public string ConfigStatus { get; set; }
        public string Address { get; set; }
        public bool? Accessible { get; set; }
        public string Type { get; set; }
        public int? NumVMs { get; set; }
        public decimal? CapacityMB { get; set; }
        public decimal? ProvisionedMB { get; set; }
        public decimal? InUseMB { get; set; }
        public decimal? FreeMB { get; set; }
        public decimal? FreePct { get; set; }
        public bool? SIOCEnabled { get; set; }
        public int? SIOCThreshold { get; set; }
        public int? NumHosts { get; set; }
        public string Hosts { get; set; }
        public int? BlockSize { get; set; }
        public long MaxBlocks { get; set; }
        public int? NumExtents { get; set; }
        public int? MajorVersion { get; set; }
        public string Version { get; set; }
        public bool? VMFSUpgradeable { get; set; }
        public bool? MHA { get; set; }
        public string URL { get; set; }
        public string VISDKServer { get; set; }
        public string VISDKUUID { get; set; }
    }

    public class VCHostEntity : BaseImportacaoEntity
    {
        public string Host { get; set; }
        public string Datacenter { get; set; }
        public string Cluster { get; set; }
        public string ConfigStatus { get; set; }
        public string CPUModel { get; set; }
        public long? Speed { get; set; }
        public string HTAvailable { get; set; }
        public string HTActive { get; set; }
        public int? NumCPU { get; set; }
        public int? CoresPerCPU { get; set; }
        public int? NumCores { get; set; }
        public decimal? CPUUsagePct { get; set; }	
        public long? Memory { get; set; }
        public decimal? MemoryUsagePct { get; set; }
        public int? Console { get; set; }
        public int? NumNICs { get; set; }
        public int? NumHBAs { get; set; }
        public int? NumVMs { get; set; }
        public decimal? VMsPerCore { get; set; }
        public int? NumvCPUs { get; set; }
        public decimal? vCPUsPerCore { get; set; }
        public long? vRAM { get; set; }
        public long? VMUsedMemory { get; set; }
        public long? VMMemorySwapped { get; set; }
        public long? VMMemoryBallooned { get; set; }
        public string VMotionSupport { get; set; }
        public string StorageVMotionSupport { get; set; }
        public string CurrentEVC { get; set; }
        public string MaxEVC { get; set; }
        public string ESXVersion { get; set; }
        public DateTime? BootTime { get; set; }
        public string DNSServers { get; set; }
        public string DHCP { get; set; }
        public string Domain { get; set; }
        public string DNSSearchOrder { get; set; }
        public string NTPServers { get; set; }
        public string NTPDRunning { get; set; }
        public string TimeZone { get; set; }
        public string TimeZoneName { get; set; }
        public int? GMTOffset { get; set; }
        public string Vendor { get; set; }
        public string Model { get; set; }
        public string ServiceTag { get; set; }
        public string OEMSpecific { get; set; }
        public string BIOSVersion { get; set; }
        public DateTime? BIOSDate { get; set; }
        public string ObjectID { get; set; }
        public string VISDKServer { get; set; }
        public string VISDKUUID { get; set; }
    }
}
