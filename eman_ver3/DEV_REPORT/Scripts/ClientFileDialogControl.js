function ClientFileDialogControl(name) {
    //Private fields
    this.name = name;
    this.popup = null;
    this.fileManager = null;
    this.txFileName = null;
    this.buttonOk = null;
    this.validationCallback = null;

    this.dialogMode = null;
    this.openFunction = null;
    this.saveFunction = null;

    //Event handlers
    this.fileManager_SelectionChanged = function (s, e) {
        if (e.isSelected) {
            this.txFileName.SetText(e.item.name);
        }
    };
    this.buttonOk_Click = function () {
        this.validationCallback.PerformCallback(this.dialogMode + '|' + this.GetFileName());
    };
    this.buttonCancel_Click = function () {
        this.popup.Hide();
    };
    this.validationCallback_CallbackComplete = function (s, e) {
        if (e.result === "") {
            this.popup.Hide();
            switch (this.dialogMode) {
                case "OPEN":
                    if (this.openFunction != null) {
                        this.openFunction(this.GetFileName());
                    }
                    break;
                case "SAVE":
                    if (this.saveFunction != null) {
                        this.saveFunction(this.GetFileName());
                    }
                    break;
            }
        }
        else {
            alert(e.result);
        }
    };

    //Private methods
    this.InitializeDialog = function () {
        switch (this.dialogMode) {
            case "OPEN":
                this.popup.SetHeaderText("Open Report");
                this.buttonOk.SetText("Open");
                break;
            case "SAVE":
                this.popup.SetHeaderText("Save Report");
                this.buttonOk.SetText("Save");
                break;
        }
        this.txFileName.SetText("");
        this.fileManager.Refresh();
        this.popup.Show();
    }
    this.GetFileName = function () {
        return "~\\" + this.fileManager.GetCurrentFolderPath() + "\\" + this.txFileName.GetText();
    };

    //Public methods
    this.ShowOpenFileDialog = function (openDialogResultFunction) {
        this.dialogMode = "OPEN";
        this.openFunction = openDialogResultFunction;
        this.InitializeDialog();
    };
    this.ShowSaveFileDialog = function (saveDialogResultFunction) {
        this.dialogMode = "SAVE";
        this.saveFunction = saveDialogResultFunction;
        this.InitializeDialog();
    };
}

function initViewer(s, e) {
	var reportPreview = s.GetPreviewModel().reportPreview;
	reportPreview.zoom(1);
	var currentExportOptions = reportPreview.exportOptionsModel;
	var optionsUpdating = false;
	var fixExportOptions = function(options) {
		try {
			optionsUpdating = true;
			if(!options) {
				currentExportOptions(null);
			} else {
				delete options["mht"];
				delete options["html"];
				delete options["textExportOptions"];
				delete options["csv"];
				delete options["rtf"];
				delete options["image"];
				currentExportOptions(options);
			}
		} finally {
			optionsUpdating = false;
		}
	};
	currentExportOptions.subscribe(function(newValue) {
		!optionsUpdating && fixExportOptions(newValue);
	});
	fixExportOptions(currentExportOptions());
}

document.write('<script src="/anco2/Scripts/jquery-1.11.3.js"></script>');
document.write('<script src="/anco2/Scripts/jquery-ui-1.11.4.js" type="text/javascript"></script>');
document.write('<script src="/anco2/Scripts/cldr.js" type="text/javascript"></script>');
document.write('<script src="/anco2/Scripts/cldr.event.js" type="text/javascript"></script>');
document.write('<script src="/anco2/Scripts/cldr.supplemental.js" type="text/javascript"></script>');
document.write('<script src="/anco2/Scripts/globalize.js" type="text/javascript"></script>');
document.write('<script src="/anco2/Scripts/globalize.message.js" type="text/javascript"></script>');
document.write('<script src="/anco2/Scripts/globalize.number.js" type="text/javascript"></script>');
document.write('<script src="/anco2/Scripts/globalize.date.js" type="text/javascript"></script>');
document.write('<script src="/anco2/Scripts/globalize.currency.js" type="text/javascript"></script>');
document.write('<script src="/anco2/Scripts/knockout-3.3.0.js" type="text/javascript"></script>');
document.write('<script src="/anco2/Scripts/ace.js" type="text/javascript"></script>');
document.write('<script src="/anco2/Scripts/ext-language_tools.js" type="text/javascript"></script>');