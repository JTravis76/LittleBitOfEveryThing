using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"C:\Users\KDJ\Desktop\20180208101955FBPSC.neu";
                //@"C:\Users\KDJ\source\repos\NeuParser\NeuParser\TestFiles\20180201094536FBPSC.neu";

            if (System.IO.File.Exists(filePath))
            {
                NeuModel model = new NeuModel();

                using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();

                        if (line.StartsWith("$"))
                        {
                            //Remove $ and : from Header
                            line = line.Remove(0, 1).Replace(":", "").Trim();

                            //manually map, in-case of more than one
                            if (line == "PROCEDURE_USED")
                            {
                                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                                for (int l = 0; l < 10; l++)
                                {
                                    string curLine = sr.ReadLine().Trim();
                                    if (!string.IsNullOrEmpty(curLine))
                                        sb.Append(curLine).Append(",");
                                    else
                                        break;
                                }
                                model.PROCEDURE_USED = sb.ToString().TrimEnd(',');
                            }
                            else
                                FillModel<NeuModel>(model, line, sr.ReadLine().Trim());
                        }
                    }
                }

                //in-case of Replicate, take first 34 char then add suffix "R"
                if (model.ITEM_ID.Length > 34)
                    model.ITEM_ID = model.ITEM_ID.Substring(0, 34) + "R";

                //Transfer data to mapped fields
                NDA_Master master = new NDA_Master
                {
                    Item_ID = model.ITEM_ID,
                    MDA = Decimal.Parse(model.MDA_MASS, System.Globalization.NumberStyles.Any),
                    MDA_SN = model.MDA_MASS,
                    Mass_U235 = Decimal.Parse(model.ASSAY_MASS, System.Globalization.NumberStyles.Any),
                    Mass_U235_SN = model.ASSAY_MASS,
                    Sigma_1 = Decimal.Parse(model.ASSAY_MASS_1SIG_UNCERTAINTY, System.Globalization.NumberStyles.Any),
                    Sigma_1_SN = model.ASSAY_MASS_1SIG_UNCERTAINTY,
                    Sigma_2 = Decimal.Parse(model.ASSAY_MASS_2SIG_UNCERTAINTY, System.Globalization.NumberStyles.Any),
                    Sigma_2_SN = model.ASSAY_MASS_2SIG_UNCERTAINTY,
                    Pounds_UO2F2 = Decimal.Parse(model.DERIVED_MASS, System.Globalization.NumberStyles.Any),
                    Pounds_UO2F2_SN = model.DERIVED_MASS,
                    Error_UO2F2 = Decimal.Parse(model.DERIVED_MASS_2SIG_UNCERTAINTY, System.Globalization.NumberStyles.Any),
                    Error_UO2F2_SN = model.DERIVED_MASS_2SIG_UNCERTAINTY,
                    Measurement_Date = Convert.ToDateTime(model.DATE_TIME_OF_ASSAY),
                    Analysis_Date = Convert.ToDateTime(model.DATE_TIME_OF_ANALYSIS),
                    Procedure_Used = model.PROCEDURE_USED,
                    //master.Batch_Num = model.BATCH_NUMBER;

                    //FROM CONDA
                    NDA_Request_Num = "NDA1800228",
                    Letter_Num = "FBP-NDA-18-00943",
                    Revision = "0",

                    Item_Type = "Cooler",
                    ComponentId = 0,
                    Length = 0.00m,
                    Diameter = 0.00m,

                    Batch_Num = "FBP-BLUEBOX1-1800002"
                };

                string jsonStr = Newtonsoft.Json.JsonConvert.SerializeObject(master);
                Console.WriteLine(jsonStr);


                Console.WriteLine("File parsed completed");
                
            }

            Console.ReadKey();
        }


        /// <summary>
        /// Fills a class model with values based on on property name
        /// </summary>
        /// <typeparam name="T">class data model</typeparam>
        /// <param name="entity">model instance to be filled</param>
        /// <param name="key">property name to search</param>
        /// <param name="value">data to assign to property</param>
        static void FillModel<T>(T entity, string key, string value) where T : class
        {
            foreach (System.Reflection.PropertyInfo prop in entity.GetType().GetProperties())
            {
                if (prop.Name == key)
                {
                    System.Reflection.PropertyInfo propertyInfo = entity.GetType().GetProperty(prop.Name);
                    propertyInfo.SetValue(entity, value);
                }
            }
        }


    }


    public class NeuModel
    {
        public string REPORT_HEADER1 { get; set; }
        public string REPORT_HEADER2 { get; set; }
        public string DATE_TIME_OF_ASSAY { get; set; }
        public string DATE_TIME_OF_ANALYSIS { get; set; }
        public string DATE_TIME_OF_BACKGROUND { get; set; }
        public string BACKGROUND_FILENAME { get; set; }
        public string ASSAY_SPE_FILENAME { get; set; }
        public string SOFTWARE_VERSION { get; set; }
        public string SOFTWARE_VERSION_BKG { get; set; }
        public string SYSTEM_ID { get; set; }
        public string ANALYSIS_BY { get; set; }
        public string ITEM_ID { get; set; }
        public string TECHNICIAN_NAME { get; set; }
        public string ANALYST_NAME { get; set; }
        public string STAGE { get; set; }
        public string BATCH_NUMBER { get; set; }
        public string GROSS_WEIGHT { get; set; }
        public string TARE_WEIGHT { get; set; }
        public string NET_WEIGHT { get; set; }
        public string CH1_DETID { get; set; }
        public string CH2_DETID { get; set; }
        public string CH3_DETID { get; set; }
        public string CH4_DETID { get; set; }
        public string PROCEDURE_USED { get; set; }
        public string MEASUREMENT_TYPE { get; set; }
        public string ITEM_TYPE { get; set; }
        public string U235_ENRICHMENT { get; set; }
        public string U235_TO_234_RATIO { get; set; }
        public string TARGET_MDA { get; set; }
        public string MEAN_PRE_VALIDATION_COUNT_RATE { get; set; }
        public string BETA { get; set; }
        public string NUM_TOT_CHANNELS { get; set; }
        public string NUM_TOT_INTERVALS { get; set; }
        public string NEUTRON_INTERVAL_COUNT_TIME { get; set; }
        public string NUM_VAL_INTERVALS { get; set; }
        public string MAX_BACKGROUND_RATE_FOR_MDA { get; set; }
        public string MIN_PCT_VAL { get; set; }
        public string VALIDATED_COUNT_TIME { get; set; }
        public string VALIDATED_COUNT_RATE { get; set; }
        public string BACKGROUND_VALIDATED_COUNT_TIME { get; set; }
        public string BACKGROUND_VALIDATED_COUNT_RATE { get; set; }
        public string MDA_COUNT_TIME { get; set; }
        public string Z0_BACKGROUND_CORRECTION_OFFSET { get; set; }
        public string Z1_BACKGROUND_CORRECTION_SLOPE { get; set; }
        public string MEAN_VALIDATED_CHANNEL_RATE { get; set; }
        public string NET_COUNT_RATE { get; set; }
        public string NET_COUNT_RATE_UNCERTAINTY { get; set; }
        public string MDA_MASS { get; set; }
        public string MDA_ACTIVITY { get; set; }
        public string SUFFICIENT_VAL_INTERVALS { get; set; }
        public string NON_ZERO_COUNT_RATE { get; set; }
        public string BKG_RATE_OK { get; set; }
        public string MDA_NUMBER_OF_CHANNELS_VALIDATED { get; set; }
        public string ERROR_MESSAGES { get; set; }
        public string ASSAY_MASS_SCALING_FACTOR { get; set; }
        public string UNCERTAINTY_SCALING_FACTOR { get; set; }
        public string BKG_UNCERTAINTY_SCALING_FACTOR { get; set; }
        public string KG_PER_POUND { get; set; }
        public string ASSAY_MASS_UNITS { get; set; }
        public string ASSAY_MASS { get; set; }
        public string ASSAY_ACTIVITY_UNITS { get; set; }
        public string ASSAY_ACTIVITY { get; set; }
        public string SPECIFIC_ACTIVITY { get; set; }
        public string ASSAY_MASS_1SIG_UNCERTAINTY { get; set; }
        public string ASSAY_MASS_2SIG_UNCERTAINTY { get; set; }
        public string DERIVED_MASS_CONV_FACTOR { get; set; }
        public string DERIVED_MASS_UNITS { get; set; }
        public string DERIVED_MASS { get; set; }
        public string DERIVED_MASS_2SIG_UNCERTAINTY { get; set; }
        public string N_SIGMA { get; set; }
        public string MANUAL_ISOTOPICS_ENTERED { get; set; }
        public string MIN_NUMBER_INTERVALS { get; set; }
        public string Q_ASSAY { get; set; }
        public string MIN_ENRICHMENT { get; set; }
        public string MAX_ENRICHMENT { get; set; }
        public string MIN_5TO4_RATIO { get; set; }
        public string MAX_5TO4_RATIO { get; set; }
        public string SNE_U234 { get; set; }
        public string SNE_U235 { get; set; }
        public string SNE_U238 { get; set; }
        public string A_U234 { get; set; }
        public string A_U235 { get; set; }
        public string A_U238 { get; set; }
        public string A_NATO { get; set; }
        public string A_NATF { get; set; }

    }

    public class NDA_Master
    {
        public int ID { get; set; }
        public string Item_ID { get; set; }
        public string Item_Type { get; set; }
        public double? NCU { get; set; }
        public decimal? MDA { get; set; }
        public decimal? Mass_U235 { get; set; }
        public decimal? Sigma_1 { get; set; }
        public decimal? Sigma_2 { get; set; }
        public decimal? Pounds_UO2F2 { get; set; }
        public decimal? Error_UO2F2 { get; set; }
        public DateTime? Measurement_Date { get; set; }
        public DateTime? Analysis_Date { get; set; }
        public string Procedure_Used { get; set; }
        public string NDA_Request_Num { get; set; }
        public string Batch_Num { get; set; }
        public string Letter_Num { get; set; }
        public string NCU_SN { get; set; }
        public string MDA_SN { get; set; }
        public string Mass_U235_SN { get; set; }
        public string Sigma_1_SN { get; set; }
        public string Sigma_2_SN { get; set; }
        public string Pounds_UO2F2_SN { get; set; }
        public string Error_UO2F2_SN { get; set; }
        public decimal? Uncorrected_Measured_Enrichment { get; set; }
        public decimal? Declared_Enrichment { get; set; }
        public int? Bias_Corrected_Factor { get; set; }
        public decimal? Bias_Corrected_Enrichment { get; set; }
        public string Uncorrected_Measured_Enrichment_SN { get; set; }
        public string Declared_Enrichment_SN { get; set; }
        public string Bias_Corrected_Factor_SN { get; set; }
        public string Bias_Corrected_Enrichment_SN { get; set; }
        public long? ComponentId { get; set; }
        public decimal? Length { get; set; }
        public decimal? Diameter { get; set; }
        public string Calced_U235_SN { get; set; }
        public string Revision { get; set; }
        public int CIDMSProcessFlag { get; set; }
        public string SRC { get; set; }
    }

}
