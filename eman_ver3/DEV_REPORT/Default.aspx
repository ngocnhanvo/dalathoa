<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="DevExpress.Web.v16.1, Version=16.1.2.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraReports.v16.1.Web, Version=16.1.2.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraReports.v16.1.Web, Version=16.1.2.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web.ClientControls" TagPrefix="cc1" %>
<html>
    <head runat="server">
        <title></title>
        <script src="Scripts/jquery-1.11.3.js" type="text/javascript"></script>
        <script src="Scripts/jquery-ui-1.11.4.js" type="text/javascript"></script>
        <script src="Scripts/cldr.js" type="text/javascript"></script>
        <script src="Scripts/cldr.event.js" type="text/javascript"></script>
        <script src="Scripts/cldr.supplemental.js" type="text/javascript"></script>
        <script src="Scripts/globalize.js" type="text/javascript"></script>
        <script src="Scripts/globalize.message.js" type="text/javascript"></script>
        <script src="Scripts/globalize.number.js" type="text/javascript"></script>
        <script src="Scripts/globalize.date.js" type="text/javascript"></script>
        <script src="Scripts/globalize.currency.js" type="text/javascript"></script>
        <script src="Scripts/knockout-3.3.0.js" type="text/javascript"></script>
        <script src="Scripts/ace.js" type="text/javascript"></script>
        <script src="Scripts/ext-language_tools.js" type="text/javascript"></script>

        <link href="Content/jquery-ui-1.11.4.css" type="text/css" rel="Stylesheet" />

        <script type="text/javascript">
            function GetDesignerUrl() {
                //return reportDesigner.GetDesignerModel().navigateByReports.currentTab().url();
                return $('#link').val();
            }

            function SetDesignerUrl(url) {
                reportDesigner.GetDesignerModel().navigateByReports.currentTab().url(url);
            }

            function reportDesigner_Open() {
                fileDialog.ShowOpenFileDialog(OpenFileDialogResult);
            }

            function reportDesigner_Save() {
                let fileName = GetDesignerUrl();
                console.log(fileName);
                if (fileName) {
                    //let data = reportDesigner.GetDesignerModel().model().serialize();
                    //let postDT = {
                    //    "reportLayout": JSON.stringify(data),
                    //    "reportUrl": fileName
                    //};
                    //console.log(JSON.stringify(postDT));
                    //$.post('DXXRD.axd',
                    //    { actionKey: 'setData', arg: encodeURI(JSON.stringify(postDT)) }, function (r) {

                    //    });
                    DevExpress.Designer.Report.ReportStorageWeb.setData(reportDesigner.GetDesignerModel().model().serialize(), fileName)
                        .done(function (r) {
                            alert("Report " + fileName + " was saved.");
                        })
                        .fail(function (f) {
                            var errorMessage = f.responseJSON.error;
                            alert("An exception occured while saving the report: " + errorMessage);
                        });
                }
                else {
                    reportDesigner_SaveAs();
                }
            }

            function reportDesigner_SaveAs() {
                //fileDialog.ShowSaveFileDialog(SaveFileDialogResult);
            }

            function OpenFileDialogResult(fileName) {
                DevExpress.Designer.Report.ReportStorageWeb.getData(fileName)
                    .done(function (result) {
                        var model = new DevExpress.Designer.Report.ReportViewModel(JSON.parse(result.reportLayout));
                        model.dataSourceRefs = result.dataSourceRefInfo;
                        reportDesigner.GetDesignerModel().model(model);
                        SetDesignerUrl(fileName);
                    })
                    .fail(function (f, status, errorMessage) {
                        var errorMessage = f.responseJSON.error;
                        alert("An exception occured while opening the report: " + errorMessage);
                    });
            }

            function SaveFileDialogResult(fileName) {
                DevExpress.Designer.Report.ReportStorageWeb.setNewData(reportDesigner.GetDesignerModel().model().serialize(), fileName)
                    .done(function (result) {
                        SetDesignerUrl(result);
                        alert("Report " + result + " was saved.");
                    })
                    .fail(function (f) {
                        var errorMessage = f.responseJSON.error;
                        alert("An exception occured while saving the report: " + errorMessage);
                    });
            }

            function reportDesigner_CustomizeMenuActions(s, e) {
                let defaultOpenAction = e.GetById(DevExpress.Designer.Report.ActionId.OpenReport);
                let defaultSaveAction = e.GetById(DevExpress.Designer.Report.ActionId.Save);
                let defaultSaveAsAction = e.GetById(DevExpress.Designer.Report.ActionId.SaveAs);
                let defaultExitAction = e.GetById(DevExpress.Designer.Report.ActionId.Exit);

                if (defaultOpenAction)
                    defaultOpenAction.clickAction = reportDesigner_Open;

                if (defaultSaveAction) {
                    defaultSaveClickAction = defaultSaveAction.clickAction;
                    defaultSaveAction.clickAction = reportDesigner_Save;
                }

                if (defaultSaveAsAction)
                    defaultSaveAsAction.clickAction = reportDesigner_SaveAs;

                var runWizardAction = e.Actions.filter(function (x) { return x.imageClassName === 'dxrd-image-new-via-wizard' })[0];
                var NewAction = e.Actions.filter(function (x) { return x.imageClassName === 'dxrd-image-newreport' })[0];
                var OpenAction = e.Actions.filter(function (x) { return x.imageClassName === 'dxrd-image-open' })[0];
                var DesignerAction = e.Actions.filter(function (x) { return x.imageClassName === 'dxrd-image-run-wizard' })[0];
                var DatasourceAction = e.Actions.filter(function (x) { return x.imageClassName === 'dxrd-image-add-datasource' })[0];
                var ExitAction = e.Actions.filter(function (x) { return x.imageClassName === 'dxrd-image-exit' })[0];
                var SaveasAction = e.Actions.filter(function (x) { return x.imageClassName === 'dxrd-image-save' })[1];
                if (runWizardAction != null)
                    runWizardAction.visible = false;
                if (NewAction != null)
                    NewAction.visible = false;
                if (OpenAction != null)
                    OpenAction.visible = false;
                if (DesignerAction != null)
                    DesignerAction.visible = false;
                if (DatasourceAction != null)
                    DatasourceAction.visible = false;
                if (ExitAction != null)
                    ExitAction.visible = true;
                if (SaveasAction != null)
                    SaveasAction.visible = false;
                //s.GetDesignerModel().reportPreviewModel.tabPanel.collapsed(true);
            }

            function WebDocumentViewer_Init(s, e) {
                s.GetPreviewModel().reportPreview.zoom(1);
            }
            //function check_reportDisplay() {
            //    setTimeout(function () {
            //        var cll_clk = $('.dxrd-right-panel-collapse');
            //        if (cll_clk.attr('class') == null) {
            //            check_reportDisplay();
            //        }
            //        else {
            //            cll_clk.click();
            //            SetDesignerUrl($('.first_tabs_esc').html());
            //        }
            //    }, 1000);
            //}
            //check_reportDisplay();
        </script>
    </head>
    <body>
        <input type="hidden" id="link" value="<%=link %>""/>
        <form id="form1" runat="server">
            <dx:ASPxReportDesigner ID="reportDesigner" runat="server" ClientInstanceName="reportDesigner">
                <ClientSideEvents CustomizeMenuActions="reportDesigner_CustomizeMenuActions" />
            </dx:ASPxReportDesigner>
        </form>
    </body>
</html>
