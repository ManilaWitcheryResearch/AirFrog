namespace Manila.AirFrog.WebService.Modules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Nancy;
    using Newtonsoft.Json;
    using Manila.AirFrog.WebService.Models;
    using Manila.AirFrog.Common.Models;
    public class MonitoringModule : BaseApiModule
    {
        private MonitoringModel requestMonitoringModel = null;
        private Response BeforeMonitoringApiRequest(NancyContext ctx)
        {
            try
            {
                requestMonitoringModel = JsonConvert.DeserializeObject<MonitoringModel>(RequestJson);
                AirFrog.LoggerMan.Log(JsonConvert.SerializeObject(requestMonitoringModel));
            }
            catch (Exception e)
            {
                AirFrog.LoggerMan.LogErr(e.ToString());
                return Response.AsJson(badRequestResponse, Nancy.HttpStatusCode.BadRequest);
            }
            return null;
        }
        private void AfterMonitoringApiResponse(NancyContext ctx)
        {
            ;
        }
        private Response OnMonitoringApiRequestError(NancyContext ctx, Exception ex)
        {
            AirFrog.LoggerMan.LogErr(ex.ToString());
            if (ex.Message.StartsWith("RequestDataOrProcessError:"))
            {
                return Response.AsJson(new { result = "failed", errormsg = ex.Message });
            }
            return Response.AsJson(internalErrorResponse, Nancy.HttpStatusCode.InternalServerError);
        }
        public MonitoringModule()
        {
            Before += BeforeMonitoringApiRequest;
            After += AfterMonitoringApiResponse;
            OnError += OnMonitoringApiRequestError;

            Post["/api/mcs/register"] = parameters =>
            {
                var x = new McsMetaModel
                {
                    Name = requestMonitoringModel.Name,
                    Address = requestMonitoringModel.Address,
                    McVersion = requestMonitoringModel.McVersion,
                    ModVersion = requestMonitoringModel.ModVersion,
                    ModPort = requestMonitoringModel.ModPort
                };
                var p = AirFrog.DataAcceess.RegisterNewServer(x);
                if (p == null)
                {
                    throw new Exception("RequestDataOrProcessError: Register Mcs failed.");
                }
                else
                {
                    return Response.AsJson(new { result = "success", errormsg = "", serverid = p.ServerId });
                }
            };

            Post["/api/mcs/update"] = parameters =>
            {
                var x = new McsMetaModel
                {
                    ServerId = requestMonitoringModel.ServerId,
                    Name = requestMonitoringModel.Name,
                    Address = requestMonitoringModel.Address,
                    McVersion = requestMonitoringModel.McVersion,
                    ModVersion = requestMonitoringModel.ModVersion,
                    ModPort = requestMonitoringModel.ModPort
                };
                var y = new McsMetaModelMask
                {
                    Name = false,
                    Address = string.IsNullOrEmpty(requestMonitoringModel.Address) ? false : true,
                    McVersion = string.IsNullOrEmpty(requestMonitoringModel.McVersion) ? false : true,
                    ModVersion = string.IsNullOrEmpty(requestMonitoringModel.ModVersion) ? false : true,
                    ModPort = requestMonitoringModel.ModPort == 0 ? false : true
                };

                AirFrog.DataAcceess.UpdateServerInfo(x, y);

                return Response.AsJson(new { result = "success", errormsg = "" });
            };

            Post["/api/mcs/golive"] = parameters =>
            {
                AirFrog.DataAcceess.UpdateServerMonitoringInfo(
                    new McsMonitoringModel {
                        ServerId = requestMonitoringModel.ServerId,
                        Status = "live",
                    },
                    new McsMonitoringModelMask
                    {
                        Status = true,
                    });

                return Response.AsJson(successResponse);
            };

            Post["/api/mcs/heartbeat"] = parameters =>
            {
                AirFrog.DataAcceess.UpdateServerLastSeen(requestMonitoringModel.ServerId);

                return Response.AsJson(successResponse);
            };

            Post["/api/mcs/server_closedown"] = parameters =>
            {
                AirFrog.DataAcceess.UpdateServerMonitoringInfo(
                    new McsMonitoringModel
                    {
                        ServerId = requestMonitoringModel.ServerId,
                        Status = "down",
                    },
                    new McsMonitoringModelMask
                    {
                        Status = true,
                    });

                return Response.AsJson(successResponse);
            };
        }
    }
}
