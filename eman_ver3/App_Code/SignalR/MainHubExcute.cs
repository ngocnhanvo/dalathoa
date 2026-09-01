using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Web;
using eNumPB;
using System.IO;
using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;
using MainHubObjects;
using System.Reflection;

public class MainHubExcute
{
    public string urlHub { get; set; }
    public string classHub { get; set; }
    public MainHubExcute()
    {

    }

    public void Exec(string name, string data)
    {
        string url = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + Security.UrlBase();
        if (string.IsNullOrWhiteSpace(urlHub))
        {
            string chub = string.IsNullOrWhiteSpace(classHub) ? Avariables.hubname : classHub;
            var magicType = Type.GetType(chub);
            var magicConstructor = magicType.GetConstructor(Type.EmptyTypes);
            object magicClassObject = magicConstructor.Invoke(new object[] { });
            var magicMethod = magicType.GetMethod(name);
            object magicValue = magicMethod.Invoke(magicClassObject, new object[] { data });
        }
        else if (urlHub != url)
        {
            var hubConnection = new HubConnection(url);
            var stockTickerHubProxy = hubConnection.CreateHubProxy(Avariables.hubname);
            hubConnection.Start().Wait(30000);

            if (hubConnection.State == ConnectionState.Connected)
            {
                stockTickerHubProxy.Invoke(name, data).Wait();
                hubConnection.Stop();
            }
        }
    }
}