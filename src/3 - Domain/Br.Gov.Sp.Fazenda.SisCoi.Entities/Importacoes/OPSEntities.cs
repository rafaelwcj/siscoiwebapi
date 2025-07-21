using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes {
    public class OpsListaEntity : BaseImportacaoEntity {
        public string MasterServer { get; set; }
        public string PolicyName { get; set; }
        public string ClientName { get; set; }
        public string PolicyType { get; set; }
        public string ScheduleLevelType { get; set; }
        public string JobDirectory { get; set; }
        public string MediaServer { get; set; }
        public string JobRetentionPeriod { get; set; }
        public string AcceleratorEnabled { get; set; }
        public string DeduplicationEnabled { get; set; }
        public string PolicyKeyword { get; set; }
    }

    public class OpsExecucaoEntity : BaseImportacaoEntity {
        public string MasterServer { get; set; }
        public string PolicyName { get; set; }
        public string ClientName { get; set; }
        public string ScheduleLevelType { get; set; }
        public string JobDirectory { get; set; }
        public string JobScheduledTime { get; set; }
        public string JobEndTime { get; set; }
        public int JobFileCount { get; set; }
        public decimal JobSizeGB { get; set; }
        public string JobStatus { get; set; }
        public string JobRetentionPeriod { get; set; }

        public DateTime? dt_job_schedule
        {
            get
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(this.JobScheduledTime) || this.JobScheduledTime == "-")
                        return null;

                    return DateTime.ParseExact(this.JobScheduledTime, "MMM d, yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public DateTime? dt_job_end
        {
            get
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(this.JobEndTime) || this.JobEndTime == "-")
                        return null;

                    return DateTime.ParseExact(this.JobEndTime, "MMM d, yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
