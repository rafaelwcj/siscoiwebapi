using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.Importacoes
{
    /// <summary>
    /// Entidade referente 'a antiga importacao de Storage da secao "Servidores"
    /// </summary>
    public class StorageEntity : BaseImportacaoEntity
    {
        public string NomeStorage { get; set; }
        public string NomeVolume { get; set; }
        public string StorageLunGroup { get; set; }
        public string TamanhoGB { get; set; }
    }

    /// <summary>
    /// Entidade para tráfego de dados dos Storages
    /// Obs.: os totais sao armazenados na base de dados em GB (gigabytes)
    /// </summary>
    public class Storage : BaseImportacaoEntity
    {
        public Int64 id_equip { get; set; }
        public string nm_equip { get; set; }
        public string ds_local { get; set; }
        public string ds_site { get; set; }
        public string cd_contrato { get; set; }

        public DateTime? dt_entrega { get; set; }   // Data de Entrega => Data do Contrato
        public DateTime? dt_aceite { get; set; }
        public DateTime? dt_vigencia { get; set; }  // Data de Vigência => Data de Garantia
        public DateTime dt_atualizacao { get; set; }
        public DateTime? dt_licitacao { get; set; }
        public DateTime? dt_desativ { get; set; }  // Data de desativacao

        public string ds_rack { get; set; }
        public string cd_ip { get; set; }
        public string nm_coml { get; set; }
        public int tp_disco { get; set; }
        public int tp_storage { get; set; }
        public int? ic_ativo { get; set; }

        // Dados ref. anexo de contrato
        public int? id_contrato { get; set; }
        public byte[] fl_contrato { get; set; }
        public string nm_contrato { get; set; }

        // Estes valores sao informados em GIGABYTES
        public decimal qt_bruto { get; set; }
        public decimal qt_liquido { get; set; }
        public decimal qt_livre { get; set; }
        public decimal qt_aloc { get; set; }

        // os mesmos valores acima, convertidos para TERABYTES
        public decimal TotalLiquidoTB { get { return qt_liquido / 1024; } }
        public decimal TotalLivreTB { get { return qt_livre / 1024; } }
        public decimal TotalAlocadoTB { get { return qt_aloc / 1024; } }

        // Os valores FTV sao informados em TB
        public decimal qt_ftv_total { get; set; }
        public decimal qt_ftv_aloc { get; set; }
        public decimal qt_ftv_dif { get { return qt_ftv_total - qt_ftv_aloc; } }

        public decimal TotalFtvTB {  get { return qt_ftv_total; } }
        public decimal TotalFtvAlocTB { get { return qt_ftv_aloc; } }
        public decimal TotalFtvDifTB { get { return qt_ftv_dif; } }

        public int AvisoGarantia { get; set; }

        public string Ativo
        {
            get
            {
                return ic_ativo.Value == 0 ? "Inativo" : "Ativo";
            }
        }

        public string TipoAbrev
        {
            get
            {
                switch(tp_storage)
                {
                    case 0:
                        return "Mid";

                    case 1:
                        return "High";

                    default:
                        return string.Empty;
                }
            }
        }

        public Storage()
        {
            qt_bruto = 0;
            qt_liquido = 0;
            qt_livre = 0;
            qt_aloc = 0;
            qt_ftv_total = qt_ftv_aloc = 0;
            nm_equip = nm_coml = ds_local = ds_site = cd_contrato = ds_rack = cd_ip = String.Empty;
            dt_aceite = dt_entrega = dt_vigencia = dt_licitacao = dt_atualizacao = DateTime.MinValue;
            tp_storage = tp_disco = -1;
            fl_contrato = null;
            ic_ativo = 0;
        }
    }

    /// <summary>
    /// Entidade para auxílio de exibição histórica com o "pivô" afixado no nome do Storage
    /// </summary>
    public class RowHistStg
    {
        public DateTime Data { get; set; }                  // Data referente 'a informacao
        public List<decimal?> ValoresQtLivre { get; set; }  // Volumetria de cada storage registrado

        public RowHistStg()
        {
            ValoresQtLivre = new List<decimal?>();
        }
    }

    /// <summary>
    /// Entidade para exibição de volumetrias como séries temporais
    /// </summary>
    public class HistoricoStorage
    {
        public int DeviceCount { get; set; }            // Quantidade de colunas
        public int RowCount { get; set; }               // Quantidade de registros
        public List<string> DeviceNames { get; set; }   // Nomes das colunas (nomes dos storages)
        public List<RowHistStg> Rows { get; set; }      // Valores: data, livre do 'Storage X', livre do 'Storage Y',  ...

        public HistoricoStorage()
        {
            DeviceNames = new List<string>();
            Rows = new List<RowHistStg>();
        }
    }

    /// <summary>
    /// Entidade para trafego de dados dos totais sumarizados
    /// </summary>
    public class TotalStorageEntity : BaseImportacaoEntity
    {
        public string Site { get; set; }
        public DateTime Data { get; set; }
        public decimal TotalBrutoGB { get; set; }
        public decimal TotalLiquidoGB { get; set; }
        public decimal TotalLivreGB { get; set; }
        public decimal TotalAlocadoGB { get; set; }
        public List<Storage> Storages { get; set; }

        public decimal TotalBrutoTB { get { return TotalBrutoGB / 1024; } }
        public decimal TotalLiquidoTB { get { return TotalLiquidoGB / 1024; } }
        public decimal TotalAlocadoTB { get { return TotalAlocadoGB / 1024; } }
        public decimal TotalLivreTB { get { return TotalLivreGB / 1024; } }

        public TotalStorageEntity()
        {
            Site = "Todos";
            TotalAlocadoGB = TotalBrutoGB = TotalLiquidoGB = TotalLivreGB = 0;
            Storages = new List<Storage>();
        }
    }

    /// <summary>
    /// Classe estatica para interpretacao dos arquivos do Storage.
    /// </summary>
    public static class StorageParser
    {
        private static int errors = 0;
        public static int Errors { get { return errors; } }

        /// <summary>
        /// Abre um arquivo de um determinado modelo de Storage e converte todo seu conteudo para entidades.
        /// </summary>
        /// <param name="tipo">Tipo de Importacao Storage</param>
        /// <param name="arquivo">Caminho completo para o arquivo</param>
        /// <returns>Entidade para persistência de dados do Storage</returns>
        public static Storage ParseFile(Enums.TipoImportacao tipo, string arquivo)
        {
            errors = 0;               

            /// A maioria dos arquivos ou traz apenas os totais dos Pools, ou as informacoes individuais
            /// desses pools sao mais precisas do que os totais sumarizados.
            /// Nesses casos os totais gerais são desconsiderados.
            Storage result = new Storage();
            List<GeneralStoragePool> poolList = new List<GeneralStoragePool>();

            // interpretar e converter o arquivo de acordo com modelo do storage
            switch (tipo)
            {
                case Enums.TipoImportacao.STG_HUAWEI_5500:
                    poolList = ConvertPools(ParseHw5500File(arquivo));
                    break;

                case Enums.TipoImportacao.STG_HUAWEI_5800T:
                    poolList = ConvertPools(ParseHw5800TFile(arquivo));
                    break;

                case Enums.TipoImportacao.STG_FUJITSU_DX8700S2:
                    poolList = ConvertPools(ParseFjDx8700S2File(arquivo));
                    ParseFtv(arquivo, result);
                    break;

                case Enums.TipoImportacao.STG_EMC_VMAX:
                    poolList = ConvertPools(ParseEmcVmaxFile(arquivo));
                    break;

                case Enums.TipoImportacao.STG_HP_3PAR8200:
                    // tratamento diferenciado.
                    // O arquivo nao traz as informacoes dos Pools, apenas os totais do Storage.
                    return ParseHp3par8200File(arquivo);

                case Enums.TipoImportacao.STG_DELL_COMPELLENT:
                    // tratamento diferenciado.
                    return ParseDellCompellentFile(arquivo);

                default:
                    throw new Exception("Tipo de arquivo não suportado pelo interpretador de Storage.");
            }

            // identificacao do storage = nome dado ao arquivo
            result.nm_equip = Path.GetFileNameWithoutExtension(arquivo);

            // somar os totais do Storage conforme informacoes de seus Pools.
            foreach (var p in poolList)
            {
                result.qt_liquido += p.LiquidoGB;
                result.qt_livre += p.LivreGB;
                result.qt_aloc += p.UsadoGB;
            }

            return result;
        }

        /// <summary>
        /// Especifico para alguns modelos de Storage:
        /// obtem informacoes dos FTV com base no caminho do arquivo dos pools.
        /// </summary>
        /// <param name="caminhoArqPools"></param>
        /// <returns></returns>
        private static bool ParseFtv(string caminhoArqPools, Storage sto)
        {
            try
            {
                string arqFtv = Directory.GetParent(Path.GetDirectoryName(caminhoArqPools)) + "\\ftv\\" + Path.GetFileNameWithoutExtension(caminhoArqPools) + ".ftv";

                if (!File.Exists(arqFtv))
                    return false;

                using (StreamReader reader = new StreamReader(arqFtv, Encoding.ASCII, true))
                {
                    while (!reader.EndOfStream)
                    {
                        try
                        {
                            var textLine = reader.ReadLine();

                            if (string.IsNullOrWhiteSpace(textLine))
                                continue;

                            var cols = textLine.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                            if (cols == null)
                                continue;

                            switch (cols.Length)
                            {
                                case 4:
                                    if (cols[0] == "Total")
                                    {
                                        var t = cols[3].Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                                        if (t != null && t.Length == 2)
                                            sto.qt_ftv_aloc = ConvertVal(t[1]);
                                    }
                                    break;

                                case 11:
                                    sto.qt_ftv_total += ConvertVal(cols[7]);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            errors++;
                        }
                    }
                }
            }
            catch (Exception)
            {
                errors++;
                return false;
            }

            return true;
        }

        /// <summary>
        /// Classe-base para os diferentes tipos de Storage Pool existentes nos arquivos.
        /// </summary>
        private class BaseStoragePool
        {
            //public int PoolId { get; set; }
            //public string Name { get; set; }
            //public string HealthStatus { get; set; }

            protected virtual decimal ConvertTotal(string total)
            {
                try
                {
                    return Convert.ToDecimal(total.Replace("TB", "").Replace("GB", "").Replace("MB", "").Replace("KB", ""), new CultureInfo("en-US"));
                }
                catch (Exception)
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// Classe que generaliza e converte informações dos diferentes tipos de "pools" dos arquivos dos Storages.
        /// No momento as informações dos Pools não são necessárias na base de dados, mas
        /// precisam ser computadas durante processamento do arquivo para obtencao dos totais corretos.
        /// Esta classe só pode ser instanciada Por Contrato através
        /// de uma das outras classes de Pool disponíveis.
        /// </summary>
        private class GeneralStoragePool
        {
            //private int poolId = -1;
            //private string nomePool = String.Empty;
            private decimal usadoGB = 0;
            private decimal liquidoGB = 0;
            private decimal livreGB = 0;

            //public int PoolId { get { return poolId; } }
            //public string NomePool { get { return nomePool; } }
            public decimal UsadoGB { get { return usadoGB; } }
            public decimal LiquidoGB { get { return liquidoGB; } }
            public decimal LivreGB { get { return livreGB; } }

            private GeneralStoragePool() { }

            public GeneralStoragePool(Hw5500Pool pool)
            {
               // this.poolId = pool.PoolId;
                //this.nomePool = pool.Name;
                this.liquidoGB = pool.TotalCapacityTB * 1024;
                this.livreGB = pool.FreeCapacityTB * 1024;
                this.usadoGB = this.liquidoGB - this.livreGB;
            }

            /*
                Total Usado	=>  Tt Subscribed
                Total Livre	=>	Tt Capacity - Tt Subscribed
                Total Geral =>	Usado + Livre
            */
            public GeneralStoragePool(Hw5800TPool pool)
            {
                //this.poolId = pool.PoolId;
                //this.nomePool = pool.Name;
                this.usadoGB = pool.TotalSubscribedCapacityMB / 1024;
                this.livreGB = (pool.TotalCapacityMB - pool.TotalSubscribedCapacityMB) / 1024;
                this.liquidoGB = this.usadoGB + this.livreGB;
            }

            public GeneralStoragePool(FjDx8700S2Pool pool)
            {
                //this.poolId = pool.PoolId;
                //this.nomePool = pool.Name;
                this.liquidoGB = pool.TotalCapacityMB / 1024;
                this.livreGB = pool.FreeCapacityMB / 1024;
                this.usadoGB = this.liquidoGB - this.livreGB;
            }

            public GeneralStoragePool(EmcVmaxPool pool)
            {
                //this.nomePool = pool.Name;
                this.liquidoGB = pool.TotalGB;
                this.usadoGB = pool.SubsPct / 100 * pool.TotalGB;
                this.livreGB = this.liquidoGB - this.usadoGB;
            }
        }

        /// <summary>
        /// Pool conforme o arquivo extraído do Storage Huawei 5500
        /// </summary>
        private class Hw5500Pool : BaseStoragePool
        {
            public string DiskDomainId { get; set; }
            public string RunningStatus { get; set; }
            public string TotalCapacityTBText { get; set; }
            public string FreeCapacityTBText { get; set; }
            public string UsageType { get; set; }

            // 2018-07-16: FIX. 
            // Devido a mudancas no storage foi possivel detectar que as linhas do arquivo
            // na realidade podem informar valores nao apenas em TB, mas tambem em GB, MB etc.
            protected override decimal ConvertTotal(string total)
            {
                decimal div = 0m;

                if (total.Contains("TB"))
                    div = 1m;
                else
                {
                    if (total.Contains("GB"))
                        div = 1024m;
                    else
                    {
                        if (total.Contains("MB"))
                            div = 1048576m;

                        else
                        {
                            if (total.Contains("KB"))
                                div = 1073741824m;
                        }
                    }
                }

                // se o valor nao estiver explicitamente em TB, GB, MB ou KB, entao devolver um valor inconsistente 
                // que sera' detectado no relatorio, visualmente.
                if (div == 0m)
                    return -1m;

                return Convert.ToDecimal(total.Replace("TB", "").Replace("GB", "").Replace("MB", "").Replace("KB", ""), new CultureInfo("en-US")) / div;
            }

            public decimal TotalCapacityTB
            {
                get
                {
                    return ConvertTotal(this.TotalCapacityTBText);
                }
            }

            public decimal FreeCapacityTB
            {
                get
                {
                    return ConvertTotal(this.FreeCapacityTBText);
                }
            }
        }

        /// <summary>
        /// Pool conforme o arquivo extraído do Storage Huawei 5800T
        /// </summary>
        private class Hw5800TPool : BaseStoragePool
        {
            public decimal TotalCapacityMB { get; set; }
            public decimal AvailableCapacityMB { get; set; }
            public decimal ConsumedCapacityMB { get; set; }
            public decimal TotalSubscribedCapacityMB { get; set; }
        }

        /// <summary>
        /// Pool conforme o arquivo extraído do Storage Fujitsu DX8700 S2
        /// </summary>
        private class FjDx8700S2Pool : BaseStoragePool
        {
            public decimal TotalCapacityMB { get; set; }
            public decimal FreeCapacityMB { get; set; }
        }

        /// <summary>
        /// Pool conforme o arquivo extraído do Storage EMC Vmax
        /// </summary>
        private class EmcVmaxPool : BaseStoragePool
        {
            public decimal TotalGB { get; set; }
            public decimal UsableGB { get; set; }
            public decimal SubsPct { get; set; }
        }

        private static List<GeneralStoragePool> ConvertPools(List<Hw5500Pool> poolList)
        {
            List <GeneralStoragePool> result = new List<GeneralStoragePool>();

            if (poolList == null || poolList.Count == 0)
                return result;

            foreach(var p in poolList)
            {
                result.Add(new GeneralStoragePool(p));
            }

            return result;
        }

        private static List<GeneralStoragePool> ConvertPools(List<Hw5800TPool> poolList)
        {
            List<GeneralStoragePool> result = new List<GeneralStoragePool>();

            if (poolList == null || poolList.Count == 0)
                return result;

            foreach (var p in poolList)
            {
                result.Add(new GeneralStoragePool(p));
            }

            return result;
        }

        private static List<GeneralStoragePool> ConvertPools(List<FjDx8700S2Pool> poolList)
        {
            List<GeneralStoragePool> result = new List<GeneralStoragePool>();

            if (poolList == null || poolList.Count == 0)
                return result;

            foreach (var p in poolList)
            {
                result.Add(new GeneralStoragePool(p));
            }

            return result;
        }

        private static List<GeneralStoragePool> ConvertPools(List<EmcVmaxPool> poolList)
        {
            var result = new List<GeneralStoragePool>();

            if (poolList == null || poolList.Count == 0)
                return result;

            foreach (var p in poolList)
            {
                result.Add(new GeneralStoragePool(p));
            }

            return result;
        }

        private static List<Hw5500Pool> ParseHw5500File(string path)
        {
            List<Hw5500Pool> result = new List<Hw5500Pool>();

            using (StreamReader reader = new StreamReader(path, Encoding.ASCII, true))
            {
                while (!reader.EndOfStream)
                {
                    try
                    {
                        var textLine = reader.ReadLine();

                        if (string.IsNullOrWhiteSpace(textLine) || textLine.StartsWith("--"))
                            continue;

                        var cols = textLine.Split(';');

                        if (cols == null || cols.Length != 8 || cols[0] == "ID")
                        {
                            continue;
                        }

                        result.Add(
                            new Hw5500Pool()
                            {
                                //PoolId = Convert.ToInt32(cols[0]),
                                //Name = cols[1],
                                DiskDomainId = cols[2],
                                //HealthStatus = cols[3],
                                RunningStatus = cols[4],
                                TotalCapacityTBText = cols[5],
                                FreeCapacityTBText = cols[6],
                                UsageType = cols[7]
                            }
                        );
                    }
                    catch (Exception)
                    {
                        errors++;
                    }
                }
            }

            return result;
        }

        private static decimal ConvertVal(string val)
        {
            return Convert.ToDecimal(val, new CultureInfo("en-US"));
        }

        private static List<Hw5800TPool> ParseHw5800TFile(string path)
        {
            List<Hw5800TPool> result = new List<Hw5800TPool>();
            Hw5800TPool pool = new Hw5800TPool();

            using (StreamReader reader = new StreamReader(path, Encoding.ASCII, true))
            {
                while (!reader.EndOfStream)
                {
                    try
                    {
                        var textLine = reader.ReadLine();

                        if (string.IsNullOrWhiteSpace(textLine))
                            continue;

                        var cols = textLine.Split('|');

                        if (cols == null || cols.Length != 2)
                        {
                            continue;
                        }

                        switch(cols[0].ToLower().Trim())
                        {
                            case "id":
                                pool = new Hw5800TPool();
                                //pool.PoolId = Convert.ToInt32(cols[1]);
                                break;

                            //case "name":
                            //    pool.Name = cols[1].Trim();
                            //    break;

                            case "total capacity(mb)":
                                pool.TotalCapacityMB = ConvertVal(cols[1]);
                                break;

                            case "available capacity(mb)":
                                pool.AvailableCapacityMB = ConvertVal(cols[1]);
                                break;

                            case "consumed capacity(mb)":
                                pool.ConsumedCapacityMB = ConvertVal(cols[1]);
                                break;

                            case "total subscribed capacity(mb)":
                                pool.TotalSubscribedCapacityMB = ConvertVal(cols[1]);
                                break;

                            //case "status":
                            //    pool.HealthStatus = cols[1].Trim();
                            //    break;

                            case "member disk list":
                                result.Add(pool);
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        errors++;
                    }
                }
            }

             return result;
        }

        private static List<FjDx8700S2Pool> ParseFjDx8700S2File(string path)
        {
            var result = new List<FjDx8700S2Pool>();

            using (StreamReader reader = new StreamReader(path, Encoding.ASCII, true))
            {
                while (!reader.EndOfStream)
                {
                    try
                    {
                        var textLine = reader.ReadLine();

                        if (string.IsNullOrWhiteSpace(textLine))
                            continue;

                        var cols = textLine.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                        if (cols == null || cols.Length != 7 || cols[0] == "RAID" || cols[0] == "No.")
                        {
                            continue;
                        }

                        result.Add
                        (
                            new FjDx8700S2Pool()
                            {
                                //PoolId = Convert.ToInt32(cols[0]),
                                //Name = cols[1],
                                TotalCapacityMB = ConvertVal(cols[5]),
                                FreeCapacityMB = ConvertVal(cols[6]),
                                //HealthStatus = "N/A"
                            }
                        );
                    }
                    catch (Exception)
                    {
                        errors++;
                    }
                }
            }

            return result;
        }

        private static List<EmcVmaxPool> ParseEmcVmaxFile(string path)
        {            
            var result = new List<EmcVmaxPool>();

            using (StreamReader reader = new StreamReader(path, Encoding.ASCII, true))
            {
                while (!reader.EndOfStream)
                {
                    try
                    {
                        var textLine = reader.ReadLine();

                        if (string.IsNullOrWhiteSpace(textLine))
                            continue;

                        var cols = textLine.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                        if (cols == null || cols.Length < 11 || cols[0] == "Pool" || cols[0] == "Name" || cols[0].StartsWith("-"))
                        {
                            continue;
                        }

                        if (cols.Length == 12)
                        {
                            if (cols[3] != "Mir")
                                continue;

                            result.Add
                            (
                               new EmcVmaxPool()
                               {
                                   //HealthStatus = "N/A",
                                   //Name = cols[0],
                                   TotalGB = ConvertVal(cols[4]),
                                   UsableGB = ConvertVal(cols[5]),
                                   SubsPct = ConvertVal(cols[9]),
                               }
                            );
                        }
                        else
                        {
                            if (cols.Length == 11)
                            {
                                result.Add
                                (
                                   new EmcVmaxPool()
                                   {
                                       //HealthStatus = "N/A",
                                       //Name = cols[0],
                                       TotalGB = ConvertVal(cols[3]),
                                       UsableGB = ConvertVal(cols[4]),
                                       SubsPct = ConvertVal(cols[8]),
                                   }
                                );
                            }
                        }
                    }
                    catch (Exception)
                    {
                        errors++;
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// Interpreta um arquivo gerado a partir do HP 3PAR.
        /// Há uma discrepância de valores devido a um dos parametros de entrada
        /// se encontrar em "Mebi Bytes" enquanto todo o resto está em Megabytes.
        /// O consenso foi continuar usando "1024" como divisor.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private static Storage ParseHp3par8200File(string path)
        {
            Storage result = new Storage();

            // identificacao do storage = nome dado ao arquivo
            result.nm_equip = Path.GetFileNameWithoutExtension(path);

            using (StreamReader reader = new StreamReader(path, Encoding.ASCII, true))
            {
                while (!reader.EndOfStream)
                {
                    try
                    {
                        var textLine = reader.ReadLine();

                        if (string.IsNullOrWhiteSpace(textLine))
                            continue;

                        var cols = textLine.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                        if (cols == null || cols.Length != 2)
                            continue;

                        // Obs.: o storage HP relata os totais em GiB. GiB na realidade seria o que
                        // antigamente entendíamos como GB (múltiplo de 2) e enquanto isso, GB mudou para se tornar
                        // múltiplo de 10. Ou seja,
                        // mesmo quando outros storages estariam dando resultados em GB na realidade estão dando em GiB
                        // porque eles usam base 2. Os storages HP seriam os únicos "politicamente corretos" no que se
                        // refere à representação da nomenclatura dessas grandezas computacionais.
                        switch (cols[0].ToLower().Trim())
                        {
                            case "total capacity":
                                result.qt_liquido = ConvertVal(cols[1]) / 1024m;
                                break;

                            case "allocated capacity":
                                result.qt_aloc = ConvertVal(cols[1]) / 1024m;
                                break;
                        }
                    }
                    catch (Exception)
                    {
                        errors++;
                    }
                }
            }

            result.qt_livre = result.qt_liquido - result.qt_aloc;
            return result;
        }

        private static Storage ParseDellCompellentFile(string path)
        {
            Storage result = new Storage();
            result.nm_equip = Path.GetFileNameWithoutExtension(path);

            using (StreamReader reader = new StreamReader(path, Encoding.ASCII, true))
            {
                while (!reader.EndOfStream)
                {
                    try
                    {
                        var textLine = reader.ReadLine();

                        if (string.IsNullOrWhiteSpace(textLine))
                            continue;

                        var cols = textLine.Split(":".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                        if (cols == null || cols.Length != 2)
                            continue;

                        if (cols[0].ToLower().Trim() == "configuredsize")
                        {
                            // os totais informados podem vir em TB, GB ou MB
                            // portanto precisam ser convertidos adequadamente.
                            var val = cols[1].Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                            if (val == null || val.Length != 2)
                                continue;

                            switch(val[1].Trim())
                            {
                                case "TB":
                                    result.qt_aloc += ConvertVal(val[0]) * 1024;
                                    break;

                                case "GB":
                                    result.qt_aloc += ConvertVal(val[0]);
                                    break;

                                case "MB":
                                    result.qt_aloc += ConvertVal(val[0]) / 1024;
                                    break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        errors++;
                    }
                }
            }

            // o valor total liquido padrao = 1.28 PetaBytes (Denis Greco e Rodrigo Avino - 10/Nov/2017)
            result.qt_liquido = 1342177.28m;    // == 1.28 PB => GB

            // espacolivre = liquido - alocado  (Denis Greco e Rodrigo Avino - 10/Nov/2017)
            result.qt_livre = result.qt_liquido - result.qt_aloc;

            return result;
        }
    }
}

