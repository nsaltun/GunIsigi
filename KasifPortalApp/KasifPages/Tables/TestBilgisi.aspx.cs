﻿using KasifBusiness.Business.KasifPageOperations;
using KasifBusiness.DB_Operations.DBOperations;
using KasifBusiness.DB_Operations.EntityObject;
using KasifBusiness.Objects.ScreenObjects;
using KasifPortalApp.Utilities;
using System;
using System.Collections.Generic;
using System.Web.Services;
using System.Web.UI;
using static KasifBusiness.DB_Operations.DBObjects.ConstDbCommands;

namespace KasifPortalApp.KasifPages.Tables
{
    public partial class TestBilgisi : BasePage
    {
        public string pageTitle = "Test Bilgisi";
        public string standardErr = "İşlem Başarılı";
        public string pageName = "TestBilgi-page";

        public override void Page_Load(object sender, EventArgs e)
        {
            try
            {
                PageOperations PageOps = new PageOperations();
                List<TestBilgiObj> lstScreenInfoObj = PageOps.RunQueryForPage<TestBilgiObj>(DbCommandList.GET_TEST_BILGI, null, null);

                tblRepeater.DataSource = lstScreenInfoObj;
                tblRepeater.DataBind();
            }
            catch (Exception ex)
            {
                standardErr = "İşlem gerçekleştirilirken bir hata oluştu.";
                RaisePopUp(ex.Message, ResultStatus.Error);
            }


        }

        private void RaisePopUp(string msg, ResultStatus resultStatus)
        {
            if (resultStatus == ResultStatus.Success)
            {
                String script = "<script>$(document).ready(function () {showSuccessModal('" + pageTitle + "','" + msg + "','" + Page.GetRouteUrl(pageName, null) + "');});</script>";
                ClientScript.RegisterStartupScript(typeof(Page), "ProcessError", script);
            }
            else
            {
                String script = "<script>$(document).ready(function () {showErrorModal('" + pageTitle + " - Hata','" + msg + "');});</script>";
                ClientScript.RegisterStartupScript(typeof(Page), "ProcessError", script);
            }
        }

        public string GenerateAddUrl()
        {
            return Page.GetRouteUrl(pageName + "-add", null);
        }

        [WebMethod()]
        public static string DeleteCurrentRow(string RowGuid)
        {
            try
            {
                TEST_BILGI testBilgiObj = new TEST_BILGI();
                testBilgiObj.GUID = Convert.ToInt64(RowGuid);
                DbOperations.Delete(testBilgiObj);
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
    }
}