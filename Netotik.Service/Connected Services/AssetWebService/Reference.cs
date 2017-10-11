﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Netotik.Service.AssetWebService {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AssetWebService.Plan_SrvSoap")]
    public interface Plan_SrvSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/InsertPlan", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        long InsertPlan(string P_PASS, string P_YEAR, int P_regn, string p_plan_num, string p_addres, string p_desc, string p_reqid, string p_sh_tasisat, string p_work_type, string p_defn_id, string p_actv_type);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/InsertPlan", ReplyAction="*")]
        System.Threading.Tasks.Task<long> InsertPlanAsync(string P_PASS, string P_YEAR, int P_regn, string p_plan_num, string p_addres, string p_desc, string p_reqid, string p_sh_tasisat, string p_work_type, string p_defn_id, string p_actv_type);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetBudget", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetBudget(string P_PASS);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetBudget", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetBudgetAsync(string P_PASS);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetRegion", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetRegion(string P_PASS);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetRegion", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetRegionAsync(string P_PASS);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetPlan", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetPlan(string P_PASS, string P_YEAR, string P_TFZL2, string PLAN_ID, string PLAN_NUM);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetPlan", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetPlanAsync(string P_PASS, string P_YEAR, string P_TFZL2, string PLAN_ID, string PLAN_NUM);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetPriceList", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet GetPriceList(string P_PASS, string P_YEAR);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/GetPriceList", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GetPriceListAsync(string P_PASS, string P_YEAR);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Nezarat", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        long Nezarat(string P_PASS, string P_WRK_YEAR, string P_WRK_SEQN, string P_TYPE, string P_SRLC_DATE, string P_ITM, string P_COUNT);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Nezarat", ReplyAction="*")]
        System.Threading.Tasks.Task<long> NezaratAsync(string P_PASS, string P_WRK_YEAR, string P_WRK_SEQN, string P_TYPE, string P_SRLC_DATE, string P_ITM, string P_COUNT);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Itm_List", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet Itm_List(string P_PASS, string P_YEAR);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Itm_List", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> Itm_ListAsync(string P_PASS, string P_YEAR);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Dedicate", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        long Dedicate(string P_PASS, string P_REGN_ID, string P_DATE, string P_DONATOR, string P_ADRS, string P_NOTE, string P_CODE, string P_ITM, string P_COUNT, string P_PRIC, string P_PLAN_NUM);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Dedicate", ReplyAction="*")]
        System.Threading.Tasks.Task<long> DedicateAsync(string P_PASS, string P_REGN_ID, string P_DATE, string P_DONATOR, string P_ADRS, string P_NOTE, string P_CODE, string P_ITM, string P_COUNT, string P_PRIC, string P_PLAN_NUM);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Soorat_Vaziyat", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet Soorat_Vaziyat(string P_PASS, string P_GHARARDAD, string P_ITEM_CODE);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Soorat_Vaziyat", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> Soorat_VaziyatAsync(string P_PASS, string P_GHARARDAD, string P_ITEM_CODE);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Peymankar", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet Peymankar(string P_PASS, string P_TAFZIL);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Peymankar", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> PeymankarAsync(string P_PASS, string P_TAFZIL);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Motammem_Gharardad", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet Motammem_Gharardad(string P_PASS, string P_GHARARDAD, string P_ITEM_CODE);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Motammem_Gharardad", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> Motammem_GharardadAsync(string P_PASS, string P_GHARARDAD, string P_ITEM_CODE);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Motammem_Dastoorkar", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet Motammem_Dastoorkar(string P_PASS, string P_WRK_YEAR, string P_WRK_SEQN, string P_ITEM_CODE);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Motammem_Dastoorkar", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> Motammem_DastoorkarAsync(string P_PASS, string P_WRK_YEAR, string P_WRK_SEQN, string P_ITEM_CODE);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Dastoorkar", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet Dastoorkar(string P_PASS, string P_WRK_YEAR, string P_WRK_SEQN, string P_ITEM_CODE);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Dastoorkar", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> DastoorkarAsync(string P_PASS, string P_WRK_YEAR, string P_WRK_SEQN, string P_ITEM_CODE);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Amval", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet Amval(string P_PASS, string P_REGN_CODE, string P_LABEL);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Amval", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> AmvalAsync(string P_PASS, string P_REGN_CODE, string P_LABEL);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Gharardad", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet Gharardad(string P_PASS, string P_TAFZIL, string P_SH_GHARARDAD);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Gharardad", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> GharardadAsync(string P_PASS, string P_TAFZIL, string P_SH_GHARARDAD);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Gharardad_Item", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet Gharardad_Item(string P_PASS, string P_SH_GHARARDAD);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Gharardad_Item", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> Gharardad_ItemAsync(string P_PASS, string P_SH_GHARARDAD);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Tasisat", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet Tasisat(string P_PASS, string P_SH_Tasisat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Tasisat", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> TasisatAsync(string P_PASS, string P_SH_Tasisat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Tasisat_Kala", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet Tasisat_Kala(string P_PASS, string P_SH_Tasisat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Tasisat_Kala", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> Tasisat_KalaAsync(string P_PASS, string P_SH_Tasisat);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface Plan_SrvSoapChannel : Netotik.Service.AssetWebService.Plan_SrvSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Plan_SrvSoapClient : System.ServiceModel.ClientBase<Netotik.Service.AssetWebService.Plan_SrvSoap>, Netotik.Service.AssetWebService.Plan_SrvSoap {
        
        public Plan_SrvSoapClient() {
        }
        
        public Plan_SrvSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Plan_SrvSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Plan_SrvSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Plan_SrvSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public long InsertPlan(string P_PASS, string P_YEAR, int P_regn, string p_plan_num, string p_addres, string p_desc, string p_reqid, string p_sh_tasisat, string p_work_type, string p_defn_id, string p_actv_type) {
            return base.Channel.InsertPlan(P_PASS, P_YEAR, P_regn, p_plan_num, p_addres, p_desc, p_reqid, p_sh_tasisat, p_work_type, p_defn_id, p_actv_type);
        }
        
        public System.Threading.Tasks.Task<long> InsertPlanAsync(string P_PASS, string P_YEAR, int P_regn, string p_plan_num, string p_addres, string p_desc, string p_reqid, string p_sh_tasisat, string p_work_type, string p_defn_id, string p_actv_type) {
            return base.Channel.InsertPlanAsync(P_PASS, P_YEAR, P_regn, p_plan_num, p_addres, p_desc, p_reqid, p_sh_tasisat, p_work_type, p_defn_id, p_actv_type);
        }
        
        public System.Data.DataSet GetBudget(string P_PASS) {
            return base.Channel.GetBudget(P_PASS);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetBudgetAsync(string P_PASS) {
            return base.Channel.GetBudgetAsync(P_PASS);
        }
        
        public System.Data.DataSet GetRegion(string P_PASS) {
            return base.Channel.GetRegion(P_PASS);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetRegionAsync(string P_PASS) {
            return base.Channel.GetRegionAsync(P_PASS);
        }
        
        public System.Data.DataSet GetPlan(string P_PASS, string P_YEAR, string P_TFZL2, string PLAN_ID, string PLAN_NUM) {
            return base.Channel.GetPlan(P_PASS, P_YEAR, P_TFZL2, PLAN_ID, PLAN_NUM);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetPlanAsync(string P_PASS, string P_YEAR, string P_TFZL2, string PLAN_ID, string PLAN_NUM) {
            return base.Channel.GetPlanAsync(P_PASS, P_YEAR, P_TFZL2, PLAN_ID, PLAN_NUM);
        }
        
        public System.Data.DataSet GetPriceList(string P_PASS, string P_YEAR) {
            return base.Channel.GetPriceList(P_PASS, P_YEAR);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GetPriceListAsync(string P_PASS, string P_YEAR) {
            return base.Channel.GetPriceListAsync(P_PASS, P_YEAR);
        }
        
        public long Nezarat(string P_PASS, string P_WRK_YEAR, string P_WRK_SEQN, string P_TYPE, string P_SRLC_DATE, string P_ITM, string P_COUNT) {
            return base.Channel.Nezarat(P_PASS, P_WRK_YEAR, P_WRK_SEQN, P_TYPE, P_SRLC_DATE, P_ITM, P_COUNT);
        }
        
        public System.Threading.Tasks.Task<long> NezaratAsync(string P_PASS, string P_WRK_YEAR, string P_WRK_SEQN, string P_TYPE, string P_SRLC_DATE, string P_ITM, string P_COUNT) {
            return base.Channel.NezaratAsync(P_PASS, P_WRK_YEAR, P_WRK_SEQN, P_TYPE, P_SRLC_DATE, P_ITM, P_COUNT);
        }
        
        public System.Data.DataSet Itm_List(string P_PASS, string P_YEAR) {
            return base.Channel.Itm_List(P_PASS, P_YEAR);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> Itm_ListAsync(string P_PASS, string P_YEAR) {
            return base.Channel.Itm_ListAsync(P_PASS, P_YEAR);
        }
        
        public long Dedicate(string P_PASS, string P_REGN_ID, string P_DATE, string P_DONATOR, string P_ADRS, string P_NOTE, string P_CODE, string P_ITM, string P_COUNT, string P_PRIC, string P_PLAN_NUM) {
            return base.Channel.Dedicate(P_PASS, P_REGN_ID, P_DATE, P_DONATOR, P_ADRS, P_NOTE, P_CODE, P_ITM, P_COUNT, P_PRIC, P_PLAN_NUM);
        }
        
        public System.Threading.Tasks.Task<long> DedicateAsync(string P_PASS, string P_REGN_ID, string P_DATE, string P_DONATOR, string P_ADRS, string P_NOTE, string P_CODE, string P_ITM, string P_COUNT, string P_PRIC, string P_PLAN_NUM) {
            return base.Channel.DedicateAsync(P_PASS, P_REGN_ID, P_DATE, P_DONATOR, P_ADRS, P_NOTE, P_CODE, P_ITM, P_COUNT, P_PRIC, P_PLAN_NUM);
        }
        
        public System.Data.DataSet Soorat_Vaziyat(string P_PASS, string P_GHARARDAD, string P_ITEM_CODE) {
            return base.Channel.Soorat_Vaziyat(P_PASS, P_GHARARDAD, P_ITEM_CODE);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> Soorat_VaziyatAsync(string P_PASS, string P_GHARARDAD, string P_ITEM_CODE) {
            return base.Channel.Soorat_VaziyatAsync(P_PASS, P_GHARARDAD, P_ITEM_CODE);
        }
        
        public System.Data.DataSet Peymankar(string P_PASS, string P_TAFZIL) {
            return base.Channel.Peymankar(P_PASS, P_TAFZIL);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> PeymankarAsync(string P_PASS, string P_TAFZIL) {
            return base.Channel.PeymankarAsync(P_PASS, P_TAFZIL);
        }
        
        public System.Data.DataSet Motammem_Gharardad(string P_PASS, string P_GHARARDAD, string P_ITEM_CODE) {
            return base.Channel.Motammem_Gharardad(P_PASS, P_GHARARDAD, P_ITEM_CODE);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> Motammem_GharardadAsync(string P_PASS, string P_GHARARDAD, string P_ITEM_CODE) {
            return base.Channel.Motammem_GharardadAsync(P_PASS, P_GHARARDAD, P_ITEM_CODE);
        }
        
        public System.Data.DataSet Motammem_Dastoorkar(string P_PASS, string P_WRK_YEAR, string P_WRK_SEQN, string P_ITEM_CODE) {
            return base.Channel.Motammem_Dastoorkar(P_PASS, P_WRK_YEAR, P_WRK_SEQN, P_ITEM_CODE);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> Motammem_DastoorkarAsync(string P_PASS, string P_WRK_YEAR, string P_WRK_SEQN, string P_ITEM_CODE) {
            return base.Channel.Motammem_DastoorkarAsync(P_PASS, P_WRK_YEAR, P_WRK_SEQN, P_ITEM_CODE);
        }
        
        public System.Data.DataSet Dastoorkar(string P_PASS, string P_WRK_YEAR, string P_WRK_SEQN, string P_ITEM_CODE) {
            return base.Channel.Dastoorkar(P_PASS, P_WRK_YEAR, P_WRK_SEQN, P_ITEM_CODE);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> DastoorkarAsync(string P_PASS, string P_WRK_YEAR, string P_WRK_SEQN, string P_ITEM_CODE) {
            return base.Channel.DastoorkarAsync(P_PASS, P_WRK_YEAR, P_WRK_SEQN, P_ITEM_CODE);
        }
        
        public System.Data.DataSet Amval(string P_PASS, string P_REGN_CODE, string P_LABEL) {
            return base.Channel.Amval(P_PASS, P_REGN_CODE, P_LABEL);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> AmvalAsync(string P_PASS, string P_REGN_CODE, string P_LABEL) {
            return base.Channel.AmvalAsync(P_PASS, P_REGN_CODE, P_LABEL);
        }
        
        public System.Data.DataSet Gharardad(string P_PASS, string P_TAFZIL, string P_SH_GHARARDAD) {
            return base.Channel.Gharardad(P_PASS, P_TAFZIL, P_SH_GHARARDAD);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> GharardadAsync(string P_PASS, string P_TAFZIL, string P_SH_GHARARDAD) {
            return base.Channel.GharardadAsync(P_PASS, P_TAFZIL, P_SH_GHARARDAD);
        }
        
        public System.Data.DataSet Gharardad_Item(string P_PASS, string P_SH_GHARARDAD) {
            return base.Channel.Gharardad_Item(P_PASS, P_SH_GHARARDAD);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> Gharardad_ItemAsync(string P_PASS, string P_SH_GHARARDAD) {
            return base.Channel.Gharardad_ItemAsync(P_PASS, P_SH_GHARARDAD);
        }
        
        public System.Data.DataSet Tasisat(string P_PASS, string P_SH_Tasisat) {
            return base.Channel.Tasisat(P_PASS, P_SH_Tasisat);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> TasisatAsync(string P_PASS, string P_SH_Tasisat) {
            return base.Channel.TasisatAsync(P_PASS, P_SH_Tasisat);
        }
        
        public System.Data.DataSet Tasisat_Kala(string P_PASS, string P_SH_Tasisat) {
            return base.Channel.Tasisat_Kala(P_PASS, P_SH_Tasisat);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> Tasisat_KalaAsync(string P_PASS, string P_SH_Tasisat) {
            return base.Channel.Tasisat_KalaAsync(P_PASS, P_SH_Tasisat);
        }
    }
}