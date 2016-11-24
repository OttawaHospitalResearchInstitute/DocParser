using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace DocParser
{
    public class SQLHelper
    {
        msSQLclass msSQL = new msSQLclass();

        public String getOHREBStatus(String id)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select s.Status from " + Ref.DBProtrac + "tblHRProtocol p left join " + Ref.DBProtrac + "tblHRStatus s on p.HRStatusID=s.HRStatusID where p.HRProtocolID='" + id + "'");
            dt = msSQL.GetData(cmd);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["Status"].ToString();
            }
            else
            {
                return "";
            }
        }

        public String getCC(String id)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("select cc.CostCentre from " + Ref.DBProtrac + "tblHRProtocol p left join " + Ref.DBProtrac + "tblProject pp on p.ProjectID=pp.ProjectID left join " + Ref.DBirisPrefix + "IrisCostCentres cc on pp.InvestigatorID Collate Database_Default = cc.InvestigatorID Collate Database_Default where p.HRProtocolID='" + id + "'");
            dt = msSQL.GetData(cmd);

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["CostCentre"].ToString();
            }
            else
            {
                return "";
            }
        }

        public void insertParse(String clientCode, String studyIndentifier, String HRProtocolID, String status, String studyInitiationDate, String costCentre)
        {
            //String studyDate = "";
            //DateTime date = new DateTime();
            //if (DateTime.TryParse(studyInitiationDate, out date))
            //{
            //    studyDate = Convert.ToDateTime(studyInitiationDate).ToString();
            //}

            SqlCommand cmd = new SqlCommand("insert into " + Ref.DBirisPrefix + "DocParse values ('" + clientCode + "','" + studyIndentifier + "','" + HRProtocolID + "','" + status + "','" + studyInitiationDate + "','" + costCentre + "')");
            msSQL.ExecuteNonQuery(cmd);
        }
    }
}