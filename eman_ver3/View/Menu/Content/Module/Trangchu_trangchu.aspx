<%@ Page Language="C#" Async="true" EnableSessionState="False" %>


<div class="sub_trangthu" style="height:calc(100% - 7px)">
    <iframe id="frameReport" src="extension/mess/mess.aspx" style="width: 100%; height: 100%; border:none;"></iframe>
</div>

<script type="text/javascript"> 
    dataToShareScreen = { ...dataToShareScreen_contructor };
    update_dataToShareScreen();

    let frameReport = $("#frameReport");
    let loaded = false;
    frameReport.load(function () {
        if (!loaded) {
            let content = frameReport.contents().find("html");
            //content.css('background-image', 'url(../images/Menu/Menu_bordertop.png)');
            //content.css('background-repeat', 'repeat-x');
            loaded = true;
        }
    });
</script>